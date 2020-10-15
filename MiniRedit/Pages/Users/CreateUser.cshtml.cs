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
    public class CreateUserModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<CreateUserModel> _logger;

        public CreateUserModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<CreateUserModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion

        public int Userid { get; set; }

        [BindProperty, Required]
        public UsersDTO User { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

            }
            else
            {
                Userid = await _usersServices.CreateUseres(User);
                if (User == null)
                    return RedirectToPage("../Error");
                else
                    return RedirectToPage("UserPage", new { userid = User.UserId });
            }

            return Page();
        }
    }
}
