using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Product;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Product
{
    class ProductTypeEntityConfiguration : BaseEntityConfiguration<Models.Product.ProductType>
    {
        ProductTypeEntityConfiguration() {
            MakeRequired(fk => fk.GeneralStatus);
            MakeRequired(fk => fk.ProductTypeParent);
        }
    }
}
