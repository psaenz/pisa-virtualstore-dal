using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Test.Factories.Security
{
    class SecurityUserFactory : BaseEntityFactory<SecurityUser>
    {
        public override SecurityUser CreateInstance()
        {
            // DO NOT CALL the SecurityAccountFactory here because it would create a infinitive loop since the CreateInstance call this CreateInstance
            return CreateInstance(SecurityAccountStatuses.GetInstance().ACTIVE, null, false, "password123!", EntitiesFactory.SecurityPersonFactory.CreateInstance(), "psaenz");
        }

        public SecurityUser CreateInstance(GeneralStatus gs, SecurityAccount lastAccountUsed, bool mustChangePassword, string password, SecurityPerson sp, string login) {
            SecurityUser su = new SecurityUser();
            su.GeneralStatus = gs;
            //su.LastAccountUsed = lastAccountUsed;
            su.MustChangeThePassword = mustChangePassword;
            su.Password = password;
            su.SecurityPerson = sp;
            su.Login = login;
            return su;
        }
    }
}
