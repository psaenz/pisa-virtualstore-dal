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
using Pisa.VirtualStore.Models.Security;
using Pisa.VirtualStore.Models.Base;
using Pisa.VirtualStore.Models.Client;
using System;
using System.Threading.Tasks;

namespace Pisa.VirtualStore.Dal.Test.CRUD
{
    [TestClass]
    public class ClientCrudTests : BaseCrudTests
    {
        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Client"), TestCategory("DAL.Client.CRUD")]
        public async Task TestClientBasket()
        {
            int userId = 0;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityUser user = EntitiesFactory.SecurityUserFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, user);

                await unitOfWork.TrySaveChangesAsync();
                userId = user.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                ClientBasket cb = new ClientBasket();
                cb.SecurityUserId = userId;
                cb.Name = "Test Basket";
                cb.Sequence = 1;
                await RunCrudTest(unitOfWork, cb, "Sequence", 2);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Client"), TestCategory("DAL.Client.CRUD")]
        public async Task TestClientBasketDetail()
        {
            int basketId = 0;
            int productId = 0;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                ClientBasket basket = EntitiesFactory.ClientBasketFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, basket);

                ProductInfo product = EntitiesFactory.ProductInfoFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, product);

                await unitOfWork.TrySaveChangesAsync();

                basketId = basket.Id;
                productId = product.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                ClientBasketDetail cbd = new ClientBasketDetail();
                cbd.BasketId = basketId;
                cbd.MoreDetails = "Test Basket Details";
                cbd.ProductId = productId;
                cbd.Quantity = 3;
                await RunCrudTest(unitOfWork, cbd, "MoreDetails", "Test Basket Details Changed");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Client"), TestCategory("DAL.Client.CRUD")]
        public async Task TestClientFeedback()
        {
            int userId = 0;
            int storeId = 0;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityUser user = EntitiesFactory.SecurityUserFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, user);

                StoreInfo store = EntitiesFactory.StoreFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, store);

                await unitOfWork.TrySaveChangesAsync();

                userId = user.Id;
                storeId = store.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                ClientFeedback cfb = new ClientFeedback();
                cfb.SecurityUserId = userId;
                cfb.StoreId = storeId;
                cfb.Answerd = "Test Feedback answerd";
                cfb.Feedback = "Test Feedback";
                cfb.FeedbackDate = DateTime.UtcNow;
                cfb.Resolved = true;
                await RunCrudTest(unitOfWork, cfb, "Feedback", "Test Feedback Changed");
            }
        }
    }
}
