using ShoelessJoeWebApi.App.ApiModels;
using ShoelessJoeWebApi.App.ApiModels.PartialModels;
using ShoelessJoeWebApi.App.ApiModels.PostModels;
using ShoelessJoeWebApi.Core.CoreModels;
using ShoelessJoeWebApi.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.App
{
    public class ApiMapper
    {
        /* -------------------------------
         * |                             |
         * |          Address            |
         * |                             |
         * -------------------------------
         */
        public static ApiAddress MapAddress(CoreAddress address)
        {
            return new ApiAddress
            {
                AddressId = address.AddressId,
                Street = address.Street,
                City = address.City,
                ZipCode = address.ZipCode,
                StateId = address.State.StateId,
                StateName = address.State.StateName,
                StateAbr = address.State.StateAbr
            };
        }

        public async static Task<CoreAddress> MapAddress(PostAddress address, IStateService stateService, int id = 0)
        {
            if (address.AddressId is not 0)
                address.AddressId = id;

            return new CoreAddress
            {
                AddressId = address.AddressId,
                Street = address.Street,
                City = address.City,
                ZipCode = address.ZipCode,
                State =  await stateService.GetStateAsync(address.StateId)
            };
        }

        public async static Task<CoreAddress> MapAddress(string street, string city, string zipCode, int stateId, IStateService stateService)
        {
            return new CoreAddress
            {
                Street = street,
                City = city,
                ZipCode = zipCode,
                State = await stateService.GetStateAsync(stateId)
            };
        }

        /* -------------------------------
         * |                             |
         * |           Comment           |
         * |                             |
         * -------------------------------
         */
        public static ApiComment MapComment(CoreComment comment)
        {
            return new ApiComment
            {
                BuyerId = comment.Buyer.UserId,
                BuyerFirstName = comment.Buyer.FirstName,
                BuyerLastName = comment.Buyer.LastName,

                UserId = comment.Seller.UserId,

                CommentBody = comment.CommentBody,
                DatePosted = comment.DatePosted,
                
                ShoeId = comment.Shoe.ShoeId
            };
        }

        public async static Task<CoreComment> MapComment(PostComment comment, IUserService userService, IShoeService shoeService, int buyerId = 0, int sellerId = 0)
        {
            if(buyerId != 0 && sellerId != 0)
            {
                comment.BuyerId = buyerId;
                comment.UserId = sellerId;
            }

            var shoe = await shoeService.GetShoeAsync(comment.ShoeId);

            return new CoreComment
            {
                Buyer = await userService.GetUserAsync(comment.BuyerId),
                Seller = shoe.User,
                Shoe = shoe,
                
                CommentBody = comment.CommentBody,
                DatePosted = comment.DatePosted
            };
        }

        public static ApiComment MapComment(CoreComment comment, ApiShoe shoe)
        {
            return new ApiComment
            {
                BuyerId = comment.Buyer.UserId,
                BuyerFirstName = comment.Buyer.FirstName,
                BuyerLastName = comment.Buyer.LastName,

                UserId = shoe.UserId,
                ShoeId = shoe.ShoeId,
                CommentBody = comment.CommentBody,
                DatePosted = comment.DatePosted
            };
        }


        /* -------------------------------
         * |                             |
         * |            Friend           |
         * |                             |
         * -------------------------------
         */
        public static ApiFriend MapFriend(CoreFriend friend)
        {
            return new ApiFriend
            {
                RecieverId = friend.Reciever.UserId,
                RecieverFirstName = friend.Reciever.FirstName,
                RecieverLastName = friend.Reciever.LastName,
                UserId = friend.Sender.UserId,
                SenderFirstName = friend.Sender.FirstName,
                SenderLastName = friend.Sender.LastName,
                DateAccepted = friend.DateAccepted
            };
        }

        public static async Task<CoreFriend> MapFriend(PostFriend friend, IUserService userService, int recieverId = 0, int senderId = 0)
        {
            if(recieverId != 0 && senderId != 0)
            {
                friend.RecieverId = recieverId;
                friend.UserId = senderId;
            }

            return new CoreFriend
            {
                Reciever = await userService.GetUserAsync(friend.RecieverId),
                Sender = await userService.GetUserAsync(friend.UserId),
                DateAccepted = friend.DateAccepted
            };
        }

        /* -------------------------------
         * |                             |
         * |          Manufacter         |
         * |                             |
         * -------------------------------
         */
        public static ApiManufacter MapManufacter(CoreManufacter manufacter)
        {
            return new ApiManufacter
            {
                ManufacterId = manufacter.ManufacterId,
                Name = manufacter.Name,
                Street = manufacter.Address.Street,
                City = manufacter.Address.City,
                ZipCode = manufacter.Address.ZipCode,
                AddressId = manufacter.Address.AddressId,
                IsApproved = manufacter.IsApproved,
                StateId = manufacter.Address.State.StateId,
                StateAbr = manufacter.Address.State.StateAbr,
                StateName = manufacter.Address.State.StateName
            };
        }

        public async static Task<CoreManufacter> MapManufacter(ApiManufacter manufacter, CoreAddress address = null, IAddressService addressService = null,  int id = 0)
        {
            if (id != 0)
                manufacter.ManufacterId = id;

            var newManufacter = new CoreManufacter
            {
                ManufacterId = manufacter.ManufacterId,
                Name = manufacter.Name,
                ZipCode = manufacter.ZipCode,
                IsApproved = false
            };

            if (address is null)
            {
                if (addressService is null)
                    return null;
                else
                    newManufacter.Address = await addressService.GetAddressAsync(manufacter.AddressId);
            }
            else
                newManufacter.Address = address;

            return newManufacter;
        }

        public static CoreManufacter MapMultiManufacters(ApiManufacter manufacter, CoreAddress address)
        {
            return new CoreManufacter
            {
                Name = manufacter.Name,
                ZipCode = manufacter.ZipCode,
                IsApproved = false,
                Address = address
            };
        }

        public static CoreManufacter MapMultiManufacters(ApiManufacter manufacter, int addressId)
        {
            return new CoreManufacter
            {
                Name = manufacter.Name,
                ZipCode = manufacter.ZipCode,
                IsApproved = manufacter.IsApproved,
                AddressId = addressId
            };
        }

        /* -------------------------------
         * |                             |
         * |           Model             |
         * |                             |
         * -------------------------------
         */
        public static ApiModel MapModel(CoreModel model)
        {
            return new ApiModel
            {
                ModelId = model.ModelId,
                ModelName = model.ModelName,
                ManufacterId = model.Manufacter.ManufacterId,
                ManufacterName = model.Manufacter.Name
            };
        }

        public async static Task<CoreModel> MapModel(PostModel model, IManufacterService manufacterService, int id = 0)
        {
            if (id is not 0)
                model.ModelId = id;

            return new CoreModel
            {
                ModelId = model.ModelId,
                ModelName = model.ModelName,
                Manufacter = await manufacterService.GetManufacterAsync(model.ManufacterId)
            };
        }

        /* -------------------------------
         * |                             |
         * |            Post             |
         * |                             |
         * -------------------------------
         */
        public static ApiPost MapPost(CorePost post)
        {
            return new ApiPost
            {
                PostId = post.PostId,
                CommentBody = post.CommentBody,
                DatePosted = post.DatePosted,
                
                UserId = post.User.UserId,
                UserLastName = post.User.LastName,
                UserFirstName = post.User.LastName
            };
        }

        public async static Task<CorePost> MapPost(PostPost post, IUserService userService, int id = 0)
        {
            if (id is not 0)
                post.PostId = id;

            return new CorePost
            {
                PostId = post.PostId,
                CommentBody = post.CommentBody,
                DatePosted = post.DatePosted,

                User = await userService.GetUserAsync(post.UserId)
            };
        }

        /* -------------------------------
         * |                             |
         * |           Reply             |
         * |                             |
         * -------------------------------
         */

        public static ApiReply MapReply(CoreReply reply)
        {
            var apiReply = new ApiReply
            {
                ReplyId = reply.ReplyId,
                ReplyBody = reply.ReplyBody,
                DatePosted = reply.DatePosted,

                UserId = reply.User.UserId,
                UserFirstName = reply.User.FirstName,
                UserLastName = reply.User.LastName
            };

            if (reply.Comment is null)
            {
                apiReply.BuyerId = reply.BuyerId;
                apiReply.SellerId = reply.SellerId;
            }
            else
            {
                apiReply.BuyerId = reply.Comment.Buyer.UserId;
                apiReply.SellerId = reply.Comment.Seller.UserId;
            }


            return apiReply;
        }

        public async static Task<CoreReply> MapReply(PostReply reply, ICommentService commentService, IUserService userService, int id = 0)
        {
            if (id != 0)
                reply.ReplyId = id;

            return new CoreReply
            {
                ReplyId = reply.ReplyId,
                ReplyBody = reply.ReplyBody,
                DatePosted = reply.DatePosted,

                Comment = await commentService.GetCommentAsync(reply.BuyerId, reply.SellerId, true),
                User = await userService.GetUserAsync(reply.UserId)
            };
        }

        /* -------------------------------
         * |                             |
         * |            School           |
         * |                             |
         * -------------------------------
         */
        public static ApiSchool MapSchool(CoreSchool school)
        {
            return new ApiSchool
            {
                SchoolId = school.SchoolId,
                SchoolName = school.SchoolName,
                AddressId = school.Address.AddressId,
                Street = school.Address.Street,
                City = school.Address.City,
                ZipCode = school.Address.ZipCode,
                StateId = school.Address.State.StateId,
                StateName = school.Address.State.StateName,
                StateAbr = school.Address.State.StateAbr
            };
        }

        public static CoreSchool MapSchool(ApiSchool school, CoreAddress address)
        {
            return new CoreSchool
            {
                SchoolId = school.SchoolId,
                SchoolName = school.SchoolName,
                Address = address
            };
        }

        /* -------------------------------
         * |                             |
         * |            State            |
         * |                             |
         * -------------------------------
         */
        public static ApiState MapState(CoreState state)
        {
            return new ApiState
            {
                StateId = state.StateId,
                StateName = state.StateName,
                StateAbr = state.StateAbr
            };
        }

        public static CoreState MapState(ApiState state, int id = 0)
        {
            if (id != 0)
                state.StateId = id;

            return new CoreState
            {
                StateId = state.StateId,
                StateName = state.StateName,
                StateAbr = state.StateAbr
            };
        }

        public static CoreState MapMultiStates(ApiState state)
        {
            return new CoreState
            {
                StateName = state.StateName,
                StateAbr = state.StateAbr
            };
        }

        /* -------------------------------
         * |                             |
         * |           Shoe              |
         * |                             |
         * -------------------------------
         */
        public static PartialShoe MapShoe(CoreShoe shoe)
        {
            return new PartialShoe
            {
                ShoeId = shoe.ShoeId,
                BothShoes = shoe.BothShoes,
                LeftShoeRight = shoe.ShoeImage.LeftShoeRight,
                ManufacterName = shoe.Model.Manufacter.Name,
                UserId = shoe.User.UserId,
                UserFirstName = shoe.User.FirstName,
                UserLastName = shoe.User.LastName
            };
        }

        public static ApiShoe MapFullShoe(CoreShoe shoe)
        {
            var comment = shoe.Comments.FirstOrDefault(c => c.Buyer.UserId == shoe.User.UserId || c.Seller.UserId == shoe.User.UserId);

            var apiShoe = new ApiShoe
            {
                ShoeId = shoe.ShoeId,
                BothShoes = shoe.BothShoes,
                LeftSize = shoe.LeftSize,
                RightSize = shoe.RightSize,
                LeftShoeRight = shoe.ShoeImage.LeftShoeRight,
                LeftShoeLeft = shoe.ShoeImage.LeftShoeLeft,
                RightShoeLeft = shoe.ShoeImage.RightShoeLeft,
                RightShoeRight = shoe.ShoeImage.RightShoeRight,
                ManufacterName = shoe.Model.Manufacter.Name,
                ModelName = shoe.Model.ModelName,
                UserId = shoe.User.UserId,
                UserFirstName = shoe.User.FirstName,
                UserLastName = shoe.User.LastName,
            };

            if (comment is null)
            {
                apiShoe.HasComment = false;
            }
            else
            {
                apiShoe.HasComment = true;
                apiShoe.Comment = MapComment(comment, apiShoe);
            }

            return apiShoe;
        }

        public async static Task<CoreShoe> MapShoe(PostShoe shoe, IUserService service, IModelService modelService, int id = 0)
        {
            if (id != 0)
                shoe.ShoeId = id;

            return new CoreShoe
            {
                ShoeId = shoe.ShoeId,
                BothShoes = shoe.BothShoes,
                LeftSize = shoe.LeftSize,
                RightSize = shoe.RightSize,
                Model = await modelService.GetModelAsync(shoe.ModelId),
                User = await service.GetUserAsync(shoe.UserId)
            };
        }

        public async static Task<CoreShoe> MapShoe(int shoeId, bool? bothSheos, double? leftShoeSize, double? rightShoeSize, int modelId, int userId, IUserService service, IModelService modelService)
        {
            return new CoreShoe
            {
                ShoeId = shoeId,
                BothShoes = bothSheos,
                LeftSize = leftShoeSize,
                RightSize = rightShoeSize,
                Model = await modelService.GetModelAsync(modelId),
                User = await service.GetUserAsync(userId)
            };
        }

        /* -------------------------------
         * |                             |
         * |           Site              |
         * |                             |
         * -------------------------------
         */
        public static ApiSite MapSite(CoreSite site)
        {
            return new ApiSite
            {
                SiteId = site.SiteId,
                SiteName = site.SiteName
            };
        }

        public static CoreSite MapSite(ApiSite site)
        {
            return new CoreSite
            {
                SiteId = site.SiteId,
                SiteName = site.SiteName
            };
        }

        /* -------------------------------
         * |                             |
         * |           User              |
         * |                             |
         * -------------------------------
         */
        public static ApiUser MapUser(CoreUser user, bool includePassword = true, string token = null)
        {
            var newUser = new ApiUser
            {
                UserId = user.UserId,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                Email = user.Email,
                IsAdmin = (bool)user.IsAdmin,
            };

            if (includePassword)
                newUser.Password = user.Password;

            if (token != null)
                newUser.Token = token;

            try
            {
                newUser.SchoolId = user.School.SchoolId;
                newUser.SchoolName = user.School.SchoolName;
            }
            catch(NullReferenceException)
            {
                newUser.SchoolId = null;
                newUser.SchoolName = null;
            }

            return newUser;
        }

        public async static Task<CoreUser> MapUser(ApiUser user, IUserService service, ISchoolService schoolService, int id = 0)
        {
            if (id != 0)
                user.UserId = id;

            if (user.Password is null)
            {
                var coreUser = await service.GetUserAsync(id);
                if (coreUser is null)
                    return null;
                else
                    user.Password = coreUser.Password;
            }
            else
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);


            var newUser = new CoreUser
            {
                UserId = user.UserId,
                FirstName = user.UserFirstName,
                LastName = user.UserLastName,
                Email = user.Email,
                Password = user.Password,
                IsAdmin = user.IsAdmin,
            };

            if (user.SchoolId != null && user.SchoolId != 0)
                newUser.School = await schoolService.GetSchoolAsync((int)user.SchoolId);

            return newUser;
        }
    }
}
