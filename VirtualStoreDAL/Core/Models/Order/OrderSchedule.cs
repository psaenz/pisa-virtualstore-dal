namespace Pisa.VirtualStore.Dal.Core.Models.Order
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.General;

    public partial class OrderSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderSchedule()
        {
        }

        public int Id { get; set; }

        [NotMapped]
        public int IdUser { get; set; }

        [NotMapped]
        public int IdOrder { get; set; }

        [NotMapped]
        public int IdGeneralStatus { get; set; }

        public GeneralSchedule Schedule { get; set; }

        public virtual Order Order { get; set; }
    }
}
