namespace TicketSalesApi.Repository.Intefaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<bool> CreateAsync(T model);
        Task<bool> CreateRangeAsync(IEnumerable<T> listModel);
        Task<bool> UpdateAsync(T model);
        Task<bool> DeleteAsync(T model);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
