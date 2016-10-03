using Pisa.VirtualStore.Models.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Base
{
    public class BaseRepository<E> : IBaseRepository<E> where E : class, IBaseModel
    {
        bool _disposedValue = false; // To detect redundant calls
        IDbSet<E> _entityCollection;

        protected VirtualStoreDbContext Context { get; private set; }

        public BaseRepository(VirtualStoreDbContext context)
        {
            this.Context = context;
            this._entityCollection = Context.Set<E>();
        }

        #region IBaseRepository<E>
        public E Delete(int id)
        {
            E entity = _entityCollection.Find(id);
            if (entity != null)
                return _entityCollection.Remove(entity);

            return null;
        }

        public IEnumerable<E> GetAll()
        {
            return _entityCollection.ToList();
        }

        public E GetById(int id)
        {
            return _entityCollection.Find(id);
        }

        public E Insert(E entity)
        {
            return _entityCollection.Add(entity);
        }

        public async Task<int> SaveAsync()
        {
            return await Context.TrySaveChangesAsync();
        }

        public bool Update(E entity)
        {
            DbEntityEntry<E> dbEntry = Context.Entry(entity);
            if (dbEntry != null)
            {
                dbEntry.State = EntityState.Modified;
                return true;
            }

            return false;
        }
        #endregion

        #region IBaseRepository
        IEnumerable<IBaseModel> IBaseRepository.GetAll()
        {
            return this.GetAll();
        }

        IBaseModel IBaseRepository.GetById(int id)
        {
            return this.GetById(id);
        }

        public IBaseModel Insert(IBaseModel entity)
        {
            return Insert((E)entity);
        }

        IBaseModel IBaseRepository.Delete(int id)
        {
            return Delete(id);
        }

        public bool Update(IBaseModel entity)
        {
            return Update((E)entity);
        }
        #endregion

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    this._entityCollection = null;
                    // Context must be disposed by who created it (in a unit of work for instance)
                    //if (Context != null)
                    //    Context.Dispose();
                }
                _disposedValue = true;
            }
        }

        ~BaseRepository()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
