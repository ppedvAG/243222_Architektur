﻿namespace BeanRider.Model
{
    public class Food : Entity
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public int KCal { get; set; }
        public bool Vegan { get; set; }
        public bool Alc { get; set; }
        public bool Vegetarian { get; set; }

        // Navigation property
        public virtual HashSet<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }

}
