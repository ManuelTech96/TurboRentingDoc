using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Api.Models.Entities
{
    public class User : AggregateRoot<int>
    {
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string RePassword { get => rePassword; set => rePassword = value; }
        public Role Role { get => role; set => role = value; }
        public bool Activo { get => activo; set => activo = value; }

        private string name;
        private string email;
        private string password;
        private string rePassword;
        private Role role;
        private bool activo;
    }
}
