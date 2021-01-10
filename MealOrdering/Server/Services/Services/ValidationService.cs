using MealOrdering.Server.Services.Infrastruce;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ValidationService(IHttpContextAccessor HttpContextAccessor)
        {
            httpContextAccessor = HttpContextAccessor;
        }

        public bool HasPermission(Guid UserId)
        {
            return IsAdmin(UserId) || HasPermissionToChange(UserId);
        }

        public bool HasPermissionToChange(Guid UserId)
        {
            String userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData).Value;

            return Guid.TryParse(userId, out Guid result) ? result == UserId : false;
        }

        public bool IsAdmin(Guid UserId)
        {
            return httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value == "salihcantekin1@gmail.com";
        }
    }
}
