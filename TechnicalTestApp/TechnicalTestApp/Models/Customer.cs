namespace TechnicalTestApp.Models
{
    /// <summary>
    /// Representation of the Customer table from the CustomerInvoiceData database
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }

    }
}
