using eMeni.Application.Common;

namespace eMeni.Application.Modules.Menu.Menu.Queries.ListOnlyMenus
{
    public sealed class ListOnlyMenusQuery:BasePagedQuery<ListOnlyMenusQueryDto>
    {
        public  int CategoryId { get; set; }
        public string? City { get; set; }
        
    }
}
