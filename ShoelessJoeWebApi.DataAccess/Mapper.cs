using ShoelessJoeWebApi.Core.CoreModels;
using ShoelessJoeWebApi.DataAccess.DataModels;
using System;
using System.Linq;

namespace ShoelessJoeWebApi.DataAccess
{
    public class Mapper
    {
        /* -------------------------------
         * |                             |
         * |           Address           |
         * |                             |
         * -------------------------------
         */
        public static Address MapAddress(CoreAddress address)
        {
            var newAddress = new Address
            {
                Street = address.Street,
                City = address.City,
                ZipCode = address.ZipCode,
                StateId = address.State.StateId
            };

            if (address.AddressId != 0)
                newAddress.AddressId = address.AddressId;

            return newAddress;
        }

        public static CoreAddress MapAddress(Address address)
        {
            return new CoreAddress
            {
                AddressId = address.AddressId,
                Street = address.Street,
                City = address.City,
                ZipCode = address.ZipCode,
                State = MapState(address.State)
            };
        }

        /* -------------------------------
         * |                             |
         * |           Comment           |
         * |                             |
         * -------------------------------
         */
        public static Comment MapComment(CoreComment comment)
        {
            return new Comment
            {
                BuyerId = comment.Buyer.UserId,
                CommentBody = comment.CommentBody,
                DatePosted = DateTime.Now,
                ShoeId = comment.Shoe.ShoeId
            };
        }

        public static CoreComment MapComment(Comment comment)
        {
            return new CoreComment
            {
                CommentId = comment.CommentId,
                Buyer = MapUser(comment.Buyer),
                CommentBody = comment.CommentBody,
                DatePosted = comment.DatePosted,
                Replies = comment.Replies.Select(MapReply).ToList()
            };
        }

        public static CoreComment MapCommentForReply(Comment comment)
        {
            return new CoreComment
            {
                CommentId = comment.CommentId,
                Buyer = MapUser(comment.Buyer),
                CommentBody = comment.CommentBody,
                DatePosted = comment.DatePosted,
            };
        }

        /* -------------------------------
         * |                             |
         * |           Friends           |
         * |                             |
         * -------------------------------
         */
        public static Friend MapFriend(CoreFriend friend)
        {
            return new Friend
            {
                RecieverId = friend.Reciever.UserId,
                SenderId = friend.Sender.UserId,
                DateAccepted = friend.DateAccepted
            };
        }

        public static CoreFriend MapFriend(Friend friend)
        {
            return new CoreFriend
            {
                Reciever = MapUser(friend.Reciever),
                Sender = MapUser(friend.Sender),
                DateAccepted = friend.DateAccepted
            };
        }

        /* -------------------------------
         * |                             |
         * |         Manufacter          |
         * |                             |
         * -------------------------------
         */
        public static Manufacter MapManufacter(CoreManufacter manufacter, int id = 0)
        {
            var newManufacter = new Manufacter
            {
                Name = manufacter.Name,
                IsApproved = manufacter.IsApproved,
                AddressId = manufacter.Address.AddressId,
            };

            if (id != 0)
                newManufacter.ManufacterId = id;

            return newManufacter;
        }

        public static Manufacter MapManufacterShort(CoreManufacter manufacter)
        {
            return new Manufacter
            {
                ManufacterId = manufacter.ManufacterId,
                Name = manufacter.Name,
                IsApproved = manufacter.IsApproved,
                AddressId = manufacter.AddressId,
            };
        }

        public static CoreManufacter MapManufacter(Manufacter manufacter)
        {
            return new CoreManufacter
            {
                ManufacterId = manufacter.ManufacterId,
                Name = manufacter.Name,
                IsApproved = manufacter.IsApproved,
                Address = MapAddress(manufacter.Address)
            };
        }

        /* -------------------------------
         * |                             |
         * |            Model            |
         * |                             |
         * -------------------------------
         */
        public static Model MapModel(CoreModel model)
        {
            var newModel = new Model
            {
                ModelName = model.ModelName,
                ManufacterId = model.Manufacter.ManufacterId
            };

            if (model.ModelId != 0)
                newModel.ModelId = model.ModelId;

            return newModel;
        }

        public static CoreModel MapModel(Model model)
        {
            return new CoreModel
            {
                ModelId = model.ModelId,
                ModelName = model.ModelName,
                Manufacter = MapManufacter(model.Manufacter)
            };
        }

        /* -------------------------------
         * |                             |
         * |             Post            |
         * |                             |
         * -------------------------------
         */
        public static Post MapPost(CorePost post)
        {
            var newPost = new Post
            {
                CommentBody = post.CommentBody,
                DatePosted = DateTime.Parse(post.DatePosted),
                UserId = post.User.UserId
            };

            if (post.PostId != 0)
                newPost.PostId = post.PostId;

            return newPost;
        }

        public static CorePost MapPost(Post post)
        {
            return new CorePost
            {
                CommentBody = post.CommentBody,
                DatePosted = post.DatePosted.ToString(),
                PostId = post.PostId,

                User = MapUser(post.User)
            };
        }

        /* -------------------------------
         * |                             |
         * |            Reply            |
         * |                             |
         * -------------------------------
         */
        public static Reply MapReply(CoreReply reply)
        {
            var newReply = new Reply
            {
                DatePosted = DateTime.Now,
                ReplyBody = reply.ReplyBody,
                CommentId = reply.Comment.CommentId,

                UserId = reply.User.UserId
            };

            if (reply.ReplyId != 0)
                newReply.ReplyId = reply.ReplyId;

            return newReply;
        }

