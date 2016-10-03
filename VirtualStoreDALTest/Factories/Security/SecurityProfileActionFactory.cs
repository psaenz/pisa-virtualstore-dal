using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Test.Factories;
using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Models.Security;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Dal.Core.Helpers;

namespace Pisa.VirtualStore.Dal.Test.Factories.Security
{
    class SecurityProfileActionFactory : BaseEntityFactory<SecurityProfileAction>
    {
        public override SecurityProfileAction CreateInstance()
        {
            GeneralStatus gs = SecurityProfileStatuses.GetInstance().ACTIVE;
            SecurityProfile sp = EntitiesFactory.SecurityProfileFactory.CreateInstance();
            SecurityAction action = EntitiesFactory.SecurityActionFactory.CreateInstance();
            return CreateInstance(true, action, sp);
        }

        public SecurityProfileAction CreateInstance(bool available, SecurityAction action, SecurityProfile profile) {
            SecurityProfileAction sa = new SecurityProfileAction();
            sa.Available = available;
            sa.SecurityAction = action;
            sa.SecurityProfile = profile;
            return sa;
        }
    }
}
