using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Models
{
    [Table("Teachers")]
    public partial class Teacher
    {
        [Key]
        [Column("TeacherID")]
        public virtual int TeacherID { get; set; }
        [Column("TeacherName")]
        public virtual string TeacherName { get; set; }
        [Column("TeacherAge")]
        public virtual int? TeacherAge { get; set; }

        [ForeignKey("StudentID")]
        [Column("StudentID")]
        public virtual int? StudentID { get; set; }

       
    }
    
}
