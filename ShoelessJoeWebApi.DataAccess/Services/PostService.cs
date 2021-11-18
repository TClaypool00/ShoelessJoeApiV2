using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.Core.CoreModels;
using ShoelessJoeWebApi.Core.Interfaces;
using ShoelessJoeWebApi.DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.DataAccess.Services
{
    public class PostService : IPostService, ISave
    {
        private readonly ShoelessdevContext _context;

        public PostService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task<CorePost> AddPostAsync(CorePost post)
        {
            var newPost = Mapper.MapPost(post);
            await _context.Posts.AddAsync(newPost);
            await SaveAsync();

            return Mapper.MapPost(newPost);
        }

        public async Task<List<CorePost>> AddPostsAsync(List<CorePost> posts)
        {
            for (int i = 0; i < posts.Count; i++)
            {
                try
                {
                    await _context.AddAsync(Mapper.MapPost(posts[i]));
                    posts.Remove(posts[i]);
                }
                catch(Exception)
                {
                    continue;
                }
            }

            return posts;
        }

        public async Task<List<int>> DeletePostsAsync(List<int> ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                try
                {
                    var post = await FindPostAsync(ids[i]);
                    _context.Posts.Remove(post);
                    ids.Remove(ids[i]);
                }
                catch(Exception)
                {
                    continue;
                }
            }

            return ids;
        }

        public async Task<CorePost> GetPostAsync(int id)
        {
            return Mapper.MapPost(await FindPostAsync(id));
        }

        public async Task<List<CorePost>> GetPostsAsync(string search = null, int? userId = null, DateTime? date = null)
        {
            var posts = await _context.Posts
                .Include(u => u.User)
                .ToListAsync();

            List<CorePost> corePosts;

            if (search is null)
                corePosts = posts.Select(Mapper.MapPost).ToList();
            else
                corePosts = SearchResultAsync(search, posts);

            if (userId is not null)
                corePosts = corePosts.Where(a => a.UserId == userId).ToList();

            if (date is not null)
                corePosts = corePosts.Where(b => b.DatePosted == date.ToString()).ToList();

            return corePosts;
        }

        public Task<bool> PostExistAsync(int id)
        {
            return _context.Posts.AnyAsync(p => p.PostId == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<CorePost> UpdatePostAsync(int id, CorePost post)
        {
            try
            {
                var oldPost = await FindPostAsync(id);
                var newPost = Mapper.MapPost(post);

                _context.Entry(oldPost).CurrentValues.SetValues(newPost);

                await SaveAsync();

                newPost.PostId = oldPost.PostId;

                return Mapper.MapPost(newPost);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await FindPostAsync(id);

            _context.Posts.Remove(post);
            await SaveAsync();
        }

        async Task<Post> FindPostAsync(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.PostId == id);
        }

        static List<CorePost> SearchResultAsync(string search, List<Post> posts)
        {
            return posts.FindAll(p => p.CommentBody.ToLower().Contains(search.ToLower()) ||
            p.DatePosted.ToString().Contains(search.ToLower()) ||
            p.User.FirstName.ToLower().Contains(search.ToLower()) ||
            p.User.LastName.ToLower().Contains(search.ToLower())
            ).Select(Mapper.MapPost).ToList();
        }
    }
}