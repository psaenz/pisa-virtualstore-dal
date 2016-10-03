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
    using Pisa.VirtualStore.Dal.Core.Repositories.Security;

    public class VirtualStoreDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<VirtualStoreDbContext>
    {

        protected override void Seed(VirtualStoreDbContext context) {

            #region Add SystemAuthor
            // Creates the SystemAuthor, it has to have the id "1" and IdSecurityUser = -1
            AuditAuthor systemAuthor = new AuditAuthor() { SecurityUserId = -1, CurrentDate = DateTime.UtcNow};
            systemAuthor = context.AuditAuthors.Add(systemAuthor);
            context.SaveChangesAsync().Wait();
            context.CurrentAuthor = systemAuthor;
            #endregion

            #region Add Product Unit Of Measures
            var productUnitOfMeasures = new List<ProductUnitOfMeasure>
            {
                new ProductUnitOfMeasure{Name="gramos", Symbol="gr"},
                new ProductUnitOfMeasure{Name="litros", Symbol="lt"}
            };
            productUnitOfMeasures.ForEach(um => context.ProductsUnitsOfMeasures.Add(um));
            #endregion

            #region Add General Statuses
            var generalStatus = new List<GeneralStatus>
            {
                new GeneralStatus {Name= "Active", Type="ContactRegion", Description="Region is Active"  },
                new GeneralStatus {Name= "Locked", Type="ContactRegion", Description="Region is Locked"  },
                new GeneralStatus {Name= "Active", Type="SecurityAccount", Description="User Account is Active"  },
                new GeneralStatus {Name= "Locked", Type="SecurityAccount", Description="User Account is Locked"  },
                new GeneralStatus {Name= "Deleted", Type="SecurityAccount", Description="User Account is marked as Deleted"  },
                new GeneralStatus {Name= "Active", Type="SecurityAction", Description="Action is Active"  },
                new GeneralStatus {Name= "Locked", Type="SecurityAction", Description="Action  is Locked"  },
                new GeneralStatus {Name= "Deleted", Type="SecurityAction", Description="Action is marked as Deleted"  },
                new GeneralStatus {Name= "Active", Type="SecurityProfile", Description="Security Profile is Active"  },
                new GeneralStatus {Name= "InActive", Type="SecurityProfile", Description="Security Profile is InActive"  },
                new GeneralStatus {Name= "Active", Type="SecurityUser", Description="User is Active"  },
                new GeneralStatus {Name= "Locked", Type="SecurityUser", Description="User is Locked"  },
                new GeneralStatus {Name= "Deleted", Type="SecurityUser", Description="User is marked as Deleted"  },
                new GeneralStatus {Name= "Active", Type="Store", Description="Store is Active"  },
                new GeneralStatus {Name= "Locked", Type="Store", Description="Store is Locked"  }
            };

            generalStatus.ForEach(gs => context.GeneralStatuses.Add(gs));
            #endregion

            #region Contact Types
            var contactTypes = new List<ContactType>
            {
                new ContactType {Name= "Phone"},
                new ContactType {Name= "Cell Phone"}
            };
            contactTypes.ForEach(ct => context.ContactsTypes.Add(ct));
            #endregion 

            #region Security Profile Types
            var securityProfileTypes = new List<SecurityProfileType>
            {
                new SecurityProfileType{Name="Administrator" },
                new SecurityProfileType{Name="Client"},
                new SecurityProfileType{Name="Store"},
                new SecurityProfileType{Name="Brand"},
                new SecurityProfileType{Name="Provider"}
            };
            securityProfileTypes.ForEach(ct => context.SecurityProfileTypes.Add(ct));
            #endregion 

            context.TrySaveChangesAsync().Wait();
        }
    }
}
