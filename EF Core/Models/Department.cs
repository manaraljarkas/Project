using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    internal class Department
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string? Name { get; set; }

        public List<Student>? Students { get; set; }
        public List<Subject>? Subjects { get; set; }
    }
}
