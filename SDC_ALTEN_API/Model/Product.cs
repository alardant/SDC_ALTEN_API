namespace SDC_ALTEN_API.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public string inventoryStatus { get; set; }
        public string category { get; set; }
        public string? image { get; set; }
        public int? rating { get; set; }

    }
}
