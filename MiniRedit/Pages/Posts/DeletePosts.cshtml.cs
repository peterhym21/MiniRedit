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
    public class DeletePostsModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<DeletePostsModel> _logger;

        public DeletePostsModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<DeletePostsModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        #region Prop
        public UsersDTO Users { get; set; }
        public PostsDTO Posts { get; set; }
        public BoardsDTO Boards { get; set; }
        #endregion


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


        public async Task<IActionResult> OnPostAsync(int id, int userid)
        {
            try
            {
                await _postsServices.DeletePoste(id, userid);
                return RedirectToPage("/Users/UserPage", new { userid });
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }

        }

    }
}
