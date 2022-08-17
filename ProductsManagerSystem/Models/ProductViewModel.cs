namespace ProductsManagerSystem.Models
{
    public class ProductViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public bool isActive { get; set; }
        public int? ProductPhotoId { get; set; }
        public int ParentCategoryId { get; set; }
        //public int? ChildCategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
