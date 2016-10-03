using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Product;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Product
{
    class ProductBrandEntityConfiguration : BaseEntityConfiguration<Models.Product.ProductBrand>
    {
        ProductBrandEntityConfiguration() {
            MakeRequired(fk => fk.GeneralStatus);
        }
    }
}
