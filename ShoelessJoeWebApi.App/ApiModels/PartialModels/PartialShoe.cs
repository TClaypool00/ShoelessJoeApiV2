using ShoelessJoeWebApi.App.Helpers;

namespace ShoelessJoeWebApi.App.ApiModels.PartialModels
{
    public class PartialShoe : UserIdModel
    {
        private string _leftShoeRight;
        private int _shoeId;

        public int ShoeId
        {
            get { return _shoeId; }
            set { _shoeId = value; }
        }
        public string LeftShoeRight
        {
            get { return FormatUrl(_leftShoeRight); }
            set { _leftShoeRight = value; }
        }
        public string ManufacterName { get; set; }
        public bool? BothShoes { get; set; }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        protected string FormatUrl(string shoeUrl)
        {
            return $"http://localhost/ShoelessJoe-pictures/Shoes/user{_userId}/shoe{_shoeId}/{shoeUrl}";
        }
    }
}