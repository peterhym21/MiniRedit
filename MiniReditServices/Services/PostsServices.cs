using MiniReditRepository.Entities;
using MiniReditRepository.Interfaces;
using MiniReditServices.DTO;
using MiniReditServices.Interfaces;
using Service.Services;
using Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditServices.Services
{
    public class PostsServices : BaseService, IPostsServices
    {
        private readonly MappingService _mappingService;
        private readonly IPostsRepository _postsRepository;

        public PostsServices(MappingService mappingService, IPostsRepository postsRepository)
        {
            _mappingService = mappingService;
            _postsRepository = postsRepository;
        }

        public async Task<int> CreatePost(PostsDTO postsDTO)
        {
            try
            {
                int resultId = await _postsRepository.CreatePost(_mappingService._mapper.Map<Posts>(postsDTO));
                LogInformation($"Successfully created a new Post : PostID {postsDTO.PostId}, Title {postsDTO.Title} :");
                return resultId;
            }
            catch (Exception ex)
            {
                LogError($"Failed to create a new post : PostID {postsDTO.PostId}, Title {postsDTO.Title} :", ex);
                return 1;

            }
        }

        public async Task DeletePoste(int postId, int userId)
        {
            try
            {
                await _postsRepository.DeletePoste(postId, userId);
                LogInformation($"Successfully removed a post : PostId {postId}, UserId {userId} :");
            }
            catch (Exception ex)
            {
                LogError($"Failed to remove a post : PostId {postId}, UserId {userId}", ex);

            }
        }

        public async Task<PostsDTO> GetPostById(int postId)
        {
            if (postId == 0)
            {
                return null;
            }
            try
            {
                PostsDTO post = _mappingService._mapper.Map<PostsDTO>(await _postsRepository.GetPostById(postId));
                LogInformation($"Successfully fetched a post : postId {post.PostId}, Title {post.Title} :");
                return post;
            }
            catch (Exception ex)
            {
                LogError($"Failed to fetch a post : postId {postId}", ex);
                return null;

            }
        }

        public async Task<List<PostsDTO>> GetPosts()
        {
            try
            {
                List<PostsDTO> posts = _mappingService._mapper.Map<List<PostsDTO>>(await _postsRepository.GetPosts());
                LogInformation($"Successfully fetched a list of Posts");
                return posts;
            }
            catch (Exception ex)
            {
                LogError("Failed to fetch a list of Posts", ex);
                return null;

            }
        }

        public async Task<List<PostsDTO>> GetPostsByBoards(int boardId)
        {
            try
            {
                List<PostsDTO> posts = _mappingService._mapper.Map<List<PostsDTO>>(await _postsRepository.GetPostsByBoards(boardId));
                LogInformation($"Successfully fetched a list of Posts by Boards");
                return posts;
            }
            catch (Exception ex)
            {
                LogError("Failed to fetch a list of Posts by Boards", ex);
                return null;

            }
        }

        public async Task<List<PostsDTO>> GetPostsFromUser(int userId)
        {
            try
            {
                List<PostsDTO> posts = _mappingService._mapper.Map<List<PostsDTO>>(await _postsRepository.GetPostsFromUser(userId));
                LogInformation($"Successfully fetched a list of Posts form user");
                return posts;
            }
            catch (Exception ex)
            {
                LogError("Failed to fetch a list of Posts from user", ex);
                return null;

            }
        }

        public async Task<List<PostsDTO>> GetTopTen()
        {
            try
            {
                List<PostsDTO> posts = _mappingService._mapper.Map<List<PostsDTO>>(await _postsRepository.GetTopTen());
                LogInformation($"Successfully fetched a list of Posts of then 10 news");
                return posts;
            }
            catch (Exception ex)
            {
                LogError("Failed to fetch a list of Posts of then 10 news", ex);
                return null;

            }
        }

        public async Task<int> UpdatePost(PostsDTO postsDTO)
        {
            try
            {
                int result = await _postsRepository.UpdatePost(_mappingService._mapper.Map<Posts>(postsDTO));
                LogInformation($"Successfully updated a post : PostId { postsDTO.PostId} :");
                return result;
            }
            catch (Exception ex)
            {
                LogError($"Failed to update a post : PostId { postsDTO.PostId} :", ex);
                return 1;
            }
        }
    }
}
