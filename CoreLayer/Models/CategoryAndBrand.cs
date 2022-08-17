namespace CoreLayer.Models
{
    public class CategoryAndBrand : BaseEntity
    {
        
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        public Category Category { get; set; }
        public Brand Brand { get; set; }
    }

}
