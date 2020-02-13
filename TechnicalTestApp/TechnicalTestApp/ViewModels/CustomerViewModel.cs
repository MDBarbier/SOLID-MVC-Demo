namespace TechnicalTestApp.ViewModels
{
    /// <summary>
    /// ViewModel providing the information required in the application GUI for Customers, some of which is calculated from other sources
    /// </summary>
    public class CustomerViewModel
    {
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public int MostRecentInvoiceRef { get; set; }
        public decimal MostRecentInvoiceAmount { get; set; }
        public decimal TotalOfAllOutstandingInvoices { get; set; }
        public decimal TotalOfAllPaidInvoices { get; set; }
        public long NumberOfOutstandingInvoices { get; set; }
    }
}
