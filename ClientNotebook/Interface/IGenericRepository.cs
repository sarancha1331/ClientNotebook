using ClientNotebook.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ClientNotebook.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        void Delete(int? id);
        List<T> GetAll();
        T GetById(int? id);
        IQueryable<T> AsQueryable();
        void SaveChanges();

    }
}