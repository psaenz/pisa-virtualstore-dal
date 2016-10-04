using Pisa.VirtualStore.Models.Service;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class ServiceTypes
    {
        public readonly ServiceType TO_HOME;
        public readonly ServiceType TO_PICK_UP;

        private static ServiceTypes _instance = null;

        private ServiceTypes()
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                TO_HOME = context.ServiceTypeRepository.GetByName("To Home");
                TO_PICK_UP = context.ServiceTypeRepository.GetByName("To Pick Up");
            }
        }

        public static ServiceTypes GetInstance()
        {
            if (_instance == null)
                _instance = new ServiceTypes();
            return _instance;
        }
    }
}
