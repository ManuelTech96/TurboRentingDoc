using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Requests.Users
{
    public class UpdateUserRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RePassword { get; set; }
        public Role Role { get; set; }
    }
}
