using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Server.Data.Models
{
    public class Users
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String EMailAddress { get; set; }

        public String Password { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }

        public virtual ICollection<OrderItems> CreatedOrderItems { get; set; }
    }
}
