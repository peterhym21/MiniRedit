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

        public SelectList BoardsSelect { get; set; }
        public int SelectedId { get; set; }
        public List<PostsDTO> Posts { get; set; }
        public List<BoardsDTO> Boards { get; set; }
        public UsersDTO User { get; set; }




        public async Task OnGetAsync(int userid)
        {
            if (userid == 0)
                userid = 1;
            User = await _usersServices.GetUserById(userid);
            BoardsSelect = new SelectList(await _boardsServices.GetBoards(), "BoardId", "Title");
            Posts = await _postsServices.GetTopTen();
            Boards = await _boardsServices.GetBoards();
        }
    }
}
