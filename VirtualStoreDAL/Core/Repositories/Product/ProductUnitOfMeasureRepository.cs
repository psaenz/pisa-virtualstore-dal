using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Product;
using System.Linq;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Product
{
    public class ProductUnitOfMeasureRepository : BaseRepository<ProductUnitOfMeasure>
    {
        public ProductUnitOfMeasureRepository(VirtualStoreDbContext context) : base(context)
        {
        }

        public ProductUnitOfMeasure GetByName(string name)
        {
            return this.Context.ProductsUnitsOfMeasures.Where(c => c.Name == name).FirstOrDefault();
        }
    }
}
