namespace Pisa.VirtualStore.Dal.Core.Models.Calculus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Client;

    public partial class CalculusBasketDetail
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdCalculusOrder { get; set; }

        [NotMapped]
        public int IdBasketDetail { get; set; }

        [NotMapped]
        public int? IdAppliedOffer { get; set; }

        public int CountWithOffer { get; set; }

        public int CountWithoutOffer { get; set; }

        public double AmountWithOffer { get; set; }

        public double AmountWithoutOffer { get; set; }

        public bool ProvidedByStore { get; set; }

        public virtual ClientBasketDetail BasketDetail { get; set; }

        public virtual CalculusOrder CalculusOrder { get; set; }

        public virtual CalculusAppliedOffer CalculusAppliedOffer { get; set; }
    }
}
