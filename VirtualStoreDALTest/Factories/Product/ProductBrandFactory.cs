using System;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Order;
using Pisa.VirtualStore.Models.Client;
using Pisa.VirtualStore.Models.Security;
using Pisa.VirtualStore.Models.Service;
using Pisa.VirtualStore.Models.Store;
using Pisa.VirtualStore.Models.Product;

namespace Pisa.VirtualStore.Dal.Test.Factories.Order
{
    class ProductBrandFactory : BaseEntityFactory<ProductBrand>
    {
        public override ProductBrand CreateInstance()
        {
            GeneralStatus status = BrandStatuses.GetInstance().EDITING;
            return CreateInstance(status, "Test Brand");
        }

        public ProductBrand CreateInstance(GeneralStatus status, string name) {
            ProductBrand brand = new ProductBrand();
            brand.GeneralStatus = status;
            brand.Name = name;
            return brand;
        }
    }
}
