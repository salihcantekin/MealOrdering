using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Services.Infrastruce;
using MealOrdering.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly MealOrderingDbContext context;

        public UserService(IMapper Mapper, MealOrderingDbContext Context)
        {
            mapper = Mapper;
            context = Context;
        }


        public async Task<UserDTO> CreateUser(UserDTO User)
        {
            var dbUser = await context.Users.Where(i => i.Id == User.Id).FirstOrDefaultAsync();

            if (dbUser != null)
                throw new Exception("İlgili Kayıt Zaten Mevcut");


            dbUser = mapper.Map<Data.Models.Users>(User);

            await context.Users.AddAsync(dbUser);
            int result = await context.SaveChangesAsync();

            return mapper.Map<UserDTO>(dbUser);
        }

        public async Task<bool> DeleteUserById(Guid Id)
        {
            var dbUser = await context.Users.Where(i => i.Id == Id).FirstOrDefaultAsync();

            if (dbUser == null)
                throw new Exception("Kullanıcı Bulunamadı");

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

        public async Task<UserDTO> UpdateUser(UserDTO User)
        {
            var dbUser = await context.Users.Where(i => i.Id == User.Id).FirstOrDefaultAsync();

            if (dbUser == null)
                throw new Exception("İlgili Kayıt Bulunamadı");

            
            mapper.Map(User, dbUser);

            int result = await context.SaveChangesAsync();

            return mapper.Map<UserDTO>(dbUser);
        }
    }
}
