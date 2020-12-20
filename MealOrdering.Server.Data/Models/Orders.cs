using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Server.Data.Models
{
    public class Orders
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid CreatedUserId { get; set; }

        public Guid SupplierId { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public DateTime ExpireDate { get; set; }


        public virtual Users CreatedUser { get; set; }

        public virtual Suppliers Supplier { get; set; }

        public virtual ICollection<OrderItems> OrderItems { get; set; }

    }
}
