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
        /// <param name="notebookServices"></param>
        public NotebookController(INotebookServices notebookServices) {
            this.notebookServices = notebookServices;
        }

        /// <summary>
        /// Отображение всех записей на странице
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.ListNotes = notebookServices.GetNotes(StatusFilterNote.All);
            return View();
        }

        /// <summary>
        /// Выборка записей через фильтр
        /// </summary>
        /// <param name="StatusFilter">фильтр</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SortByStatusNote(StatusFilterNote statusFilter)
        {
            ViewBag.ChoiceStatus = statusFilter.ToString();                     //<option> после фильтрации записей
            ViewBag.ListNotes = notebookServices.GetNotes(statusFilter);
            return View("Index");
        }

        /// <summary>
        /// Удаление записи по ID
        /// </summary>
        /// <param name="id">Ключевое поле каждой записи</param>
        /// <returns></returns>
        public IActionResult DelNote(int? id) {
            notebookServices.DelNote(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Форма добавления новой записи
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddNote()
        {
            return View();
        }

        /// <summary>
        /// Запрос на добавление новой записи
        /// </summary>
        /// <param name="noteOption">Параметр, необходимый для первичной обработки данных в сервисе(NotebookServices)</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddNote(AddNoteOption noteOption)
        {
            notebookServices.AddNote(noteOption);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Поиск выбранной записи и отображение выбранной информации для дальнейшего редактирования
        /// </summary>
        /// <param name="id">Ключевое поле записи</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CorrectNote(int? id)
        {
            NoteModel note = notebookServices.GetNoteById(id);
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
        /// <returns></returns>
        [HttpPost]
        public IActionResult CorrectNote(AddNoteOption noteOption)
        {
            notebookServices.CorrectNote(noteOption);
            return RedirectToAction("Index");
        }
    }
}