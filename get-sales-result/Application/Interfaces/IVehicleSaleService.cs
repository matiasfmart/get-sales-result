using get_sales_result.Application.DTOs;
using get_sales_result.Domain.Enums;

namespace get_sales_result.Application.Interfaces
{
    public interface IVehicleSaleService
    {
        void InsertSale(CreateSaleRequestDto request);
        decimal GetTotalSalesAmount();
        Dictionary<DistributionCenter, decimal> GetSalesVolumeByCenter();
        Dictionary<DistributionCenter, Dictionary<VehicleType, double>> GetModelPercentageByCenter();
    }
}
