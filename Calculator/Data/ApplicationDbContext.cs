using Calculator.Models.AppModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PaymentStructure> PaymentStructures { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<EffectiveDate> EffectiveDates { get; set; }
        public DbSet<PayCategory> PayCategorys { get; set; }


    }
}
