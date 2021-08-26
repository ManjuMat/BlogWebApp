using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDbRepoLayer.ViewModel;
using BlogDAL.Models;

namespace BlogDbRepoLayer
{
    public interface IPost
    {               
        Task<List<PostCategoryVM>> GetPosts();

        Task<PostCategoryVM> GetPost(int? postId);

        Task<int> AddPost(Post post);

        Task<int> DeletePost(int? postId);

        Task UpdatePost(Post post);
    }
}
