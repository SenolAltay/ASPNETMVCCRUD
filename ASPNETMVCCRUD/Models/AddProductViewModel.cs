namespace ASPNETMVCCRUD.Models
{
    public class AddProductViewModel
    {
        public string PriceListName { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string MaterialGroupCode { get; set; }
        public string BrandCode { get; set; }
        public string Model { get; set; }
        public string PriceList { get; set; }
        public int Stock { get; set; }
    }
}
