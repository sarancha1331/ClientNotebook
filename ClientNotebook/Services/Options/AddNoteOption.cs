using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Services.Option
{
    /// <summary>
    /// Набор параметров сущности Note, которые клиент отправляет в сервис для дальнейшей обработки.
    /// </summary>
    public class AddNoteOption  //О сущности Note не должен знать клиент на прямую.
    {
        /// <summary>
        /// Копия ключевого поля сущности Note
        /// </summary>
        public int NumberCheck { get; set; }    //При добавлении новой записи это поле не обязательное
        
        /// <summary>
        ///Строка заметки(копия строкового поля сущности Note) 
        /// </summary>
        [Required]
        public string TextNotes { get; set; }

        /// <summary>
        /// Копия поля даты сущности Note
        /// </summary>
        [Required]
        public DateTime CurrentTime { get; set; }

    }
}
