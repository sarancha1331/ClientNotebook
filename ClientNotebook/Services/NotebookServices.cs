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
        /// <summary>
        /// Репозиторий Notebook
        /// </summary>
        private readonly INotebookRepository notebookRepository;

        /// <summary>
        /// Инициализация репозитория
        /// </summary>
        /// <param name="notebookRepository"></param>
        public NotebookServices(INotebookRepository notebookRepository)
        {
            this.notebookRepository = notebookRepository;
        }

        /// <summary>
        /// Возвращает список всех заметок
        /// </summary>
        /// <param name="statusFilterNote">Фильтр</param>
        /// <returns></returns>
        public List<NoteModel> GetNotes(StatusFilterNote? statusFilterNote)
        {
            var note = notebookRepository.GetNotes(statusFilterNote);       //Получаем список записей с репозитория          
            return note.Select(p => p.ToNoteModel()).ToList();              //Формируем List NoteModel из List Note с помощью мапинга
        }

        /// <summary>
        /// Получение запись по ID
        /// </summary>
        /// <param name="id">Ключевое поле</param>
        /// <returns></returns>
        public NoteModel GetNoteById(int? id)
        {
            var note = notebookRepository.GetNoteById(id);                  //Находим нужную запись
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
            notebookRepository.DelNote(id);
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
            notebookRepository.AddNote(noteOption.ToNote());                //Прокидываем в репозиторий Note с помощью мапинга
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
            Note note = notebookRepository.GetNoteById(noteOption.NumberCheck);     //Находим нужную запись
            
            note.Check = noteOption.TextNotes;                              //Редактируем
            note.Datatime = noteOption.CurrentTime;

            notebookRepository.UpdateNote(note);                            //Обновляем в БД
        }


    }
}
