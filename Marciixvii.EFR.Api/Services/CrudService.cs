using Marciixvii.EFR.App.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marciixvii.EFR.App.Services {
    public abstract class CrudService<TEntity> where TEntity : class {
        protected readonly ILogger<CrudService<TEntity>> _logger;
        protected AppDbContext Context { get; }

        public CrudService(ILogger<CrudService<TEntity>> logger, AppDbContext context) {
            _logger = logger;
            Context = context;
        }
        public async Task<TEntity> Create(TEntity tEntity) {
            try {
                Context.Set<TEntity>().Add(tEntity);
                await Context.SaveChangesAsync();
                return tEntity;
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                throw ex;
            } catch(DbUpdateException ex) {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }

        }

        public async Task<bool> Delete(int id) {
            try {
                TEntity tEntity = await Context.Set<TEntity>().FindAsync(id);
                if(tEntity == null) {
                    return false;
                }
                Context.Set<TEntity>().Remove(tEntity);
                await Context.SaveChangesAsync();

                return true;
            } catch(DbUpdateException ex) {
                _logger.LogError(ex, ex.Message);
                throw ex;
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                throw ex;
            }

        }

        public async Task<List<TEntity>> GetAll() {
            try {
                return await Context.Set<TEntity>().ToListAsync();
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<TEntity> GetById(int id) { 
            try {
                return await Context.Set<TEntity>().FindAsync(id);
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<TEntity> Update(TEntity tEntity) {
            try {
                Context.Entry(tEntity).State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return tEntity;
            } catch(DbUpdateException ex) {
                _logger.LogError(ex, ex.Message);
                throw ex;
            } catch(InvalidOperationException ex) {
                _logger.LogCritical(ex, ex.Message);
                throw ex;
            }

        }

    }
}
