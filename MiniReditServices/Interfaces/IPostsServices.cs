using MiniReditServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditServices.Interfaces
{
    public interface IPostsServices
    {
        Task<List<PostsDTO>> GetPosts();
        Task<List<PostsDTO>> GetTopTen();
        Task<List<PostsDTO>> GetPostsByBoards(int boardId);
        Task<List<PostsDTO>> GetPostsFromUser(int userId);
        Task<PostsDTO> GetPostById(int postId);
        Task<int> CreatePost(PostsDTO postsDTO);
        Task<int> UpdatePost(PostsDTO postsDTO);
        Task DeletePoste(int postId, int userid);
    }
}
