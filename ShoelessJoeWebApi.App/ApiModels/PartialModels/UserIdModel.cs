using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.App.ApiModels.PartialModels
{
    public class UserIdModel
    {
        protected int _userId;
        [Required]
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
    }
}
