namespace Pisa.VirtualStore.Dal.Core
{
    using System;
    using System.Collections.Generic;
    using Pisa.VirtualStore.Dal.Core.Models;
    using Pisa.VirtualStore.Dal.Core.Models.Archived;
    using Pisa.VirtualStore.Dal.Core.Models.Audit;
    using Pisa.VirtualStore.Dal.Core.Models.Calculus;
    using Pisa.VirtualStore.Dal.Core.Models.Client;
    using Pisa.VirtualStore.Dal.Core.Models.Contact;
    using Pisa.VirtualStore.Dal.Core.Models.General;
    using Pisa.VirtualStore.Dal.Core.Models.Offer;
    using Pisa.VirtualStore.Dal.Core.Models.Order;
    using Pisa.VirtualStore.Dal.Core.Models.Product;
    using Pisa.VirtualStore.Dal.Core.Models.Security;
    using Pisa.VirtualStore.Dal.Core.Models.Service;
    using Pisa.VirtualStore.Dal.Core.Models.Store;

    public class VirtualStoreDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<VirtualStoreDbContext>
    {

        protected override void Seed(VirtualStoreDbContext context) {

            // Creates the SystemAuthor, it has to have the id "1" and IdSecurityUser = -1
            AuditAuthor systemAuthor = new AuditAuthor() { IdSecurityUser = -1, CurrentDate = DateTime.UtcNow};
            systemAuthor = context.AuditAuthors.Add(systemAuthor);
            context.SaveChangesAsync().Wait();
            context.CurrentAuthor = systemAuthor;

            var productUnitOfMeasures = new List<ProductUnitOfMeasure>
            {
                new ProductUnitOfMeasure{Name="gramos", Symbol="gr"},
                new ProductUnitOfMeasure{Name="litros", Symbol="lt"}
            };

            productUnitOfMeasures.ForEach(s => context.ProductsUnitsOfMeasures.Add(s));
            context.TrySaveChangesAsync().Wait();
        }
    }
}
