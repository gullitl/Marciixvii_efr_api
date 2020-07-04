using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Contracts {
    public interface ICrud<TEntity> {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<bool> Delete(int id);
        Task<TEntity> Create(TEntity tEntity);
        Task<TEntity> Update(TEntity tEntity);
    }
}
