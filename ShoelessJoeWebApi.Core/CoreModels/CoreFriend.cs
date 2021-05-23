using System;

namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreFriend
    {
        public int SenderId { get; set; }
        public CoreUser Sender { get; set; }

        public int RecieverId { get; set; }
        public CoreUser Reciever { get; set; }
        public DateTime? DateAccepted { get; set; }
    }
}
