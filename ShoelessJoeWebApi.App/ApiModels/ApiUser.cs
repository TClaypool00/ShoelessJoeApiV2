using ShoelessJoeWebApi.App.ApiModels.PartialModels;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiUser : PartialUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public int? SchoolId { get; set; }
        public string SchoolName { get; set; }
    }
}
