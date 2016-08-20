namespace Pisa.VirtualStore.Dal.Core.Models.Archived
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Pisa.VirtualStore.Dal.Core.Models.Client;

    public partial class ArchivedCalculusBasketDetail
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

        public virtual ArchivedCalculusOrder ArchivedCalculusOrder { get; set; }

        public virtual ArchivedCalculusAppliedOffer ArchivedCalculusAppliedOffer { get; set; }

        public virtual ClientBasketDetail ClientBasketDetail { get; set; }
    }
}
