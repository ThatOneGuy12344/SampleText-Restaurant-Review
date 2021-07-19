using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SampleText_Restaurant_Review.Models
{
    public class AuditRecord
    {
        [Key]
        public int Audit_ID { get; set; }
        [Display(Name = "Audit Action")]
        public string AuditActionType { get; set; }
        [Display(Name = "Performed By")]
        public string FullName { get; set; }
        [Display(Name = "Date/Time Stamp")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStamp { get; set; }
        [Display(Name = "Movie Record ID ")]
        public Restaurant Restaurant { get; set; }
        public Review Review { get; set; }
    }

}
