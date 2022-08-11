using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models
{
    public class EFOrderRepository:IOrderRepository//Позволяет извлекать набор сохраненных объектов Order и создавать либо изменять заказы
    {
        private StoreDbContext context;
        public EFOrderRepository(StoreDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);
        public void SaveOrder(Order order) 
        {
            context.AttachRange(order.Lines.Select(l => l.Product));//Объекты существуют и не должны сохраняться в базе данных до тех порб пока они не будут модифицированы
            if(order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
