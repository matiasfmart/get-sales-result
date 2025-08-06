using get_sales_result.Application.DTOs;
using get_sales_result.Application.Services;
using get_sales_result.Domain.Entities;
using get_sales_result.Domain.Enums;
using get_sales_result.Infra;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace get_sales_result_test.Services
{
    public class VehicleSaleServiceTests
    {
        private readonly Mock<InMemorySalesRepository> _mockRepository;
        private VehicleSaleService _service;
        private InMemorySalesRepository _repository;

        public VehicleSaleServiceTests()
        {
            _repository = new InMemorySalesRepository();
            _repository.ClearSales(); //limpiamos
            _mockRepository = new Mock<InMemorySalesRepository>();
            var mockLogger = new Mock<ILogger<VehicleSaleService>>();
            _service = new VehicleSaleService(_mockRepository.Object, mockLogger.Object);
        }

        [Fact]
        public void InsertSale_ShouldAddSaleCorrectly()
        {
            var request = new CreateSaleRequestDto
            {
                VehicleType = VehicleType.Suv,
                DistributionCenter = DistributionCenter.Center1
            };

            _service.InsertSale(request);

            var allSales = _repository.GetAllSales();
            Assert.Single(allSales);
            Assert.Equal(VehicleType.Suv, allSales[0].VehicleType);
            Assert.Equal(9500m, allSales[0].FinalPrice);
        }

        [Fact]
        public void InsertSale_WithSport_ShouldApplyTax()
        {
            var request = new CreateSaleRequestDto
            {
                VehicleType = VehicleType.Sport,
                DistributionCenter = DistributionCenter.Center2
            };

            _service.InsertSale(request);

            var sale = _repository.GetAllSales()[0];
            Assert.Equal(18200m * 1.07m, sale.FinalPrice);
        }

        [Fact]
        public void GetTotalSalesAmount_ShouldReturnCorrectSum()
        {
            _service.InsertSale(new CreateSaleRequestDto
            {
                VehicleType = VehicleType.Suv,
                DistributionCenter = DistributionCenter.Center1
            });

            _service.InsertSale(new CreateSaleRequestDto
            {
                VehicleType = VehicleType.Sedan,
                DistributionCenter = DistributionCenter.Center1
            });

            var total = _service.GetTotalSalesAmount();

            Assert.Equal(9500m + 8000m, total);
        }

        [Fact]
        public void GetSalesVolumeByCenter_ShouldGroupAndSumCorrectly()
        {
            _service.InsertSale(new CreateSaleRequestDto
            {
                VehicleType = VehicleType.Suv,
                DistributionCenter = DistributionCenter.Center1
            });

            _service.InsertSale(new CreateSaleRequestDto
            {
                VehicleType = VehicleType.Sedan,
                DistributionCenter = DistributionCenter.Center2
            });

            _service.InsertSale(new CreateSaleRequestDto
            {
                VehicleType = VehicleType.Sport,
                DistributionCenter = DistributionCenter.Center1
            });

            var result = _service.GetSalesVolumeByCenter();

            Assert.Equal(2, result.Count);
            Assert.Equal(9500m + (18200m * 1.07m), result[DistributionCenter.Center1]);
            Assert.Equal(8000m, result[DistributionCenter.Center2]);
        }

        [Fact]
        public void GetModelPercentageByCenter_ShouldReturnCorrectPercentages()
        {
            _service.InsertSale(new CreateSaleRequestDto
            {
                VehicleType = VehicleType.Suv,
                DistributionCenter = DistributionCenter.Center1
            });

            _service.InsertSale(new CreateSaleRequestDto
            {
                VehicleType = VehicleType.Suv,
                DistributionCenter = DistributionCenter.Center1
            });

            _service.InsertSale(new CreateSaleRequestDto
            {
                VehicleType = VehicleType.Sedan,
                DistributionCenter = DistributionCenter.Center2
            });

            var result = _service.GetModelPercentageByCenter();

            Assert.Equal(2, result.Count);
            Assert.Equal(66.67, result[DistributionCenter.Center1][VehicleType.Suv]);
            Assert.Equal(33.33, result[DistributionCenter.Center2][VehicleType.Sedan]);
        }
    }
}
