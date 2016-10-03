using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class SecurityProfileStatuses : BaseStatus
    {
        public readonly GeneralStatus ACTIVE;
        public readonly GeneralStatus INACTIVE;

        private static SecurityProfileStatuses _instance = null;

        private SecurityProfileStatuses() : base("SecurityProfile")
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                ACTIVE = GetStatus(context, "Active");
                INACTIVE = GetStatus(context, "InActive");
            }
        }

        public static SecurityProfileStatuses GetInstance()
        {
            if (_instance == null)
                _instance = new SecurityProfileStatuses();
            return _instance;
        }
    }
}
