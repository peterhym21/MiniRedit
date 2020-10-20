using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MiniReditServices.DTO;
using MiniReditServices.Interfaces;

namespace MiniRedit.Pages.Users
{
    public class DeleteUserModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<DeleteUserModel> _logger;

        public DeleteUserModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<DeleteUserModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion


        [BindProperty]
        public UsersDTO User { get; set; }

        public async Task<IActionResult> OnGetAsync(int userid)
        {
            try
            {
                User = await _usersServices.GetUserById(userid);
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            
        }

        public async Task<IActionResult> OnPostAsync(int userid)
        {
            try
            {
                await _usersServices.DeleteUser(userid);
                return RedirectToPage("../Index");
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            
        }
    }
}
