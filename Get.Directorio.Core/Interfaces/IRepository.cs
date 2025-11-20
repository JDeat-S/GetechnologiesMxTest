using System;
using System.Collections.Generic;
using System.Text;

namespace Get.Directorio.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //Se definen interfaces genericos y específicos.
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
