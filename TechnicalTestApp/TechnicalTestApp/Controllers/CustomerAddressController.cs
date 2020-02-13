using Microsoft.AspNetCore.Mvc;
using TechnicalTestApp.ServiceLayer;
using TechnicalTestApp.Database;

namespace TechnicalTestApp.Controllers
{
    /// <summary>
    /// Web API controller which allows a GET and supplies the address for the specified customer
    /// Paramters:
    /// - customerId: The id of the customer to find
    /// Returns: 
    /// - a string array containing the name of the customer and the composite address
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private CustomerAccessMethods CustomerAccessMethods;

        public CustomerAddressController(IApplicationDatabaseContext databaseContext, IInvoiceAccessMethods invoiceAccessMethods)
        {
            CustomerAccessMethods = new CustomerAccessMethods(databaseContext);
        }
            
        // GET: api/CustomerAddress/{customerId}
        [HttpGet("{customerId}", Name = "Get")]
        public string[] Get(int customerId)
        {
            var customerRecord = CustomerAccessMethods.GetCustomerById(customerId);

            var customerAddressComposite = customerRecord.Address1;

            if (!string.IsNullOrEmpty(customerRecord.Address2))
            {
                customerAddressComposite += $", {customerRecord.Address2}";
            }

            customerAddressComposite += $", {customerRecord.Postcode}";

            return new string[] { customerRecord.Name, customerAddressComposite };            
            
        }
    }
}
