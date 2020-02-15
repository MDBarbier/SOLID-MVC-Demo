using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTestApp.Models;
using TechnicalTestApp.ServiceLayer;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.Controllers
{
    /// <summary>
    /// Main controller of the application
    /// </summary>
    public class HomeController : Controller
    {        
        private readonly ICustomerAccessMethods CustomerAccessMethods;
        private readonly IInvoiceAccessMethods InvoiceAccessMethods;

        public HomeController(IInvoiceAccessMethods invoiceAccessMethods, ICustomerAccessMethods customerAccessMethods)
        {            
            CustomerAccessMethods = customerAccessMethods;
            InvoiceAccessMethods = invoiceAccessMethods;            
        }

        public IActionResult Index()
        {
            var sw = new Stopwatch();
            sw.Start();

            var homeViewModel = new HomeViewModel()
            {
                TotalPaidInvoiceCount = InvoiceAccessMethods.GetSumOfInvoicesHeld(true),
                PaidInvoiceTotal = InvoiceAccessMethods.GetTotalFundsInvoiced(),
                Customers = GetCustomers()
            };

            sw.Stop();
            homeViewModel.LoadingTime = sw.ElapsedMilliseconds;

            return View(homeViewModel);
        }        

        private Dictionary<int, CustomerViewModel> GetCustomers()
        {
            var customers = CustomerAccessMethods.GetAllCustomers();
            var invoices = InvoiceAccessMethods.GetAllInvoices();

            var customerDataList = new Dictionary<int, CustomerViewModel>();

            Parallel.ForEach(customers, (customer) =>
            {

                var customerToAdd = new CustomerViewModel
                {
                    Name = customer.Value.Name
                };

                var customerInvoices = GetInvoicesForCustomer(customer.Key, invoices);

                var mostRecentInvoice = GetMostRecentInvoiceForCustomer(customerInvoices);
                customerToAdd.MostRecentInvoiceRef = mostRecentInvoice.InvoiceId;
                customerToAdd.MostRecentInvoiceAmount = mostRecentInvoice.Value;
                customerToAdd.NumberOfOutstandingInvoices = GetNumberOfOutstandingInvoicesForCustomer(customerInvoices);
                customerToAdd.TotalOfAllOutstandingInvoices = GetAmountOwedOnOutstandingInvoicesForCustomer(customerInvoices);
                customerToAdd.TotalOfAllPaidInvoices = GetAmountPaidOnInvoices(customerInvoices);

                lock (customerDataList)
                {
                    customerDataList.Add(customer.Key, customerToAdd);
                }

            });

            return customerDataList;
        }

        private Dictionary<int, Invoice> GetInvoicesForCustomer(int customerId, Dictionary<int, Invoice> Invoices)
        {
            return Invoices.Values.Where(invoice => invoice.CustomerId == customerId).ToDictionary(invoice => invoice.InvoiceId, invoice => invoice);
        }

        private Invoice GetMostRecentInvoiceForCustomer(Dictionary<int, Invoice> Invoices)
        {
            return Invoices.Values
                .OrderByDescending(invoice => invoice.InvoiceDate)
                .FirstOrDefault();
        }

        private long GetNumberOfOutstandingInvoicesForCustomer(Dictionary<int, Invoice> Invoices)
        {
            return Invoices.Values.Where(i => !i.IsPaid).LongCount();
        }
        private decimal GetAmountOwedOnOutstandingInvoicesForCustomer(Dictionary<int, Invoice> Invoices)
        {
            return Invoices.Values.Where(i => !i.IsPaid).Select(i => i.Value).AsEnumerable().Sum();
        }
        private decimal GetAmountPaidOnInvoices(Dictionary<int, Invoice> Invoices)
        {
            return Invoices.Values.Where(i => i.IsPaid).Select(i => i.Value).AsEnumerable().Sum();
        }
    }
}
