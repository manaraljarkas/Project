using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    internal class Exam
    {
        public int? Id { get; set; }
        public DateTime? Date { get; set; }
        public short? Term { get; set; }

        public int? SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public ICollection<Student>? Students { get; set; }
        public List<StudentMark>? StudentMarks { get; set; }
    }
}
