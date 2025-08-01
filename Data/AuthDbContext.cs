using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Baykasoglu.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "c7e2e080-67c2-47f3-8ea7-f8b19e9e2dc4";
            var writerRoleId = "87fb338b-42b0-4333-b9da-18183b141c5e";
            var adminUserId = "de529a21-4b9a-4e1a-a48f-71484412f7e0";

            // Roles (sabit tüm alanlar)
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                    ConcurrencyStamp = "reader-concurrency-stamp"
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER",
                    ConcurrencyStamp = "writer-concurrency-stamp"
                }
            );




            var hasher = new PasswordHasher<IdentityUser>();
            var adminUser = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "ismetmetebaykasoglu1@gmail.com",
                NormalizedEmail = "ISMETMETEBAYKASOGLU1@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = "static-security-stamp",
                ConcurrencyStamp = "static-concurrency-stamp"
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123!"); // 🔐 düz şifre burada tanımlanır

            builder.Entity<IdentityUser>().HasData(adminUser);



            // Admin kullanıcı (sabit hash: "1234")
          /*  builder.Entity<IdentityUser>().HasData(
                 new IdentityUser
                {
                    Id = adminUserId,
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "ismetmetebaykasoglu@gmail.com",
                    NormalizedEmail = "ISMETMETEBAYKASOGLU@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEDNQITn3TH09CtsFgDgEvKz1H8XpCHMZ3TtZ27NwzL4yqej7WbQb0SgmTSKvdShc7Q==",
                    SecurityStamp = "static-security-stamp",
                    ConcurrencyStamp = "static-concurrency-stamp"
                }
            );
          */
            // Admin'e rollerini ata
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = readerRoleId },
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = writerRoleId }
            );
        }
      
    }
}
