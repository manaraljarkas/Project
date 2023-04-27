using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    internal class Subject
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public int? MinimumDegree { get; set; }
        public short? Term { get; set;}
        public short? Year { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<SubjectLecture>? Lectures { get; set; }
        public List<Exam>? Exams { get; set; }

    }
}
