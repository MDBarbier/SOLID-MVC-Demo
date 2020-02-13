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
    public class InvoiceTests
    {
        [Theory]
        [InlineData(457, 384)]
        public void TestGetInvoiceById(int invoiceId, int value)
        {
            //Arrange
            var mockSet = new Mock<DbSet<Invoice>>();
            var fixture = new Fixture();
            IQueryable<Invoice> data = CreateInvoicesQueryable(fixture, invoiceId, value);
            SetupInvoiceMockSet(mockSet, data);
            var mockContext = new Mock<IApplicationDatabaseContext>();
            mockContext.Setup(m => m.Invoices).Returns(mockSet.Object);
            var service = new InvoiceAccessMethods(mockContext.Object);

            //Act
            var invoice = service.GetInvoiceById(invoiceId);

            //Assert
            invoice.Value.Should().Be(value);
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
