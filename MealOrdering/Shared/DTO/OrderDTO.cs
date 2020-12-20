using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Shared.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid CreatedUserId { get; set; }

        public Guid SupplierId { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public DateTime ExpireDate { get; set; }


        public String CreatedUserFullName { get; set; }

        public String SupplierName { get; set; }
    }
}
