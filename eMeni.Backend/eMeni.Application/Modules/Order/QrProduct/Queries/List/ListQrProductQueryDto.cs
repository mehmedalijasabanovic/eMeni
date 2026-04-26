namespace eMeni.Application.Modules.Order.QrProduct.Queries.List
{
    public sealed class ListQrProductQueryDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string MaterialType { get; set; }
        public string Size { get; set; }
    }
}
