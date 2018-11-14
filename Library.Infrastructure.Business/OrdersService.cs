using Library.Domain.Interfaces;
using Library.Services.Interfaces;
using System;
using System.Linq;

namespace Library.Infrastructure.Business
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void ChargeFine()
        {
            DateTime dateTime = DateTime.Now;
            TimeSpan ts = new TimeSpan(23, 59, 59);
            dateTime = dateTime.Date + ts;

            var overdueOrders = this.unitOfWork.Orders
                .Find(t => t.Status == Domain.Core.Enums.OrderStatus.OnHands)
                .Where(ord => ord.ReturnDate.Value < dateTime).ToList();

            foreach (var order in overdueOrders)
            {
                var penalty = dateTime.Subtract(order.ReturnDate.Value).Days;

                if (order.LateFine == 0)
                {
                    order.LateFine += 10 * penalty;
                }
                else
                {
                    order.LateFine += 10;
                }

                order.Status = Domain.Core.Enums.OrderStatus.Overdue;
                this.unitOfWork.Orders.Update(order.Id, order);
                this.unitOfWork.Save();
            }
        }
    }
}
