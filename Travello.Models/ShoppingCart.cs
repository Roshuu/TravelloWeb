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
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int TravelId { get; set; }
        [ForeignKey("TravelId")]
        [ValidateNever]
        public Travel Travel { get; set; }

        [Range(0, 1000, ErrorMessage = "PLEASE ENTER A VALUE BETWEEN 1 AND 30")]
        public int Ticket { get; set; }

        
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
       
    }
}
