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
    class SecurityPersonFactory : BaseEntityFactory<SecurityPerson>
    {
        public override SecurityPerson CreateInstance()
        {
            return CreateInstance("abc@test.com", true, "Test", "Saenz", "Avila");
        }

        public SecurityPerson CreateInstance(string email, bool emailVerified, string firstName, string lastName, string maidenName) {
            SecurityPerson sp = new SecurityPerson();
            sp.Email = email;
            sp.EmailVerified = emailVerified;
            sp.FirstName = firstName;
            sp.LastName = lastName;
            sp.MaidenName = maidenName;
            return sp;
        }
    }
}
