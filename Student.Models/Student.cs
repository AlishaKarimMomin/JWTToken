using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Students.Models
{

    [Table("students")]
    public partial class student
    {
        [Key]
        [Column("Id")]
        public virtual int Id { get; set; }
        [Column("Name")]
        public virtual string Name { get; set; }
        [Column("Age")]
        public virtual int? Age { get; set; }
        [Column("Class")]
        public virtual string Class { get; set; }
        [Column("RollNumber")]
        public virtual int? RollNumber { get; set; }
        /*public virtual IEnumerable<Teacher> Teachers { get; set; }*/
    }


}