using System;

namespace Shoevintory.Models
{
    public class UserShoeViewModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Retail { get; set; }
        public int ShoeId { get; set; }
        public int CollectionId { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
