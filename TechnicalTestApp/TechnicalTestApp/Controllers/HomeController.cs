using Microsoft.AspNetCore.Mvc;
using TechnicalTestApp.ServiceLayer;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.Controllers
{
    /// <summary>
    /// Main controller of the application
    /// </summary>
    public class HomeController : Controller
    {        
        private ICustomerAccessMethods CustomerAccessMethods;
        private IInvoiceAccessMethods InvoiceAccessMethods;

        public HomeController(IInvoiceAccessMethods invoiceAccessMethods, ICustomerAccessMethods customerAccessMethods)
        {            
            CustomerAccessMethods = customerAccessMethods;
            InvoiceAccessMethods = invoiceAccessMethods;            
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                TotalPaidInvoiceCount = InvoiceAccessMethods.GetSumOfInvoicesHeld(true),
                PaidInvoiceTotal = InvoiceAccessMethods.GetTotalFundsInvoiced(),
                Customers = CustomerAccessMethods.GetCustomers()
            };

            return View(homeViewModel);
        }        
    }
}
