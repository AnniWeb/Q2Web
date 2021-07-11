using System.Collections.Generic;

namespace Database.Repository
{
    public interface IRepository<TEntity>
    {
        bool Add(TEntity entity);
 
        IEnumerable<TEntity> Get();
 
        bool Update(TEntity entity);
 
        bool Delete(int id);
    }

}