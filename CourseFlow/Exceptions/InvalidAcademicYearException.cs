using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseFlow.Exceptions
{
    public class InvalidAcademicYearException : Exception
    {
        public InvalidAcademicYearException(string message = "Invalid Academic Year") : base(message)
        {
            
        }
    }
}
