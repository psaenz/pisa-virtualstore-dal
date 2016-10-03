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
    class SecurityProfileFactory : BaseEntityFactory<SecurityProfile>
    {
        public override SecurityProfile CreateInstance()
        {
            GeneralStatus gs = SecurityProfileStatuses.GetInstance().ACTIVE;
            SecurityProfileType spt = SecurityProfileTypes.GetInstance().CLIENT;

            return CreateInstance(gs, "Security Profile Test", spt, null);
        }

        public SecurityProfile CreateInstance(GeneralStatus gs, string name, SecurityProfileType spt, SecurityProfile parent) {
            SecurityProfile sp = new SecurityProfile();
            sp.GeneralStatus = gs;
            sp.Name = name;
            sp.SecurityProfileType = spt;
            sp.SecurityProfileParent = parent;
            return sp;
        }
    }
}
