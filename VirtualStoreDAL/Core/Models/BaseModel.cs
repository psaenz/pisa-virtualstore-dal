using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pisa.VirtualStore.Dal.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Audit;
    using Pisa.VirtualStore.Dal.Core.Models.Archived;
    using Pisa.VirtualStore.Dal.Core.Models.Client;
    using Pisa.VirtualStore.Dal.Core.Models.Calculus;
    using Pisa.VirtualStore.Dal.Core.Models.Contact;
    using Pisa.VirtualStore.Dal.Core.Models.General;
    using Pisa.VirtualStore.Dal.Core.Models.Offer;
    using Pisa.VirtualStore.Dal.Core.Models.Order;
    using Pisa.VirtualStore.Dal.Core.Models.Product;
    using Pisa.VirtualStore.Dal.Core.Models.Security;
    using Pisa.VirtualStore.Dal.Core.Models.Service;
    using Pisa.VirtualStore.Dal.Core.Models.Store;

    public class BaseModel : IBaseModel
    {
        [Key]
        public int Id { get; set; }

        public T getPropertyValue<T>(string propertyName)
        {
            return (T) this.GetType().GetProperty(propertyName).GetValue(this);
        }

        public void setPropertyValue<T>(string propertyName, T value)
        {
            this.GetType().GetProperty(propertyName).SetValue(this, value);
        }
    }
}
