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
            AuditAuthor systemAuthor = new AuditAuthor() { SecurityUserId = -1, CurrentDate = DateTime.UtcNow };
            systemAuthor = context.AuditAuthors.Add(systemAuthor);
            context.SaveChangesAsync().Wait();
            context.CurrentAuthor = systemAuthor;
            #endregion

            #region Add Product Unit Of Measures
            var productUnitOfMeasures = new List<ProductUnitOfMeasure>
            {
                new ProductUnitOfMeasure{Name="Gramos", Symbol="gr"},
                new ProductUnitOfMeasure{Name="Litros", Symbol="lt"}
            };
            productUnitOfMeasures.ForEach(um => context.ProductsUnitsOfMeasures.Add(um));
            #endregion

            #region Add General Statuses
            var generalStatus = new List<GeneralStatus>
            {
                new GeneralStatus { Type = "Brand", Name = "Editing", Description = "The Brand is in Edition mode"},
                new GeneralStatus { Type = "Brand", Name = "Published", Description = "Brand is published"},
                new GeneralStatus { Type = "Brand", Name = "UnPublished", Description = "Brand is unpublished"},
                new GeneralStatus { Type = "ContactRegion", Name = "Active",  Description = "Region is Active"  },
                new GeneralStatus { Type = "ContactRegion", Name = "Locked",  Description = "Region is Locked"  },
                new GeneralStatus { Type = "Offer", Name = "Editing", Description = "The Offer is in Edition mode"},
                new GeneralStatus { Type = "Offer", Name = "Published", Description = "Offer is published"},
                new GeneralStatus { Type = "Offer", Name = "UnPublished", Description = "Offer is unpublished"},
                new GeneralStatus { Type = "Offer", Name = "Cancelled", Description = "Offer was cancelled"},
                new GeneralStatus { Type = "Order", Name = "Pending", Description = "Order is pending"},
                new GeneralStatus { Type = "Order", Name = "Processing", Description = "Order is being processed" },
                new GeneralStatus { Type = "Order", Name = "Delivering", Description = "Order is being delivered" },
                new GeneralStatus { Type = "Order", Name = "Delivered", Description = "Order was delivered" },
                new GeneralStatus { Type = "Order", Name = "Cancelled", Description = "Order was cancelled" },
                new GeneralStatus { Type = "SecurityAccount", Name = "Active",  Description = "User Account is Active"  },
                new GeneralStatus { Type = "SecurityAccount", Name = "Locked",  Description = "User Account is Locked"  },
                new GeneralStatus { Type = "SecurityAccount", Name = "Deleted", Description = "User Account is marked as Deleted"  },
                new GeneralStatus { Type = "SecurityAction", Name = "Active", Description = "Action is Active"  },
                new GeneralStatus { Type = "SecurityAction", Name = "Locked", Description = "Action  is Locked"  },
                new GeneralStatus { Type = "SecurityAction", Name = "Deleted", Description = "Action is marked as Deleted"  },
                new GeneralStatus { Type = "SecurityProfile", Name = "Active", Description = "Security Profile is Active"  },
                new GeneralStatus { Type = "SecurityProfile", Name = "InActive", Description = "Security Profile is InActive"  },
                new GeneralStatus { Type = "SecurityUser", Name = "Active", Description = "User is Active"  },
                new GeneralStatus { Type = "SecurityUser", Name = "Locked",  Description = "User is Locked"  },
                new GeneralStatus { Type = "SecurityUser", Name = "Deleted", Description = "User is marked as Deleted"  },
                new GeneralStatus { Type = "StoreInfo", Name = "Active",  Description = "StoreInfo is Active"  },
                new GeneralStatus { Type = "StoreInfo", Name = "Locked",  Description = "StoreInfo is Locked"  }
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
                new SecurityProfileType{Name="StoreInfo"},
                new SecurityProfileType{Name="Brand"},
                new SecurityProfileType{Name="Provider"}
            };
            securityProfileTypes.ForEach(ct => context.SecurityProfileTypes.Add(ct));
            #endregion 

            #region Service Types
            var serviceTypes = new List<ServiceType>
            {
                new ServiceType{Name="To Home", Description="The store will send the order to the address provided" },
                new ServiceType{Name="To Pick Up", Description="The store will prepare the order for the client to pick it up"},
            };
            serviceTypes.ForEach(ct => context.ServicesTypes.Add(ct));
            #endregion 

            context.TrySaveChangesAsync().Wait();
        }
    }
}
