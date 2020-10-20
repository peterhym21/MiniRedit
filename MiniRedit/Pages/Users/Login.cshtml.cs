using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MiniReditServices.DTO;
using MiniReditServices.Interfaces;

namespace MiniRedit.Pages.Users
{
    public class LoginModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<LoginModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        #region Prop
        [BindProperty, Required]
        public string Username { get; set; }
        [BindProperty, Required]
        public string Password { get; set; }
        public UsersDTO User { get; set; }
        #endregion

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                User = await _usersServices.GetUserlogin(Username, Password);
                if (User == null)
                    return RedirectToPage("../Error");
                else
                    return RedirectToPage("UserPage", new { userid = User.UserId });
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            
        }

        public IActionResult OnPostCreate()
        {
            return RedirectToPage("CreateUser");
        }
    }
}
