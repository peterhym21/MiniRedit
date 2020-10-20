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
    public class EditPostsModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<EditPostsModel> _logger;

        public EditPostsModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<EditPostsModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        [BindProperty]
        public PostsDTO PostEdit { get; set; }

        public PostsDTO Post { get; set; }

        public BoardsDTO Board { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Post = await _postsServices.GetPostById(id);
                Board = await _boardsServices.GetOneBoard(Post.BoardId);
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
                Post = await _postsServices.GetPostById(id);
                if (PostEdit == null)
                {
                    await _postsServices.UpdatePost(Post);
                    return RedirectToPage("PostDetails", new { id = id });
                }
                if (PostEdit.Title != null)
                {
                    Post.Title = PostEdit.Title;
                    await _postsServices.UpdatePost(Post);
                    return RedirectToPage("PostDetails", new { id = id });
                }
                if (PostEdit.Content != null)
                {
                    Post.Content = PostEdit.Content;
                    await _postsServices.UpdatePost(Post);
                    return RedirectToPage("PostDetails", new { id = id });
                }
                else
                {
                    Post.Title = PostEdit.Title;
                    Post.Content = PostEdit.Content;
                    await _postsServices.UpdatePost(Post);
                    return RedirectToPage("PostDetails", new { id = id });
                }
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            
        }

    }
}
