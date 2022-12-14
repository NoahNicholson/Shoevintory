using System;

namespace Shoevintory.Models
{
    public class ShoeCollection
    {
        public int Id { get; set; }
        public int ShoeId { get; set; }
        public int CollectionId { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
