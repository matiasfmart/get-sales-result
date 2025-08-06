using get_sales_result.Domain.Entities;

namespace get_sales_result.Infra
{
    public interface ISalesRepository
    {
        void AddSale(VehicleSale sale);
        List<VehicleSale> GetAllSales();
    }
}
