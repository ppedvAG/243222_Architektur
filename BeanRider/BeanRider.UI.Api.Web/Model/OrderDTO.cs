namespace BeanRider.UI.Api.Web.Model
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime Time { get; set; } 
        public bool ToGo { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public int CustomerId { get; set; }

    }

}
