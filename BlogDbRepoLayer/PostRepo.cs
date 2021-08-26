using BlogDbRepoLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogDbRepoLayer
{
    public class PostRepo : IPost
    {
        BlogDbContext bloAppgDbContext;
        public PostRepo(BlogDbContext blogContext)
        {
            this.bloAppgDbContext = blogContext;

        }


        async Task<int> IPost.AddPost(Post post)
        {
            if (bloAppgDbContext != null)
            {
                await bloAppgDbContext.Posts.AddAsync(post);
                await bloAppgDbContext.SaveChangesAsync();

                return post.PostId;
            }

            return 0;
        }

       async Task<int> IPost.DeletePost(int? postId)
        {
            int result = 0;

            if (bloAppgDbContext != null)
            {
                //Find the post for specific post id
                var post = await bloAppgDbContext.Posts.FirstOrDefaultAsync(x => x.PostId == postId);

                if (post != null)
                {
                    //Delete that post
                    bloAppgDbContext.Posts.Remove(post);

                    //Commit the transaction
                    result = await bloAppgDbContext.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        async Task<PostCategoryVM> IPost.GetPost(int? postId)
        {
            if (bloAppgDbContext != null)
            {
                return await(from p in bloAppgDbContext.Posts
                             from c in bloAppgDbContext.Categories
                             where p.PostId == postId
                             select new PostCategoryVM
                             {
                                 PostId = p.PostId,
                                 Title = p.Title,
                                 Description = p.Description,
                                 CategoryId = p.CategoryId,
                                 CategoryName = c.Name,
                                 CreatedDate = p.CreatedDate
                             }).FirstOrDefaultAsync();
            }

            return null;
        }

        async Task<List<PostCategoryVM>> IPost.GetPosts()
        {
            return await(from p in bloAppgDbContext.Posts
                         from c in bloAppgDbContext.Categories
                         where p.CategoryId == c.Id
                         select new PostCategoryVM
                         {
                             PostId = p.PostId,
                             Title = p.Title,
                             Description = p.Description,
                             CategoryId = p.CategoryId,
                             CategoryName = c.Name,
                             CreatedDate = p.CreatedDate
                         }).ToListAsync();
        }

       async Task IPost.UpdatePost(Post post)
        {
            if (bloAppgDbContext != null)
            {
                //Delete that post
                bloAppgDbContext.Posts.Update(post);

                //Commit the transaction
                await bloAppgDbContext.SaveChangesAsync();
            }
        }
    }
}
