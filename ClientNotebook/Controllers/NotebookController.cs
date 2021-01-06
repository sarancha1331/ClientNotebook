using ClientNotebook.Enum;
using ClientNotebook.Interface;
using ClientNotebook.Services.Models;
using ClientNotebook.Services.Option;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Controllers
{
    public class NotebookController : Controller
    {
        /// <summary>
        /// Связь с главным сервисом
        /// </summary>
        private readonly INotebookServices notebookServices;

        /// <summary>
        /// Конструктор с доступом к интерфейсу сервиса
        /// </summary>
        public NotebookController(INotebookServices notebookServices) {
            this.notebookServices = notebookServices;
        }

        /// <summary>
        /// Отображение всех записей на странице
        /// </summary>
        public async Task<IActionResult> Index()
        {
            ViewBag.ListNotes = await notebookServices.GetNotesAsync(StatusFilterNote.All);
            return View();
        }

        /// <summary>
        /// Выборка записей через фильтр
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> SortByStatusNote(StatusFilterNote statusFilter)
        {
            ViewBag.ChoiceStatus = statusFilter.ToString();                     //<option> после фильтрации записей
            ViewBag.ListNotes = await notebookServices.GetNotesAsync(statusFilter);
            return View("Index");
        }

        /// <summary>
        /// Удаление записи по ID
        /// </summary>
        public async Task<IActionResult> DelNote(int? id) {
            await notebookServices.DelNoteAsync(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Форма добавления новой записи
        /// </summary>
        [HttpGet]
        public IActionResult AddNote()
        {
            return View();
        }

        /// <summary>
        /// Запрос на добавление новой записи
        /// </summary>
        /// <param name="noteOption">Параметр, необходимый для первичной обработки данных в сервисе(NotebookServices)</param>
        [HttpPost]
        public async Task<IActionResult> AddNote(AddNoteOption noteOption)
        {
            await notebookServices.AddNoteAsync(noteOption);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Поиск выбранной записи и отображение выбранной информации для дальнейшего редактирования
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> CorrectNote(int? id)
        {
            NoteModel note = await notebookServices.GetNoteByIdAsync(id);
            if (note != null)
            {
                ViewBag.InfoNote = note;
                return View();
            }
            else {
                return RedirectToAction("Index");                       //Заглушка
            }
        }

        /// <summary>
        /// Отправка запроса на редактирование данных
        /// </summary>
        /// <param name="noteOption">Параметр, необходимый для первичной обработки данных в сервисе(NotebookServices)</param>
        [HttpPost]
        public async Task<IActionResult> CorrectNote(AddNoteOption noteOption)
        {
            await notebookServices.CorrectNoteAsync(noteOption);
            return RedirectToAction("Index");
        }
    }
}