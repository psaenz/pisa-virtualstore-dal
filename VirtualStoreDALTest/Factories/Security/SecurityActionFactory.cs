using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Test.Factories.Security
{
    class SecurityActionFactory : BaseEntityFactory<SecurityAction>
    {
        public override SecurityAction CreateInstance()
        {
            GeneralStatus gs = SecurityActionStatuses.GetInstance().ACTIVE;
            return CreateInstance("Test Action", "Test Action Description", gs, "ActionTest", 0, null);
        }

        public SecurityAction CreateInstance(string action, string description, GeneralStatus gs, string name, int order, SecurityAction parent) {
            SecurityAction sa = new SecurityAction();
            sa.ActionToExecute = action;
            sa.Description = description;
            sa.GeneralStatus = gs;
            sa.Name = name;
            sa.Order = order;
            sa.SecurityActionParent = parent;
            return sa;
        }
    }
}
