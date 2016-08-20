using Pisa.VirtualStore.Dal.Core.Models.General;namespace Pisa.VirtualStore.Dal.Core.Models.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Contact;
    using Pisa.VirtualStore.Dal.Core.Models.Service;

    public partial class StoreZone
    {
        public int Id { get; set; }

        [NotMapped]
        public int? IdContactRegion { get; set; }

        [NotMapped]
        public int IdServiceZone { get; set; }

        public virtual ContactRegion ContactRegion { get; set; }

        public virtual ServiceZone ServiceZone { get; set; }
    }
}
