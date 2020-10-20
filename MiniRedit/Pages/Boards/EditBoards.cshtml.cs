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
    public class EditBoardsModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<EditBoardsModel> _logger;

        public EditBoardsModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<EditBoardsModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        #region Prop
        [BindProperty]
        public string Title { get; set; }
        public BoardsDTO Boards { get; set; }
        #endregion


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


        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                Boards = await _boardsServices.GetOneBoard(id);

                if (Title != null)
                {
                    Boards.Title = Title;
                    await _boardsServices.UpdateBoard(Boards);
                }
                else
                    await _boardsServices.UpdateBoard(Boards);

                return RedirectToPage("ViewAllBoards");
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            
        }
    }
}
