using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MiniReditServices.DTO;
using MiniReditServices.Interfaces;

namespace MiniRedit.Pages.Boards
{
    public class BoardDetailsModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<BoardDetailsModel> _logger;

        public BoardDetailsModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<BoardDetailsModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        public BoardsDTO Boards { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Boards = await _boardsServices.GetOneBoard(id);
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
        }

    }
}
