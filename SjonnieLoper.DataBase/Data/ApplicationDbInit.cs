using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Core.Helpers;
using SjonnieLoper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SjonnieLoper.DataBase.Data
{
    public class ApplicationDbInit
    { 
        public static void Seed(UserManager<ApplicationUser> userManager, ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {

            if (roleManager.FindByNameAsync("Employee").Result == null)
            {
                    IdentityRole Employees = new IdentityRole
                    {
                        Name = "Employee",
                        NormalizedName = "EMPLOYEE"
                    };
                db.Add(Employees);
                    db.SaveChanges();
            };

                if (db.Whiskeys.FirstOrDefault(w => w.Name == "Lagavulin 9 years House Lannister ") == null)
                {
                    WhiskeyBase whiskey = new WhiskeyBase
                    {
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
                    };
                db.Add(whiskey);
                }

                if (db.Whiskeys.FirstOrDefault(w => w.Name == "Ardbeg 5 years Wee Beastie") == null)
                {
                    WhiskeyBase whiskey = new WhiskeyBase
                    {
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
                    };
                db.Add(whiskey);
                }
                if (db.Whiskeys.FirstOrDefault(w => w.Name == "Glenfiddich Fire & Cane") == null)
                {
                    WhiskeyBase whiskey = new WhiskeyBase
                    {
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
                    };
                db.Add(whiskey);
                }
                if (db.Whiskeys.FirstOrDefault(w => w.Name == "Jack Daniels") == null)
                {
                    WhiskeyBase whiskey = new WhiskeyBase
                    {
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
                    };
                db.Add(whiskey);
                }
                if (db.Whiskeys.FirstOrDefault(w => w.Name == "Talisker 10 years Gift Tube") == null)
                {
                    WhiskeyBase whiskey = new WhiskeyBase
                    {
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
                    };
                db.Add(whiskey);
                }
                if (db.Whiskeys.FirstOrDefault(w => w.Name == "Tullamore Dew") == null)
                {
                    WhiskeyBase whiskey = new WhiskeyBase
                    {
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
                    };
                db.Add(whiskey);
                }

                if (userManager.FindByNameAsync("Admin1@Admin1").Result == null)
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        FName = "Rowan",
                        MName = "",
                        LName = "Brouwer",
                        SoftDeleted = false,
                        Employee = true,
                        Email = "Admin1@Admin1",
                        NormalizedEmail = "ADMIN1@ADMIN1",
                        EmailConfirmed = true,
                        UserName = "Admin1@Admin1",
                        NormalizedUserName = "ADMIN1@ADMIN1",
                    };

                    IdentityResult result = userManager.CreateAsync(user, "!Admin123").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Employee").Wait();
                        var roleId = db.UserRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId.ToString();
                        userManager.AddClaimAsync(user, new Claim("EmployeeRole", roleId)).Wait();
                    }
                    db.Add(user);
                }
                db.SaveChanges();
        }
    }
}
    
