using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Client;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Client
{
    class ClientBasketDetailEntityConfiguration : BaseEntityConfiguration<ClientBasketDetail>
    {
        ClientBasketDetailEntityConfiguration() {
            MakeRequired(fk => fk.Basket);
            MakeRequired(fk => fk.Product);
        }
    }
}
