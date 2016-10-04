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
    public class BaseCrudTests
    {
        IList<IBaseModel> _objectsToDelete = new List<IBaseModel>();

        // -2 means unit test users. -1 means system user
        public AuditAuthor TestAuthor = new AuditAuthor() { SecurityUserId = -2, CurrentDate = DateTime.UtcNow};

        public void InsertObjectInDB(VirtualStoreUnitOfWork unitOfWork, IBaseModel model)
        {
            IBaseRepository repository = unitOfWork.GetRepositoryFor(model);
            repository.Insert(model);
            _objectsToDelete.Add(TestAuthor);
        }

        [TestInitialize]
        public async void TestInitialize()
        {
            using (var unitOfWork = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork()) {
                TestAuthor = unitOfWork.AuditAuthorRepository.Insert(TestAuthor);
                InsertObjectInDB(unitOfWork, TestAuthor);
                await unitOfWork.TrySaveChangesAsync();
            }
        }

        [TestCleanup]
        public async void TestCleanup() {
            using (var unitOfWork = VirtualStoreUnitOfWork.GetSystemVirtualStoreUnitOfWork())
            {
                foreach (IBaseModel delete in _objectsToDelete)
                    unitOfWork.GetRepositoryFor(delete).Delete(delete.Id);
                await unitOfWork.TrySaveChangesAsync();
            }
        }

        public async Task RunCrudTest<T>(VirtualStoreUnitOfWork unitOfWork, T objectToSave, string propertyToChange, object propertyValue) where T : BaseModel
        {
            IBaseRepository repository = unitOfWork.GetRepositoryFor(objectToSave);
            string objType = objectToSave.GetType().Name;
            int countBeforeTest = repository.GetAll().Count();
            InsertObjectInDB(unitOfWork, repository.Insert(objectToSave));

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
