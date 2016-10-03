using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Models.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Base
{
    public interface IBaseRepository : IDisposable {
        IEnumerable<IBaseModel> GetAll();
        IBaseModel GetById(int id);
        IBaseModel Insert(IBaseModel entity);
        IBaseModel Delete(int id);
        bool Update(IBaseModel entity);
        Task<int> SaveAsync();
    }

    public interface IBaseRepository<E> : IBaseRepository where E : IBaseModel
    {
        IEnumerable<E> GetAll();
        E GetById(int id);
        E Insert(E entity);
        E Delete(int id);
        bool Update(E entity);
        Task<int> SaveAsync();
    }
}
