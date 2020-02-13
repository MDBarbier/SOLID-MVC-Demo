using Microsoft.EntityFrameworkCore;
using TechnicalTestApp.Models;

namespace TechnicalTestApp.Database
{
    /// <summary>
    /// EF Core DbContext file, implements an associated interface to follow IOC and allow mocking
    /// </summary>
    public class DatabaseContext : DbContext, IApplicationDatabaseContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbContext Instance => this;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Database\\CustomerInvoiceData.db");
        }

    }
}
