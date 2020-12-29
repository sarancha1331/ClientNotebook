using ClientNotebook.App_Data;
using ClientNotebook.Entities;
using ClientNotebook.Enum;
using ClientNotebook.Interface;
using ClientNotebook.Services.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Repository
{

    /// <summary>
    /// Нижний уроверь работы с бд через контекст данных
    /// </summary>
    public class NoteRepository : INotebookRepository
    {
        /// <summary>
        /// Контекст данных для работы с БД
        /// </summary>
        private readonly DbNotebookContext dbNotebookContext;

        /// <summary>
        /// Инициализация контекста 
        /// </summary>
        /// <param name="dbNotebookContext"></param>
        public NoteRepository(DbNotebookContext dbNotebookContext)
        {
            this.dbNotebookContext = dbNotebookContext;
        }

        /// <summary>
        /// Выборка всех записей(нижний уровень)
        /// </summary>
        /// <param name="statusFilterNote">Фильтр</param>
        /// <returns></returns>
        public List<Note> GetNotes(StatusFilterNote? statusFilterNote)
        {
            var query = dbNotebookContext.Notes.AsQueryable();          
            if (statusFilterNote != null)
            {
                switch (statusFilterNote.Value)
                {
                    case StatusFilterNote.Expired:
                        query = query.Where(l => l.Datatime < DateTime.Now);   //Фильтр просроченых записей
                        break;
                    case StatusFilterNote.Active:
                        query = query.Where(l => l.Datatime >= DateTime.Now);   //Фильтр активных записей
                        break;
                    default:
                        break;
                }
            }
            return query.ToList(); ;
        }

        /// <summary>
        /// Получение выбранной записи по ключевому полю
        /// </summary>
        /// <param name="id">Ключевое поле</param>
        /// <returns></returns>
        public Note GetNoteById(int? id) {
            return dbNotebookContext.Notes.Find(id);
        }

        /// <summary>
        /// Запрос на удаление выбранной записи по ID
        /// </summary>
        /// <param name="id">Ключевое поле</param>
        public void DelNote(int? id)
        {
            Note note = dbNotebookContext.Notes.Find(id);
            if (note != null)
            {
                dbNotebookContext.Notes.Remove(note);
                dbNotebookContext.SaveChanges();
            }
        }

        /// <summary>
        /// Запрос на добавление новой записи
        /// </summary>
        /// <param name="note"></param>
        public void AddNote(Note note) {
            dbNotebookContext.Add(note);
            dbNotebookContext.SaveChanges();
        }

        /// <summary>
        /// Запрос на обновление записи
        /// </summary>
        /// <param name="note">Обновленная запись редактируемая пользователем</param>
        public void UpdateNote(Note note)
        {
            dbNotebookContext.Notes.Update(note);
            dbNotebookContext.SaveChanges();
        }

    }
}
