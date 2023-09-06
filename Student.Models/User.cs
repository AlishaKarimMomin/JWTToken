using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Models
{
    [Table("Users")]
    public partial class User
    {
        [Key]
        [Column("UserId")]
        public virtual int UserId { get; set; }
        [Column("Username")]
        public virtual string Username { get; set; }
        [Column("Email")]
        public virtual string Email { get; set; }
        [Column("Password")]
        public virtual string Password { get; set; }
        [Column("Fullname")]
        public virtual string Fullname { get; set; }

        [ForeignKey("UserTypeId")]
        [Column("UserTypeId")]
        public virtual int? UserTypeId { get; set; }
    }
}
