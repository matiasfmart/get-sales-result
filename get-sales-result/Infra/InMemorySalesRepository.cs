using get_sales_result.Domain.Entities;

namespace get_sales_result.Infra
{
    public class InMemorySalesRepository : ISalesRepository
    {
        private static readonly List<VehicleSale> _sales = new();

        public void AddSale(VehicleSale sale)
        {
            _sales.Add(sale);
        }

        public List<VehicleSale> GetAllSales()
        {
            return _sales.ToList();
        }

        public void ClearSales()
        {
            _sales.Clear();
        }
    }
}
