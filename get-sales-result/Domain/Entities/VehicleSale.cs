using get_sales_result.Domain.Enums;

namespace get_sales_result.Domain.Entities
{
    public class VehicleSale
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public VehicleType VehicleType { get; set; }
        public DistributionCenter DistributionCenter { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public decimal FinalPrice { get; set; }
    }
}
