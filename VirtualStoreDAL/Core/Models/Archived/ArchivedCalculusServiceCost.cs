namespace Pisa.VirtualStore.Dal.Core.Models.Archived
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Service;

    public partial class ArchivedCalculusServiceCost
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdCalculusOrder { get; set; }

        [NotMapped]
        public int IdServiceType { get; set; }

        public double ServiceCost { get; set; }

        public virtual ArchivedCalculusOrder ArchivedCalculusOrder { get; set; }

        public virtual ServiceType ServiceType { get; set; }
    }
}
