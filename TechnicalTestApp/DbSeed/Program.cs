using Microsoft.EntityFrameworkCore;
using System;

namespace DbSeed
{
    class Program
    {
        static void Main(string[] args)
        {
            int startingCustomerId = 1;
            int startingInvoiceId = 1;
            int numberOfCustomersToInsert = 750;
            string dbPath = string.Empty;
            Random numberOfInvoicesGenerator = new Random();
            Random isPaidGenerator = new Random();
            Random poundsGenerator = new Random();
            Random penceGenerator = new Random();
            Random referenceGenerator = new Random();


            Console.WriteLine("Starting db seed program");

            if (args.Length != 1)
            {
                throw new Exception("Unexpected number of arguments");
            }

            try
            {                
                dbPath = args[0].ToString();
                Console.WriteLine("Collected dbpath from parameter: " + dbPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not read the database path from the arguments. Error: " + ex.Message);
                Environment.Exit(-1);
            }            
            
            Console.WriteLine("Setting up db context");
            InvoiceDataDbContext mdbc = null;

            try
            {
                mdbc = new InvoiceDataDbContext(dbPath);
            }
            catch (Exception dex)
            {
                Console.WriteLine("Could not create database context, check db path: " + dex.Message);
                Environment.Exit(-1);
            }

            Console.WriteLine("Deleting all existing data");
            mdbc.Database.ExecuteSqlRaw("delete from Customers; delete from Invoices;");           

            int currentCustomerId = startingCustomerId;
            int currentInvoiceId = startingInvoiceId;

            Console.WriteLine($"Adding {numberOfCustomersToInsert} new customers and a random number of invoices");

            for (int i = 0; i < numberOfCustomersToInsert; i++)
            {
                Console.WriteLine($"Creating customer {i} of {numberOfCustomersToInsert}");

                Customer c = new Customer()
                {
                    Address1 = "Test Road",
                    Address2 = "Test Town",
                    CustomerId = currentCustomerId,
                    Name = "Test Customer #" + currentCustomerId,
                    Postcode = "Test Post Code",
                    Telephone = "123 4567889"
                };
                
                var numbeOfInvoices = numberOfInvoicesGenerator.Next(50, 250);

                for (int k = 0; k < numbeOfInvoices; k++)
                {
                    var isPaid = isPaidGenerator.Next(0, 100) > 50 ? true: false;                    
                    var pounds = poundsGenerator.Next(1, 2000);
                    var pence = penceGenerator.Next(0, 99);
                    
                    Invoice invoice = new Invoice()
                    {
                        CustomerId = currentCustomerId,
                        InvoiceDate = DateTime.Now,
                        InvoiceId = currentInvoiceId,
                        IsPaid = isPaid,
                        Ref = referenceGenerator.Next(0, 9999999).ToString(),
                        Value = decimal.Parse($"{pounds}.{pence}")
                    };

                    mdbc.Invoices.Add(invoice);
                    currentInvoiceId++;
                }

                mdbc.Customers.Add(c);                

                currentCustomerId++;
            }

            mdbc.SaveChanges();

            Console.WriteLine("Execution completed");            
        }
    }
}
