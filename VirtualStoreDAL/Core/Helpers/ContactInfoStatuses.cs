using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class ContactInfoStatuses : BaseStatus
    {
        public readonly GeneralStatus EDITING;
        public readonly GeneralStatus CONFIRMED;

        private static ContactInfoStatuses _instance = null;

        private ContactInfoStatuses() : base("Contact")
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                EDITING = GetStatus(context, "Editing");
                CONFIRMED = GetStatus(context, "Confirmed");
            }
        }

        public static ContactInfoStatuses GetInstance()
        {
            if (_instance == null)
                _instance = new ContactInfoStatuses();
            return _instance;
        }
    }
}
