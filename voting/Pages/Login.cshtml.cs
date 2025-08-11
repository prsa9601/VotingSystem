using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Security.Claims;
using VotingLibrary.Data.Entities;

namespace voting.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (UserName == "AdminReza" && Password == "@PassAdmin1404")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "Reza"),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(claimPrincipal);
                return Redirect("/Admin/Election");
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
  

    }
}
