using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Store;

namespace Pisa.VirtualStore.Dal.Test.Factories.Store
{
    class StoreFactory : BaseEntityFactory<Models.Store.StoreInfo>
    {
        public override Models.Store.StoreInfo CreateInstance()
        {
            return CreateInstance("StoreInfo Test 1", null, StoreStatus.GetInstance().ACTIVE, null);
        }

        public Models.Store.StoreInfo CreateInstance(string name, GeneralMedia logo, GeneralStatus gs, Models.Store.StoreInfo parent) {
            Models.Store.StoreInfo store = new Models.Store.StoreInfo();
            store.GeneralMediaLogo = logo;
            store.GeneralStatus = gs;
            store.Name = name;
            store.StoreParent = parent;
            return store;
        }
    }
}
