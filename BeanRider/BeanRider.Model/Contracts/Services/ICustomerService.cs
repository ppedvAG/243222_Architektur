using BeanRider.Model.DomainModel;

namespace BeanRider.Logic
{
    public interface ICustomerService
    {
        Customer GetCustomerWithMostUmsatz();
    }
}