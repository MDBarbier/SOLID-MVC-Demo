using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;

namespace TechnicalTestApp.ServiceLayer
{
    /// <summary>
    /// Business logic pertaining to the Invoice Model
    /// </summary>
    public class InvoiceAccessMethods : IInvoiceAccessMethods
    {
        public IApplicationDatabaseContext DbContext { get; }

        public InvoiceAccessMethods(IApplicationDatabaseContext databaseContext)
        {
            DbContext = databaseContext;
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return DbContext.Invoices.Where(invoice => invoice.InvoiceId == invoiceId).FirstOrDefault();
        }

        public Dictionary<int, Invoice> GetAllInvoices()
        {
            return DbContext.Invoices.AsNoTracking().ToDictionary(invoice => invoice.InvoiceId, invoice => invoice);
        }

        public long GetSumOfInvoicesHeld(bool paidInvoicesOnly)
        {
            return 
                paidInvoicesOnly ? 
                DbContext.Invoices.Where(invoice => invoice.IsPaid).LongCount() :
                DbContext.Invoices.LongCount();          
        }

        public decimal GetTotalFundsInvoiced()
        {
            return DbContext.Invoices.Where(invoice => invoice.IsPaid).Select(invoice => invoice.Value).AsEnumerable().Sum();
        }
    }
}
