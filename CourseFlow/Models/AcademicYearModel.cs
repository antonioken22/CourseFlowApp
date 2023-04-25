using CourseFlow.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace CourseFlow.Models
{
    public class AcademicYearModel : AuditableEntity
    {

        private int startYear;
        public int StartYear
        {
            get => startYear;
            set
            {
                if(value < 2010) throw new InvalidAcademicYearException();
                if (value > 2099) throw new InvalidAcademicYearException();


                startYear = value;
            }
        }

        public int EndYear { get; set; }
        public string DescriptiveName
        {
            get => $"A.Y. {StartYear}-{EndYear}";
        }

        public override string ToString()
        {
            return DescriptiveName;
        }
    }
}
