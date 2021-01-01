using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Shared.CustomExceptions
{
    public class HttpException : Exception
    {
        public HttpException(String Message) : base(Message) { }

        public HttpException(String Message, Exception InnerException) : base(Message, InnerException) { }
    }
}
