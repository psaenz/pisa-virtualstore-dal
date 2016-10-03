using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Store;

namespace Pisa.VirtualStore.Dal.Test.Factories.Store
{
    class StoreFactory : BaseEntityFactory<Models.Store.Store>
    {
        public override Models.Store.Store CreateInstance()
        {
            return CreateInstance("Store Test 1", null, StoreStatus.GetInstance().ACTIVE, null);
        }

        public Models.Store.Store CreateInstance(string name, GeneralMedia logo, GeneralStatus gs, Models.Store.Store parent) {
            Models.Store.Store store = new Models.Store.Store();
            store.GeneralMediaLogo = logo;
            store.GeneralStatus = gs;
            store.Name = name;
            store.StoreParent = parent;
            return store;
        }
    }
}
