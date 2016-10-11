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
    public class GeneralCrudTests : BaseCrudTests
    {
        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.General"), TestCategory("DAL.General.CRUD")]
        public async Task TestGeneralMedia()
        {
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                GeneralMedia gm = EntitiesFactory.GeneralMediaFactory.CreateInstance();
                gm.Height = 40.40;
                await RunCrudTest(unitOfWork, gm, "Height", 50.50);
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.General"), TestCategory("DAL.General.CRUD")]
        public async Task TestGeneralMediaType()
        {
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                GeneralMediaType gmt = new GeneralMediaType();
                gmt.Name = "Media Type Test";
                await RunCrudTest(unitOfWork, gmt, "Name", "Media Type Test 2");
            }
        }

        [TestMethod]
        [TestCategory("DAL"), TestCategory("DAL.CRUD"), TestCategory("DAL.General"), TestCategory("DAL.General.CRUD")]
        public async Task TestGeneralSchedule()
        {
            using (var unitOfWork = new VirtualStoreUnitOfWork(TestAuthor))
            {
                GeneralSchedule gs = new GeneralSchedule();
                gs.EndDate = DateTime.UtcNow;
                gs.EndInRuns = 2;
                gs.EndWhen = "End When Test";
                gs.Frequency = "Weekly";
                gs.FrequencyValue = 1;
                gs.GeneralStatusId = GeneralScheduleStatuses.GetInstance().EDITING.Id;
                gs.SpecificMonthDay = 23;
                gs.SpecificWeekday = "L";
                gs.StartDate = DateTime.UtcNow;
                await RunCrudTest(unitOfWork, gs, "EndWhen", "End When Test 2");
            }
        }
    }
}
