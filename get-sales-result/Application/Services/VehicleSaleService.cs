using get_sales_result.Application.DTOs;
using get_sales_result.Application.Interfaces;
using get_sales_result.Domain.Entities;
using get_sales_result.Domain.Enums;
using get_sales_result.Infra;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace get_sales_result.Application.Services
{
    public class VehicleSaleService : IVehicleSaleService
    {
        private readonly InMemorySalesRepository _repository;
        private readonly ILogger<VehicleSaleService> _logger;

        public VehicleSaleService(InMemorySalesRepository repository, ILogger<VehicleSaleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void InsertSale(CreateSaleRequestDto request)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var basePrice = GetBasePrice(request.VehicleType);
                var finalPrice = ApplyExtraTaxesIfNeeded(request.VehicleType, basePrice);

                var sale = new VehicleSale
                {
                    VehicleType = request.VehicleType,
                    DistributionCenter = request.DistributionCenter,
                    FinalPrice = finalPrice
                };

                _repository.AddSale(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar una venta.");
                throw;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("InsertSale ejecutado en {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
            }
        }

        public decimal GetTotalSalesAmount()
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                return _repository.GetAllSales().Sum(s => s.FinalPrice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el monto total de ventas.");
                throw;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("GetTotalSalesAmount ejecutado en {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
            }
        }

        public Dictionary<DistributionCenter, decimal> GetSalesVolumeByCenter()
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                return _repository
                    .GetAllSales()
                    .GroupBy(s => s.DistributionCenter)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Sum(s => s.FinalPrice)
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener volumen de ventas por centro.");
                throw;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("GetSalesVolumeByCenter ejecutado en {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
            }
        }

        public Dictionary<DistributionCenter, Dictionary<VehicleType, double>> GetModelPercentageByCenter()
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var allSales = _repository.GetAllSales();
                int totalSales = allSales.Count;

                if (totalSales == 0)
                {
                    throw new InvalidOperationException("No hay datos cargados para calcular los porcentajes.");
                }

                return allSales
                    .GroupBy(s => s.DistributionCenter)
                    .ToDictionary(
                        g => g.Key,
                        g => g
                            .GroupBy(s => s.VehicleType)
                            .ToDictionary(
                                gg => gg.Key,
                                gg => Math.Round((double)gg.Count() * 100 / totalSales, 2)
                            )
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el porcentaje de modelos por centro.");
                throw;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("GetModelPercentageByCenter ejecutado en {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
            }
        }

        private decimal GetBasePrice(VehicleType type)
        {
            return type switch
            {
                VehicleType.Sedan => 8000,
                VehicleType.Suv => 9500,
                VehicleType.Offroad => 12500,
                VehicleType.Sport => 18200,
                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Tipo de vehículo no soportado: {type}")
            };
        }

        private decimal ApplyExtraTaxesIfNeeded(VehicleType type, decimal basePrice)
        {
            return type == VehicleType.Sport
                ? basePrice * 1.07m
                : basePrice;
        }
    }
}
