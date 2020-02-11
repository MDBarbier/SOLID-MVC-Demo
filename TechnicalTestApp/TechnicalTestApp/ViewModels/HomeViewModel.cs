using System.Collections.Generic;
using TechnicalTestApp.Models;

namespace TechnicalTestApp.ViewModels
{
    public class HomeViewModel
    {
        public decimal PaidInvoiceTotal { get; set; }
        public long TotalPaidInvoiceCount { get; set; }
        public Dictionary<int, CustomerData> Customers { get; set; }
    }
}
