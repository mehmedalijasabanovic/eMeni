namespace eMeni.Application.Modules.Identity.Commands.ChangePassword
{
    public sealed class ChangePasswordCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
