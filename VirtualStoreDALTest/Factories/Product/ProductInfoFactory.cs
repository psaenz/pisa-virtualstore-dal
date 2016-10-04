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
    class ProductInfoFactory : BaseEntityFactory<ProductInfo>
    {
        public override ProductInfo CreateInstance()
        {
            ProductBrand brand = EntitiesFactory.ProductBrandFactory.CreateInstance();
            ProductUnitOfMeasure unitOfMeasure = ProductUnitOfMeasures.GetInstance().GRAMOS;
            return CreateInstance("Test Product Description", null, "Test Product", brand, unitOfMeasure);
        }

        public ProductInfo CreateInstance(string description, GeneralMedia media, string name, ProductBrand brand, ProductUnitOfMeasure unitOfMeasure) {
            ProductInfo product = new ProductInfo();
            product.Description = description;
            product.GeneralMedia = media;
            product.Name = name;
            product.ProductBrand = brand;
            product.ProductUnitOfMeasure = unitOfMeasure;
            return product;
        }
    }
}
