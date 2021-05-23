using System;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiFriend
    {
        public int RecieverId { get; set; }
        public string RecieverFirstName { get; set; }
        public string RecieverLastName { get; set; }

        public int SenderId { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }

        public DateTime? DateAccepted { get; set; } = null;
    }
}
