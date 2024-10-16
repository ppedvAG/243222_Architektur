using BeanRider.Model.Contracts;
using BeanRider.Model.DomainModel;

namespace BeanRider.Logic
{
    public class OrderService
    {
        public IRepository Repository { get; }

        public OrderService(IRepository repository)
        {
            Repository = repository;
        }

        public IEnumerable<Order> GetOpenOrdersThatAreNotVegetarian()
        {
            return Repository.GetAll<Order>().Where(o => o.Status == OrderStatus.Pending && o.Items.Any(i => !i.Food.Vegetarian));
        }

        public decimal CalculateTotalPrice(Order order)
        {
            ArgumentNullException.ThrowIfNull(order, nameof(order));

            decimal totalPrice = 0;
            foreach (var item in order.Items)
            {
                decimal itemPrice = item.Food.Price;
                if (item.OrderPrice.HasValue)
                    itemPrice = item.OrderPrice.Value;

                //todo
                //if (item.Discount.HasValue)
                //    itemPrice *= item.Discount.Value * -1;

                totalPrice += itemPrice * item.Amount;
            }
            return totalPrice;
        }

    }
}
