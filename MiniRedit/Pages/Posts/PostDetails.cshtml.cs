using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MiniReditServices.DTO;
using MiniReditServices.Interfaces;

namespace MiniRedit.Pages.Posts
{
    public class PostDetailsModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<PostDetailsModel> _logger;

        public PostDetailsModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<PostDetailsModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        public UsersDTO Users { get; set; }
        public PostsDTO Posts { get; set; }
        public BoardsDTO Boards { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Posts = await _postsServices.GetPostById(id);
                Boards = await _boardsServices.GetOneBoard(Posts.BoardId);
                Users = await _usersServices.GetUserById(Posts.UserId);
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            
        }
    }
}
