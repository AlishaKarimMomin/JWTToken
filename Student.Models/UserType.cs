using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Models
{
    [Table("UserType")]
    public partial class UserType
    {
        [Key]
        [Column("UserTypeId")]
        public virtual int UserTypeId { get; set; }
        [Column("UserTypeName")]
        public virtual string UserTypeName { get; set; }
        /*public virtual IEnumerable<User> Users { get; set; }*/

    }

}
