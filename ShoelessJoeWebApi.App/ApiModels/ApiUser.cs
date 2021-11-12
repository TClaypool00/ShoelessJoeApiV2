using ShoelessJoeWebApi.App.ApiModels.PartialModels;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiUser : PartialUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }

        public int? SchoolId { get; set; } = null;
        public string SchoolName { get; set; }

        public bool CheckPassword()
        {
            if (Password == ConfirmPassword)
            {
                return true;
            }

            return false;
        }
    }
}
