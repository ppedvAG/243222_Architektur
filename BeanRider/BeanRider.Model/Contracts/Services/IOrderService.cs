using BeanRider.Model.DomainModel;

namespace BeanRider.Logic
{
    public interface IOrderService
    {
        decimal CalculateTotalPrice(Order order);
        IEnumerable<Order> GetOpenOrdersThatAreNotVegetarian();
    }
}