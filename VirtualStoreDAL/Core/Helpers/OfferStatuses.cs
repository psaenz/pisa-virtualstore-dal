using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class OfferStatuses : BaseStatus
    {
        public readonly GeneralStatus EDITING;
        public readonly GeneralStatus PUBLISHED;
        public readonly GeneralStatus UNPUBLISHED;
        public readonly GeneralStatus CANCELLED;

        private static OfferStatuses _instance = null;

        private OfferStatuses() : base("Offer")
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                EDITING = GetStatus(context, "Editing");
                PUBLISHED = GetStatus(context, "Published");
                UNPUBLISHED = GetStatus(context, "UnPublished");
                CANCELLED = GetStatus(context, "Cancelled");
            }
        }

        public static OfferStatuses GetInstance()
        {
            if (_instance == null)
                _instance = new OfferStatuses();
            return _instance;
        }
    }
    }
