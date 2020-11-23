using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Core.Helpers;
using SjonnieLoper.Core.Models;
using SjonnieLoper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;

namespace SjonnieLoper.DataBase.Data
{
    public class ApplicationDbInit
    {
        public static void Seed(UserManager<ApplicationUser> userManager, ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {
            /// <summary>
            /// Checks if a role namen Employee exists, if not it gets generated on startup.
            /// </summary>
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

            /// <summary>
            /// Checks the database for countries based on their name, if not exsisting generates them on startup.
            /// </summary>
            if (db.Countries.FirstOrDefault(c => c.Name == "Scotland") == null)
            {
                Country country = new Country
                {
                    Name = "Scotland"
                };
                db.Add(country);
                db.SaveChanges();
            }
            if (db.Countries.FirstOrDefault(c => c.Name == "America") == null)
            {
                Country country = new Country
                {
                    Name = "America"
                };
                db.Add(country);
                db.SaveChanges();
            }
            if (db.Countries.FirstOrDefault(c => c.Name == "Ireland") == null)
            {
                Country country = new Country
                {
                    Name = "Ireland"
                };
                db.Add(country);
                db.SaveChanges();
            }

            /// <summary>
            /// Checks the database for whiskeys based on their name, if not exsisting generates them on startup.
            /// </summary>
            if (db.Whiskeys.FirstOrDefault(w => w.Name == "Lagavulin 9 years House Lannister ") == null)
            {
                WhiskeyBase whiskey = new WhiskeyBase
                {
                    Name = "Lagavulin 9 years House Lannister ",
                    Brand = "Lagavulin",
                    AgeYears = 9,
                    Type = WhiskeyType.Single_Malt,
                    CountryOfOrigin = db.Countries.FirstOrDefault(c => c.Name == "Scotland"),
                    Price = 74.95m,
                    Percentage = 0.46m,
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
                    AgeYears = 4,
                    Type = WhiskeyType.Single_Malt,
                    CountryOfOrigin = db.Countries.FirstOrDefault(c => c.Name == "Scotland"),
                    Price = 44m,
                    Percentage = 0.47m,
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
                    AgeYears = 18,
                    Type = WhiskeyType.Single_Malt,
                    CountryOfOrigin = db.Countries.FirstOrDefault(c => c.Name == "Scotland"),
                    Price = 44m,
                    Percentage = 0.43m,
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
                    AgeYears = 5,
                    Type = WhiskeyType.Blended_Grain,
                    CountryOfOrigin = db.Countries.FirstOrDefault(c => c.Name == "America"),
                    Price = 23.50m,
                    Percentage = 0.40m,
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
                    AgeYears = 10,
                    Type = WhiskeyType.Single_Malt,
                    CountryOfOrigin = db.Countries.FirstOrDefault(c => c.Name == "Scotland"),
                    Price = 37.50m,
                    Percentage = 0.45m,
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
                    AgeYears = 21,
                    Type = WhiskeyType.Blended,
                    CountryOfOrigin = db.Countries.FirstOrDefault(c => c.Name == "Ireland"),
                    Price = 25m,
                    Percentage = 0.4m,
                    ImagePath = ImageNames.Img1,
                    AmountInStorage = 22,
                    SoftDeleted = false
                };
                db.Add(whiskey);
            }

            /// <summary>
            /// Checks the database for an User based on their UserName, if not exsisting generates it on startup.
            /// </summary>
            if (userManager.FindByNameAsync("Admin1@Admin1").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = "Rowan",
                    LastName = "Brouwer",
                    AgeYears = 23,
                    SoftDeleted = false,
                    Employee = true,
                    Email = "Admin1@Admin1",
                    NormalizedEmail = "ADMIN1@ADMIN1",
                    EmailConfirmed = true,
                    UserName = "Admin1@Admin1",
                    NormalizedUserName = "ADMIN1@ADMIN1",
                };

                IdentityResult result = userManager.CreateAsync(user, "!Admin123").Result;

                /// <summary>
                /// Adds the user to the Employee role and creates a claim based on the role.
                /// </summary>
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
    
