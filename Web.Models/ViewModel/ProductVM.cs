using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models.Models;

namespace Web.Models.ViewModel;

public class ProductVM
{
    public Product Product{get;set;}
    [ValidateNever]
    public IEnumerable< SelectListItem> CategoryList{get; set;}

}
