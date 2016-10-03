using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class ContactRegionStatuses : BaseStatus
    {
        public readonly GeneralStatus ACTIVE;
        public readonly GeneralStatus LOCKED;

        private static ContactRegionStatuses _instance = null;

        private ContactRegionStatuses() : base ("ContactRegion")
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                ACTIVE = GetStatus(context, "Active");
                LOCKED = GetStatus(context, "Locked");
            }
        }

        public static ContactRegionStatuses GetInstance() {
            if (_instance == null)
                _instance = new ContactRegionStatuses();
            return _instance;
        }
    }
}
