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
    public class BaseEntity      
    {
        /// <summary>
        /// Ключевое поле
        /// </summary>
        [Key]
        public int Id { get; set; }     
       
    }
}
