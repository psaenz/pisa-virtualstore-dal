using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pisa.VirtualStore.Dal.Core;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Dal.Test.Factories;
using Pisa.VirtualStore.Models.Contact;
using Pisa.VirtualStore.Models.Security;
using Pisa.VirtualStore.Models.Store;
using System.Threading.Tasks;

namespace Pisa.VirtualStore.Dal.Test.CRUD
{
    [TestClass]
    public class SecurityCrudTests : BaseCrudTests
    {
        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAccount()
        {
            int status = 0;
            int securityOwner = 0;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = null;
                acc = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, acc.SecurityUserOwner);
                InsertObjectInDB(unitOfWork, acc.SecurityUserOwner.SecurityPerson);

                int objs = await unitOfWork.TrySaveChangesAsync();

                status = acc.GeneralStatus.Id;
                securityOwner = acc.SecurityUserOwner.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = new SecurityAccount();
                acc.GeneralStatusId = status;
                acc.SecurityUserOwnerId = securityOwner;

                await RunCrudTest(unitOfWork, acc, "GeneralStatusId", SecurityAccountStatuses.GetInstance().DELETED.Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAccountAddress()
        {
            int accountId = 0;
            int addressId = 0;
            int address2Id = 0;

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = null;
                acc = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, acc);
                InsertObjectInDB(unitOfWork, acc.GeneralStatus);
                InsertObjectInDB(unitOfWork, acc.SecurityUserOwner);

                ContactAddress address1 = new ContactAddress();
                address1.ContactRegion = EntitiesFactory.ContactRegionFactory.CreateInstance();
                address1.Details = "Address 1";
                InsertObjectInDB(unitOfWork, address1.ContactRegion);
                InsertObjectInDB(unitOfWork, address1);

                ContactAddress address2 = new ContactAddress();
                address2.ContactRegion = address1.ContactRegion;
                address2.Details = "Address 2";
                InsertObjectInDB(unitOfWork, address2);

                int objs = await unitOfWork.TrySaveChangesAsync();
                accountId = acc.Id;
                addressId = address1.Id;
                address2Id = address2.Id;
            }
            
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccountAddress accAddress = new SecurityAccountAddress();
                accAddress.SecurityAccountId = accountId;
                accAddress.ContactAddressId = addressId;

                await RunCrudTest(unitOfWork, accAddress, "ContactAddressId", address2Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAccountContact()
        {
            int accountId = 0;
            int contactId = 0;
            int contact2Id = 0;

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = null;
                acc = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, acc);
                InsertObjectInDB(unitOfWork, acc.GeneralStatus);
                InsertObjectInDB(unitOfWork, acc.SecurityUserOwner);

                ContactInfo contact1 = new ContactInfo();
                contact1.ContactTypeId = ContactTypes.GetInstance().PHONE.Id;
                contact1.Details = "ContactInfo 1";
                InsertObjectInDB(unitOfWork, unitOfWork.ContactRepository.Insert(contact1));

                ContactInfo contact2 = new ContactInfo();
                contact2.ContactTypeId = ContactTypes.GetInstance().CELL_PHONE.Id;
                contact2.Details = "ContactInfo 2";
                InsertObjectInDB(unitOfWork, unitOfWork.ContactRepository.Insert(contact2));

                int objs = await unitOfWork.TrySaveChangesAsync();
                accountId = acc.Id;
                contactId = contact1.Id;
                contact2Id = contact2.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccountContact accContact = new SecurityAccountContact();
                accContact.SecurityAccountId = accountId;
                accContact.ContactId = contactId;

                await RunCrudTest(unitOfWork, accContact, "ContactId", contact2Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAccountStore()
        {
            int accountId = 0;
            int storeId = 0;
            int store2Id = 0;

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = null;
                acc = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, acc);
                InsertObjectInDB(unitOfWork, acc.GeneralStatus);
                InsertObjectInDB(unitOfWork, acc.SecurityUserOwner);

                StoreInfo store = EntitiesFactory.StoreFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, unitOfWork.StoreRepository.Insert(store));

                StoreInfo store2 = EntitiesFactory.StoreFactory.CreateInstance();
                store2.Name = "StoreInfo 2";
                InsertObjectInDB(unitOfWork, unitOfWork.StoreRepository.Insert(store2));

                int objs = await unitOfWork.TrySaveChangesAsync();
                accountId = acc.Id;
                storeId = store.Id;
                store2Id = store2.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccountStore accStore = new SecurityAccountStore();
                accStore.SecurityAccountId = accountId;
                accStore.StoreId = storeId;

                await RunCrudTest(unitOfWork, accStore, "StoreId", store2Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAccountUser()
        {
            int accountId = 0;
            int securityUserId = 0;
            int profileId = 0;
            int profile2Id = 0;

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccount acc = null;
                acc = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, acc);
                InsertObjectInDB(unitOfWork, acc.GeneralStatus);
                InsertObjectInDB(unitOfWork, acc.SecurityUserOwner);

                SecurityUser su = EntitiesFactory.SecurityUserFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, unitOfWork.SecurityUserRepository.Insert(su));

                SecurityProfile sp = EntitiesFactory.SecurityProfileFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, unitOfWork.SecurityProfileRepository.Insert(sp));

