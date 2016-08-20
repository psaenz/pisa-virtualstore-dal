namespace Pisa.VirtualStore.Dal.Core.Models.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
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

    /// <summary>
    /// Links an <see cref="SecurityAccount"/> with and <see cref="Store"/>
    /// We could have a property in the Store that points to an SecurityAccount...
    /// but I dont want to have security stuff in non-security related classes
    /// </summary>
    public partial class SecurityAccountStore : BaseModel
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdSecurityAccount { get; set; }

        [NotMapped]
        public int IdSecurityStore { get; set; }

        public virtual SecurityAccount SecurityAccount { get; set; }

        public virtual Store Store { get; set; }
    }
}
