using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class SecurityAccountStatuses : BaseStatus
    {
        public readonly GeneralStatus ACTIVE;
        public readonly GeneralStatus LOCKED;
        public readonly GeneralStatus DELETED;

        private static SecurityAccountStatuses _instance = null;

        private SecurityAccountStatuses() : base("SecurityAccount")
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                ACTIVE = GetStatus(context, "Active");
                LOCKED = GetStatus(context, "Locked");
                DELETED = GetStatus(context, "Deleted");
            }
        }

        public static SecurityAccountStatuses GetInstance()
        {
            if (_instance == null)
                _instance = new SecurityAccountStatuses();
            return _instance;
        }
    }
    }
