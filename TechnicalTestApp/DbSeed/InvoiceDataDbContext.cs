using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DbSeed
{
    class InvoiceDataDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public string DbPath { get; set; }

        public InvoiceDataDbContext(string dbPath)
        {
            DbPath = dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlite($"Filename={DbPath}");
        }

        public Customer GetCustomerById(int customerId)
        {
            return this.Customers.Where(customer => customer.CustomerId == customerId).FirstOrDefault();
        }
    }
}
