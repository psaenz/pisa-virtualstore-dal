namespace Pisa.VirtualStore.Dal.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using System.Data.Entity.SqlServer;
    using System.Data.Entity.Infrastructure.Interception;
    using Pisa.VirtualStore.Dal.Core.DbInterceptors;

    class VirtualStoreDbConfiguration : DbConfiguration
    {
        public VirtualStoreDbConfiguration()
        {
            // transient errors will be automatically re-tried, if fails several times the actual exception returned will be wrapped in the RetryLimitExceededException exception.
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());

            // Initializer could also be defined in the app.config file
            //Database.SetInitializer<VirtualStoreDbContext>(new DropCreateDatabaseIfModelChanges<VirtualStoreDbContext>());
            //Database.SetInitializer<VirtualStoreDbContext>(new DropCreateDatabaseAlways<VirtualStoreDbContext>());
            //Database.SetInitializer<VirtualStoreDbContext>(new VirtualStoreDbInitializer());
            this.SetDatabaseInitializer(new VirtualStoreDbInitializer());
            this.SetContextFactory<VirtualStoreDbContext>(() => VirtualStoreDbContext.GetSystemVirtualStoreDbContext());

            this.AddInterceptor(new DbCommandInterceptorLogging());
            this.AddInterceptor(new DbCommandInterceptorTrasientErrors());
        }
    }
}
