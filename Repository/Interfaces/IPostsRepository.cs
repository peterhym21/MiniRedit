using MiniReditRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditRepository.Interfaces
{
    public interface IPostsRepository
    {
        Task<List<Posts>> GetPosts();
        Task<List<Posts>> GetTopTen();
        Task<List<Posts>> GetPostsByBoards(int boardId);
        Task<List<Posts>> GetPostsFromUser(int userId);
        Task<Posts> GetPostById(int postId);
        Task<int> CreatePost(Posts postsEnt);
        Task<int> UpdatePost(Posts postsEnt);
        Task DeletePoste(int postId, int userid);
    }
}
