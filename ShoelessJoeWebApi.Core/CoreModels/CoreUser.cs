using System.Collections.Generic;

namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreUser
    {
        private string _lastName;

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName
        {
            get { return $"{_lastName[0]}."; }
            set { _lastName = value; }
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }

        public CoreSchool School { get; set; }
        public int? SchoolId { get; set; }

        public List<CoreShoe> Shoes { get; set; } = new List<CoreShoe>();
        public List<CoreComment> SellerComments { get; set; } = new List<CoreComment>();
        public List<CoreComment> BuyerComments { get; set; } = new List<CoreComment>();
        public List<CoreReply> Replies { get; set; } = new List<CoreReply>();
    }
}
