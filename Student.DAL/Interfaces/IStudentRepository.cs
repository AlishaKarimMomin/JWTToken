using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.DAL.Interfaces
{
    public interface IStudentRepository: IGenericRepository<student>
    {
        /*public IEnumerable<student> GetStudents();
        public student? GetStudentByID(int id);
        public string AddStudentPost(student student);
        public string PutStudent(int id, student student);
        public int DeleteStudents();
        public int DeleteStudentByID(int id);*/
    }
}
