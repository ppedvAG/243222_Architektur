using BeanRider.Model.Contracts.Data;
using BeanRider.Model.DomainModel;
using System.Numerics;

namespace BeanRider.Logic
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderService orderService;

        public CustomerService(IUnitOfWork uow, IOrderService orderService)
        {
            this.unitOfWork = uow;
            this.orderService = orderService;
        }

        public Customer GetCustomerWithMostUmsatz()
        {
            return unitOfWork.CustomerRepo.Query().ToList().GroupBy(x => x.Orders.Sum(o => orderService.CalculateTotalPrice(o)))
                                          .OrderByDescending(x => x.Key)
                                          .FirstOrDefault()
                                          .FirstOrDefault();    
        }
    }
}
