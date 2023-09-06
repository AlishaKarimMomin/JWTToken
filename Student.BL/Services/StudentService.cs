using Students.BL.Interface;
using Students.DAL;
using Students.DAL.Interfaces;
using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.BL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository IstudentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            IstudentRepository = studentRepository;
        }

        public IEnumerable<student> GetStudents()
        {
            return IstudentRepository.GetAll();

        }
        public student? GetStudentByID(int id)
        {
            return IstudentRepository.FindById(id);
        }

        public bool GetExistingID(int id)
        {
            student students = GetStudentByID(id);
            if (students != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddStudent(student student)
        {

            IstudentRepository.Add(student);
            return true;

        }

        public bool PutStudent(int id, student student)
        {
            student students = GetStudentByID(id);
            if (students != null)
            {
                student.Id = id;
                IstudentRepository.Update(student);
                return true;
            }
            else
            {
                AddStudent(student);
                return false;
            }

        }

        public bool DeleteStudents()
        {
            IstudentRepository.DeleteAll();
            return true;
        }

        public bool DeleteStudentByID(int id)
        {
            var student = IstudentRepository.FindById(id);
            if (student != null)
            {
                IstudentRepository.Delete(student);
                return true;
            }
            return false;
        }


    }
}
