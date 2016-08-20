namespace Pisa.VirtualStore.Dal.Core.Models.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Security;

    public partial class ClientBasket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientBasket()
        {
            ClientBasketDetails = new HashSet<ClientBasketDetail>();
        }

        public int Id { get; set; }

        [NotMapped]
        public int IdUser { get; set; }

        public int Sequence { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual SecurityUser SecurityUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientBasketDetail> ClientBasketDetails { get; set; }
    }
}
