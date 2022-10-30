namespace ClientApiFridge.Models
{
    public class FridgeProductSpDto
    {
        public IEnumerable<FridgeProductsDto> FridgeProducts { get; set; } = null!;

        public Guid FridgeId { get; set; }
    }
}