                SecurityProfile sp2 = EntitiesFactory.SecurityProfileFactory.CreateInstance();
                sp2.Name = "Security Profile Test 2";
                InsertObjectInDB(unitOfWork, unitOfWork.SecurityProfileRepository.Insert(sp2));

                int objs = await unitOfWork.TrySaveChangesAsync();
                accountId = acc.Id;
                securityUserId = su.Id;
                profileId = sp.Id;
                profile2Id = sp2.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAccountUser accUser = new SecurityAccountUser();
                accUser.SecurityAccountId = accountId;
                accUser.SecurityUserId = securityUserId;
                accUser.SecurityProfileId = profileId;

                await RunCrudTest(unitOfWork, accUser, "SecurityProfileId", profile2Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityAction()
        {
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityAction action = EntitiesFactory.SecurityActionFactory.CreateInstance();
                action.GeneralStatusId = action.GeneralStatus.Id;
                action.GeneralStatus = null;
                await RunCrudTest(unitOfWork, action, "ActionToExecute", "Test Action To Execute Updated");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityDefaultProfile()
        {
            int securityProfileId = 0;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityProfile sp = EntitiesFactory.SecurityProfileFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, unitOfWork.SecurityProfileRepository.Insert(sp));
                await unitOfWork.TrySaveChangesAsync();
                securityProfileId = sp.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor)) {
                SecurityDefaultProfile sdp = new SecurityDefaultProfile();
                sdp.SecurityProfileId = securityProfileId;
                sdp.SecurityProfileTypeId = SecurityProfileTypes.GetInstance().STORE.Id;
                await RunCrudTest(unitOfWork, sdp, "SecurityProfileTypeId", SecurityProfileTypes.GetInstance().CLIENT.Id);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityPassword()
        {
            int userId = 0;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityUser user = EntitiesFactory.SecurityUserFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, unitOfWork.SecurityUserRepository.Insert(user));
                await unitOfWork.TrySaveChangesAsync();
                userId = user.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityPassword sp = new SecurityPassword();
                sp.Password = "testPassword";
                sp.Sequence = 0;
                sp.SecurityUserId = userId;
                await RunCrudTest(unitOfWork, sp, "Sequence", 1);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityPerson()
        {
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityPerson person = EntitiesFactory.SecurityPersonFactory.CreateInstance("psaenz@gmail.com", false, "Pedro", "Saenz", "Avila");
                await RunCrudTest(unitOfWork, person, "Email", "psaenz2@gmail.com");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityProfile()
        {
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityProfile sp = new SecurityProfile();
                sp.GeneralStatusId = SecurityProfileStatuses.GetInstance().ACTIVE.Id;
                sp.Name = "Profile Test";
                sp.SecurityProfileTypeId = SecurityProfileTypes.GetInstance().STORE.Id;
                await RunCrudTest(unitOfWork, sp, "Name", "Profile Test 2");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityProfileAction()
        {
            int actionId = 0;
            int profileId = 0;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityProfileAction spa = EntitiesFactory.SecurityProfileActionFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, spa.SecurityAction);
                InsertObjectInDB(unitOfWork, unitOfWork.SecurityProfileRepository.Insert(spa.SecurityProfile));
                await unitOfWork.TrySaveChangesAsync();

                actionId = spa.SecurityAction.Id;
                profileId = spa.SecurityProfile.Id;
                await unitOfWork.TrySaveChangesAsync();
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityProfileAction spa = new SecurityProfileAction();
                spa.Available = true;
                spa.SecurityActionId = actionId;
                spa.SecurityProfileId = profileId;
                await RunCrudTest(unitOfWork, spa, "Available", false);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityProfileType()
        {
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityProfileType spt = new SecurityProfileType();
                spt.Name = "Security Profile Type Test";
                await RunCrudTest(unitOfWork, spt, "Name", "Security Profile Type Test 2");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Security"), TestCategory("DAL.Security.CRUD")]
        public async Task TestSecurityUser()
        {
            int personId = 0;
            int lastAccountId = 0;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityPerson sp = EntitiesFactory.SecurityPersonFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, unitOfWork.SecurityPersonRepository.Insert(sp));

                SecurityAccount sa = EntitiesFactory.SecurityAccountFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, sa);
                await unitOfWork.TrySaveChangesAsync();

                personId = sp.Id;
                lastAccountId = sa.Id;
                await unitOfWork.TrySaveChangesAsync();
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityUser su = new SecurityUser();
                su.GeneralStatusId = SecurityUserStatuses.GetInstance().ACTIVE.Id;
                su.LastAccountUsedId = lastAccountId;
                su.Login = "test";
                su.MustChangeThePassword = false;
                su.Password = "password";
                su.SecurityPersonId = personId;
                await RunCrudTest(unitOfWork, su, "LastAccountUsedId", null);
            }
        }
    }
}
