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

namespace MiniRedit.Pages.Boards
{
    public class ViewBoardPostModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<ViewBoardPostModel> _logger;

        public ViewBoardPostModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<ViewBoardPostModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        #region Prop
        [BindProperty(SupportsGet = true)]
        public int SelectedId { get; set; }
        public SelectList BoardSelect { get; set; }
        public List<PostsDTO> Posts { get; set; }
        public BoardsDTO Boards { get; set; }
        #endregion


        public async Task<IActionResult> OnGetAsync(int SelectedId)
        {
            try
            {
                Posts = await _postsServices.GetPostsByBoards(SelectedId);
                Boards = await _boardsServices.GetOneBoard(Posts[0].BoardId);
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            
        }
    }
}
