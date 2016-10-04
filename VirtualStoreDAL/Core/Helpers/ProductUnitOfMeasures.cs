using Pisa.VirtualStore.Models.Product;

namespace Pisa.VirtualStore.Dal.Core.Helpers
{
    public class ProductUnitOfMeasures
    {
        public readonly ProductUnitOfMeasure GRAMOS;
        public readonly ProductUnitOfMeasure LITROS;

        private static ProductUnitOfMeasures _instance = null;

        private ProductUnitOfMeasures()
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                GRAMOS = context.ProductUnitOfMeasureRepository.GetByName("Gramos");
                LITROS = context.ProductUnitOfMeasureRepository.GetByName("Litros");
            }
        }

        public static ProductUnitOfMeasures GetInstance()
        {
            if (_instance == null)
                _instance = new ProductUnitOfMeasures();
            return _instance;
        }
    }
}
