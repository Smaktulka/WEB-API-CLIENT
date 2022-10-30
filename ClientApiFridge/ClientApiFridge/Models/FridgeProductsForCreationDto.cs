using Entities.Models;
using fridge.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApiFridge.Models
{
    public class FridgeProductsForCreationDto
    {
        [ForeignKey(nameof(Products))]
        [Required(ErrorMessage = "Product Id is required field.")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required field.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity is required and it can't be less than 0.")]
        public int Quantity { get; set; }
    }
}
