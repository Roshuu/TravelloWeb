using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travello.Models
{
    public class Travel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public DateTime TravelTime { get; set; }
        [Required]
        [Display(Name = "Guide")]
        public int GuideId { get; set; }
        [ValidateNever]
        public Guide Guide { get; set; }
        [Range(10, 1000)]
        [Required]
        public int Price { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
