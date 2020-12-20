using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Server.Data.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MealOrderingDbContext>
    {
        public MealOrderingDbContext CreateDbContext(string[] args)
        {
            String connectionString = "User ID=pgloader_user;password=pgloader_user;Host=db.postgres.local;Port=5432;Database=mealordering;SearchPath=public";

            var builder = new DbContextOptionsBuilder<MealOrderingDbContext>();
            
            builder.UseNpgsql(connectionString);

            return new MealOrderingDbContext(builder.Options);
        }
    }
}
