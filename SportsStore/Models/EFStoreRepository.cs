using System.Linq;

namespace SportsStore.Models
{
    public class EFStoreRepository:IStoreRepository//Патерн "Хранилище"
    {
        private StoreDbContext context;
        public EFStoreRepository(StoreDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;
    }
}
