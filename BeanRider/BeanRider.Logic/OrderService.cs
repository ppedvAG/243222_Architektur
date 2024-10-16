using BeanRider.Model.Contracts.Data;
using BeanRider.Model.DomainModel;

namespace BeanRider.Logic
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repo;

        public OrderService(IRepository repository)
        {
            this.repo = repository;
        }

        public IEnumerable<Order> GetOpenOrdersThatAreNotVegetarian()
        {
            return repo.GetAll<Order>().Where(o => o.Status == OrderStatus.Pending && o.Items.Any(i => !i.Food.Vegetarian));
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
