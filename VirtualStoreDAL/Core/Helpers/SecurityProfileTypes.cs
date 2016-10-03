using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class SecurityProfileTypes
    {
        public readonly SecurityProfileType ADMINISTRATOR;
        public readonly SecurityProfileType CLIENT;
        public readonly SecurityProfileType STORE;
        public readonly SecurityProfileType BRAND;
        public readonly SecurityProfileType PROVIDER; // Deliveres products to stores or clients

        private static SecurityProfileTypes _instance = null;

        private SecurityProfileTypes()
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                ADMINISTRATOR = context.SecurityProfileTypeRepository.GetByName("Administrator");
                CLIENT = context.SecurityProfileTypeRepository.GetByName("Client");
                STORE = context.SecurityProfileTypeRepository.GetByName("Store");
                BRAND = context.SecurityProfileTypeRepository.GetByName("Brand");
                PROVIDER = context.SecurityProfileTypeRepository.GetByName("Provider");
            }
        }

        public static SecurityProfileTypes GetInstance()
        {
            if (_instance == null)
                _instance = new SecurityProfileTypes();
            return _instance;
        }
    }
}
