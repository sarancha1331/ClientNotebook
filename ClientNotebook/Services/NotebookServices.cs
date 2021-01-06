using ClientNotebook.Entities;
using ClientNotebook.Enum;
using ClientNotebook.Interface;
using ClientNotebook.Maping;
using ClientNotebook.Services.Models;
using ClientNotebook.Services.Option;
using Microsoft.EntityFrameworkCore;
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
        public NotebookServices(IGenericRepository<Note> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        /// <summary>
        /// Возвращает список всех заметок
        /// </summary>
        public async Task<List<NoteModel>> GetNotesAsync(StatusFilterNote? statusFilterNote)     //Queryable read
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
            return (await query.ToListAsync()).Select(p => p.ToNoteModel()).ToList();
        }

        /// <summary>
        /// Получение запись по ID
        /// </summary>
        public async Task<NoteModel> GetNoteByIdAsync(int? id)
        {
            Note note = await genericRepository.GetByIdAsync(id);                  //Находим нужную запись
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
        public async Task DelNoteAsync(int? id)
        {
            await genericRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Запрос на добавление новой записи
        /// </summary>
        public async Task AddNoteAsync(AddNoteOption noteOption)
        {
            if (noteOption.CurrentTime.ToString() == "01.01.0001 0:00:00" || noteOption.TextNotes == null)
            {
                return;                                                     //Заглушка
            }
            await genericRepository.AddAsync(noteOption.ToNote());                //Прокидываем в репозиторий Note с помощью мапинга
        }

        /// <summary>
        /// Запрос на редактирование выбранной записи
        /// </summary>
        public async Task CorrectNoteAsync(AddNoteOption noteOption)
        {
            if (noteOption.CurrentTime.ToString() == "01.01.0001 0:00:00" || noteOption.TextNotes == null)
            {
                return;                                                     //Заглушка
            }
            Note note = await genericRepository.GetByIdAsync(noteOption.NumberCheck);     //Находим нужную запись
            note.Check = noteOption.TextNotes;                              //Редактируем
            note.Datatime = noteOption.CurrentTime;

            await genericRepository.SaveChangesAsync();                       //Обновляем в БД
        }


    }
}
