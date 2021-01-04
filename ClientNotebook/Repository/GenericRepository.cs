using ClientNotebook.App_Data;
using ClientNotebook.Entities;
using ClientNotebook.Enum;
using ClientNotebook.Interface;
using ClientNotebook.Services.Option;
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
        /// <param name="dbNotebookContext"></param>
        public GenericRepository(DbNotebookContext dbNotebookContext)
        {
            this.context = dbNotebookContext;
        }

        /// <summary>
        /// Получение выбранной записи по ключевому полю
        /// </summary>
        /// <param name="id">Ключевое поле</param>
        /// <returns></returns>
        public T GetById(int? id)
        {
            //return dbNotebookContext.T.Find(id);
            return context.Set<T>().Find(id);
        }

        /// <summary>
        /// Выборка всех записей(нижний уровень)
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public IQueryable<T> AsQueryable()
        {
            return context.Set<T>().AsQueryable();
        }


        /// <summary>
        /// Запрос на удаление выбранной записи по ID
        /// </summary>
        /// <param name="id">Ключевое поле</param>
        public void Delete(int? id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Запрос на добавление новой записи
        /// </summary>
        /// <param name="note"></param>
        public void Add(T entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }


    }

}
