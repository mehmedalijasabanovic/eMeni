namespace eMeni.Application.Modules.Business.BusinessCategory.Queries.List
{
    public sealed class ListBusinessesCategoryQueryDto
    {
        public required int Id { get; init; }
        public required string CategoryName { get; init; }
        public required string CategoryDescription { get; init; }
    }
}
