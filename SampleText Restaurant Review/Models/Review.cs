using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleText_Restaurant_Review.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string Reviewer { get; set; }
        public string Text { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
