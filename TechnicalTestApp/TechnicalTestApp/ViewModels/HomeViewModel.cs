using System.Collections.Generic;

namespace TechnicalTestApp.ViewModels
{
    /// <summary>
    /// Contains the information used by the view of the Home page
    /// </summary>
    public class HomeViewModel
    {
        public decimal PaidInvoiceTotal { get; set; }
        public long TotalPaidInvoiceCount { get; set; }
        public Dictionary<int, CustomerViewModel> Customers { get; set; }
    }
}
