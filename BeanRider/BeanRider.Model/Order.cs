﻿namespace BeanRider.Model
{
    public class Order : Entity
    {
        public DateTime Time { get; set; } = DateTime.Now;
        public bool ToGo { get; set; }
        public OrderStatus Status { get; set; }
        
        // Foreign key and navigation property
        public required virtual Customer Customer { get; set; }

        public virtual HashSet<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
    }

    public enum OrderStatus
    {
        New,
        Pending,
        InProgress,
        Ready,
        Delivered,
        Canceled
    }
}
