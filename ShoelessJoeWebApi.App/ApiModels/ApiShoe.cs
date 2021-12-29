using ShoelessJoeWebApi.App.ApiModels.PartialModels;
using System.Collections.Generic;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiShoe : PartialShoe
    {
        private string _leftShoeLeft;
        private string _rightShoeLeft;
        private string _rightShoeRight;

        public string LeftShoeLeft
        {
            get { return FormatUrl(_leftShoeLeft); }
            set { _leftShoeLeft = value; }
        }

        public string RightShoeLeft
        {
            get { return FormatUrl(_rightShoeLeft); }
            set { _rightShoeLeft = value; }
        }
        public string RightShoeRight
        {
            get { return FormatUrl(_rightShoeRight); }
            set { _rightShoeRight = value; }
        }

        public string ModelName { get; set; }
        public double? LeftSize { get; set; }
        public double? RightSize { get; set; }
        public bool HasComment { get; set; }

        public ApiComment Comment { get; set; } = null;
        public List<ApiComment> Comments { get; set; } = new List<ApiComment>();
    }
}
