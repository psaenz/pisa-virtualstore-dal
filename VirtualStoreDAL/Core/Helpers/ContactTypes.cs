using Pisa.VirtualStore.Models.Contact;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class ContactTypes
    {
        public readonly ContactType PHONE;
        public readonly ContactType CELL_PHONE;

        private static ContactTypes _instance = null;

        private ContactTypes()
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                PHONE = context.ContactTypeRepository.GetByName("Phone");
                CELL_PHONE = context.ContactTypeRepository.GetByName("Cell Phone");
            }
        }

        public static ContactTypes GetInstance()
        {
            if (_instance == null)
                _instance = new ContactTypes();
            return _instance;
        }
    }
}
