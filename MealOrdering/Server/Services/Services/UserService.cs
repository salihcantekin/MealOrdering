using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Services.Infrastruce;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly MealOrderingDbContext context;
        private readonly IConfiguration configuration;

        public UserService(IMapper Mapper, MealOrderingDbContext Context, IConfiguration Configuration)
        {
            mapper = Mapper;
            context = Context;
            configuration = Configuration;
        }


        public async Task<UserDTO> CreateUser(UserDTO User)
        {
            var dbUser = await context.Users.Where(i => i.Id == User.Id).FirstOrDefaultAsync();

            if (dbUser != null)
                throw new Exception("User already exists");


            dbUser = mapper.Map<Data.Models.Users>(User);

            await context.Users.AddAsync(dbUser);
            int result = await context.SaveChangesAsync();

            return mapper.Map<UserDTO>(dbUser);
        }

        public async Task<bool> DeleteUserById(Guid Id)
        {
            var dbUser = await context.Users.FirstOrDefaultAsync(i => i.Id == Id);

            if (dbUser == null)
                throw new Exception("User not found");

            context.Users.Remove(dbUser);
            int result = await context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<UserDTO> GetUserById(Guid Id)
        {
            return await context.Users
                        .Where(i => i.Id == Id)
                        .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            return await context.Users
                        .Where(i => i.IsActive)
                        .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        public async Task<UserLoginResponseDTO> Login(string EMail, string Password)
        {
            // Veritabanı Kullanıcı Doğrulama İşlemleri Yapıldı.

            var encryptedPassword = PasswordEncrypter.Encrypt(Password);

            var dbUser = await context.Users.FirstOrDefaultAsync(i => i.EMailAddress == EMail && i.Password == encryptedPassword);

            if (dbUser == null)
                throw new Exception("User not found or given information is wrong");

            if (!dbUser.IsActive)
                throw new Exception("User state is Passive!");


            UserLoginResponseDTO result = new UserLoginResponseDTO();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(int.Parse(configuration["JwtExpiryInDays"].ToString()));

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, EMail),
                new Claim(ClaimTypes.Name, dbUser.FirstName + " " + dbUser.LastName),
                new Claim(ClaimTypes.UserData, dbUser.Id.ToString())
            };

            var token = new JwtSecurityToken(configuration["JwtIssuer"], configuration["JwtAudience"], claims, null, expiry, creds);

            result.ApiToken = new JwtSecurityTokenHandler().WriteToken(token);
            result.User = mapper.Map<UserDTO>(dbUser);

            return result;
        }

        public async Task<UserDTO> UpdateUser(UserDTO User)
        {
            var dbUser = await context.Users.Where(i => i.Id == User.Id).FirstOrDefaultAsync();

            if (dbUser == null)
                throw new Exception("User not found");


            mapper.Map(User, dbUser);

            int result = await context.SaveChangesAsync();

            return mapper.Map<UserDTO>(dbUser);
        }

        
    }
}
