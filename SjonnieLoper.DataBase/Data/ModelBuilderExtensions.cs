using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;
using SjonnieLoper.Core.Helpers;
using System;
using System.Collections.Generic;
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
                AgeYears = 21,
                Type = WhiskeyType.Blended,
                CountryOfOrigin = "Ireland",
                Price = 25M,
                Percentage = 0.4M,
                ImagePath = ImageNames.Img1,
                AmountInStorage = 22,
                SoftDeleted = false
            });

            modelBuilder.Entity<WhiskeyBase>().HasData(new WhiskeyBase
            {
                Id = 2,
                Name = "Talisker 10 years Gift Tube",
                Brand = "Talisker",
                AgeYears = 10,
                Type = WhiskeyType.Single_Malt,
                CountryOfOrigin = "Scotland",
                Price = 37.50M,
                Percentage = 0.45M,
                ImagePath = ImageNames.Img2,
                AmountInStorage = 20,
                SoftDeleted = false
            });

            modelBuilder.Entity<WhiskeyBase>().HasData(new WhiskeyBase
            {
                Id = 3,
                Name = "Jack Daniels",
                Brand = "Jack Daniels",
                AgeYears = 2,
                Type = WhiskeyType.Blended_Grain,
                CountryOfOrigin = "America",
                Price = 23.50M,
                Percentage = 0.40M,
                ImagePath = ImageNames.Img3,
                AmountInStorage = 10,
                SoftDeleted = false
            });

            modelBuilder.Entity<WhiskeyBase>().HasData(new WhiskeyBase
            {
                Id = 4,
                Name = "Glenfiddich Fire & Cane",
                Brand = "Glenfiddich",
                AgeYears = 18,
                Type = WhiskeyType.Single_Malt,
                CountryOfOrigin = "Scotland",
                Price = 44M,
                Percentage = 0.43M,
                ImagePath = ImageNames.Img2,
                AmountInStorage = 12,
                SoftDeleted = false
            });

            modelBuilder.Entity<WhiskeyBase>().HasData(new WhiskeyBase
            {
                Id = 5,
                Name = "Ardbeg 5 years Wee Beastie",
                Brand = "Ardbeg",
                AgeYears = 5,
                Type = WhiskeyType.Single_Malt,
                CountryOfOrigin = "Scotland",
                Price = 44M,
                Percentage = 0.47M,
                ImagePath = ImageNames.Img1,
                AmountInStorage = 18,
                SoftDeleted = false
            });


            modelBuilder.Entity<WhiskeyBase>().HasData(new WhiskeyBase
            {
                Id = 6,
                Name = "Lagavulin 9 years House Lannister ",
                Brand = "Lagavulin",
                AgeYears = 9,
                Type = WhiskeyType.Single_Malt,
                CountryOfOrigin = "Scotland",
                Price = 74.95M,
                Percentage = 0.46M,
                ImagePath = ImageNames.Img3,
                AmountInStorage = 7,
                SoftDeleted = false
            });
        }
    }
}
