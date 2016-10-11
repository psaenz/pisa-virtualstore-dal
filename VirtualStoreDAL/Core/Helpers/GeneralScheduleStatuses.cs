using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class GeneralScheduleStatuses : BaseStatus
    {
        public readonly GeneralStatus EDITING;
        public readonly GeneralStatus DELETED;
        public readonly GeneralStatus RUNNING;
        public readonly GeneralStatus CANCELLED;

        private static GeneralScheduleStatuses _instance = null;

        private GeneralScheduleStatuses() : base("GeneralSchedule")
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                EDITING = GetStatus(context, "Editing");
                DELETED = GetStatus(context, "Deleted");
                RUNNING = GetStatus(context, "Running");
                CANCELLED = GetStatus(context, "Cancelled");
            }
        }

        public static GeneralScheduleStatuses GetInstance()
        {
            if (_instance == null)
                _instance = new GeneralScheduleStatuses();
            return _instance;
        }
    }
}
