﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApiFridge.Models
{
    public class FridgeForUpdateDto
    {

        [Required(ErrorMessage = "Fridge Name is required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length of Name is 60 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Fridge Owner_Name is required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length of Owner_Name is 30 characters.")]
        public string? Owner_Name { get; set; }

        [ForeignKey(nameof(FridgeModels))]
        public Guid ModelId { get; set; }
    }
}
