using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MiniReditServices.DTO;
using MiniReditServices.Interfaces;

namespace MiniRedit.Pages.Users
{
    public class UserPageModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<UserPageModel> _logger;

        public UserPageModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<UserPageModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        public List<PostsDTO> Posts { get; set; }
        public List<BoardsDTO> Boards { get; set; }
        public UsersDTO User { get; set; }

        public async Task<IActionResult> OnGetAsync(int userid)
        {
            try
            {
                User = await _usersServices.GetUserById(userid);
                Posts = await _postsServices.GetPostsFromUser(userid);
                Boards = await _boardsServices.GetBoards();
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            
        }
    }
}
