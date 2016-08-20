namespace Pisa.VirtualStore.Dal.Core.Models.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.General;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProductTypes = new HashSet<ProductType>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [NotMapped]
        public int IdBrand { get; set; }

        [NotMapped]
        public int IdUnitOfMeasure { get; set; }

        [NotMapped]
        public int? IdGeneralMedia { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public virtual GeneralMedia GeneralMedia { get; set; }

        public virtual ProductBrand ProductBrand { get; set; }

        public virtual ProductUnitOfMeasure ProductUnitOfMeasure { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductType> ProductTypes { get; set; }
    }
}
