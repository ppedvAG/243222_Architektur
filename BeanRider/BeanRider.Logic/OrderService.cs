using BeanRider.Model;

namespace BeanRider.Logic
{
    public class OrderService
    {

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
