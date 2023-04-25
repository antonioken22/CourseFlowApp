using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseFlow.Models
{
    public class SubjectModel : AuditableEntity
    {
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }

    }
}
