using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Unipluss.Sign.Common.Validation;
namespace ClientApiFridge.Models
{
    public class Fridges
	{
        [Column("FridgeId")]
        [Required(ErrorMessage = "Id is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Fridge Name is required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length of Name is 60 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Fridge Owner_Name is required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length of Owner_Name is 30 characters.")]
        public string? Owner_Name { get; set; }

        [Required(ErrorMessage = "Model Id is required field.")]
        public Guid ModelId { get; set; }

    }
}
