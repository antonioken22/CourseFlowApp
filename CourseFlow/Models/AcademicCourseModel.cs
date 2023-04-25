using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseFlow.Models
{
    public class AcademicCourseModel : AuditableEntity
    {
        public string Name { get; set; }
        public AcademicYearModel AcademicYear { get; set; }

    }
}
