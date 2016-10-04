using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Models.Contact;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Client;
using Pisa.VirtualStore.Models.Product;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Test.Factories.Contact
{
    class ClientBasketDetailFactory : BaseEntityFactory<ClientBasketDetail>
    {
        public override ClientBasketDetail CreateInstance()
        {
            ClientBasket basket = EntitiesFactory.ClientBasketFactory.CreateInstance();
            ProductInfo product = EntitiesFactory.ProductInfoFactory.CreateInstance();
            return CreateInstance(basket, "Details for Test Produt", product, 3);
        }

        public ClientBasketDetail CreateInstance(ClientBasket basket, string details, ProductInfo product, double quantity) {
            ClientBasketDetail detail = new ClientBasketDetail();
            detail.Basket = basket;
            detail.MoreDetails = details;
            detail.Product = product;
            detail.Quantity = quantity;
            return detail;
        }
    }
}
