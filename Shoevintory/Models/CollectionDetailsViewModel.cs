using System.Collections.Generic;

namespace Shoevintory.Models
{
    public class CollectionDetailsViewModel
    {
        public int CollectionId { get; set; }
        public string CollectionName { get; set; }  
        public List <UserShoeViewModel> Shoes { get; set; } 
    }
}
