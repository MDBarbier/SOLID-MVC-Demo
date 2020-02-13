using Microsoft.EntityFrameworkCore;
using TechnicalTestApp.Models;

namespace TechnicalTestApp.Database
{
    /// <summary>
    /// Interface for EF Core DbContext file to follow IOC and allow mocking
    /// </summary>
    public interface IApplicationDatabaseContext : IDatabaseContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
