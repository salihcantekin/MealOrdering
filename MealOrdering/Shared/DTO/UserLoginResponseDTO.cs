using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Shared.DTO
{
    public class UserLoginResponseDTO
    {
        public String ApiToken { get; set; }

        public UserDTO User { get; set; }

    }
}
