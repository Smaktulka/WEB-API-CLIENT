using System.ComponentModel.DataAnnotations;

namespace ClientApiFridge.Models
{
    public class FridgeModelsAndProductsObject
    {
        [Required(ErrorMessage = "Product is required field.")]
        public IEnumerable<ProductsDto> Products { get; set; } = null!;
   
        [Required(ErrorMessage = "Fridge Model is required field.")]
        public IEnumerable<FridgeModelsDto> FridgeModels { get; set; } = null!;

        public IEnumerable<FridgesDto>? Fridges { get; set; }

        [Required(ErrorMessage = "Fridge Name is required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length of Name is 60 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Fridge Owner_Name is required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length of Owner_Name is 30 characters.")]
        public string? Owner_Name { get; set; }

    }
}
