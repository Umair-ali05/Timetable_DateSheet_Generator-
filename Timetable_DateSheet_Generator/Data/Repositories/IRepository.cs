using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetable_DateSheet_Generator.Data.Repositories
{
    interface IRepository<T,IdType>
    {
        Task<List<T>> GetAll(string TextSearch);
        Task<T> GetById(IdType ID);
        Task Insert(T Object);
        void Update(T Object);
        Task Delete(IdType ID);
        Task SaveChangesAsync();
        void SaveChanges();
        Task<bool> IsExists(IdType ID);
    }
}
