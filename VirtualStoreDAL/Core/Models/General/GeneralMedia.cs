namespace Pisa.VirtualStore.Dal.Core.Models.General
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GeneralMedia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralMedia()
        {
        }

        public int Id { get; set; }

        [Column(TypeName = "Image")]
        [Required]
        public byte[] MediaData { get; set; }

        [Required]
        [StringLength(100)]
        public string MediaReference { get; set; }

        public string MediaType { get; set; }

        public float? Height { get; set; }

        public float? Width { get; set; }
    }
}
