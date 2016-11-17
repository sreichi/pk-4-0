using Microsoft.AspNetCore.Builder;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database
{
    public static class DataSeeder
    {
        public static void SeedData(this IApplicationBuilder applicationBuilder)
        {
            var db = applicationBuilder.ApplicationServices.GetService(typeof (ApplicationDbContext)) as ApplicationDbContext;
            CreateTestData(db);
        }

        private static void CreateTestData(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            CreateRoles(dbContext);
        }

        private static void CreateRoles(ApplicationDbContext dbContext)
        {
            dbContext.Role.Add(
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                });
            dbContext.Role.Add(
                new Role
                {
                    Id = 2,
                    Name = "User"
                });
        }
    }
}