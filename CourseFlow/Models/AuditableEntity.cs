using System;

namespace CourseFlow.Models
{
    public abstract class AuditableEntity
    {
        public int Id { get; set; } 
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set;}
        public DateTime? UpdatedDate { get; set;}
        public int IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get;set; }
    }   
}
