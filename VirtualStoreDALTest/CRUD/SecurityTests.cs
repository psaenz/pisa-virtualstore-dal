using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pisa.VirtualStore.Dal.Core;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Dal.Test.Factories;
using Pisa.VirtualStore.Models.Audit;
using Pisa.VirtualStore.Models.Base;
using Pisa.VirtualStore.Models.Contact;
using Pisa.VirtualStore.Models.Security;
using Pisa.VirtualStore.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisa.VirtualStore.Dal.Test.CRUD
{
    [TestClass]
    public class SecurityTests
    {

        IList<IBaseModel> objectsToDelete = new List<IBaseModel>();

        // -2 means unit test users. -1 means system user
        AuditAuthor TestAuthor = new AuditAuthor() { SecurityUserId = -2, CurrentDate = DateTime.UtcNow};

        [TestInitialize]
        public async void TestInitialize()
        {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork()) {
                TestAuthor = context.AuditAuthorRepository.Insert(TestAuthor);
                objectsToDelete.Add(TestAuthor);
                await context.TrySaveChangesAsync();
            }
        }

        [TestCleanup]
        public async void TestCleanup() {
            using (var context = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                foreach (IBaseModel delete in objectsToDelete)
                    context.GetRepositoryFor(delete).Delete(delete.Id);
                await context.TrySaveChangesAsync();
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAccount()
        {
            int status = 0;
            int securityOwner = 0;
            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = null;
                acc = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                objectsToDelete.Add(context.SecurityUserRepository.Insert(acc.SecurityUserOwner));
                objectsToDelete.Add(context.SecurityPersonRepository.Insert(acc.SecurityUserOwner.SecurityPerson));

                int objs = await context.TrySaveChangesAsync();

                status = acc.GeneralStatus.Id;
                securityOwner = acc.SecurityUserOwner.Id;
            }

            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = new SecurityAccount();
                acc.GeneralStatusId = status;
                acc.SecurityUserOwnerId = securityOwner;

                await RunTest(context, acc, "GeneralStatusId", SecurityAccountStatuses.GetInstance().DELETED.Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAccountAddress()
        {
            int accountId = 0;
            int addressId = 0;
            int address2Id = 0;

            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = null;
                acc = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                objectsToDelete.Add(context.SecurityAccountRepository.Insert(acc));
                objectsToDelete.Add(acc.GeneralStatus);
                objectsToDelete.Add(acc.SecurityUserOwner);

                ContactAddress address1 = new ContactAddress();
                address1.ContactRegion = EntitiesFactory.ContactRegionFactory.CreateInstance();
                address1.Details = "Address 1";
                objectsToDelete.Add(address1.ContactRegion);
                objectsToDelete.Add(context.ContactAddressRepository.Insert(address1));

                ContactAddress address2 = new ContactAddress();
                address2.ContactRegion = address1.ContactRegion;
                address2.Details = "Address 2";
                objectsToDelete.Add(context.ContactAddressRepository.Insert(address2));

                int objs = await context.TrySaveChangesAsync();
                accountId = acc.Id;
                addressId = address1.Id;
                address2Id = address2.Id;
            }
            
            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccountAddress accAddress = new SecurityAccountAddress();
                accAddress.SecurityAccountId = accountId;
                accAddress.ContactAddressId = addressId;

                await RunTest(context, accAddress, "ContactAddressId", address2Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAccountContact()
        {
            int accountId = 0;
            int contactId = 0;
            int contact2Id = 0;

            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = null;
                acc = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                objectsToDelete.Add(context.SecurityAccountRepository.Insert(acc));
                objectsToDelete.Add(acc.GeneralStatus);
                objectsToDelete.Add(acc.SecurityUserOwner);

                Contact contact1 = new Contact();
                contact1.ContactTypeId = ContactTypes.GetInstance().PHONE.Id;
                contact1.Details = "Contact 1";
                objectsToDelete.Add(context.ContactRepository.Insert(contact1));

                Contact contact2 = new Contact();
                contact2.ContactTypeId = ContactTypes.GetInstance().CELL_PHONE.Id;
                contact2.Details = "Contact 2";
                objectsToDelete.Add(context.ContactRepository.Insert(contact2));

                int objs = await context.TrySaveChangesAsync();
                accountId = acc.Id;
                contactId = contact1.Id;
                contact2Id = contact2.Id;
            }

            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccountContact accContact = new SecurityAccountContact();
                accContact.SecurityAccountId = accountId;
                accContact.ContactId = contactId;

                await RunTest(context, accContact, "ContactId", contact2Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAccountStore()
        {
            int accountId = 0;
            int storeId = 0;
            int store2Id = 0;

            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = null;
                acc = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                objectsToDelete.Add(context.SecurityAccountRepository.Insert(acc));
                objectsToDelete.Add(acc.GeneralStatus);
                objectsToDelete.Add(acc.SecurityUserOwner);

                Store store = EntitiesFactory.StoreFactory.CreateInstance();
                objectsToDelete.Add(context.StoreRepository.Insert(store));

                Store store2 = EntitiesFactory.StoreFactory.CreateInstance();
                store2.Name = "Store 2";
                objectsToDelete.Add(context.StoreRepository.Insert(store2));

                int objs = await context.TrySaveChangesAsync();
                accountId = acc.Id;
                storeId = store.Id;
                store2Id = store2.Id;
            }

            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccountStore accStore = new SecurityAccountStore();
                accStore.SecurityAccountId = accountId;
                accStore.StoreId = storeId;

                await RunTest(context, accStore, "StoreId", store2Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAccountUser()
        {
            int accountId = 0;
            int securityUserId = 0;
            int profileId = 0;
            int profile2Id = 0;

            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = null;
                acc = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                objectsToDelete.Add(context.SecurityAccountRepository.Insert(acc));
                objectsToDelete.Add(acc.GeneralStatus);
                objectsToDelete.Add(acc.SecurityUserOwner);

                SecurityUser su = EntitiesFactory.SecurityUserFactory.CreateInstance();
                objectsToDelete.Add(context.SecurityUserRepository.Insert(su));

                SecurityProfile sp = EntitiesFactory.SecurityProfileFactory.CreateInstance();
                objectsToDelete.Add(context.SecurityProfileRepository.Insert(sp));

                SecurityProfile sp2 = EntitiesFactory.SecurityProfileFactory.CreateInstance();
                sp2.Name = "Security Profile Test 2";
                objectsToDelete.Add(context.SecurityProfileRepository.Insert(sp2));

                int objs = await context.TrySaveChangesAsync();
                accountId = acc.Id;
                securityUserId = su.Id;
                profileId = sp.Id;
                profile2Id = sp2.Id;
            }

            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccountUser accUser = new SecurityAccountUser();
                accUser.SecurityAccountId = accountId;
                accUser.SecurityUserId = securityUserId;
                accUser.SecurityProfileId = profileId;

                await RunTest(context, accUser, "SecurityProfileId", profile2Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAction()
        {
            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAction action = EntitiesFactory.SecurityActionFactory.CreateInstance();
                action.GeneralStatusId = action.GeneralStatus.Id;
                action.GeneralStatus = null;
                await RunTest(context, action, "ActionToExecute", "Test Action To Execute Updated");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityDefaultProfile()
        {
            int securityProfileId = 0;
            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityProfile sp = EntitiesFactory.SecurityProfileFactory.CreateInstance();
                objectsToDelete.Add(context.SecurityProfileRepository.Insert(sp));
                await context.TrySaveChangesAsync();
                securityProfileId = sp.Id;
            }

            using (var context = new VirtualStoreUnitOfWork(TestAuthor)) {
                SecurityDefaultProfile sdp = new SecurityDefaultProfile();
                sdp.SecurityProfileId = securityProfileId;
                sdp.SecurityProfileTypeId = SecurityProfileTypes.GetInstance().STORE.Id;
                await RunTest(context, sdp, "SecurityProfileTypeId", SecurityProfileTypes.GetInstance().CLIENT.Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityPassword()
        {
            int userId = 0;
            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityUser user = EntitiesFactory.SecurityUserFactory.CreateInstance();
                objectsToDelete.Add(context.SecurityUserRepository.Insert(user));
                await context.TrySaveChangesAsync();
                userId = user.Id;
            }

            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityPassword sp = new SecurityPassword();
                sp.Password = "testPassword";
                sp.Sequence = 0;
                sp.SecurityUserId = userId;
                await RunTest(context, sp, "Sequence", 1);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityPerson()
        {
            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityPerson person = EntitiesFactory.SecurityPersonFactory.CreateInstance("psaenz@gmail.com", false, "Pedro", "Saenz", "Avila");
                await RunTest(context, person, "Email", "psaenz2@gmail.com");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityProfile()
        {
            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityProfile sp = new SecurityProfile();
                sp.GeneralStatusId = SecurityProfileStatuses.GetInstance().ACTIVE.Id;
                sp.Name = "Profile Test";
                sp.SecurityProfileTypeId = SecurityProfileTypes.GetInstance().STORE.Id;
                await RunTest(context, sp, "Name", "Profile Test 2");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityProfileAction()
        {
            int actionId = 0;
            int profileId = 0;
            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityProfileAction spa = EntitiesFactory.SecurityProfileActionFactory.CreateInstance();
                objectsToDelete.Add (context.SecurityActionRepository.Insert(spa.SecurityAction));
                objectsToDelete.Add(context.SecurityProfileRepository.Insert(spa.SecurityProfile));
                await context.TrySaveChangesAsync();

                actionId = spa.SecurityAction.Id;
                profileId = spa.SecurityProfile.Id;
                await context.TrySaveChangesAsync();
            }

            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityProfileAction spa = new SecurityProfileAction();
                spa.Available = true;
                spa.SecurityActionId = actionId;
                spa.SecurityProfileId = profileId;
                await RunTest(context, spa, "Available", false);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityProfileType()
        {
            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityProfileType spt = new SecurityProfileType();
                spt.Name = "Security Profile Type Test";
                await RunTest(context, spt, "Name", "Security Profile Type Test 2");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityUser()
        {
            int personId = 0;
            int lastAccountId = 0;
            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityPerson sp = EntitiesFactory.SecurityPersonFactory.CreateInstance();
                objectsToDelete.Add(context.SecurityPersonRepository.Insert(sp));

                SecurityAccount sa = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                objectsToDelete.Add(context.SecurityAccountRepository.Insert(sa));
                await context.TrySaveChangesAsync();

                personId = sp.Id;
                lastAccountId = sa.Id;
                await context.TrySaveChangesAsync();
            }

            using (var context = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityUser su = new SecurityUser();
                su.GeneralStatusId = SecurityUserStatuses.GetInstance().ACTIVE.Id;
                su.LastAccountUsedId = lastAccountId;
                su.Login = "test";
                su.MustChangeThePassword = false;
                su.Password = "password";
                su.SecurityPersonId = personId;
                await RunTest(context, su, "LastAccountUsedId", null);
            }
        }

        private async Task RunTest<T>(VirtualStoreUnitOfWork unitOfWork, T objectToSave, string propertyToChange, object propertyValue) where T : BaseModel
        {
            IBaseRepository repository = unitOfWork.GetRepositoryFor(objectToSave);
            string objType = objectToSave.GetType().Name;
            int countBeforeTest = repository.GetAll().Count();
            objectsToDelete.Add(repository.Insert(objectToSave));

            int i = await repository.SaveAsync();
            Assert.AreEqual(1, i, i + " " + objType + " were saved, only 1 was expected.");
            Assert.AreEqual(1, repository.GetAll().Count() - countBeforeTest, objType +" wasn't added");
            Assert.AreNotEqual(0, objectToSave.Id, objType +" PK wasn't set.");

            T objectSaved = (T) repository.GetById(objectToSave.Id);
            Assert.AreSame(objectToSave, objectSaved, objType + " is not cached by the EF");

            objectToSave.SetPropertyValue(propertyToChange, propertyValue);
            i = await repository.SaveAsync();
            Assert.AreEqual(1, i, i + " objects were updated. 1 was expected");
            Assert.AreEqual(1, repository.GetAll().Count() - countBeforeTest, objType + " was added during update.");

            objectSaved = (T) repository.Delete(objectToSave.Id);
            i = await repository.SaveAsync();
            Assert.AreEqual(1, i, objType + " wasn't deleted");
            Assert.AreEqual(repository.GetAll().Count(), countBeforeTest, objType + " wasn't removed");
            Assert.AreSame(objectToSave, objectSaved, objType + " is not cached by the EF");

            T objectDeleted = (T) repository.GetById(objectSaved.Id);
            Assert.IsNull(objectDeleted, objType + " wasn't deleted");
        }
    }
}
