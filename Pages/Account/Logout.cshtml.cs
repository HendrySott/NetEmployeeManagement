
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetEmployee.Pages.Account
{
	public class Logout : PageModel
    {

        private readonly IAuthenticationService _authenticationService;

        public Logout(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }

                return RedirectPermanent("https://account.syscrafter.com/account/logout");
            }else{
                return RedirectPermanent("https://account.syscrafter.com/account/logout");
            }

            
        }
    }
}
