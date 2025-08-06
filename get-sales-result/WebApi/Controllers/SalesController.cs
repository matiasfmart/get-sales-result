using get_sales_result.Application.DTOs;
using get_sales_result.Application.Interfaces;
using get_sales_result.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace get_sales_result.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IVehicleSaleService _saleService;

        public SalesController(IVehicleSaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Inserta una nueva venta
        /// </summary>
        [HttpPost]
        public IActionResult InsertSale([FromBody] CreateSaleRequestDto request)
        {
            try
            {
                _saleService.InsertSale(request);
                return Ok(new ApiResponse<string>("Venta insertada correctamente."));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }
        }

        /// <summary>
        /// Devuelve el volumen total de ventas(total en USD)
        /// </summary>
        [HttpGet("total-volume")]
        public IActionResult GetTotalSalesAmount()
        {
            try
            {
                var total = _saleService.GetTotalSalesAmount();
                return Ok(new ApiResponse<decimal>(total));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }
        }

        /// <summary>
        /// Devuelve el volumen de ventas por centro(total por centro)
        /// </summary>
        [HttpGet("volume-by-center")]
        public IActionResult GetSalesVolumeByCenter()
        {
            try
            {
                var result = _saleService.GetSalesVolumeByCenter();
                return Ok(new ApiResponse<Dictionary<DistributionCenter, decimal>>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }
        }

        /// <summary>
        /// Devuelve el porcentaje de ventas por modelo de cada centro
        /// </summary>
        [HttpGet("percentages-by-model-and-center")]
        public IActionResult GetModelPercentageByCenter()
        {
            try
            {
                var result = _saleService.GetModelPercentageByCenter();
                return Ok(new ApiResponse<Dictionary<DistributionCenter, Dictionary<VehicleType, double>>>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}