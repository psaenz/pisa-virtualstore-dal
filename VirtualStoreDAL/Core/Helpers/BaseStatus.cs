using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class BaseStatus
    {
        private string _statusType;

        public BaseStatus(string statusType)
        {
            _statusType = statusType;
        }

        public GeneralStatus GetStatus(VirtualStoreUnitOfWork context, string name) {
            return context.GeneralStatusRepository.GetByTypeAndName(_statusType, name);
        }
    }
}
