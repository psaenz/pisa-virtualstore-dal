using Pisa.VirtualStore.Dal.Test.Factories.Contact;
using Pisa.VirtualStore.Dal.Test.Factories.Security;
using Pisa.VirtualStore.Dal.Test.Factories.Store;

namespace Pisa.VirtualStore.Dal.Test.Factories
{
    static class EntitiesFactory
    {
        public static ContactRegionFactory ContactRegionFactory = new ContactRegionFactory();

        public static SecurityAccountFactory SecurityAccountFactory = new SecurityAccountFactory();

        public static SecurityActionFactory SecurityActionFactory = new SecurityActionFactory();

        public static SecurityPersonFactory SecurityPersonFactory = new SecurityPersonFactory();

        public static SecurityProfileActionFactory SecurityProfileActionFactory = new SecurityProfileActionFactory();

        public static SecurityProfileFactory SecurityProfileFactory = new SecurityProfileFactory();

        public static SecurityUserFactory SecurityUserFactory = new SecurityUserFactory();

        public static StoreFactory StoreFactory = new StoreFactory();
    }
}
