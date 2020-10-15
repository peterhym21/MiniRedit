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
    public class ViewAllBoardsModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<ViewAllBoardsModel> _logger;

        public ViewAllBoardsModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<ViewAllBoardsModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        public int SelectetId { get; set; }

        public List<BoardsDTO> Boards { get; set; }


        public async Task OnGetAsync()
        {
            Boards = await _boardsServices.GetBoards();
        }
    }
}
