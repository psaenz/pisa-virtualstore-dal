using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class BrandStatuses : BaseStatus
    {
        public readonly GeneralStatus EDITING;
        public readonly GeneralStatus PUBLISHED;
        public readonly GeneralStatus UNPUBLISHED;

        private static BrandStatuses _instance = null;

        private BrandStatuses() : base("Brand")
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                EDITING = GetStatus(context, "Editing");
                PUBLISHED = GetStatus(context, "Published");
                UNPUBLISHED = GetStatus(context, "UnPublished");
            }
        }

        public static BrandStatuses GetInstance()
        {
            if (_instance == null)
                _instance = new BrandStatuses();
            return _instance;
        }
    }
}
