using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class Friend
    {
        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int RecieverId { get; set; }
        public User Reciever { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? DateAccepted { get; set; } = null;
    }
}
