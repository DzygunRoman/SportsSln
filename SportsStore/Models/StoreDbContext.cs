using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class StoreDbContext:DbContext//базовый класс DbContext обеспечивает доступ к лежащей в основе функциональности EntityFramework Core,
    {//класс StoreDbContext добавляет свойства, которые будут применятся для чтения и записи данных приложения
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }//Предоставляет доступ к объектам Product в базе данных
        public DbSet<Order> Orders { get; set; }//возможность объектам Order сохраняться в базе данных
    }
}
