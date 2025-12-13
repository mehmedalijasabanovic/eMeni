namespace eMeni.Application.Modules.Business.Business.Queries.GetByUserId
{
    public sealed class GetBusinessByUserIdQueryDto
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
