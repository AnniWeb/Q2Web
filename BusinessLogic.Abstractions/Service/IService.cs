using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Abstractions.Model;

namespace BusinessLogic.Abstractions.Service
{
    public interface IService <TEntity, TIdType> 
        where TEntity: BaseModel<TIdType>
        where TIdType: struct
    {
        Task<TIdType> Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TIdType id);
        Task<TEntity> GetById(TIdType id);
        Task<IReadOnlyCollection<TEntity>> GetList(int offset, int limit);
        Task<IReadOnlyCollection<TEntity>> SearchByTerm(string term);
    }
}