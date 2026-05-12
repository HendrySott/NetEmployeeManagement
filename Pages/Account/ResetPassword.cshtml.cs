
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using NetEmployee.Model;

namespace NetEmployee.Pages.Account;

public class ResetPassword : PageModel
{
    
    private readonly DataServicesAuthentication _auth;
    private readonly AccountController _ctr;

    public ResetPassword(DataServicesAuthentication auth, AccountController ctr)
    {
        _auth = auth;
        _ctr = ctr;
    }
    [BindProperty]
    public AuthenticationModel Input { get; set; }
    public PersonProfile pp { get; set; }

    public async Task<IActionResult> OnGetAsync(){
        var cookie = _ctr.GetCookieUsingKey("CODE");
            if (cookie == null)
            {
                return Page();
            }
            else
            {
                return Redirect("/Home");
            }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrEmpty(Input.EMAIL))
        {


            var user = _auth.userLogin(Input);
            var emailandid = _auth.TestUserExistanceByID(pp.IDENTIFICATION, pp.PERSONAL_EMAIL);
            if (user.Count() <= 0)
            {
                ModelState.AddModelError(string.Empty, "Contraseña o correo incorrectos.");
                return Page();
            }
            else
            {
                if (emailandid.Count() <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Contraseña o correo incorrectos.");
                    return Page();
                }
                else
                {
                    foreach (var item in _auth.TestUserExistanceByID(pp.IDENTIFICATION, pp.PERSONAL_EMAIL))
                    {
                        _ctr.SetCookies("CODE", item.CODE.ToString(), 5000);
                        _ctr.SetCookies("INSTITUTION", item.INSTITUTION, 5000);
                    }

                    return RedirectToPage("./NewPswd");


                }
            }


            

        }
        return Page();
    }

}