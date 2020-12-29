using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientNotebook.Entities;

namespace ClientNotebook.App_Data
{
    /// <summary>
    /// Контекст данных для обращения к бд(нижний уровень)
    /// </summary>
    public class DbNotebookContext : DbContext
    {
        public DbNotebookContext(DbContextOptions<DbNotebookContext> options) : base(options) { }

        /// <summary>
        /// "Context" для работы с бд через сущность Note
        /// </summary>
        public DbSet<Note> Notes { get; set; }

    }
}
