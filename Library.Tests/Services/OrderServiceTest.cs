using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.Domain.Core.Models;
using Library.Domain.Interfaces;
using Library.Infrastructure.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Library.Tests.Services
{
    [TestClass]
    public class OrderServiceTest
    {
        [TestMethod]
        public void ShouldChargeFineOnOrdersForEveryOverdueDay()
        {
            // Arragne
            var mock = new Mock<IUnitOfWork>();
            List<Order> orders = new List<Order>
            {
                new Order { Status = Domain.Core.Enums.OrderStatus.OnHands, ReturnDate = DateTime.Now.AddDays(-1), LateFine = 0 },
                new Order { Status = Domain.Core.Enums.OrderStatus.OnHands, ReturnDate = DateTime.Now.AddDays(-2), LateFine = 0 },
                new Order { Status = Domain.Core.Enums.OrderStatus.OnHands, ReturnDate = DateTime.Now.AddDays(-3), LateFine = 0 },
            };

            mock.Setup(f => f.Orders.Find(It.IsAny<Expression<Func<Order, bool>>>()))
                .Returns((Expression<Func<Order, bool>> o) => orders
                .Where(f => f.Status == Domain.Core.Enums.OrderStatus.OnHands && f.ReturnDate < DateTime.Now));

            var service = new OrdersService(mock.Object);

            // Act
            service.ChargeFine();

            // Assert
            Assert.AreEqual(30, orders[2].LateFine);
        }
    }
}
