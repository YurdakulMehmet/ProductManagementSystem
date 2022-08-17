namespace ProductsManagerSystem.Models
{
    public class ProductPhotoViewModel 
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public bool isActive { get; set; }
    }
}
