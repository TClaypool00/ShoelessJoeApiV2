using System;

namespace ShoelessJoeWebApi.Core.PartialModels
{
    public class PartialPost
    {
        private DateTime _dateTime;

        public string CommentBody { get; set; }
        public string DatePosted
        {
            get { return _dateTime.ToString("MMMM dd yyyy h:mm"); }
            set { _dateTime = DateTime.Parse(value); }
        }
    }
}
