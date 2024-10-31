using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DCSS.CW1.FRONTEND._14610.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Calories { get; set; }
        public bool IsVegetarian { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}