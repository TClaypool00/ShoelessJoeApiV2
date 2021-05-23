using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class UpdatePasswordViewModel
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ComfirmPassword { get; set; }
    }
}
