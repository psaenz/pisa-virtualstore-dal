using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class StoreStatus : BaseStatus
    {
        public readonly GeneralStatus ACTIVE;
        public readonly GeneralStatus LOCKED;

        private static StoreStatus _instance = null;

        private StoreStatus() : base ("StoreInfo")
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                ACTIVE = GetStatus(context, "Active");
                LOCKED = GetStatus(context, "Locked");
            }
        }

        public static StoreStatus GetInstance()
        {
            if (_instance == null)
                _instance = new StoreStatus();
            return _instance;
        }
    }
}
