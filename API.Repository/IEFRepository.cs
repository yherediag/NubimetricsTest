using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IEFRepository<TEntity>
    {
        public Task<IList<TEntity>> Get();

        public Task<TEntity> Get(int id);

        Task Post(TEntity data);

        Task Put(TEntity data);

        Task Delete(TEntity data);

        Task Save();
    }
}
