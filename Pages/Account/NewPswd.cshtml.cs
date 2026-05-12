using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using NetEmployee.Model;

namespace NetEmployee.Pages.Account;

public class NewPswd : PageModel
{
    
    private readonly DataServicesAuthentication _auth;
    private readonly AccountController _ctr;

    public NewPswd(DataServicesAuthentication auth, AccountController ctr)
    {
        
        _auth = auth;
        _ctr = ctr;
    }
    
    [BindProperty]
    public AuthenticationModel newdata { get; set; }
    [BindProperty]
    public string confirm { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {

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
        var user = _auth.ValidateEmail(newdata);
        
        if (user.Count() == 0)
        {
            ModelState.AddModelError(string.Empty, "Correo institucional inexistente.");
            return Page();
        }
        if (newdata.PASSWORD == confirm)
        {
            foreach (var item in user)
            {
                _auth.IUpdatePassword(newdata.PASSWORD, item.CODE, item.INSTIITUTION);
                return RedirectToPage("./Login");
            }
            
        }
        else
        {
            ModelState.AddModelError(string.Empty, "La contraseñas no son iguales.");
        }
        return Page();
    }
}