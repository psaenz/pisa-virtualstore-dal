using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pisa.VirtualStore.Dal.Core;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Dal.Test.Factories;
using Pisa.VirtualStore.Models.Audit;
using Pisa.VirtualStore.Models.Product;
using Pisa.VirtualStore.Models.Calculus;
using Pisa.VirtualStore.Models.Client;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Store;
using Pisa.VirtualStore.Models.Order;
using Pisa.VirtualStore.Models.Offer;
using Pisa.VirtualStore.Models.Service;
using Pisa.VirtualStore.Models.Base;
using System;
using System.Threading.Tasks;

namespace Pisa.VirtualStore.Dal.Test.CRUD
{
    [TestClass]
    public class CalculusCrudTests : BaseCrudTests
    {
        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Calculus"), TestCategory("DAL.Calculus.CRUD")]
        public async Task TestCalculusAppliedOffer()
        {
            int offerId = 0;
            int calculusOrderId = 0;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                CalculusOrder calculusOrder = EntitiesFactory.CalculusOrderFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, calculusOrder);

                OfferInfo offerInfo = EntitiesFactory.OfferInfoFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, offerInfo);

                await unitOfWork.TrySaveChangesAsync();

                offerId = offerInfo.Id;
                calculusOrderId = calculusOrder.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                CalculusAppliedOffer cao = new CalculusAppliedOffer();
                cao.NumberApplied = 1;
                cao.OfferId = offerId;
                cao.CalculusOrderId = calculusOrderId;
                await RunCrudTest(unitOfWork, cao, "NumberApplied", 2);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Calculus"), TestCategory("DAL.Calculus.CRUD")]
        public async Task TestCalculusBasketDetail()
        {
            int calculusAppliedOfferId;
            int calculusOrderId;
            int clientBasketDetailId;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                CalculusBasketDetail cbd = EntitiesFactory.CalculusBasketDetailFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, cbd.CalculusAppliedOffer);
                InsertObjectInDB(unitOfWork, cbd.CalculusOrder);
                InsertObjectInDB(unitOfWork, cbd.ClientBasketDetail);

                await unitOfWork.TrySaveChangesAsync();

                calculusAppliedOfferId = cbd.CalculusAppliedOffer.Id;
                calculusOrderId = cbd.CalculusOrder.Id;
                clientBasketDetailId = cbd.ClientBasketDetail.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                CalculusBasketDetail cbd = new CalculusBasketDetail();
                cbd.CalculusAppliedOfferId = calculusAppliedOfferId;
                cbd.CalculusOrderId = calculusOrderId;
                cbd.ClientBasketDetailId = clientBasketDetailId;
                cbd.AmountWithOffer = 100;
                cbd.AmountWithoutOffer = 200;
                cbd.CountWithOffer = 1;
                cbd.CountWithoutOffer = 3;
                cbd.ProvidedByStore = true;
                await RunCrudTest(unitOfWork, cbd, "AmountWithOffer", 75);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Calculus"), TestCategory("DAL.Calculus.CRUD")]
        public async Task TestCalculusFreeProduct()
        {
            int calculusOrderId;
            int productId;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                CalculusOrder calculusOrder = EntitiesFactory.CalculusOrderFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, calculusOrder);

                ProductInfo product = EntitiesFactory.ProductInfoFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, product);

                await unitOfWork.TrySaveChangesAsync();

                calculusOrderId = calculusOrder.Id;
                productId = product.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                CalculusFreeProduct cfp = new CalculusFreeProduct();
                cfp.CalculusOrderId = calculusOrderId;
                cfp.ProductId = productId;
                cfp.Quantity = 3;
                await RunCrudTest(unitOfWork, cfp, "Quantity", 5);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Calculus"), TestCategory("DAL.Calculus.CRUD")]
        public async Task TestCalculusOrder()
        {
            int orderId;
            int storeId;
            int storeId2;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                CalculusOrder calculusOrder = EntitiesFactory.CalculusOrderFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, calculusOrder.Order);
                InsertObjectInDB(unitOfWork, calculusOrder.Store);

                StoreInfo store2 = EntitiesFactory.StoreFactory.CreateInstance();
                store2.Name = "Store2 for test";
                InsertObjectInDB(unitOfWork, store2);

                await unitOfWork.TrySaveChangesAsync();

                orderId = calculusOrder.Order.Id;
                storeId = calculusOrder.Store.Id;
                storeId2 = store2.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                CalculusOrder order = new CalculusOrder();
                order.CalculusDate = DateTime.UtcNow;
                order.OrderId = orderId;
                order.StoreId = storeId;
                await RunCrudTest(unitOfWork, order, "StoreId", storeId2);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Calculus"), TestCategory("DAL.Calculus.CRUD")]
        public async Task TestCalculusServiceCost()
        {
            int orderId;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                CalculusOrder calculusOrder = EntitiesFactory.CalculusOrderFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, calculusOrder);

                await unitOfWork.TrySaveChangesAsync();

                orderId = calculusOrder.Order.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                CalculusServiceCost csc = new CalculusServiceCost();
                csc.CalculusOrderId = orderId;
                csc.ServiceCost = 1500;
                csc.ServiceTypeId = ServiceTypes.GetInstance().TO_HOME.Id;
                await RunCrudTest(unitOfWork, csc, "ServiceCost", 1200);
            }
        }
    }
}
