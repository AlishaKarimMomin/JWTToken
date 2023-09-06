using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Students.API.Attributes;
using Students.API.Filters;
using Students.API.Models.Request;
using Students.API.Models.Response;
using Students.BL.Interface;
using Students.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Utility.Security;

namespace Students.API.Controllers
{

    [Route("Students")]
    [ApiController]
    //[AuthorizationAttribute]


    public class StudentsController : ControllerBase
    {

        //instance
        private readonly IStudentService IstudentService;
        //Constructor
        public StudentsController(IStudentService studentService)
        {
            this.IstudentService = studentService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<StudentResponse> GetStudents()
        {
            
            IEnumerable<student> student = IstudentService.GetStudents();

            // We dont need the above code now since it will do the mapping itself
            //student.Adapt -> It is the source 
            // <IEnumerable<StudentResponse>>  -> It is the destination 

            return student.Adapt<IEnumerable<StudentResponse>>();
        }

        [HttpGet]
        [Route("{id}")]
        public StudentResponse? GetStudentByID([FromRoute] int id)
        {

            student student = IstudentService.GetStudentByID(id);
            StudentResponse studentResponse = new StudentResponse();

            return TypeAdapter.Adapt<student, StudentResponse>(student, studentResponse);
        }

        [Route("AddStudents")]
        [HttpPost]
        public StudentResponse AddStudent(StudentRequest studentRequest)
        {
            Students.Models.student student = new Students.Models.student();

            student.Name = studentRequest.Name;
            student.Age = studentRequest.Age;
            student.RollNumber = studentRequest.RollNumber;
            student.Class = studentRequest.Class;

            bool Check = IstudentService.AddStudent(student);
            StudentResponse response = new StudentResponse();
            if (Check == true)
            {
                response.Response = "New user has been Added";
                return response;
            }
            else
            {
                response.Response = "Not Added";
                return response;
            }
        }

        [Route("DeleteStudents")]
        [HttpDelete]
        public StudentResponse DeleteAll()
        {
            bool Check = IstudentService.DeleteStudents();
            StudentResponse response = new StudentResponse();
            if (Check == true)
            {
                response.Response = "All the users has been deleted";
                return response;
            }
            else
            {
                response.Response = "Not deleted";
                return response;
            }
        }


        [Route("DeleteStudentsByID/{id}")]
        [HttpDelete]
        public StudentResponse DeleteStudentByID([FromRoute] int id)
        {
            bool CheckByID = IstudentService.DeleteStudentByID(id);
            StudentResponse response = new StudentResponse();
            if (CheckByID == true)
            {
                response.Response = "User on ID = " + id + " has been deleted";
                return response;
            }
            else
            {
                response.Response = "Not deleted";
                return response;
            }
        }

        [HttpPut]
        [Route("Put/{id}")]
        public StudentResponse Put([FromRoute] int id, [FromBody] StudentRequest updatedStudent)
        {
            Students.Models.student student = new Students.Models.student();
            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.RollNumber = updatedStudent.RollNumber;
            student.Class = updatedStudent.Class;
            bool Check = IstudentService.PutStudent(id, student);
            StudentResponse response = new StudentResponse();
            if (Check == true)
            {
                response.Response = "User on ID = " + id + " has been updated";
                return response;
            }
            else
            {
                response.Response = "New User has been Added";
                return response;
            }

        }

        [Route("Patch/{id}")]
        [HttpPatch]
        public string PatchStudent(int id, [FromBody] StudentRequest patchStudent)
        {
            student existing_student = IstudentService.GetStudentByID(id);

            TypeAdapterConfig<StudentRequest, student>.NewConfig().IgnoreNullValues(true)
                .IgnoreIf((src, dest) => string.IsNullOrWhiteSpace(src.Name), dest => dest.Name)
                .IgnoreIf((src, dest) => string.IsNullOrWhiteSpace(src.Class), dest => dest.Class);



            student edit_student = TypeAdapter.Adapt<StudentRequest, student>(patchStudent, existing_student);

            bool Check = IstudentService.PutStudent(id, edit_student);
            if (Check == true) { return "updated"; }
            else { return "not upated"; }
        }
    }
}
