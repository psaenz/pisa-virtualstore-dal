namespace Pisa.VirtualStore.Dal.Core.Models.Order
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Archived;
    using Pisa.VirtualStore.Dal.Core.Models.Calculus;
    using Pisa.VirtualStore.Dal.Core.Models.Contact;
    using Pisa.VirtualStore.Dal.Core.Models.Client;
    using Pisa.VirtualStore.Dal.Core.Models.Service;
    using Pisa.VirtualStore.Dal.Core.Models.Store;
    using Pisa.VirtualStore.Dal.Core.Models.Security;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            ArchivedCalculusOrders = new HashSet<ArchivedCalculusOrder>();
            CalculusOrders = new HashSet<CalculusOrder>();
            OrderSchedules = new HashSet<OrderSchedule>();
        }

        public int Id { get; set; }

        [NotMapped]
        public int IdRequestedBySecurityUser { get; set; }

        [NotMapped]
        public int IdBasket { get; set; }

        [NotMapped]
        public int IdGeneralStatus { get; set; }

        [NotMapped]
        public int IdServiceType { get; set; }

        [NotMapped]
        public int IdContactRegion { get; set; }

        [NotMapped]
        public int IdStore { get; set; }

        public double Amount { get; set; }

        public DateTime DateRequested { get; set; }

        public DateTime? DateDelivered { get; set; }

        public bool Scheduled { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArchivedCalculusOrder> ArchivedCalculusOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalculusOrder> CalculusOrders { get; set; }

        public virtual ContactRegion ContactRegion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderSchedule> OrderSchedules { get; set; }

        public virtual ClientBasket Basket { get; set; }

        public virtual ServiceType ServiceType { get; set; }

        public virtual Store Store { get; set; }

        public virtual SecurityUser RequestedBySecurityUser { get; set; }
    }
}
