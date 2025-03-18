using E_commerce.DAl.Model;

namespace E_CommerceApp.ViewModel
{
    public class SearchResultViewModel
    {
        public string SearchItem { get; set; }
        public int CategoryId { get; set; }
        public List<Category> CategoryList { get; set; }
    }
}
