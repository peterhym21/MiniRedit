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
    public class SeAllPostsModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<SeAllPostsModel> _logger;

        public SeAllPostsModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<SeAllPostsModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        public int SelectedCategoryId { get; set; }
        public List<PostsDTO> Posts { get; set; }
        public List<BoardsDTO> Boards { get; set; }


        public async Task OnGetAsync()
        {
            Posts = await _postsServices.GetPosts();
            Boards = await _boardsServices.GetBoards();
        }
    }
}
