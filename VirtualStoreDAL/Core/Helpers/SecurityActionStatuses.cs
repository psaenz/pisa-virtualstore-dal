using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class SecurityActionStatuses : BaseStatus
    {
        public readonly GeneralStatus ACTIVE;
        public readonly GeneralStatus LOCKED;
        public readonly GeneralStatus DELETED;

        private static SecurityActionStatuses _instance = null;

        private SecurityActionStatuses() : base("SecurityAction")
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                ACTIVE = GetStatus(context, "Active");
                LOCKED = GetStatus(context, "Locked");
                DELETED = GetStatus(context, "Deleted");
            }
        }

        public static SecurityActionStatuses GetInstance()
        {
            if (_instance == null)
                _instance = new SecurityActionStatuses();
            return _instance;
        }
    }
}
