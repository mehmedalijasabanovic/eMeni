namespace eMeni.Application.Modules.Identity.Queries.GetById
{
    public sealed class GetUserByIdQueryDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
    }
}
