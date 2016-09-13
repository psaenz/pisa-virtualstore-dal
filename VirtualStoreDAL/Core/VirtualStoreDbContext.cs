namespace Pisa.VirtualStore.Dal.Core
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Pisa.VirtualStore.Models;
    using Pisa.VirtualStore.Models.Base;
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
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Reflection;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq.Expressions;

    /// <summary>
    /// Extends the System.Data.Entity to represent a combination of the Unit Of Work and Repository
    /// patterns such that it can be used to query from a database and group together
    /// changes that will then be written back to the store as a unit.
    /// DbContext is conceptually similar to ObjectContext.
    /// </summary>
    /// 
    public partial class VirtualStoreDbContext : DbContext
    {

        private Type _baseAuditableModelType = typeof(BaseAuditableModel);
        private static AuditAuthor _fakeUserContext = new AuditAuthor() { Id = -200, IdSecurityUser=-200 };

        public AuditAuthor CurrentAuthor { get; set; }

        // Callers are not allow to create a context without an AuditAuthor
        // But it is necessary when the Entity Framework is initializing the database when it doesnt exist
        private VirtualStoreDbContext() : this(_fakeUserContext) { }

#pragma warning disable 1591 // turn off comment generation
        public VirtualStoreDbContext(AuditAuthor author)
        {
            this.CurrentAuthor = author;
            Configuration.ProxyCreationEnabled = false;
        }

        public static VirtualStoreDbContext GetSystemVirtualStoreDbContext()
        {
            return new VirtualStoreDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Defines the default schema to use
            modelBuilder.HasDefaultSchema("VirtualStore");

            // Let's disable cascade on delete for all the relations
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Defines primary Keys for all the properties named Id
            modelBuilder.Properties().Where(p => p.Name == "Id").Configure(p => p.IsKey());

            _prepareBaseModel<SecurityUser>(modelBuilder);
            _prepareBaseModel<SecurityPerson>(modelBuilder);
            _prepareBaseModel<ProductUnitOfMeasure>(modelBuilder);

            // Lets now define which relations are options
            {
                // SecurityUser.LastAccountUsed should be optional
                modelBuilder.Entity<SecurityUser>().HasOptional<SecurityAccount>(l => l.LastAccountUsed).WithOptionalDependent();
            }

            //modelBuilder.Entity<ProductUnitOfMeasure>().HasRequired<AuditAuthor>(c => c.AddedBy).WithRequiredDependent().WillCascadeOnDelete(false);
            //modelBuilder.Entity<ProductUnitOfMeasure>().HasRequired<AuditAuthor>(c => c.UpdatedBy).WithRequiredDependent().WillCascadeOnDelete(false);

            /*
            modelBuilder.Entity<SecurityPerson>()
                .HasMany(left => left.Contacts)
                .WithMany(right => right.SecurityPersons)
                .Map(
                    m => m.ToTable("ContactsSecurityPersons")
                );

            modelBuilder.Entity<SecurityPerson>()
                .HasMany(left => left.ContactAddresses)
                .WithMany(right => right.SecurityPersons)
                .Map(
                    m => m.ToTable("ContactsAddressesSecurityPersons")
                );
            */
            base.OnModelCreating(modelBuilder);
        }

        private void _prepareBaseModel<T>(DbModelBuilder modelBuilder) where T : BaseModel
        {
            _ignoreNavigationPropertyKeys<T>(modelBuilder);
            _setRequiredDependent<T>(modelBuilder);
            //_setPrimaryKeys(modelBuilder);
            //_setRequiredDependentNoCascade<T, AuditAuthor>(modelBuilder, c => c.AddedBy);
            //_setRequiredDependentNoCascade<T, AuditAuthor>(modelBuilder, c => c.UpdatedBy);

        }

        /// <summary>
        /// All the relations in the models have 2 properties:
        /// - One for the Id only (which is primitive and normally and int)
        /// - The other is the object it selft, called navigation property.
        /// 
        /// In order to prevent EF to create 2 properties per relation, we will
        /// need to tell to ignore those that represents the Id only.
        /// 
        /// For instance, the SecurityUser has 2 properties "IdSecurityPerson" and "SecurityPerson",
        /// the first one is the primitive value for the relation and the
        /// second is the navigation property.
        /// 
        /// This method looks for all the properties that starts with the word "Id" and configure them 
        /// to be ignored by the EF.
        /// 
        /// Note, primary key property is always called "Id", this shouldn't be ignored, only Id*
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelBuilder"></param>
        private void _ignoreNavigationPropertyKeys<T>(DbModelBuilder modelBuilder) where T : BaseModel
        {
            // Using fluent Api this expression would go like this:
            // - modelBuilder.Entity<SecurityUser>().Ignore(prop => prop.SecurityPerson);
            // This method does the same in a dynamic way for all the properties starting with Id*

            foreach (PropertyInfo property in typeof(T).GetProperties().Where(p => p.Name.StartsWith("Id") && p.Name != "IdAddedBy" && p.Name != "IdUpdatedBy" && p.Name.Length > 2))
            {
                // Takes a reference of the Ignore method for the giving type (T) that represents a model class like SecurityUser
                var t = typeof(EntityTypeConfiguration<>);
                t = t.MakeGenericType(typeof(T));
                MethodInfo method = t.GetMethod("Ignore");
                MethodInfo genericMethod = method.MakeGenericMethod(property.PropertyType);

                // builds the expression 'x => x.PropertyName', required by the Ignore method
                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression member = Expression.Property(parameter, property.Name);
                var selector = Expression.Lambda(member, new ParameterExpression[] { parameter });

                // now calls the method => modelBuilder.Entity<SecurityUser>().Ignore(prop => prop.SecurityPerson);
                genericMethod.Invoke(modelBuilder.Entity<T>(), new object[] { selector });
            }
        }

        private void _setRequiredDependent<T>(DbModelBuilder modelBuilder) where T : class
        {
            // modelBuilder.Entity<SecurityUser>().HasRequired<SecurityAccount>(l => l.LastAccountUsed).WithRequiredDependent();
            IEnumerable<Type> allModels = ModelRegistry.GetInstance().GetAllModels();
            foreach (PropertyInfo property in typeof(T).GetProperties().Where(p => allModels.Select(m => m.Name).Contains(p.PropertyType.Name)))
            {
                // takes a reference of the HasRequired method for the giving type (T) that represents a model class like SecurityUser
                var t = typeof(EntityTypeConfiguration<>);
                t = t.MakeGenericType(typeof(T));
                MethodInfo hasRequiredMethod = t.GetMethod("HasRequired");
                MethodInfo hasRequiredGenericMethod = hasRequiredMethod.MakeGenericMethod(property.PropertyType);

                // builds the expression 'x => x.PropertyName', required by the HasRequired method
                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                MemberExpression member = Expression.Property(parameter, property.Name);
                var selector = Expression.Lambda(member, new ParameterExpression[] { parameter });

                // now calls the method => modelBuilder.Entity<SecurityUser>().HasRequired(x => x.SecurityPerson)
                var requiredNavigationPropertyConfiguration = hasRequiredGenericMethod.Invoke(modelBuilder.Entity<T>(), new object[] { selector });

                // takes a reference of the method WithRequiredPrincipal parameters less from the RequiredNavigationPropertyConfiguration class
                MethodInfo withRequiredPrincipalMethod = requiredNavigationPropertyConfiguration.GetType().GetMethods().Where(m => m.Name == "WithRequiredDependent" && m.GetParameters().Length == 0).FirstOrDefault();
                withRequiredPrincipalMethod.Invoke(requiredNavigationPropertyConfiguration, new object[] { });
            }
        }

        public virtual async Task<int> TrySaveChangesAsync()
        {
            try
            {
                DateTime saveTime = DateTime.UtcNow;

                // Updates the AddedOn date to all the BaseAuditableModel entities that will be inserted 
                foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added && _baseAuditableModelType.IsAssignableFrom(e.Entity.GetType())))
                {
                    BaseAuditableModel bm = (BaseAuditableModel)entry.Entity;
                    bm.AddedOn = saveTime;
                    bm.UpdatedOn = saveTime;
                    bm.IdAddedBy = CurrentAuthor.Id;
                    bm.IdUpdatedBy = CurrentAuthor.Id;
                }

                // Updates the UpdatedOn date to all the BaseAuditableModel entities that will be updated
                foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && _baseAuditableModelType.IsAssignableFrom(e.Entity.GetType())))
                {
                    BaseAuditableModel bm = (BaseAuditableModel)entry.Entity;
                    bm.UpdatedOn = saveTime;
                    bm.IdUpdatedBy = CurrentAuthor.Id;
                }

                return await this.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }

                throw new VirtualStoreDalException("Unexpected error while saving changes.", dbEx); //re-throw exception 
            }
        }

        public virtual DbSet<AuditAuthor> AuditAuthors { get; set; }
        public virtual DbSet<ArchivedCalculusBasketDetail> ArchivedCalculusBasketsDetails { get; set; }
        public virtual DbSet<ArchivedCalculusFreeProduct> ArchivedCalculusFreeProducts { get; set; }
        public virtual DbSet<ArchivedCalculusAppliedOffer> ArchivedCalculusAppliedOffers { get; set; }
        public virtual DbSet<ArchivedCalculusOrder> ArchivedCalculusOrders { get; set; }
        public virtual DbSet<ArchivedCalculusServiceCost> ArchivedCalculusServicesCosts { get; set; }
        public virtual DbSet<ClientBasket> Baskets { get; set; }
        public virtual DbSet<ClientBasketDetail> BasketsDetails { get; set; }
        public virtual DbSet<CalculusBasketDetail> CalculusBasketsDetails { get; set; }
        public virtual DbSet<CalculusFreeProduct> CalculusFreeProducts { get; set; }
        public virtual DbSet<CalculusAppliedOffer> CalculusAppliedOffers { get; set; }
        public virtual DbSet<CalculusOrder> CalculusOrders { get; set; }
        public virtual DbSet<CalculusServiceCost> CalculusServicesCosts { get; set; }
        public virtual DbSet<ClientFeedback> ClientsFeedbacks { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactAddress> ContactsAddresses { get; set; }
        public virtual DbSet<ContactRegion> ContactsRegions { get; set; }
        public virtual DbSet<ContactType> ContactsTypes { get; set; }
        public virtual DbSet<GeneralMedia> GeneralMedias { get; set; }
        public virtual DbSet<GeneralStatus> GeneralStatuses { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<OffersDetail> OffersDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderSchedule> OrdersSchedules { get; set; }
        public virtual DbSet<OrderScheduleControl> OrdersSchedulesControls { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductBrand> ProductsBranches { get; set; }
        public virtual DbSet<ProductType> ProductsTypes { get; set; }
        public virtual DbSet<ProductUnitOfMeasure> ProductsUnitsOfMeasures { get; set; }
        public virtual DbSet<SecurityAccount> SecurityAccounts { get; set; }
        public virtual DbSet<SecurityAccountAddress> SecurityAccountAddresses { get; set; }
        public virtual DbSet<SecurityAccountContact> SecurityAccountContacts { get; set; }
        public virtual DbSet<SecurityAccountStore> SecurityAccountStores { get; set; }
        public virtual DbSet<SecurityAccountUser> SecurityAccountUsers { get; set; }
        public virtual DbSet<SecurityAction> SecurityActions { get; set; }
        public virtual DbSet<SecurityDefaultProfile> SecurityDefaultProfiles { get; set; }
        public virtual DbSet<SecurityPassword> SecurityPasswords { get; set; }
        public virtual DbSet<SecurityPerson> SecurityPersons { get; set; }
        public virtual DbSet<SecurityProfile> SecurityProfiles { get; set; }
        public virtual DbSet<SecurityProfileAction> SecurityProfilesActions { get; set; }
        public virtual DbSet<SecurityUser> SecurityUsers { get; set; }
        public virtual DbSet<ServiceByStore> ServicesByStores { get; set; }
        public virtual DbSet<ServiceRule> ServicesRules { get; set; }
        public virtual DbSet<ServiceType> ServicesTypes { get; set; }
        public virtual DbSet<ServiceZone> ServicesZones { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreAddress> StoresAddresses { get; set; }
        public virtual DbSet<StoreContact> StoresContacts { get; set; }
        public virtual DbSet<StoreProduct> StoresProducts { get; set; }
        public virtual DbSet<StoreZone> StoresZones { get; set; }
        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArchivedCalculusAppliedOffers>()
                .HasMany(e => e.ArchivedCalculusBasketsDetails)
                .WithOptional(e => e.ArchivedCalculusAppliedOffers)
                .HasForeignKey(e => e.IdAppliedOffer);

            modelBuilder.Entity<ArchivedCalculusOrder>()
                .HasMany(e => e.ArchivedCalculusBasketsDetails)
                .WithRequired(e => e.ArchivedCalculusOrder)
                .HasForeignKey(e => e.IdCalculusOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ArchivedCalculusOrder>()
                .HasMany(e => e.ArchivedCalculusFreeProducts)
                .WithRequired(e => e.ArchivedCalculusOrder)
                .HasForeignKey(e => e.IdCalculosCanasta_ArchivedCalculusOrders)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ArchivedCalculusOrder>()
                .HasMany(e => e.ArchivedCalculusAppliedOffers)
                .WithRequired(e => e.ArchivedCalculusOrder)
                .HasForeignKey(e => e.IdCalculusOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ArchivedCalculusOrder>()
                .HasMany(e => e.ArchivedCalculusServiceCosts)
                .WithRequired(e => e.ArchivedCalculusOrder)
                .HasForeignKey(e => e.IdCalculusOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientBasket>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ClientBasket>()
                .HasMany(e => e.BasketsDetails)
                .WithRequired(e => e.Basket)
                .HasForeignKey(e => e.IdBasket)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientBasketDetail>()
                .Property(e => e.MoreDetails)
                .IsUnicode(false);

            modelBuilder.Entity<ClientBasketDetail>()
                .HasMany(e => e.ArchivedCalculusBasketsDetails)
                .WithRequired(e => e.BasketsDetail)
                .HasForeignKey(e => e.IdBasketDetail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientBasketDetail>()
                .HasMany(e => e.CalculusBasketDetails)
                .WithRequired(e => e.BasketsDetail)
                .HasForeignKey(e => e.IdBasketDetail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CalculusAppliedOffer>()
                .HasMany(e => e.CalculusBasketDetails)
                .WithOptional(e => e.CalculusAppliedOffers)
                .HasForeignKey(e => e.IdAppliedOffer);

            modelBuilder.Entity<CalculusOrder>()
                .HasMany(e => e.CalculusBasketDetails)
                .WithRequired(e => e.CalculusOrder)
                .HasForeignKey(e => e.IdCalculusOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CalculusOrder>()
                .HasMany(e => e.CalculusFreeProducts)
                .WithRequired(e => e.CalculusOrder)
                .HasForeignKey(e => e.IdCalculusOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CalculusOrder>()
                .HasMany(e => e.CalculusAppliedOffers)
                .WithRequired(e => e.CalculusOrder)
                .HasForeignKey(e => e.IdCalculusOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CalculusOrder>()
                .HasMany(e => e.CalculusServiceCosts)
                .WithRequired(e => e.CalculusOrder)
                .HasForeignKey(e => e.IdCalculusOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientFeedback>()
                .Property(e => e.Feedback)
                .IsUnicode(false);

            modelBuilder.Entity<ClientFeedback>()
                .Property(e => e.Answerd)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Contact1)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.SecurityPersonsContacts)
                .WithOptional(e => e.Contact)
                .HasForeignKey(e => e.IdContact);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.StoreContacts)
                .WithRequired(e => e.Contact)
                .HasForeignKey(e => e.IdContact)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContactsAddress>()
                .Property(e => e.Details)
                .IsUnicode(false);

            modelBuilder.Entity<ContactsAddress>()
                .HasMany(e => e.SecurityPersons)
                .WithOptional(e => e.ContactsAddress)
                .HasForeignKey(e => e.IdContactAddress);

            modelBuilder.Entity<ContactsAddress>()
                .HasMany(e => e.StoreAddresses)
                .WithRequired(e => e.ContactsAddress)
                .HasForeignKey(e => e.IdContactsAddress)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContactRegion>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ContactRegion>()
                .HasMany(e => e.ContactAddresses)
                .WithRequired(e => e.ContactRegion)
                .HasForeignKey(e => e.IdContactRegion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContactRegion>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.ContactRegion)
                .HasForeignKey(e => e.IdContactsRegion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContactRegion>()
                .HasMany(e => e.StoreZones)
                .WithOptional(e => e.ContactRegion)
                .HasForeignKey(e => e.IdContactRegion);

            modelBuilder.Entity<ContactRegion>()
                .HasMany(e => e.ContactsRegions1)
                .WithOptional(e => e.ContactsRegion1)
                .HasForeignKey(e => e.IdContactRegionParent);

            modelBuilder.Entity<ContactsType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ContactsType>()
                .HasMany(e => e.Contacts)
                .WithRequired(e => e.ContactsType)
                .HasForeignKey(e => e.IdContactType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralMedia>()
                .Property(e => e.MediaReference)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralMedia>()
                .HasMany(e => e.Offers)
                .WithOptional(e => e.GeneralMedia)
                .HasForeignKey(e => e.IdGeneralMedia);

            modelBuilder.Entity<GeneralMedia>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.GeneralMedia)
                .HasForeignKey(e => e.IdGeneralMedia);

            modelBuilder.Entity<GeneralMedia>()
                .HasMany(e => e.Stores)
                .WithOptional(e => e.GeneralMedia)
                .HasForeignKey(e => e.IdGeneralMediaLogo);

            modelBuilder.Entity<GeneralStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralStatus>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralStatus>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<GeneralStatus>()
                .HasMany(e => e.ContactsRegions)
                .WithRequired(e => e.GeneralStatus)
                .HasForeignKey(e => e.IdGeneralStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralStatus>()
                .HasMany(e => e.Offers)
                .WithRequired(e => e.GeneralStatus)
                .HasForeignKey(e => e.IdStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralStatus>()
                .HasMany(e => e.ProductsBranches)
                .WithRequired(e => e.GeneralStatus)
                .HasForeignKey(e => e.IdStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralStatus>()
                .HasMany(e => e.ProductTypes)
                .WithRequired(e => e.GeneralStatus)
                .HasForeignKey(e => e.IdStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralStatus>()
                .HasMany(e => e.SecurityProfiles)
                .WithRequired(e => e.GeneralStatus)
                .HasForeignKey(e => e.IdStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralStatus>()
                .HasMany(e => e.SecurityUsers)
                .WithRequired(e => e.GeneralStatus)
                .HasForeignKey(e => e.IdGeneralStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralStatus>()
                .HasMany(e => e.OrdersSchedules)
                .WithRequired(e => e.GeneralStatus)
                .HasForeignKey(e => e.IdGeneralStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralStatus>()
                .HasMany(e => e.Stores)
                .WithRequired(e => e.GeneralStatus)
                .HasForeignKey(e => e.IdGeneralStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Offer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Offer>()
                .HasMany(e => e.ArchivedCalculusAppliedOffers)
                .WithRequired(e => e.Offer)
                .HasForeignKey(e => e.IdOffer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Offer>()
                .HasMany(e => e.CalculusAppliedOffers)
                .WithRequired(e => e.Offer)
                .HasForeignKey(e => e.IdOffer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Offer>()
                .HasMany(e => e.OfferDetails)
                .WithRequired(e => e.Offer)
                .HasForeignKey(e => e.IdOffer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.ArchivedCalculusOrders)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.IdOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.CalculusOrders)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.IdOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrdersSchedules)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.IdOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Orders1)
                .WithRequired(e => e.Order1)
                .HasForeignKey(e => e.IdBasket);

            modelBuilder.Entity<OrdersSchedulesMonthDay>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ArchivedCalculusFreeProducts)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProducto_Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.BasketsDetails)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.CalculusFreeProducts)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OfferDetails)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.StoreProducts)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductTypes)
                .WithMany(e => e.Products)
                .Map(m => m.ToTable("ProductsAndTypes", "VirtualStore").MapLeftKey("IdProduct").MapRightKey("IdProductType"));

            modelBuilder.Entity<ProductBrand>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductBrand>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductBrand)
                .HasForeignKey(e => e.IdBranch)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.ProductsTypes1)
                .WithOptional(e => e.ProductsType1)
                .HasForeignKey(e => e.IdProductTypeParent);

            modelBuilder.Entity<ProductUnitOfMeasure>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductUnitOfMeasure>()
                .Property(e => e.Symbol)
                .IsUnicode(false);

            modelBuilder.Entity<ProductUnitOfMeasure>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductUnitOfMeasure)
                .HasForeignKey(e => e.IdUnitOfMeasure)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityAction>()
                .Property(e => e.Action)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityAction>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityAction>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityAction>()
                .Property(e => e.ActionToExecute)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityAction>()
                .HasMany(e => e.SecurityActions1)
                .WithOptional(e => e.SecurityAction1)
                .HasForeignKey(e => e.IdSecurityActionParent);

            modelBuilder.Entity<SecurityAction>()
                .HasMany(e => e.SecurityActionsAccesses)
                .WithRequired(e => e.SecurityAction)
                .HasForeignKey(e => e.IdSecurityAction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityAction>()
                .HasMany(e => e.SecurityProfileActions)
                .WithRequired(e => e.SecurityAction)
                .HasForeignKey(e => e.IdSecurityAction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityActionsAccess>()
                .Property(e => e.Access)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityPassword>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityPerson>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityPerson>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityPerson>()
                .Property(e => e.MaidenName)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityPerson>()
                .Property(e => e.Identification)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityPerson>()
                .HasMany(e => e.SecurityPersonsContacts)
                .WithRequired(e => e.SecurityPerson)
                .HasForeignKey(e => e.IdPerson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityPerson>()
                .HasMany(e => e.SecurityUsers)
                .WithRequired(e => e.SecurityPerson)
                .HasForeignKey(e => e.IdSecurityPerson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityProfile>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityProfile>()
                .HasMany(e => e.SecurityProfileActions)
                .WithRequired(e => e.SecurityProfile)
                .HasForeignKey(e => e.IdSecurityProfile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityProfile>()
                .HasMany(e => e.SecurityStoreProfiles)
                .WithRequired(e => e.SecurityProfile)
                .HasForeignKey(e => e.IdSecurityProfile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityProfile>()
                .HasMany(e => e.SecurityUsers)
                .WithRequired(e => e.SecurityProfile)
                .HasForeignKey(e => e.IdSecurityProfile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityProfilesAction>()
                .HasMany(e => e.SecurityStoresProfilesActions)
                .WithRequired(e => e.SecurityProfilesAction)
                .HasForeignKey(e => e.IdSecurityStoresProfile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityStoresProfile>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityStoresProfile>()
                .HasMany(e => e.SecurityStoresProfilesActions)
                .WithRequired(e => e.SecurityStoresProfile)
                .HasForeignKey(e => e.IdSecurityProfilesAction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityStoresProfile>()
                .HasMany(e => e.SecurityStoresUsers)
                .WithRequired(e => e.SecurityStoresProfile)
                .HasForeignKey(e => e.IdSecurityStoresProfiles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityUser>()
                .Property(e => e.User)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityUser>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityUser>()
                .HasMany(e => e.Baskets)
                .WithRequired(e => e.SecurityUser)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityUser>()
                .HasMany(e => e.ClientsFeedbacks)
                .WithRequired(e => e.SecurityUser)
                .HasForeignKey(e => e.IdClient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityUser>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.SecurityUser)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityUser>()
                .HasMany(e => e.OrdersSchedules)
                .WithRequired(e => e.SecurityUser)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityUser>()
                .HasMany(e => e.SecurityPasswords)
                .WithRequired(e => e.SecurityUser)
                .HasForeignKey(e => e.IdSecurityUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityUser>()
                .HasMany(e => e.SecurityStoresUsers)
                .WithRequired(e => e.SecurityUser)
                .HasForeignKey(e => e.IdSecurityUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecurityUsersType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<SecurityUsersType>()
                .HasMany(e => e.SecurityProfiles)
                .WithRequired(e => e.SecurityUsersType)
                .HasForeignKey(e => e.IdSecurityUserType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServicesByStore>()
                .HasMany(e => e.ServiceRules)
                .WithRequired(e => e.ServicesByStore)
                .HasForeignKey(e => e.IdServiceByStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServicesType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ServicesType>()
                .Property(e => e.ShortDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ServicesType>()
                .HasMany(e => e.ArchivedCalculusServiceCosts)
                .WithRequired(e => e.ServicesType)
                .HasForeignKey(e => e.IdServicesTypes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServicesType>()
                .HasMany(e => e.CalculusServiceCosts)
                .WithRequired(e => e.ServicesType)
                .HasForeignKey(e => e.IdServicesTypes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServicesType>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.ServicesType)
                .HasForeignKey(e => e.IdServiceType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServicesType>()
                .HasMany(e => e.ServiceByStores)
                .WithRequired(e => e.ServicesType)
                .HasForeignKey(e => e.IdServiceType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceZone>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceZone>()
                .HasMany(e => e.ServiceRules)
                .WithRequired(e => e.ServiceZone)
                .HasForeignKey(e => e.IdServiceZone)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceZone>()
                .HasMany(e => e.StoreZones)
                .WithRequired(e => e.ServiceZone)
                .HasForeignKey(e => e.IdServiceZone)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.ArchivedCalculusOrders)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.CalculusOrders)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.SecurityStoreProfiles)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.ServiceByStores)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.ServiceZones)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.StoreContacts)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.StoreAddresses)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.StoreProducts)
                .WithRequired(e => e.Store)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.Stores1)
                .WithOptional(e => e.Store1)
                .HasForeignKey(e => e.IdStoreParent);

            modelBuilder.Entity<StoresProduct>()
                .Property(e => e.ProductCode)
                .IsUnicode(false);
        }
        */
    }
}
