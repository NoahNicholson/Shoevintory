﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Shoevintory.Models
{
    public class AddShoeToCollectionViewModel
    {
     public int CollectionId { get; set; }
  public  List<SelectListItem> Shoes { get; set; }
    public int SelectedShoe { get; set;}
    }
}
