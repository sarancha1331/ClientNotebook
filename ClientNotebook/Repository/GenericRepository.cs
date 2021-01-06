using ClientNotebook.App_Data;
using ClientNotebook.Entities;
using ClientNotebook.Enum;
using ClientNotebook.Interface;
using ClientNotebook.Services.Option;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Repository     //ИЗМЕНИТЬ КОМЕНТЫ
{
    /// <summary>
    /// Нижний уроверь работы с бд через контекст данных
    /// </summary>
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Контекст данных для работы с БД
        /// </summary>
        private readonly DbNotebookContext context;

        /// <summary>
        /// Инициализация контекста 
        /// </summary>
        public GenericRepository(DbNotebookContext dbNotebookContext)
        {
            context = dbNotebookContext;
        }

        /// <summary>
        /// Получение выбранной записи по ключевому полю
        /// </summary>
        public async Task<T> GetByIdAsync(int? id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Выборка всех записей
        /// </summary>
        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Получение сущности через AsQueryable
        /// </summary>
        public IQueryable<T> AsQueryable()  //?
        {
            return context.Set<T>().AsQueryable();
        }

        /// <summary>
        /// Запрос на удаление выбранной записи по ID
        /// </summary>
        public async Task DeleteAsync(int? id)
        {
            T entity = await GetByIdAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Запрос на добавление новой записи
        /// </summary>
        public async Task AddAsync(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновление БД
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }


    }

}
