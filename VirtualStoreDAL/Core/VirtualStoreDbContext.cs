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
    using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Archived;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Reflection;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Linq.Expressions;
    using Common.Logging;
    using System.Data.Entity.Infrastructure;

    /// <summary>
    /// Extends the System.Data.Entity to represent a combination of the Unit Of Work and Repository
    /// patterns such that it can be used to query from a database and group together
    /// changes that will then be written back to the store as a unit.
    /// DbContext is conceptually similar to ObjectContext.
    /// </summary>
    /// 
    public partial class VirtualStoreDbContext : DbContext
    {
        private static readonly ILog _log = LogManager.GetLogger(
           MethodBase.GetCurrentMethod().DeclaringType
        );

        private Type _baseModelType = typeof(IBaseModel);
        private Type _baseAuditableModelType = typeof(IBaseAuditableModel);
        private static AuditAuthor _fakeUserContext = new AuditAuthor() { Id = -200, SecurityUserId=-200 };

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
            
            Configuration.AutoDetectChangesEnabled = false;

            modelBuilder.Configurations.AddFromAssembly(typeof(ArchivedCalculusAppliedOfferEntityConfiguration).Assembly);

            {
                #region configuring Archived namespace
                    _prepareBaseModel<ArchivedCalculusAppliedOffer>(modelBuilder);
                    _prepareBaseModel<ArchivedCalculusBasketDetail>(modelBuilder);
                    _prepareBaseModel<ArchivedCalculusFreeProduct>(modelBuilder);
                    _prepareBaseModel<ArchivedCalculusOrder>(modelBuilder);
                    _prepareBaseModel<ArchivedCalculusServiceCost>(modelBuilder);
                #endregion

                #region configuring Calculus namespace
                    _prepareBaseModel<CalculusAppliedOffer>(modelBuilder);
                    _prepareBaseModel<CalculusBasketDetail>(modelBuilder);
                    _prepareBaseModel<CalculusFreeProduct>(modelBuilder);
                    _prepareBaseModel<CalculusOrder>(modelBuilder);
                    _prepareBaseModel<CalculusServiceCost>(modelBuilder);
                #endregion

                #region configuring Client namespace
                    _prepareBaseModel<ClientBasket>(modelBuilder);
                    _prepareBaseModel<ClientBasketDetail>(modelBuilder);
                    _prepareBaseModel<ClientFeedback>(modelBuilder);
                #endregion

                #region configuring Contact namespace
                    _prepareBaseModel<Contact>(modelBuilder);
                    _prepareBaseModel<ContactAddress>(modelBuilder);
                    _prepareBaseModel<ContactRegion>(modelBuilder);
                    _prepareBaseModel<ContactType>(modelBuilder);
                #endregion

                #region configuring General namespace
                    _prepareBaseModel<GeneralMedia>(modelBuilder);
                    _prepareBaseModel<GeneralSchedule>(modelBuilder);
                    _prepareBaseModel<GeneralStatus>(modelBuilder);
                #endregion

                #region configuring Offer namespace
                    _prepareBaseModel<OfferInfo>(modelBuilder);
                    _prepareBaseModel<OffersDetail>(modelBuilder);
                #endregion

                #region configuring Order namespace
                    _prepareBaseModel<OrderInfo>(modelBuilder);
                    _prepareBaseModel<OrderSchedule>(modelBuilder);
                    _prepareBaseModel<OrderScheduleControl>(modelBuilder);
                #endregion

                #region configuring Product namespace
                    _prepareBaseModel<ProductInfo>(modelBuilder);
                    _prepareBaseModel<ProductBrand>(modelBuilder);
                    _prepareBaseModel<ProductType>(modelBuilder);
                    _prepareBaseModel<ProductUnitOfMeasure>(modelBuilder);
                #endregion

                #region configuring Security namespace
                    _prepareBaseModel<SecurityAccount>(modelBuilder);
                    _prepareBaseModel<SecurityAccountAddress>(modelBuilder);
                    _prepareBaseModel<SecurityAccountContact>(modelBuilder);
                    _prepareBaseModel<SecurityAccountStore>(modelBuilder);
                    _prepareBaseModel<SecurityAccountUser>(modelBuilder);
                    _prepareBaseModel<SecurityAction>(modelBuilder);
                    _prepareBaseModel<SecurityDefaultProfile>(modelBuilder);
                    _prepareBaseModel<SecurityPassword>(modelBuilder);
                    _prepareBaseModel<SecurityPerson>(modelBuilder);
                    _prepareBaseModel<SecurityProfile>(modelBuilder);
                    _prepareBaseModel<SecurityProfileAction>(modelBuilder);
                    _prepareBaseModel<SecurityUser>(modelBuilder);
                #endregion

                #region configuring Service namespace
                    _prepareBaseModel<ServiceByStore>(modelBuilder);
                    _prepareBaseModel<ServiceRule>(modelBuilder);
                    _prepareBaseModel<ServiceType>(modelBuilder);
                    _prepareBaseModel<ServiceZone>(modelBuilder);
                #endregion

                #region configuring Sore namespace
                    _prepareBaseModel<StoreInfo>(modelBuilder);
                    _prepareBaseModel<StoreAddress>(modelBuilder);
                    _prepareBaseModel<StoreContact>(modelBuilder);
                    _prepareBaseModel<StoreProduct>(modelBuilder);
                    _prepareBaseModel<StoreZone>(modelBuilder);
                #endregion
            }

            // Lets now define which relations are options
            {
                // SecurityUser.LastAccountUsed should be optional
                //modelBuilder.Entity<SecurityUser>().HasOptional<SecurityAccount>(l => l.LastAccountUsed).WithOptionalDependent().Map(m => m.MapKey("IdLastAccountUsed"));
                //modelBuilder.Entity<SecurityUser>().HasOptional(l => l.LastAccountUsed); //.WithOptionalPrincipal().Map(fk => fk.MapKey("IdLastAccountUsed"));
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

        private void _prepareBaseModel<T>(DbModelBuilder modelBuilder) where T : class, IBaseModel
        {
            _ignoreNavigationPropertyKeys<T>(modelBuilder);
            //_setRequiredDependent<T>(modelBuilder);
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
        private void _ignoreNavigationPropertyKeys<T>(DbModelBuilder modelBuilder) where T : class, IBaseModel
        {
            // Using fluent Api this expression would go like this:
            // - modelBuilder.Entity<SecurityUser>().Ignore(prop => prop.IdSecurityPerson);
            // This method does the same in a dynamic way for all the properties starting with Id*

            Type thisType = typeof(T);
            foreach (PropertyInfo property in thisType.GetProperties().Where(p => p.Name.StartsWith("Id") && p.Name != "IdAddedBy" && p.Name != "IdUpdatedBy" && p.Name.Length > 2))
            {
                // Takes a reference of the Ignore method for the giving type (T) that represents a model class like SecurityUser
                var t = typeof(EntityTypeConfiguration<>);
                t = t.MakeGenericType(thisType);
                MethodInfo method = t.GetMethod("Ignore");
                MethodInfo genericMethod = method.MakeGenericMethod(property.PropertyType);

                // builds the expression 'x => x.PropertyName', required by the Ignore method
                ParameterExpression parameter = Expression.Parameter(thisType, "x");
                MemberExpression member = Expression.Property(parameter, property.Name);
                var selector = Expression.Lambda(member, new ParameterExpression[] { parameter });

                // now calls the method => modelBuilder.Entity<SecurityUser>().Ignore(prop => prop.SecurityPerson);
                genericMethod.Invoke(modelBuilder.Entity<T>(), new object[] { selector });
            }
        }

        private void _setRequiredDependent<T>(DbModelBuilder modelBuilder) where T : class, IBaseModel
        {
            // BEFORE: modelBuilder.Entity<SecurityUser>().HasRequired<SecurityAccount>(l => l.LastAccountUsed).WithRequiredDependent().Map(m => m.MapKey("LastAccountUsed_id"));
            // NOW: modelBuilder.Entity<SecurityUser>().HasRequired<SecurityAccount>(l => l.LastAccountUsed).WithMany().HasForeignKey(fk => fk.IdLastAccountUsed);

            Type tType = typeof(T);
            IEnumerable<string> allModels = ModelRegistry.GetInstance().GetAllModels().Select(m => m.Name);
            foreach (PropertyInfo property in tType.GetProperties().Where(p => allModels.Contains(p.PropertyType.Name)))
            {
                EntityTypeConfiguration<T> entity = modelBuilder.Entity<T>();

                // takes a reference of the HasRequired method for the giving type (T) that represents a model class like SecurityUser
                MethodInfo hasRequiredMethod = entity.GetType().GetMethod("HasRequired");
                MethodInfo hasRequiredGenericMethod = hasRequiredMethod.MakeGenericMethod(property.PropertyType);

                // builds the expression 'l => l.LastAccountUsed', required by the HasRequired method
                ParameterExpression parameter = Expression.Parameter(tType, "l");
                MemberExpression member = Expression.Property(parameter, property.Name);
                var selector = Expression.Lambda(member, new ParameterExpression[] { parameter });

                // now calls this method dynamically =>  modelBuilder.Entity<SecurityUser>().HasRequired<SecurityAccount>(l => l.LastAccountUsed)
                var requiredNavigationPropertyConfiguration = hasRequiredGenericMethod.Invoke(entity, new object[] { selector });

                #region Before
                // takes a reference of the method WithRequiredDependent parameters less from the RequiredNavigationPropertyConfiguration class
                /*
                MethodInfo withRequiredPrincipalMethod = requiredNavigationPropertyConfiguration.GetType().GetMethods().Where(m => m.Name == "WithRequiredDependent" && m.GetParameters().Length == 0).FirstOrDefault();
                var foreignKeyNavigationPropertyConfiguration = withRequiredPrincipalMethod.Invoke(requiredNavigationPropertyConfiguration, new object[] { });
                */

                // takes the Map method and calls it with the expression 'm => m.MapKey("LastAccountUsed_id")'
                /*
                MethodInfo mapMethod = typeof(ForeignKeyNavigationPropertyConfiguration).GetMethod("Map");
                Action<ForeignKeyAssociationMappingConfiguration> mapKeyParameter = m => m.MapKey(property.Name + "_Id");
                mapMethod.Invoke(foreignKeyNavigationPropertyConfiguration, new object[] { mapKeyParameter });
                */
                #endregion

                // Continues with the rest of the expression : .....WithMany().HasForeignKey(fk => fk.IdLastAccountUsed);

                // Call the WithMany method without parameters dynamically
                MethodInfo withManyMethod = requiredNavigationPropertyConfiguration.GetType().GetMethods().Where(m => m.Name == "WithMany" && m.GetParameters().Length == 0).FirstOrDefault();
                var dependentNavigationPropertyConfiguration = withManyMethod.Invoke(requiredNavigationPropertyConfiguration, null);

                // And now the HasForeignKey with the Expression : fk => fk.IdLastAccountUsed as parameter
                MethodInfo hasForeignKeyMethod = dependentNavigationPropertyConfiguration.GetType().GetMethod("HasForeignKey");
                hasForeignKeyMethod = hasForeignKeyMethod.MakeGenericMethod(tType.GetProperties().Where(p  => p.Name == "Id" + property.Name).FirstOrDefault().PropertyType); // normally Int32, but just in case find for the type of the idproperty

                ParameterExpression fkParameter = Expression.Parameter(tType, "fk");
                MemberExpression fkProperty = Expression.Property(fkParameter, property.Name + "_Id");
                var fkExpression = Expression.Lambda(fkProperty, new ParameterExpression[] { fkParameter });

                // Calls this method dynamically : .HasForeignKey(fk => fk.IdLastAccountUsed);
                hasForeignKeyMethod.Invoke(dependentNavigationPropertyConfiguration, new object[] { fkExpression });
                
            }
        }

        public void Attach<T>(T entity) where T : class, IBaseModel {
            this.Set<T>().Attach(entity);
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
                    bm.AddedById = CurrentAuthor.Id;
                    bm.UpdatedById = CurrentAuthor.Id;
                }

                // Updates the UpdatedOn date to all the BaseAuditableModel entities that will be updated
                foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && _baseAuditableModelType.IsAssignableFrom(e.Entity.GetType())))
                {
                    BaseAuditableModel bm = (BaseAuditableModel)entry.Entity;
                    bm.UpdatedOn = saveTime;
                    bm.UpdatedById = CurrentAuthor.Id;
                }

                return await this.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceError("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }

                throw new VirtualStoreDalException("Validation error while saving changes.", dbEx);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx) {
                foreach (DbEntityEntry e in dbEx.Entries)
                {
                    Trace.TraceError("Property of type : {0} ", e.Entity + ", is required");
                }

                //System.Data.Entity.Infrastructure.DbUpdateException
                //System.Data.DataException
                throw new VirtualStoreDalException("Update error while saving changes.", dbEx);
            }
            catch (Exception dbEx) {
                throw new VirtualStoreDalException("Unexpected error while saving changes.", dbEx);
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
        public virtual DbSet<OfferInfo> Offers { get; set; }
        public virtual DbSet<OffersDetail> OffersDetails { get; set; }
        public virtual DbSet<OrderInfo> Orders { get; set; }
        public virtual DbSet<OrderSchedule> OrdersSchedules { get; set; }
        public virtual DbSet<OrderScheduleControl> OrdersSchedulesControls { get; set; }
        public virtual DbSet<ProductInfo> Products { get; set; }
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
        public virtual DbSet<SecurityProfileType> SecurityProfileTypes { get; set; }
        public virtual DbSet<SecurityUser> SecurityUsers { get; set; }
        public virtual DbSet<ServiceByStore> ServicesByStores { get; set; }
        public virtual DbSet<ServiceRule> ServicesRules { get; set; }
        public virtual DbSet<ServiceType> ServicesTypes { get; set; }
        public virtual DbSet<ServiceZone> ServicesZones { get; set; }
        public virtual DbSet<StoreInfo> Stores { get; set; }
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

            modelBuilder.Entity<OfferInfo>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<OfferInfo>()
                .HasMany(e => e.ArchivedCalculusAppliedOffers)
                .WithRequired(e => e.OfferInfo)
                .HasForeignKey(e => e.IdOffer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OfferInfo>()
                .HasMany(e => e.CalculusAppliedOffers)
                .WithRequired(e => e.OfferInfo)
                .HasForeignKey(e => e.IdOffer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OfferInfo>()
                .HasMany(e => e.OfferDetails)
                .WithRequired(e => e.OfferInfo)
                .HasForeignKey(e => e.IdOffer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderInfo>()
                .HasMany(e => e.ArchivedCalculusOrders)
                .WithRequired(e => e.OrderInfo)
                .HasForeignKey(e => e.IdOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderInfo>()
                .HasMany(e => e.CalculusOrders)
                .WithRequired(e => e.OrderInfo)
                .HasForeignKey(e => e.IdOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderInfo>()
                .HasMany(e => e.OrdersSchedules)
                .WithRequired(e => e.OrderInfo)
                .HasForeignKey(e => e.IdOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderInfo>()
                .HasMany(e => e.Orders1)
                .WithRequired(e => e.Order1)
                .HasForeignKey(e => e.IdBasket);

            modelBuilder.Entity<OrdersSchedulesMonthDay>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ProductInfo>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductInfo>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ProductInfo>()
                .HasMany(e => e.ArchivedCalculusFreeProducts)
                .WithRequired(e => e.ProductInfo)
                .HasForeignKey(e => e.IdProducto_Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductInfo>()
                .HasMany(e => e.BasketsDetails)
                .WithRequired(e => e.ProductInfo)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductInfo>()
                .HasMany(e => e.CalculusFreeProducts)
                .WithRequired(e => e.ProductInfo)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductInfo>()
                .HasMany(e => e.OfferDetails)
                .WithRequired(e => e.ProductInfo)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductInfo>()
                .HasMany(e => e.StoreProducts)
                .WithRequired(e => e.ProductInfo)
                .HasForeignKey(e => e.IdProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductInfo>()
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

            modelBuilder.Entity<StoreInfo>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<StoreInfo>()
                .HasMany(e => e.ArchivedCalculusOrders)
                .WithRequired(e => e.StoreInfo)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoreInfo>()
                .HasMany(e => e.CalculusOrders)
                .WithRequired(e => e.StoreInfo)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoreInfo>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.StoreInfo)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoreInfo>()
                .HasMany(e => e.SecurityStoreProfiles)
                .WithRequired(e => e.StoreInfo)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoreInfo>()
                .HasMany(e => e.ServiceByStores)
                .WithRequired(e => e.StoreInfo)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoreInfo>()
                .HasMany(e => e.ServiceZones)
                .WithRequired(e => e.StoreInfo)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoreInfo>()
                .HasMany(e => e.StoreContacts)
                .WithRequired(e => e.StoreInfo)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoreInfo>()
                .HasMany(e => e.StoreAddresses)
                .WithRequired(e => e.StoreInfo)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoreInfo>()
                .HasMany(e => e.StoreProducts)
                .WithRequired(e => e.StoreInfo)
                .HasForeignKey(e => e.IdStore)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StoreInfo>()
                .HasMany(e => e.Stores1)
                .WithOptional(e => e.Store1)
                .HasForeignKey(e => e.IdStoreParent);

            modelBuilder.Entity<StoresProduct>()
                .Property(e => e.ProductCode)
                .IsUnicode(false);
        }
        */

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
