using get_sales_result.Domain.Enums;

namespace get_sales_result.Application.DTOs
{
    public class CreateSaleRequestDto
    {
        public VehicleType VehicleType { get; set; }
        public DistributionCenter DistributionCenter { get; set; }
    }
}
