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
    public class DeleteBoardModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<DeleteBoardModel> _logger;

        public DeleteBoardModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<DeleteBoardModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        public BoardsDTO Boards { get; set; }

        public async Task OnGetAsync(int id)
        {
            Boards = await _boardsServices.GetOneBoard(id);
        }

        public async Task<IActionResult> OnPostAsynce(int id)
        {
            await _boardsServices.DeleteBoard(id);
            return RedirectToPage("ViewAllBoards");
        }

    }
}
