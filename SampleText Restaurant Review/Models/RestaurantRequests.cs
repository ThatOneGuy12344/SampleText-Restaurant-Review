using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SampleText_Restaurant_Review.Models
{
    public class RestaurantRequests
    {
        public int ID { get; set; }
        [Display(Name = "Restaurant Name"), StringLength(20, MinimumLength = 1), Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }
    }
}
