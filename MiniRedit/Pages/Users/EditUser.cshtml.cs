using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Conventions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MiniReditServices.DTO;
using MiniReditServices.Interfaces;

namespace MiniRedit.Pages.Users
{
    public class EditUserModel : PageModel
    {
        #region Feltes and ctor
        private readonly IBoardsServices _boardsServices;
        private readonly IPostsServices _postsServices;
        private readonly IUsersServices _usersServices;
        private readonly ILogger<EditUserModel> _logger;

        public EditUserModel(IBoardsServices boardsServices, IPostsServices postsServices, IUsersServices usersServices, ILogger<EditUserModel> logger)
        {
            _boardsServices = boardsServices;
            _postsServices = postsServices;
            _usersServices = usersServices;
            _logger = logger;
        }
        #endregion


        [BindProperty]
        public UsersDTO User { get; set; }
        public UsersDTO UserEdit { get; set; }

        public async Task OnGetAsync(int userid)
        {
            User = await _usersServices.GetUserById(userid);
        }

        public async Task<IActionResult> OnPostAsync(int userid)
        {
            User = await _usersServices.GetUserById(userid);

            if (UserEdit == null)
            {
                await _usersServices.UpdateUser(User);
                return RedirectToPage("UserPage", new { userid });
            }

            #region if 1 perameter
            if (UserEdit.Name != null)
            {
                User.Name = UserEdit.Name;
                await _usersServices.UpdateUser(User);
                return RedirectToPage("UserPage", new { userid });
            }
            if (UserEdit.UserName != null)
            {
                User.UserName = UserEdit.UserName;
                await _usersServices.UpdateUser(User);
                return RedirectToPage("UserPage", new { userid });
            }
            if (UserEdit.Password != null)
            {
                User.Password = UserEdit.Password;
                await _usersServices.UpdateUser(User);
                return RedirectToPage("UserPage", new { userid });
            }
            #endregion

            #region if 2 perameter
            if (UserEdit.Password != null && UserEdit.UserName != null)
            {
                User.UserName = UserEdit.UserName;
                User.Password = UserEdit.Password;
                await _usersServices.UpdateUser(User);
                return RedirectToPage("UserPage", new { userid });
            }
            if (UserEdit.Name != null && UserEdit.UserName != null)
            {
                User.UserName = UserEdit.UserName;
                User.Name = UserEdit.Name;
                await _usersServices.UpdateUser(User);
                return RedirectToPage("UserPage", new { userid });
            }
            if (UserEdit.Password != null && UserEdit.Name != null)
            {
                User.Name = UserEdit.Name;
                User.Password = UserEdit.Password;
                await _usersServices.UpdateUser(User);
                return RedirectToPage("UserPage", new { userid });
            }
            #endregion

            else
            {
                await _usersServices.UpdateUser(User);
                return RedirectToPage("UserPage", new { userid });

            }

            return RedirectToPage("UserPage", new { userid });
        }
    }
}
