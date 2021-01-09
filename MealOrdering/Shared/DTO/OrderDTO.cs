using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MealOrdering.Shared.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid CreatedUserId { get; set; }

        public Guid SupplierId { get; set; }

        [MinLength(3, ErrorMessage = "Minimum lenght must be 3 characters for Name Field")]
        [StringLength(10)]
        public String Name { get; set; }

        [StringLength(100)]
        public String Description { get; set; }

        public DateTime ExpireDate { get; set; }


        public String CreatedUserFullName { get; set; }

        public String SupplierName { get; set; }
    }
}
