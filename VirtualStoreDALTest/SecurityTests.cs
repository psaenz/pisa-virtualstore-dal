using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pisa.VirtualStore.Dal;
using System.Data.Entity.Validation;
using System.Linq;
using System.Data.Entity;

namespace Pisa.VirtualStore.Dal.Test
{
    using Pisa.VirtualStore.Dal.Core;
    using Pisa.VirtualStore.Models;
    using Pisa.VirtualStore.Models.Archived;
    using Pisa.VirtualStore.Models.Audit;
    using Pisa.VirtualStore.Models.Base;
    using Pisa.VirtualStore.Models.Calculus;
    using Pisa.VirtualStore.Models.Client;
    using Pisa.VirtualStore.Models.Contact;
    using Pisa.VirtualStore.Models.General;
    using Pisa.VirtualStore.Models.Offer;
    using Pisa.VirtualStore.Models.Order;
    using Pisa.VirtualStore.Models.Product;
    using Pisa.VirtualStore.Models.Security;
    using Pisa.VirtualStore.Models.Service;
    using Pisa.VirtualStore.Models.Store;

    [TestClass]
    public class SecurityTests
    {
        // -2 means unit test users. -1 means system user
        AuditAuthor TestAuthor = new AuditAuthor() { IdSecurityUser = -2, CurrentDate = DateTime.UtcNow};

        [TestInitialize]
        public void TestInitialize()
        {
            using (var context = VirtualStoreDbContext.GetSystemVirtualStoreDbContext()) {
                TestAuthor = context.AuditAuthors.Add(TestAuthor);
                context.SaveChangesAsync().Wait();
            }
        }

        [TestCleanup]
        public void TestCleanup() {
            using (var context = VirtualStoreDbContext.GetSystemVirtualStoreDbContext())
            {
                context.AuditAuthors.Attach(TestAuthor);
                context.AuditAuthors.Remove(TestAuthor);
                context.SaveChangesAsync().Wait();
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("Security")]
        public async System.Threading.Tasks.Task TestSecurityPerson()
        {
            using (var context = new VirtualStoreDbContext(TestAuthor))
            {
                SecurityPerson person = new SecurityPerson();
                person.FirstName = "Pedro";
                person.LastName = "Saenz";
                person.MaidenName = "Avila";
                person.Email = "psaenz@gmail.com";
                context.SecurityPersons.Add(person);
                RunTest(context, context.SecurityPersons, person, "Email", "psaenz2@gmail.com");
            }
        }

        private async void RunTest<T>(VirtualStoreDbContext context, DbSet<T> objectCollection, T objectToSave, string propertyToChange, string propertyValue) where T : BaseModel
        {
            string objType = objectToSave.GetType().Name;
            int countBeforeTest = objectCollection.Count();
            objectCollection.Add(objectToSave);

            int i = await context.TrySaveChangesAsync();
            Assert.AreEqual(1, i, i + " " + objType + " were saved, only 1 was expected.");
            Assert.AreEqual(1, context.SecurityPersons.Count() - countBeforeTest, objType +" wasn't added");
            Assert.AreNotEqual(0, objectToSave.Id, objType +" PK wasn't set.");

            T objectSaved = objectCollection.Find(objectToSave.Id);
            Assert.AreSame(objectToSave, objectSaved, objType + " is not cached by the EF");

            objectToSave.setPropertyValue(propertyToChange, propertyValue);
            i = context.TrySaveChangesAsync().Result;
            Assert.AreEqual(1, i, objType + " wasn't updated");
            Assert.AreEqual(1, context.SecurityPersons.Count() - countBeforeTest, objType + " was added during update.");

            objectSaved = objectCollection.Remove(objectToSave);
            i = await context.TrySaveChangesAsync();
            Assert.AreEqual(1, i, objType + " wasn't deleted");
            Assert.AreEqual(context.SecurityPersons.Count(), countBeforeTest, objType + " wasn't removed");
            Assert.AreSame(objectToSave, objectSaved, objType + " is not cached by the EF");

            T objectDeleted = objectCollection.Find(objectSaved.Id);
            Assert.IsNull(objectDeleted, "User wasn't deleted");
        }
    }
}
