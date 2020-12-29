using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNotebook.Enum
{
    /// <summary>
    /// 1 - Все записи, 2 - просроченные, 3 - активные
    /// </summary>
    public enum StatusFilterNote
    {
        All = 1,        //Все записи
        Expired = 2,    //Просроченные
        Active = 3      //Активные
       
    }
}
