using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SjonnieLoper.Core;

namespace SjonnieLoper.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<WhiskeyBase> whiskeys { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }

    }
}
