using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Services.Models
{
    /// <summary>
    /// Копия нашей сущности Note, которую мы будем возвращать в полном либо частичном обьеме при обращении к сущности Note
    /// </summary>
    public class NoteModel  //О сущности Note не должен знать клиент на прямую.
    {
        /// <summary>
        /// Копия ключевого поля ID сущности Note
        /// </summary>
        public int NumberCheck { get; set; }

        /// <summary>
        /// Копия строки заметки сущности Note
        /// </summary>
        public string TextNotes { get; set; }

        /// <summary>
        /// Копия поля даты сущности Note
        /// </summary>
        public DateTime CurrentTime { get; set; }

        /// <summary>
        /// Статус заметки
        /// </summary>
        public bool StatusCheck { get; set; }

    }
}
