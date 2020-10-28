using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MiniReditServices.DTO;
using MiniReditServices.Interfaces;
using Service.Services.Base;

namespace MiniRedit.Pages
{
    public class IndexModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<IndexModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        #region Prop
        public SelectList BoardsSelect { get; set; }
        public int SelectedId { get; set; }
        public List<PostsDTO> Posts { get; set; }
        public List<BoardsDTO> Boards { get; set; }
        public UsersDTO User { get; set; }
        #endregion

        public async Task<IActionResult> OnGetAsync(int userid)
        {
            try
            {
                // if no userid set to default user
                if (userid == 0)
                    userid = 1;

                // get all info for font page
                User = await _usersServices.GetUserById(userid);
                BoardsSelect = new SelectList(await _boardsServices.GetBoards(), "BoardId", "Title");
                Posts = await _postsServices.GetTopTen();
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
