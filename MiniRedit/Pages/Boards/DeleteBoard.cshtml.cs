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

        /// <summary>
        /// Prop
        /// </summary>
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


        public async Task<IActionResult> OnPostAsynce(int id)
        {
            try
            {
                await _boardsServices.DeleteBoard(id);
                return RedirectToPage("ViewAllBoards");
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            
        }

    }
}
