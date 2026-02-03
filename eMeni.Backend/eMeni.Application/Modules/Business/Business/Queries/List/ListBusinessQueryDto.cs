namespace eMeni.Application.Modules.Business.Business.Queries.List
{
    public sealed class ListBusinessQueryDto
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public byte? PromotionRank { get; set; }
    }
}
