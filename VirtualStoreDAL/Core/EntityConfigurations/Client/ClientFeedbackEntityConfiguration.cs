using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Client;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Client
{
    class ClientFeedbackEntityConfiguration : BaseEntityConfiguration<ClientFeedback>
    {
        ClientFeedbackEntityConfiguration() {
            MakeRequired(fk => fk.SecurityUser);
            MakeRequired(fk => fk.Store);
        }
    }
}
