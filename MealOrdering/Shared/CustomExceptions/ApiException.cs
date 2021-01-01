using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Shared.CustomExceptions
{
    public class ApiException: Exception
    {

        public ApiException(String Message, Exception InnerException): base(Message, InnerException)
        {

        }

        public ApiException(String Message): base(Message)
        {

        }

    }
}
