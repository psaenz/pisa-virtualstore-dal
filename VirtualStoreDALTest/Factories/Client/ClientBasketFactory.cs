using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Models.Contact;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Client;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Test.Factories.Contact
{
    class ClientBasketFactory : BaseEntityFactory<ClientBasket>
    {
        public override ClientBasket CreateInstance()
        {
            SecurityUser user = EntitiesFactory.SecurityUserFactory.CreateInstance();
            return CreateInstance("Basket Test", user, 1);
        }

        public ClientBasket CreateInstance(string name, SecurityUser user, int sequence) {
            ClientBasket basket = new ClientBasket();
            basket.Name = name;
            basket.SecurityUser = user;
            basket.Sequence = sequence;
            return basket;
        }
    }
}
