using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    internal class StudentMark
    {
        public int? ExamId { get; set; }

        public Exam? Exam { get; set; }

        public int? StudentId { get; set; }

        public Student? Student { get; set; }

        [Range(0, 100)]
        public int? Marks { get; set; }

    }
}
