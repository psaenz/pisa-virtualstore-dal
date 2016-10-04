using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Product;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Product
{
    class ProductEntityConfiguration : BaseEntityConfiguration<Models.Product.ProductInfo>
    {
        ProductEntityConfiguration() {
            MakeOptional(fk => fk.GeneralMedia, fk=> fk.GeneralMediaId);
            MakeRequired(fk => fk.ProductBrand);
            MakeRequired(fk => fk.ProductUnitOfMeasure);
        }
    }
}
