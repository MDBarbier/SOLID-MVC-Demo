using System.Collections.Generic;
using System.Linq;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;

namespace TechnicalTestApp.DataAccessLayer
{
    public class CustomerAccessMethods
    {
        private readonly DatabaseContext _myDbContext;

        public CustomerAccessMethods(DatabaseContext databaseContext)
        {
            _myDbContext = databaseContext;
        }

        public Customer GetCustomerById(int customerId)
        {
            return _myDbContext.Customers.Where(customer => customer.CustomerId == customerId).FirstOrDefault();
        }

        public Dictionary<int, CustomerData> GetCustomers()
        {
            var customers = _myDbContext.Customers.ToList();
            var customerDataList = new Dictionary<int, CustomerData>();
            var invoiceAccessMethods = new InvoiceAccessMethods(_myDbContext);

            foreach (var customer in customers)
            {
                var customerToAdd = new CustomerData();                                
                customerToAdd.Name = customer.Name;

                //Calculate most recent invoice ref
                customerToAdd.MostRecentInvoiceRef = invoiceAccessMethods.GetMostRecentInvoiceRef(customer.CustomerId);

                //Calculate most recent invoice amount
                customerToAdd.MostRecentInvoiceAmount = invoiceAccessMethods.GetMostRecentInvoiceAmount(customer.CustomerId);

                //Calculate number of outstanding invoices
                customerToAdd.NumberOfOutstandingInvoices = invoiceAccessMethods.GetNumberOfOutstandingInvoicesForCustomer(customer.CustomerId);

                //Calculate amount of all outstanding invoices
                customerToAdd.TotalOfAllOutstandingInvoices = invoiceAccessMethods.GetAmountOwedOnInvoices(customer.CustomerId, false);

                //Calculate total of all paid invoices
                customerToAdd.TotalOfAllPaidInvoices = invoiceAccessMethods.GetAmountOwedOnInvoices(customer.CustomerId, true);

                customerDataList.Add(customer.CustomerId, customerToAdd);
            }

            return customerDataList;
        }
    }
}
