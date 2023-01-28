using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travello.Models;

namespace Travello.Models.ViewModels
{
    public class ProductVM
    {
        public Travel Travel { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> GuideList { get; set; }

    }
}
