using Students.Models;
using System.Data.Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Students.DAL.Interfaces;
using System.Collections;

namespace Students.DAL.Repositories
{
    public class StudentRepository : GenericRepository<student>, IStudentRepository
    {

        public IEnumerable<student> getstudents()
        {
            return GetAll();
        }

        public student getstudentbyid(int id)
        {
            return FindById(id);
        }

        public string addstudentpost(student student)
        {
            Add(student);
            return "student added successfully.";
        }

        public string putstudent(int id, student student)
        {
            student.Id = id;
            Update(student);
            return "student updated successfully.";
        }

        public int deletestudents()
        {
            DeleteAll();
            return 1; // return appropriate value indicating success or failure.
        }

        public int deletestudentbyid(int id)
        {
            var student = new student { Id = id };
            Delete(student);
            return 1; // return appropriate value indicating success or failure.
        }
        
    }
}