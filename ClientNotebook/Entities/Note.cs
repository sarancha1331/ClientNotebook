using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Entities
{
    /// <summary>
    /// Сущность таблицы всех заметок
    /// </summary>
    public class Note : BaseEntity     
    {       
        /// <summary>
        ///Строка записи редактируемая пользователем 
        /// </summary>
        [Required]
        public string Check { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [Required]
        public DateTime Datatime { get; set; }
    }
}
