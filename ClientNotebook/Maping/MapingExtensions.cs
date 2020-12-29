using ClientNotebook.Entities;
using ClientNotebook.Services.Models;
using ClientNotebook.Services.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Maping
{
    public static class MapingExtensions
    {
        /// <summary>
        /// Мапинг convert Note -> NoteModel
        /// </summary>
        /// <param name="item">Входящий параметр Note</param>
        /// <returns>Возвращает сформированный NoteModel</returns>
        public static NoteModel ToNoteModel(this Note item)
        {
            bool status;
            if (item.Datatime >= DateTime.Now) status = true;
            else status = false;
            NoteModel newElement = new NoteModel
            {
                NumberCheck = item.Id,
                CurrentTime = item.Datatime,
                TextNotes = item.Check,
                StatusCheck = status
            };
            return newElement;
        }

        /// <summary>
        /// Мапинг convert AddNoteOption -> Note
        /// </summary>
        /// <param name="item">Входящий параметр AddNoteOption</param>
        /// <returns>Возвращает сформированный Note</returns>
        public static Note ToNote(this AddNoteOption item)
        {
            Note note;
            if (item.NumberCheck != 0)  {   //Для редактирования заметки мы прокидываем Id
                note = new Note
                {
                    Id = item.NumberCheck,
                    Check = item.TextNotes,
                    Datatime = item.CurrentTime
                };
            }
            else {                      //Для добавления новой заметки присвоение Id не требуется
                note = new Note
                {
                    Check = item.TextNotes,
                    Datatime = item.CurrentTime
                };
            }
            return note;
        }


    }
}
