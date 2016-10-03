using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pisa.VirtualStore.Dal.Test.Factories.Base
{
    abstract class BaseEntityFactory<E> where E : class, new()
    {
        public abstract E CreateInstance();
    }
}
