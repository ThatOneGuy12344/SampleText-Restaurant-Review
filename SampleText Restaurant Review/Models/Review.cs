using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SampleText_Restaurant_Review.Models
{
    public class Review
    {
        public int ID { get; set; }
        [Range(1,5),Required]
        public int Rating { get; set; }
        public string Reviewer { get; set; }
        [StringLength(500, MinimumLength = 1),Required]
        public string Text { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReviewTime { get; set; }
        public virtual Restaurant Restaurant { get; set; }

    }
}
