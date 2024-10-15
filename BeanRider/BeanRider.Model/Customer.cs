namespace BeanRider.Model
{
    public class Customer : Entity
    {
        public required string Name { get; set; }
        public required string Number { get; set; }

        // Navigation property
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }

}
