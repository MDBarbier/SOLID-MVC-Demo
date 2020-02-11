using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using TechnicalTestApp.DataAccessLayer;
using TechnicalTestApp.Database;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CustomerAccessMethods CustomerAccessMethods;
        private InvoiceAccessMethods InvoiceAccessMethods;

        public HomeController(ILogger<HomeController> logger)
        {            
            CustomerAccessMethods = new CustomerAccessMethods(new DatabaseContext());
            InvoiceAccessMethods = new InvoiceAccessMethods(new DatabaseContext());
            _logger = logger;
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

        [Route("Home/Address/{customerId}")]
        public string GetCustomerAddress(int customerId)
        {
            //TODO: wire this up to a db method which gets customer address
            return "12 Daniels Walk";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
