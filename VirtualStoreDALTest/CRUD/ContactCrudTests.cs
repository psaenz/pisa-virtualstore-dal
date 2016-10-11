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
using Pisa.VirtualStore.Models.Contact;
using Pisa.VirtualStore.Models.Security;
using Pisa.VirtualStore.Models.Base;
using Pisa.VirtualStore.Models.Client;
using System;
using System.Threading.Tasks;

namespace Pisa.VirtualStore.Dal.Test.CRUD
{
    [TestClass]
    public class ContactCrudTests : BaseCrudTests
    {
        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Contact"), TestCategory("DAL.Contact.CRUD")]
        public async Task TestContactInfo()
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
                ContactInfo c = new ContactInfo();
                c.ContactTypeId = ContactTypes.GetInstance().CELL_PHONE.Id;
                c.Details = "8880-0909";
                c.StatusId = ContactInfoStatuses.GetInstance().EDITING.Id;
                await RunCrudTest(unitOfWork, c, "Details", "8880-7777");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Contact"), TestCategory("DAL.Contact.CRUD")]
        public async Task TestContactAddress()
        {
            int regionId = 0;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                ContactRegion region = EntitiesFactory.ContactRegionFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, region);

                await unitOfWork.TrySaveChangesAsync();
                regionId = region.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                ContactAddress ca = new ContactAddress();
                ca.ContactRegionId = regionId;
                ca.Details = "Test Address Details";
                await RunCrudTest(unitOfWork, ca, "Details", "Test Address Details Changed");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Contact"), TestCategory("DAL.Contact.CRUD")]
        public async Task TestContactRegion()
        {
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                ContactRegion cr = new ContactRegion();
                cr.GeneralStatusId = ContactRegionStatuses.GetInstance().LOCKED.Id;
                cr.Name = "Test Region";
                await RunCrudTest(unitOfWork, cr, "GeneralStatusId", ContactRegionStatuses.GetInstance().ACTIVE.Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Contact"), TestCategory("DAL.Contact.CRUD")]
        public async Task TestContactType()
        {
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                ContactType ct = new ContactType();
                ct.Name = "Test Contact Type";
                await RunCrudTest(unitOfWork, ct, "Name", "Test Contact Type Changed");
            }
        }
    }
}
