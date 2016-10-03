using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Product;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Product
{
    class ProductEntityConfiguration : BaseEntityConfiguration<Models.Product.Product>
    {
        ProductEntityConfiguration() {
            MakeRequired(fk => fk.GeneralMedia);
            MakeRequired(fk => fk.ProductBrand);
            MakeRequired(fk => fk.ProductUnitOfMeasure);
        }
    }
}
