using Pisa.VirtualStore.Dal.Test.Factories.Contact;
using Pisa.VirtualStore.Dal.Test.Factories.Security;
using Pisa.VirtualStore.Dal.Test.Factories.Store;
using Pisa.VirtualStore.Dal.Test.Factories.Order;

namespace Pisa.VirtualStore.Dal.Test.Factories
{
    static class EntitiesFactory
    {
        public static CalculusAppliedOfferFactory CalculusAppliedOfferFactory = new CalculusAppliedOfferFactory();

        public static CalculusBasketDetailFactory CalculusBasketDetailFactory = new CalculusBasketDetailFactory();

        public static CalculusOrderFactory CalculusOrderFactory = new CalculusOrderFactory();

        public static ClientBasketDetailFactory ClientBasketDetailFactory = new ClientBasketDetailFactory();

        public static ClientBasketFactory ClientBasketFactory = new ClientBasketFactory();

        public static ContactRegionFactory ContactRegionFactory = new ContactRegionFactory();

        public static OfferInfoFactory OfferInfoFactory = new OfferInfoFactory();

        public static OrderFactory OrderFactory = new OrderFactory();

        public static ProductBrandFactory ProductBrandFactory = new ProductBrandFactory();

        public static ProductInfoFactory ProductInfoFactory = new ProductInfoFactory();

        public static SecurityAccountAddressFactory SecurityAccountAddressFactory = new SecurityAccountAddressFactory();

        public static SecurityAccountFactory SecurityAccountFactory = new SecurityAccountFactory();

        public static SecurityActionFactory SecurityActionFactory = new SecurityActionFactory();

        public static SecurityPersonFactory SecurityPersonFactory = new SecurityPersonFactory();

        public static SecurityProfileActionFactory SecurityProfileActionFactory = new SecurityProfileActionFactory();

        public static SecurityProfileFactory SecurityProfileFactory = new SecurityProfileFactory();

        public static SecurityUserFactory SecurityUserFactory = new SecurityUserFactory();

        public static StoreFactory StoreFactory = new StoreFactory();
    }
}
