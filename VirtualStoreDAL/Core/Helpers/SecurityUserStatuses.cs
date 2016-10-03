using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class SecurityUserStatuses : BaseStatus
    {
        public readonly GeneralStatus ACTIVE;
        public readonly GeneralStatus LOCKED;
        public readonly GeneralStatus DELETED;

        private static SecurityUserStatuses _instance = null;

        private SecurityUserStatuses() : base("SecurityUser")
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                ACTIVE = GetStatus(context, "Active");
                LOCKED = GetStatus(context, "Locked");
                DELETED = GetStatus(context, "Deleted");
            }
        }

        public static SecurityUserStatuses GetInstance()
        {
            if (_instance == null)
                _instance = new SecurityUserStatuses();
            return _instance;
        }
    }
}
