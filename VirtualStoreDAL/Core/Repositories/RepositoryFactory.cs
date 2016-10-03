using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Base;
using System;

namespace Pisa.VirtualStore.Dal.Core.Repositories
{
    class RepositoryFactory
    {
        public static IBaseRepository CreateRepositoyFor<M>(VirtualStoreDbContext context) where M : class, IBaseModel
        {
            Type repositoryType = RepositoryRegistry.GetInstance().GetRepositoryFor<M>();
            return (IBaseRepository) Activator.CreateInstance(repositoryType, context);
        }
    }
}
