using ClientNotebook.Entities;
using ClientNotebook.Enum;
using ClientNotebook.Services.Models;
using ClientNotebook.Services.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Interface
{
    /// <summary>
    /// Компонент-посредник для защищенного доступа между клиентом и сервисом(разделение бизнес-логики)
    /// </summary>
    public interface INotebookServices
    {
        /// <summary>
        /// Возвращает список всех записей
        /// </summary>
        /// <param name="statusFilterNote">Фильтр</param>
        /// <returns></returns>
        List<NoteModel> GetNotes(StatusFilterNote? statusFilterNote);

        /// <summary>
        /// Запрос на удаление записи по ID
        /// </summary>
        /// <param name="id">Ключевое поле</param>
        void DelNote(int? id);

        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="noteOption">Необработанный параметр полученный напрямую от клиента</param>
        void AddNote(AddNoteOption noteOption);

        /// <summary>
        /// Получение записи по ID
        /// </summary>
        /// <param name="id">Ключевое поле</param>
        /// <returns>Возвращает копию нашей сущности Note реализованную через модель NoteModel</returns>
        NoteModel GetNoteById(int? id);

        /// <summary>
        /// Редактирование выбранной записи
        /// </summary>
        /// <param name="noteOption"> Список параметров для редактирования записи</param>
        void CorrectNote(AddNoteOption noteOption);
    }
}
