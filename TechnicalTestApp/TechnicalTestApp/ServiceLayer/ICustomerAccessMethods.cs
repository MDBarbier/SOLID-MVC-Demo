using System.Collections.Generic;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.ServiceLayer
{
    /// <summary>
    /// Interface for the CustomerAccessMethods which contains business logic pertaining to the Customer Model
    /// </summary>
    public interface ICustomerAccessMethods
    {
        public IApplicationDatabaseContext DbContext { get; }

        public abstract Customer GetCustomerById(int customerId);

        public abstract Dictionary<int, CustomerViewModel> GetCustomers();
    }
}
