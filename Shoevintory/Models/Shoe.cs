using System;
using System.Security.Policy;

namespace Shoevintory.Models
{
    public class Shoe
    {
        public int Id { get; set; }
        public string Name {get; set; }
        public string ImageUrl { get; set; }
        public decimal Retail { get; set; }
      
    }
}
