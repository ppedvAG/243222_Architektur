using BeanRider.Model.DomainModel;
using BeanRider.UI.Api.Web.Model;

namespace BeanRider.UI.Api.Web.Mapper
{
    public class OrderMapper
    {
        // Mapping von Order zu OrderDTO
        public static OrderDTO ToDTO(Order order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                Time = order.Time,
                ToGo = order.ToGo,
                Status = order.Status.ToString(), // Enum zu String
                CustomerName = order.Customer.Name,
                CustomerId = order.Customer.Id
                // Items können hier auch gemappt werden, falls nötig
            };
        }

        // Mapping von OrderDTO zu Order
        public static Order ToEntity(OrderDTO orderDTO, Customer customer)
        {
            return new Order
            {
                Id = orderDTO.Id,
                Time = orderDTO.Time,
                ToGo = orderDTO.ToGo,
                Status = Enum.Parse<OrderStatus>(orderDTO.Status), // String zu Enum
                Customer = customer,
                
                // Items können hier auch gemappt werden, falls nötig
            };
        }
    }

}
