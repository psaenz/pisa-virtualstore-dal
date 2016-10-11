using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class GeneralMediaTypes
    {
        public readonly GeneralMediaType IMAGE;
        public readonly GeneralMediaType VIDEO;
        public readonly GeneralMediaType PDF;

        private static GeneralMediaTypes _instance = null;

        private GeneralMediaTypes()
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                IMAGE = context.GeneralMediaTypeRepository.GetByName("Image");
                VIDEO = context.GeneralMediaTypeRepository.GetByName("Video");
                PDF = context.GeneralMediaTypeRepository.GetByName("PDF");
            }
        }

        public static GeneralMediaTypes GetInstance()
        {
            if (_instance == null)
                _instance = new GeneralMediaTypes();
            return _instance;
        }
    }
}
