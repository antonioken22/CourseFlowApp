using System;

namespace CourseFlow.Exceptions
{
    public class InvalidAcademicYearException : Exception
    {
        public InvalidAcademicYearException(string message = "Invalid Academic Year") : base(message)
        {
            
        }
    }
}
