using BeanRider.Model.Contracts.Data;
using BeanRider.Model.DomainModel;

namespace BeanRider.Logic
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository repo;
        private readonly IOrderService orderService;

        public CustomerService(IRepository repo, IOrderService orderService)
        {
            this.repo = repo;
            this.orderService = orderService;
        }

        public Customer GetCustomerWithMostUmsatz()
        {
            return repo.Query<Customer>().ToList().GroupBy(x => x.Orders.Sum(o => orderService.CalculateTotalPrice(o)))
                                          .OrderByDescending(x => x.Key)
                                          .FirstOrDefault()
                                          .FirstOrDefault();    
        }
    }
}