        public static CoreReply MapReply(Reply reply)
        {
            return new CoreReply
            {
                ReplyId = reply.ReplyId,
                DatePosted = reply.DatePosted,
                ReplyBody = reply.ReplyBody,
                User = MapUser(reply.User)
            };
        }

        public static CoreReply MapReplyReturn(Reply reply)
        {
            return new CoreReply
            {
                ReplyId = reply.ReplyId,
                DatePosted = reply.DatePosted,
                ReplyBody = reply.ReplyBody,

                CommentId = reply.Comment.CommentId,
                User = MapUser(reply.User)
            };
        }
        /* -------------------------------
         * |                             |
         * |          School             |
         * |                             |
         * -------------------------------
         */
        public static School MapSchool(CoreSchool school)
        {
            return new School
            {
                SchoolId = school.SchoolId,
                SchoolName = school.SchoolName,
                AddressId = school.Address.AddressId
            };
        }

        public static CoreSchool MapSchool(School school)
        {
            return new CoreSchool
            {
                SchoolId = school.SchoolId,
                SchoolName = school.SchoolName,
                Address = MapAddress(school.Address)
            };
        }

        /* -------------------------------
         * |                             |
         * |           Shoe              |
         * |                             |
         * -------------------------------
         */
        public static Shoe MapShoe(CoreShoe shoe)
        {
            var dataShoe = new Shoe
            {
                BothShoes = shoe.BothShoes,
                RightSize = shoe.RightSize,
                LeftSize = shoe.LeftSize,
                UserId = shoe.User.UserId,
                ModelId = shoe.Model.ModelId,
                
            };

            if (shoe.ShoeId != 0)
                dataShoe.ShoeId = shoe.ShoeId;

            return dataShoe;
        }

        public static CoreShoe MapShoeWithComment(Shoe shoe, int? userId = null)
        {
            return new CoreShoe
            {
                ShoeId = shoe.ShoeId,
                BothShoes = shoe.BothShoes,
                RightSize = shoe.RightSize,
                LeftSize = shoe.LeftSize,

                Model = MapModel(shoe.Model),
                User = MapUser(shoe.User),
                ShoeImage = MapImage(shoe.ShoeImage),
                Comment = MapComment(shoe.Comments.FirstOrDefault(c => c.BuyerId == userId))
            };
        }

        public static CoreShoe MapShoe(Shoe shoe)
        {
            return new CoreShoe
            {
                ShoeId = shoe.ShoeId,
                BothShoes = shoe.BothShoes,
                RightSize = shoe.RightSize,
                LeftSize = shoe.LeftSize,

                Model = MapModel(shoe.Model),
                User = MapUser(shoe.User),
                ShoeImage = MapImage(shoe.ShoeImage),
            };
        }

        /* -------------------------------
         * |                             |
         * |         Shoe Image          |
         * |                             |
         * -------------------------------
         */

        public static ShoeImage MapImage(CoreShoeImg image)
        {
            return new ShoeImage
            {
                ImgGroupId = image.ImgGroupId,
                LeftShoeLeft = image.LeftShoeLeft,
                LeftShoeRight = image.LeftShoeRight,

                RightShoeLeft = image.RightShoeLeft,
                RightShoeRight = image.RightShoeRight,

                ShoeId = image.Shoe.ShoeId
            };
        }

        public static CoreShoeImg MapImage(ShoeImage image)
        {
            return new CoreShoeImg
            {
                ImgGroupId = image.ImgGroupId,

                LeftShoeLeft = image.LeftShoeLeft,
                LeftShoeRight = image.LeftShoeRight,

                RightShoeLeft = image.RightShoeLeft,
                RightShoeRight = image.RightShoeRight,

                //Shoe = MapShoe(image.Shoe)
            };
        }

        /* -------------------------------
         * |                             |
         * |            Site             |
         * |                             |
         * -------------------------------
         */
        public static Site MapSite(CoreSite site)
        {
            return new Site
            {
                SiteId = site.SiteId,
                SiteName = site.SiteName
            };
        }

        public static CoreSite MapSite(Site site)
        {
            return new CoreSite
            {
                SiteId = site.SiteId,
                SiteName = site.SiteName
            };
        }

        /* -------------------------------
         * |                             |
         * |           State             |
         * |                             |
         * -------------------------------
         */
        public static State MapState(CoreState state)
        {
            var newState = new State
            {
                StateName = state.StateName,
                StateAbr = state.StateAbr
            };

            if (state.StateId != 0)
                newState.StateId = state.StateId;

            return newState;
        }

        public static CoreState MapState(State state)
        {
            return new CoreState
            {
                StateId = state.StateId,
                StateAbr = state.StateAbr,
                StateName = state.StateName
            };
        }

        /* -------------------------------
         * |                             |
         * |           User              |
         * |                             |
         * -------------------------------
         */
        public static CoreUser MapUser(User user)
        {
            var coreUser = new CoreUser
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                IsAdmin = user.IsAdmin,
            };

            try
            {
                var school = MapSchool(user.School);
                coreUser.SchoolId = school.SchoolId;
            }
            catch(NullReferenceException)
            {
                coreUser.SchoolId = 0;
            }

            return coreUser;
        }

        public static User MapUser(CoreUser user)
        {
            var dbUser = new User
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                IsAdmin = user.IsAdmin
            };

            try
            {
                dbUser.SchoolId = user.School.SchoolId;
            }
            catch(NullReferenceException)
            {
                dbUser.SchoolId = null;
            }

            return dbUser;
        }
    }
}