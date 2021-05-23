using ShoelessJoeWebApi.Core.CoreModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface IFriendService
    {
        Task<List<CoreFriend>> GetFriendsAsync(string search = null, int? recieverId = null, int? senderId = null, bool? recieverAndSender = null, DateTime? dateAccepted = null);

        Task<CoreFriend> GetFriendAsync(int recieverId, int senderId);

        Task<bool> FriendExistAsync(int recieverId, int senderId);

        Task AddFriendAsync(CoreFriend friend);

        Task AddFriendsAsync(List<CoreFriend> friends);

        Task UpdateFriendAsync(int recieverId, int senderId, CoreFriend friend);

        Task DeleteFriendAsync(int recieverId, int senderId);

        Task DeleteFriendsAsync(List<int> ids);
    }
}
