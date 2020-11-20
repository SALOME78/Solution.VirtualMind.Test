using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Test.Model;

namespace VirtualMind.Test.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ExchangeCurrency> ExchangeCurrency { get; set; }
        public DbSet<CurrencyPurchase> CurrencyPurchase { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Documents> Documents { get; set; }
    }
}
