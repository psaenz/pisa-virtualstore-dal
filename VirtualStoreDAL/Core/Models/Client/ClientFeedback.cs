namespace Pisa.VirtualStore.Dal.Core.Models.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Security;
    using Pisa.VirtualStore.Dal.Core.Models.Store;

    public partial class ClientFeedback
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdClient { get; set; }

        [NotMapped]
        public int IdStore { get; set; }

        [Column(TypeName = "date")]
        public DateTime FeedbackDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Feedback { get; set; }

        [StringLength(255)]
        public string Answerd { get; set; }

        public bool Resolved { get; set; }

        public virtual SecurityUser SecurityUser { get; set; }
        public virtual Store Store { get; set; }
    }
}
