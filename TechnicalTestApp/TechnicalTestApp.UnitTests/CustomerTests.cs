using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;
using TechnicalTestApp.ServiceLayer;
using Xunit;

namespace TechnicalTestApp.UnitTests
{
    public class CustomerTests
    {
        [Theory]
        [InlineData(384, "Mr Smith")]
        public void TestGetCustomerById(int customerId, string customerName)
        {
            //Arrange            
            var mockSet = new Mock<DbSet<Customer>>();
            var fixture = new Fixture();
            IQueryable<Customer> data = CreateCustomerQueryable(fixture, customerId, customerName);
            SetupCustomerMockSet(mockSet, data);
            var mockContext = new Mock<IApplicationDatabaseContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);            
            var service = new CustomerAccessMethods(mockContext.Object);

            //Act
            var customer = service.GetCustomerById(customerId);

            //Assert
            customer.Name.Should().Be(customerName);
        }

        [Theory]
        [InlineData(384, "Mr Smith", 123, 999)]
        public void TestGetCustomers(int customerId, string customerName, int invoiceId, int value)
        {
            //Arrange            
            var mockSet = new Mock<DbSet<Customer>>();
            var mockSetInvoices = new Mock<DbSet<Invoice>>();
            var customerFixture = new Fixture();
            var invoiceFixture = new Fixture();
            IQueryable<Customer> data = CreateCustomerQueryable(customerFixture, customerId, customerName);
            SetupCustomerMockSet(mockSet, data);
            IQueryable<Invoice> invoiceData = CreateInvoicesQueryable(invoiceFixture, invoiceId, value);
            SetupInvoiceMockSet(mockSetInvoices, invoiceData);
            var mockContext = new Mock<IApplicationDatabaseContext>();

            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            mockContext.Setup(m => m.Invoices).Returns(mockSetInvoices.Object);            
            var customerService = new CustomerAccessMethods(mockContext.Object);

            //Act
            var customer = customerService.GetAllCustomers();

            //Assert
            customer.Count.Should().Be(1);
        }

        private static IQueryable<Customer> CreateCustomerQueryable(Fixture fixture, int customerId, string customerName)
        {
            return new List<Customer>()
            {
                fixture.Build<Customer>().With(u => u.Name, customerName).With(u => u.CustomerId, customerId).Create()
            }.AsQueryable();
        }

        private static void SetupCustomerMockSet(Mock<DbSet<Customer>> mockSet, IQueryable<Customer> data)
        {
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }

        private static IQueryable<Invoice> CreateInvoicesQueryable(Fixture fixture, int invoiceId, int value)
        {
            return new List<Invoice>()
            {
                fixture.Build<Invoice>().With(u => u.InvoiceId, invoiceId).With(u => u.Value, value).Create()
            }.AsQueryable();
        }

        private static void SetupInvoiceMockSet(Mock<DbSet<Invoice>> mockSet, IQueryable<Invoice> data)
        {
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }
    }
}
