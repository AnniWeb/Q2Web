using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Model;

namespace DataLayer.Abstractions.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseModel<int>
    {
        Task Add(TEntity entity);
 
        Task<IEnumerable<TEntity>> Get();
        
        Task<TEntity> GetById(int id);
 
        Task Update(TEntity entity);
 
        Task Delete(int id);
    }

}