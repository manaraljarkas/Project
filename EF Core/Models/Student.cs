using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    //[Table("Students",Schema = "dbo")]
    internal class Student
    {
        //[Key]
        public int? Id { get; set; }

        //[Required, Comment("You Can Login by Using Username")]

        //[Column(TypeName = "VARCHAR(255)"), MaxLength(255)]
        public string? Username { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        //[Column("Phone Number")]
        public string? Phone { get; set; }
        
        //[NotMapped]
        public DateTime? RegisterDate { get; set; }

        public int? DepartmentId { get; set; }

        public Department? Department { get; set; }

        public ICollection<Exam>? Exams { get; set; }

        public List<StudentMark>? StudentMarks { get; set; }

    }
}
