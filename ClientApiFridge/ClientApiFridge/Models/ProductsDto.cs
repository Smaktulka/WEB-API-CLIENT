﻿using System.ComponentModel.DataAnnotations;

namespace ClientApiFridge.Models
{
    public class ProductsDto
    { 
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Products Name is required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length of Name is 60 characters.")]
        public string? Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Default_Quantity is required and can't be less than 1.")]
        public int Default_Quantity { get; set; } = 0!;
    }
}
