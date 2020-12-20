using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Shared.DTO
{
    public class SupplierDTO
    {
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        public String Name { get; set; }

        public String WebURL { get; set; }

        public bool IsActive { get; set; }
    }
}
