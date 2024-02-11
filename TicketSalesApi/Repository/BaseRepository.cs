using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TicketSalesApi.Context;
using TicketSalesApi.Models;
using TicketSalesApi.Repository.Intefaces;

namespace TicketSalesApi.Repository
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _db;
        protected BaseRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public virtual async Task<bool> CreateAsync(T model)
        {
            await _db.Set<T>().AddAsync(model);

            return await SaveDbChanges();
        }

        public virtual async Task<bool> CreateRangeAsync(IEnumerable<T> listModel)
        {
            await _db.Set<T>().AddRangeAsync(listModel);

            return await SaveDbChanges();
        }

        public virtual async Task<bool> UpdateAsync(T model)
        {
            _db.Set<T>().Update(model);
            return await SaveDbChanges();
        }

        public virtual async Task<bool> DeleteAsync(T model)
        {
            _db.Set<T>().Remove(model);
            return await SaveDbChanges();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var obj = await _db.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
            return obj;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = _db.Set<T>().AsQueryable();

            return await query.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<bool> SaveDbChanges()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _db.Dispose();
        }



    }
}
