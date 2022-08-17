namespace ProductsManagerSystem.Models
{
    public class CategoryViewModel : BaseViewModel
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public char Code { get; set; }
        public bool isActive { get; set; }
    }
}
