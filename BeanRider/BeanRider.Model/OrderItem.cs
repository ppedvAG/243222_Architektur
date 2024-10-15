namespace BeanRider.Model
{
    public class OrderItem : Entity
    {
        public int Amount { get; set; } = 1;
        public decimal? OrderPrice { get; set; }
        public decimal? Discount { get; set; }

        // Foreign key and navigation property
        public required virtual Food Food { get; set; }
        public required virtual Order Order { get; set; }
    }

}
