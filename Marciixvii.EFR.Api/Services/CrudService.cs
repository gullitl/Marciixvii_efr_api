using Marciixvii.EFR.App.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Services {
    public abstract class CrudService<TEntity> where TEntity : class {
        protected AppDbContext Context { get; }

        public CrudService(AppDbContext context) {
            Context = context;
        }
        public async Task<TEntity> Create(TEntity tEntity) {
            Context.Set<TEntity>().Add(tEntity);
            await Context.SaveChangesAsync();
            return tEntity;
        }

        public async Task<bool> Delete(int id) {
            TEntity tEntity = await Context.Set<TEntity>().FindAsync(id);
            if(tEntity == null) {
                return false;
            }
            Context.Set<TEntity>().Remove(tEntity);
            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<List<TEntity>> GetAll() => await Context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetById(int id) => await Context.Set<TEntity>().FindAsync(id);

        public async Task<bool> Update(TEntity tEntity) {
            Context.Entry(tEntity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return true;
        }

    }
}
