using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        public IEnumerable<T> GetAll();
        T FindById(int Id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void DeleteAll();
        public User FindByUsername(string username);

    }

}
