using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pisa.VirtualStore.Dal.Core;
using Pisa.VirtualStore.Dal.Test.Factories;
using Pisa.VirtualStore.Models.Audit;
using Pisa.VirtualStore.Models.Security;
using System;
using System.Threading.Tasks;

namespace Pisa.VirtualStore.Dal.Test.CRUD
{
    [TestClass]
    public class AuditCRUDTests : BaseCrudTests
    {
        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.Audit"), TestCategory("DAL.Audit.CRUD")]
        public async Task TestAuditAuthor()
        {
            int userId = 0;
            int userId2 = 0;
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                SecurityUser user = EntitiesFactory.SecurityUserFactory.CreateInstance();
                InsertObjectInDB(unitOfWork, user);

                SecurityUser user2 = EntitiesFactory.SecurityUserFactory.CreateInstance();
                user2.Login = "login test 2";
                InsertObjectInDB(unitOfWork, user2);

                await unitOfWork.TrySaveChangesAsync();

                userId = user.Id;
                userId2 = user2.Id;
            }

            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                AuditAuthor author = new AuditAuthor();
                author.CurrentDate = DateTime.UtcNow;
                author.SecurityUserId = userId;
                await RunCrudTest(unitOfWork, author, "SecurityUserId", userId2);
            }
        }
    }
}
