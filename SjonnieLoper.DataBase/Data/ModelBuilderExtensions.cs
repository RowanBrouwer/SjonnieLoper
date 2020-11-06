using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SjonnieLoper.Core;
using SjonnieLoper.Core.Helpers;
using SjonnieLoper.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace SjonnieLoper.DataBase.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

        modelBuilder.Entity<WhiskeyBase>().HasData(new WhiskeyBase
            {
                Id = 1,
                Name = "Tullamore Dew",
                Brand = "Tullamore",
                DateOfBottling = new DateTime(1999, 8, 12),
                Type = WhiskeyType.Blended,
                CountryOforigin = "Ireland",
                Price = 25,
                Procentage = 0.4,
                ImagePath = ImageNames.Img1,
                AmountInStorage = 22,
                SoftDeleted = false
            });

            modelBuilder.Entity<WhiskeyBase>().HasData(new WhiskeyBase
            {
                Id = 2,
                Name = "Talisker 10 years Gift Tube",
                Brand = "Talisker",
                DateOfBottling = new DateTime(2010, 11, 3),
                Type = WhiskeyType.Single_Malt,
                CountryOforigin = "Scotland",
                Price = 37.50,
                Procentage = 0.45,
                ImagePath = ImageNames.Img2,
                AmountInStorage = 20,
                SoftDeleted = false
            });

            modelBuilder.Entity<WhiskeyBase>().HasData(new WhiskeyBase
            {
                Id = 3,
                Name = "Jack Daniels",
                Brand = "Jack Daniels",
                DateOfBottling = new DateTime(2018, 11, 3),
                Type = WhiskeyType.Blended_Grain,
                CountryOforigin = "America",
                Price = 23.50,
                Procentage = 0.40,
                ImagePath = ImageNames.Img3,
                AmountInStorage = 10,
                SoftDeleted = false
            });

            modelBuilder.Entity<WhiskeyBase>().HasData(new WhiskeyBase
            {
                Id = 4,
                Name = "Glenfiddich Fire & Cane",
                Brand = "Glenfiddich",
                DateOfBottling = new DateTime(2002, 10, 4),
                Type = WhiskeyType.Single_Malt,
                CountryOforigin = "Scotland",
                Price = 44,
                Procentage = 0.43,
                ImagePath = ImageNames.Img2,
                AmountInStorage = 12,
                SoftDeleted = false
            });

            modelBuilder.Entity<WhiskeyBase>().HasData(new WhiskeyBase
            {
                Id = 5,
                Name = "Ardbeg 5 years Wee Beastie",
                Brand = "Ardbeg",
                DateOfBottling = new DateTime(2016, 5, 9),
                Type = WhiskeyType.Single_Malt,
                CountryOforigin = "Scotland",
                Price = 44,
                Procentage = 0.47,
                ImagePath = ImageNames.Img1,
                AmountInStorage = 18,
                SoftDeleted = false
            });


            modelBuilder.Entity<WhiskeyBase>().HasData(new WhiskeyBase
            {
                Id = 6,
                Name = "Lagavulin 9 years House Lannister ",
                Brand = "Lagavulin",
                DateOfBottling = new DateTime(2011, 3, 12),
                Type = WhiskeyType.Single_Malt,
                CountryOforigin = "Scotland",
                Price = 74.95,
                Procentage = 0.46,
                ImagePath = ImageNames.Img3,
                AmountInStorage = 7,
                SoftDeleted = false
            });
                
            var hasher = new PasswordHasher<IdentityUser>();

            string ROLE_ID = Guid.NewGuid().ToString();
            string USER_ID = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole {Id= ROLE_ID, Name = "Employee", NormalizedName = "EMPLOYEE".ToUpper() });


            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
                {
                Id = USER_ID,
                FName = "Rowan",
                MName = "",
                LName = "Brouwer",
                SoftDeleted = false,
                Email = "Admin1@Admin1",
                NormalizedEmail = "ADMIN1@ADMIN1",
                EmailConfirmed = true,
                UserName = "Admin1@Admin1",
                NormalizedUserName = "ADMIN1@ADMIN1",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null, "!Admin123") 
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = USER_ID
            });
        }
    }
}
