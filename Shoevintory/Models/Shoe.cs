using System;

namespace Shoevintory.Models
{
    public class Shoe
    {
        public int Id { get; set; }
        public string Name {get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public DateTime DatePurchased { get; set; }
        public int SoldPrice { get; set; }
    }
}
