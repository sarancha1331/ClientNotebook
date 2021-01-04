using ClientNotebook.Entities;
using ClientNotebook.Enum;
using ClientNotebook.Interface;
using ClientNotebook.Maping;
using ClientNotebook.Services.Models;
using ClientNotebook.Services.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Services
{
    /// <summary>
    /// Главный управлющий компонент выбранными репозиториями. В данном проекте... областью записей в Notebook. В данном компоненте происходит основная бизнес-логика.
    /// </summary>
    public class NotebookServices : INotebookServices
    {

        private readonly IGenericRepository<Note> genericRepository;

        /// <summary>
        /// Инициализация репозитория
        /// </summary>
        /// <param name="notebookRepository"></param>
        public NotebookServices(IGenericRepository<Note> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        /// <summary>
        /// Возвращает список всех заметок
        /// </summary>
        /// <param name="statusFilterNote">Фильтр</param>
        /// <returns></returns>
        public List<NoteModel> GetNotes(StatusFilterNote? statusFilterNote)     //Queryable read
        {
            var query = genericRepository.AsQueryable();
            query = query.OrderByDescending(l => l.Datatime);
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
            return query.ToList().Select(p => p.ToNoteModel()).ToList();              //Формируем List NoteModel из List Note с помощью мапинга
        }

        /// <summary>
        /// Получение запись по ID
        /// </summary>
        /// <param name="id">Ключевое поле</param>
        /// <returns></returns>
        public NoteModel GetNoteById(int? id)
        {
            var note = genericRepository.GetById(id);                  //Находим нужную запись
            if (note != null)
            {
                return note.ToNoteModel();                                  //Формируем NoteModel из Note с помощью мапинга
            }
            else {
                return null;                                                //Заглушка
            }
                                               
        }

        /// <summary>
        /// Запрос на удаление выбранной записи
        /// </summary>
        /// <param name="id">Ключевое поле</param>
        public void DelNote(int? id)
        {
            genericRepository.Delete(id);
        }

        /// <summary>
        /// Запрос на добавление новой записи
        /// </summary>
        /// <param name="noteOption">Параметр принятый от клиента</param>
        public void AddNote(AddNoteOption noteOption)
        {
            if (noteOption.CurrentTime.ToString() == "01.01.0001 0:00:00" || noteOption.TextNotes == null)
            {
                return;                                                     //Заглушка
            }
            genericRepository.Add(noteOption.ToNote());                //Прокидываем в репозиторий Note с помощью мапинга
        }

        /// <summary>
        /// Запрос на редактирование выбранной записи
        /// </summary>
        /// <param name="noteOption">Параметр принятный от клиента</param>
        public void CorrectNote(AddNoteOption noteOption)
        {
            if (noteOption.CurrentTime.ToString() == "01.01.0001 0:00:00" || noteOption.TextNotes == null)
            {
                return;                                                     //Заглушка
            }
            Note note = genericRepository.GetById(noteOption.NumberCheck);     //Находим нужную запись
            
            note.Check = noteOption.TextNotes;                              //Редактируем
            note.Datatime = noteOption.CurrentTime;

            genericRepository.SaveChanges();                       //Обновляем в БД
        }


    }
}
