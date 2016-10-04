using Pisa.VirtualStore.Dal.Core.Repositories;
using Pisa.VirtualStore.Dal.Core.Repositories.Archived;
using Pisa.VirtualStore.Dal.Core.Repositories.Audit;
using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Dal.Core.Repositories.Calculus;
using Pisa.VirtualStore.Dal.Core.Repositories.Client;
using Pisa.VirtualStore.Dal.Core.Repositories.Contact;
using Pisa.VirtualStore.Dal.Core.Repositories.General;
using Pisa.VirtualStore.Dal.Core.Repositories.Offer;
using Pisa.VirtualStore.Dal.Core.Repositories.Order;
using Pisa.VirtualStore.Dal.Core.Repositories.Product;
using Pisa.VirtualStore.Dal.Core.Repositories.Security;
using Pisa.VirtualStore.Dal.Core.Repositories.Service;
using Pisa.VirtualStore.Dal.Core.Repositories.Store;
using Pisa.VirtualStore.Models.Archived;
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

using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pisa.VirtualStore.Dal.Core
{
    public class VirtualStoreUnitOfWork : IDisposable
    {
        IDictionary<Type, IBaseRepository> _repositories = new Dictionary<Type, IBaseRepository>();

        public VirtualStoreDbContext Context { get; private set; }

        #region Constructors...
        public VirtualStoreUnitOfWork(AuditAuthor author) {
            Context = new VirtualStoreDbContext(author);
        }
        private VirtualStoreUnitOfWork(VirtualStoreDbContext context)
        {
            Context = context;
        }

        public static VirtualStoreUnitOfWork GetSystemVirtualStoreUnitOfWork()
        {
            return new VirtualStoreUnitOfWork(VirtualStoreDbContext.GetSystemVirtualStoreDbContext());
        }

        public IBaseRepository GetRepositoryFor<M>(M model)
            where M : class, IBaseModel
        {
            MethodInfo getGenericRepositoryForMethod = this.GetType().GetMethods().Where(m => m.Name == "GetRepositoryFor" && m.GetParameters().Count() == 0).First().MakeGenericMethod(model.GetType());
            return (IBaseRepository)getGenericRepositoryForMethod.Invoke(this, null);
        }

        public IBaseRepository<M> GetRepositoryFor<M>() 
            where M : class, IBaseModel {

            Type modelType = typeof(M);
            if (!_repositories.ContainsKey(modelType))
            {
                var repositoryInstance = RepositoryFactory.CreateRepositoyFor<M>(Context);
                _repositories.Add(modelType, repositoryInstance);
            }
        
            return (IBaseRepository<M>) _repositories[modelType];
        }
        #endregion Constructors

        #region Repositories...
        #region Archived Repositories
        public ArchivedCalculusAppliedOfferRepository ArchivedCalculusAppliedOfferRepository
        {
            get{return (ArchivedCalculusAppliedOfferRepository) GetRepositoryFor<ArchivedCalculusAppliedOffer>();}
        }
        public ArchivedCalculusBasketDetailRepository ArchivedCalculusBasketDetailRepository
        {
            get { return (ArchivedCalculusBasketDetailRepository) GetRepositoryFor<ArchivedCalculusBasketDetail>(); }
        }
        public ArchivedCalculusFreeProductRepository ArchivedCalculusFreeProductRepository
        {
            get { return (ArchivedCalculusFreeProductRepository) GetRepositoryFor<ArchivedCalculusFreeProduct>(); }
        }
        public ArchivedCalculusOrderRepository ArchivedCalculusOrderRepository
        {
            get { return (ArchivedCalculusOrderRepository) GetRepositoryFor<ArchivedCalculusOrder>(); }
        }
        public ArchivedCalculusServiceCostRepository ArchivedCalculusServiceCostRepository
        {
            get { return (ArchivedCalculusServiceCostRepository) GetRepositoryFor<ArchivedCalculusServiceCost>(); }
        }
        #endregion

        #region Audit Repositories
        public AuditAuthorRepository AuditAuthorRepository
        {
            get { return (AuditAuthorRepository) GetRepositoryFor<AuditAuthor>(); }
        }
        #endregion

        #region Calculus Repositories
        public CalculusAppliedOfferRepository CalculusAppliedOfferRepository
        {
            get { return (CalculusAppliedOfferRepository) GetRepositoryFor<CalculusAppliedOffer>(); }
        }
        public CalculusBasketDetailRepository CalculusBasketDetailRepository
        {
            get { return (CalculusBasketDetailRepository) GetRepositoryFor<CalculusBasketDetail>(); }
        }
        public CalculusFreeProductRepository CalculusFreeProductRepository
        {
            get { return (CalculusFreeProductRepository) GetRepositoryFor<CalculusFreeProduct>(); }
        }
        public CalculusOrderRepository CalculusOrderRepository
        {
            get { return (CalculusOrderRepository) GetRepositoryFor<CalculusOrder>(); }
        }
        public CalculusServiceCostRepository CalculusServiceCostRepository
        {
            get { return (CalculusServiceCostRepository) GetRepositoryFor<CalculusServiceCost>(); }
        }
        #endregion

        #region Client Repositories
        public ClientBasketRepository ClientBasketRepository
        {
            get { return (ClientBasketRepository) GetRepositoryFor<ClientBasket>(); }
        }
        public ClientBasketDetailRepository ClientBasketDetailRepository
        {
            get { return (ClientBasketDetailRepository) GetRepositoryFor<ClientBasketDetail>(); }
        }
        public ClientFeedbackRepository ClientFeedbackRepository
        {
            get { return (ClientFeedbackRepository) GetRepositoryFor<ClientFeedback>(); }
        }
        #endregion

        #region Contact Repositories
        public ContactRepository ContactRepository
        {
            get { return (ContactRepository) GetRepositoryFor<Contact>(); }
        }
        public ContactAddressRepository ContactAddressRepository
        {
            get { return (ContactAddressRepository) GetRepositoryFor<ContactAddress>(); }
        }
        public ContactRegionRepository ContactRegionRepository
        {
            get { return (ContactRegionRepository) GetRepositoryFor<ContactRegion>(); }
        }
        public ContactTypeRepository ContactTypeRepository
        {
            get { return (ContactTypeRepository)GetRepositoryFor<ContactType>(); }
        }
        #endregion

        #region General Repositories
        public GeneralMediaRepository GeneralMediaRepository
        {
            get { return (GeneralMediaRepository) GetRepositoryFor<GeneralMedia>(); }
        }
        public GeneralScheduleRepository GeneralScheduleRepository
        {
            get { return (GeneralScheduleRepository) GetRepositoryFor<GeneralSchedule>(); }
        }
        public GeneralStatusRepository GeneralStatusRepository
        {
            get { return (GeneralStatusRepository) GetRepositoryFor<GeneralStatus>(); }
        }
        #endregion

        #region Offer Repositories
        public OfferRepository OfferRepository
        {
            get { return (OfferRepository) GetRepositoryFor<OfferInfo>(); }
        }
        public OffersDetailRepository OffersDetailRepository
        {
            get { return (OffersDetailRepository) GetRepositoryFor<OffersDetail>(); }
        }
        #endregion

        #region Order Repositories
        public OrderRepository OrderRepository
        {
            get { return (OrderRepository) GetRepositoryFor<OrderInfo>(); }
        }
        public OrderScheduleRepository OrderScheduleRepository
        {
            get { return (OrderScheduleRepository) GetRepositoryFor<OrderSchedule>(); }
        }
        public OrderScheduleControlRepository OrderScheduleControlRepository
        {
            get { return (OrderScheduleControlRepository) GetRepositoryFor<OrderScheduleControl>(); }
        }
        #endregion

        #region Product Repositories
        public ProductRepository ProductRepository
        {
            get { return (ProductRepository) GetRepositoryFor<ProductInfo>(); }
        }
        public ProductBrandRepository ProductBrandRepository
        {
            get { return (ProductBrandRepository) GetRepositoryFor<ProductBrand>(); }
        }
        public ProductTypeRepository ProductTypeRepository
        {
            get { return (ProductTypeRepository) GetRepositoryFor<ProductType>(); }
        }
        public ProductUnitOfMeasureRepository ProductUnitOfMeasureRepository
        {
            get { return (ProductUnitOfMeasureRepository) GetRepositoryFor<ProductUnitOfMeasure>(); }
        }
        #endregion

        #region Security Repositories
        public SecurityAccountRepository SecurityAccountRepository
        {
            get { return (SecurityAccountRepository) GetRepositoryFor<SecurityAccount>(); }
        }
        public SecurityAccountAddressRepository SecurityAccountAddressRepository
        {
            get { return (SecurityAccountAddressRepository) GetRepositoryFor<SecurityAccountAddress>(); }
        }
        public SecurityAccountContactRepository SecurityAccountContactRepository
        {
            get { return (SecurityAccountContactRepository) GetRepositoryFor<SecurityAccountContact>(); }
        }
        public SecurityAccountStoreRepository SecurityAccountStoreRepository
        {
            get { return (SecurityAccountStoreRepository) GetRepositoryFor<SecurityAccountStore>(); }
        }
        public SecurityAccountUserRepository SecurityAccountUserRepository
        {
            get { return (SecurityAccountUserRepository) GetRepositoryFor<SecurityAccountUser>(); }
        }
        public SecurityActionRepository SecurityActionRepository
        {
            get { return (SecurityActionRepository) GetRepositoryFor<SecurityAction>(); }
        }
        public SecurityDefaultProfileRepository SecurityDefaultProfileRepository
        {
            get { return (SecurityDefaultProfileRepository) GetRepositoryFor<SecurityDefaultProfile>(); }
        }
        public SecurityPasswordRepository SecurityPasswordRepository
        {
            get { return (SecurityPasswordRepository) GetRepositoryFor<SecurityPassword>(); }
        }
        public SecurityPersonRepository SecurityPersonRepository
        {
            get { return (SecurityPersonRepository) GetRepositoryFor<SecurityPerson>(); }
        }
        public SecurityProfileRepository SecurityProfileRepository
        {
            get { return (SecurityProfileRepository) GetRepositoryFor<SecurityProfile>(); }
        }
        public SecurityProfileActionRepository SecurityProfileActionRepository
        {
            get { return (SecurityProfileActionRepository) GetRepositoryFor<SecurityProfileAction>(); }
        }
        public SecurityProfileTypeRepository SecurityProfileTypeRepository
        {
            get { return (SecurityProfileTypeRepository) GetRepositoryFor<SecurityProfileType>(); }
        }
        public SecurityUserRepository SecurityUserRepository
        {
            get { return (SecurityUserRepository) GetRepositoryFor<SecurityUser>(); }
        }
        #endregion

        #region Service Repositories
        public ServiceByStoreRepository ServiceByStoreRepository
        {
            get { return (ServiceByStoreRepository) GetRepositoryFor<ServiceByStore>(); }
        }
        public ServiceRuleRepository ServiceRuleRepository
        {
            get { return (ServiceRuleRepository) GetRepositoryFor<ServiceRule>(); }
        }
        public ServiceTypeRepository ServiceTypeRepository
        {
            get { return (ServiceTypeRepository) GetRepositoryFor<ServiceType>(); }
        }
        public ServiceZoneRepository ServiceZoneRepository
        {
            get { return (ServiceZoneRepository) GetRepositoryFor<ServiceZone>(); }
        }
        #endregion

        #region Store Repositories
        public StoreRepository StoreRepository
        {
            get { return (StoreRepository) GetRepositoryFor<StoreInfo>(); }
        }
        public StoreAddressRepository StoreAddressRepository
        {
            get { return (StoreAddressRepository) GetRepositoryFor<StoreAddress>(); }
        }
        public StoreContactRepository StoreContactRepository
        {
            get { return (StoreContactRepository) GetRepositoryFor<StoreContact>(); }
        }
        public StoreProductRepository StoreProductRepository
        {
            get { return (StoreProductRepository) GetRepositoryFor<StoreProduct>(); }
        }
        public StoreZoneRepository StoreZoneRepository
        {
            get { return (StoreZoneRepository) GetRepositoryFor<StoreZone>(); }
        }
        #endregion

        #endregion

        public async Task<int> TrySaveChangesAsync()
        {
            return await Context.TrySaveChangesAsync();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositories.Clear();
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
