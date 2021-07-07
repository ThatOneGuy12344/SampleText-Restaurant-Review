using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SampleText_Restaurant_Review.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        [Display(Name ="Restaurant Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }
    }
}
