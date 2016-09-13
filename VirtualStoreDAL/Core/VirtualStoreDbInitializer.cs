namespace Pisa.VirtualStore.Dal.Core
{
    using System;
    using System.Collections.Generic;
    using Pisa.VirtualStore.Models;
    using Pisa.VirtualStore.Models.Archived;
    using Pisa.VirtualStore.Models.Audit;
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
