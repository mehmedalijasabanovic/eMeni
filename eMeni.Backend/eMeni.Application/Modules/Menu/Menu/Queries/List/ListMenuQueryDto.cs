namespace eMeni.Application.Modules.Menu.Menu.Queries.List
{
    public sealed class ListMenuQueryDto
    {
        public int Id { get; set; }
        public string MenuTitle { get; set; }
        public string? MenuDescription { get; set; }
        public List<MenuCategoryDto>? Categories { get; set; }
    }
    public sealed class MenuCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int OrderIndex { get; set; }
        public string? Description { get; set; }
        public List<MenuItemDto>? Items { get; set; }
    }
    public sealed class MenuItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public string Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}