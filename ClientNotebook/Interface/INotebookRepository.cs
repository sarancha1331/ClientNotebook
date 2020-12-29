using ClientNotebook.Entities;
using ClientNotebook.Enum;
using ClientNotebook.Services.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Interface
{

    /// <summary>
    /// Компонент-посредник для защищенного доступа между сервисом и репозиторием(нижний уровень)
    /// </summary>
    public interface INotebookRepository
    {
        /// <summary>
        /// Получение списка заметок
        /// </summary>
        /// <param name="statusFilterNote">Фильтр</param>
        /// <returns></returns>
        List<Note> GetNotes(StatusFilterNote? statusFilterNote);

        /// <summary>
        /// Удаляет запись по выбранному ID
        /// </summary>
        /// <param name="id">Ключевое поле записи</param>
        void DelNote(int? id);
        
        /// <summary>
        /// Добавление новое записи
        /// </summary>
        /// <param name="note">Новая заметка</param>
        void AddNote(Note note);
        
        /// <summary>
        /// Возвращает записи по выбранному ID
        /// </summary>
        /// <param name="id">Ключевое поле</param>
        /// <returns></returns>
        Note GetNoteById(int? id);

        /// <summary>
        ///Запрос на редактирование записи 
        /// </summary>
        /// <param name="note">Обновленная заметка</param>
        void UpdateNote(Note note);
    }
    
}
