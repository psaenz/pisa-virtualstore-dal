using Pisa.VirtualStore.Dal.Core.Repositories.Archived;
using Pisa.VirtualStore.Dal.Core.Repositories.Audit;
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
using Pisa.VirtualStore.Models.Base;
using Pisa.Utils.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Pisa.VirtualStore.Dal.Core.Repositories
{
    public class RepositoryRegistry
    {
        private IDictionary<Type, Type> _repositoriesByEntity = new Dictionary<Type, Type>();

        private static readonly RepositoryRegistry _instance = new RepositoryRegistry();
        public static RepositoryRegistry GetInstance()
        {
            return _instance;
        }

        private RepositoryRegistry(){
            RegisterNamespaceClasses(typeof(ArchivedCalculusAppliedOfferRepository).Namespace);
            RegisterNamespaceClasses(typeof(AuditAuthorRepository).Namespace);
            RegisterNamespaceClasses(typeof(CalculusAppliedOfferRepository).Namespace);
            RegisterNamespaceClasses(typeof(ClientBasketRepository).Namespace);
            RegisterNamespaceClasses(typeof(ContactAddressRepository).Namespace);
            RegisterNamespaceClasses(typeof(GeneralMediaRepository).Namespace);
            RegisterNamespaceClasses(typeof(OfferRepository).Namespace);
            RegisterNamespaceClasses(typeof(OrderRepository).Namespace);
            RegisterNamespaceClasses(typeof(ProductBrandRepository).Namespace);
            RegisterNamespaceClasses(typeof(SecurityAccountAddressRepository).Namespace);
            RegisterNamespaceClasses(typeof(ServiceByStoreRepository).Namespace);
            RegisterNamespaceClasses(typeof(StoreAddressRepository).Namespace);
        }

        private void RegisterNamespaceClasses(string nameSpace)
        {
            IEnumerable<Type> types = TypeUtils.GetTypesFromNamespace(this.GetType().Assembly, nameSpace);
            foreach (Type t in types)
                RegisterRepository(t);
        }

        private void RegisterRepository(Type repositoryType)
        {
            Type modelType = repositoryType.BaseType.GenericTypeArguments[0];
            _repositoriesByEntity.Add(modelType, repositoryType);
        }

        public Type GetRepositoryFor<M>() where M : IBaseModel
        {
            Type modelType = typeof(M);
            if (_repositoriesByEntity.ContainsKey(modelType))
                return _repositoriesByEntity[modelType];
            return null;
        }
    }
}
