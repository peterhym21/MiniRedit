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
    public class CreatePostsModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<CreatePostsModel> _logger;

        public CreatePostsModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<CreatePostsModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        #region Prop
        [BindProperty]
        public string Board { get; set; }
        public int BoardId { get; set; }
        public int postId { get; set; }

        [BindProperty]
        public PostsDTO posts { get; set; }
        public UsersDTO User { get; set; }
        public BoardsDTO board { get; set; }
        public List<PostsDTO> Posts { get; set; }
        public List<BoardsDTO> Boards { get; set; }
        #endregion


        public async Task<IActionResult> OnGetAsync(int userid)
        {
            try
            {
                if (userid == 0)
                    userid = 1;

                User = await _usersServices.GetUserById(userid);
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            
        }


        public async Task<IActionResult> OnPostAsync(int userid)
        {
            try
            {
                posts.UserId = userid;
                board.Title = Board;

                posts.BoardId = await _boardsServices.CreateBoard(board);
                postId = await _postsServices.CreatePost(posts);

                return RedirectToPage("PostDetails", new { id = postId });
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            
        }

    }
}
