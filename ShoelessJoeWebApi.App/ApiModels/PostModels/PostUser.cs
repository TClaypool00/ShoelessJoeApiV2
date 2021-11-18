namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostUser : ApiUser
    {
        public string ConfirmPassword { get; set; }

        public bool CheckPassword()
        {
            if (Password == ConfirmPassword)
            {
                return true;
            }

            return false;
        }
    }
}
