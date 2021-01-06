using ClientNotebook.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Шаблонная функция добавления новых записей
        /// </summary>
        /// <param name="entity">Приниемая сущность</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Шаблонная функция удаления записи(шаблон для сущности)
        /// </summary>
        Task DeleteAsync(int? id);

        /// <summary>
        /// Получение всех полей шаблонной сущности
        /// </summary>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Получение шаблонной сущности по ID
        /// </summary>
        Task<T> GetByIdAsync(int? id);

        /// <summary>
        /// Получение сущности с помощью AsQueryable
        /// </summary>
        IQueryable<T> AsQueryable();

        /// <summary>
        /// Запрос на Update
        /// </summary>
        Task SaveChangesAsync();

    }
}