using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Test.Factories.Security
{
    class SecurityAccountFactory : BaseEntityFactory<SecurityAccount>
    {
        public override SecurityAccount CreateInstance()
        {
            return CreateInstance(SecurityAccountStatuses.GetInstance().ACTIVE, EntitiesFactory.SecurityUserFactory.CreateInstance());
        }

        public SecurityAccount CreateInstance(GeneralStatus gs, SecurityUser owner) {
            SecurityAccount sa = new SecurityAccount();
            sa.GeneralStatus = gs;
            sa.SecurityUserOwner = owner;
            return sa;
        }
    }
}
