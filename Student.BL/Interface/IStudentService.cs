using Students.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.BL.Interface
{
    public interface IStudentService
    {
        public IEnumerable<student> GetStudents();
        public student? GetStudentByID(int id);
        public bool GetExistingID(int id);
        public bool AddStudent(student student);
        public bool PutStudent(int id, student student);
        public bool DeleteStudents();
        public bool DeleteStudentByID(int id);

    }
}
