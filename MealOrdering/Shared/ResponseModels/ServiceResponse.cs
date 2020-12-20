using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Shared.ResponseModels
{
    public class ServiceResponse<T>: BaseResponse
    {
        public T Value { get; set; }
    }
}
