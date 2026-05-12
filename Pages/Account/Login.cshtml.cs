using System.Security.Claims;
using System.Xml;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using NetEmployee.Model;
using NetEmployee.Data;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Asn1.Misc;
using MySqlX.XDevAPI.Common;


namespace NetEmployee.Pages.Account;
public class LoginModel : PageModel
{
    private readonly AccountController _ctr;
    private readonly DataServices _data;
    public LoginModel(AccountController ctr,DataServices data)
    {
       _data = data;
     
        _ctr = ctr;
    }

    [BindProperty]public Autentication Input { get; set; }
    [BindProperty]public string ReturnUrl { get; set; }
    [BindProperty]public AuthenticationModel ModelInput { get; set; }
    [TempData] public string ErrorMessage { get; set; }

        
        // public async Task<IActionResult> OnGetAsync()
        // {

        //     var cookie = _ctr.GetCookieUsingKey("CODE");
        //     if (cookie == null)
        //     {
        //         return Page();
        //     }
        //     else
        //     {
        //         return Redirect("~/Dashboard");
        //     }
        // }


        public async Task<IActionResult> OnGetAsync(int id, string ins)
    {
       
        string role = "";
        string pswx = "";
        string inx = "";
        int codex = 0;

        
        // if (!User.Identity.IsAuthenticated){
        //     return Page();
        // }

        var validator = _data.validateUser(id);
        if(validator.Count() > 0 ){
            foreach(var item in validator){
                codex = item.Code;
                inx = item.Institution ;
            }
        }

            var cookieOptions = new CookieOptions
            {
            Expires = DateTime.Now.AddHours(12), // Cookie expiration
            Path = "/", // Limit cookie to the root path
            HttpOnly = true, // Cookie not accessible via JavaScript
            Secure = true, // Send cookie only over HTTPS (if HTTPS is used)
            SameSite = SameSiteMode.Strict // Adjust based on your requirements


            };


        if(codex != null && inx != null){


           var psw = _data.LoadInstitutionDetails(inx);
                        if (psw.Count() > 0)

                        {
                            

                                
                                        foreach (var usr in _data.ILoadAllUserByUID(inx, codex))
                                        {
                                            // SetCookies("CODE", usr.CODE.ToString(), 12);
                                            // SetCookies("INSTITUTION", usr.INSTITUTION, 12);
                                            
                                            
                                            if(usr.F_NAME.ToString() != null){ Response.Cookies.Append("fname", usr.F_NAME.ToString(), cookieOptions);}
                                            if(usr.S_NAME.ToString() != null){ Response.Cookies.Append("sname", usr.S_NAME.ToString(), cookieOptions);}
                                            if(usr.F_LASTN.ToString() != null){ Response.Cookies.Append("flastname", usr.F_LASTN.ToString(), cookieOptions);}
                                            if(usr.S_LASTN.ToString() != null){ Response.Cookies.Append("slastname", usr.S_LASTN.ToString(), cookieOptions);}
                                            if(usr.PHONE.ToString() != null){ Response.Cookies.Append("phone", usr.PHONE.ToString(), cookieOptions);}
                                            if(usr.ADMITION_DATE.ToString() != null){ Response.Cookies.Append("admitionDate", usr.ADMITION_DATE.ToString(), cookieOptions);}
                                            if(usr.TEAM_ID.ToString() != null){ Response.Cookies.Append("team", usr.TEAM_ID.ToString(), cookieOptions);}
                                            if(usr.POSITION_KEY.ToString() != null){ 
                                                foreach(var item in _data.GetPositionById(ins, usr.POSITION_KEY)){
                                                    Response.Cookies.Append("positionName", item.POSITION_NAME.ToString(), cookieOptions);
                                                    ModelState.AddModelError(string.Empty, item.POSITION_NAME.ToString());

                                                }
                                                Response.Cookies.Append("positionId", usr.POSITION_KEY.ToString(), cookieOptions);
                                                
                                            }
                                            if(usr.PERSONAL_EMAIL.ToString() != null){Response.Cookies.Append("personalEmail", usr.PERSONAL_EMAIL.ToString(), cookieOptions);}


                                            
                                            
                                        }
                        
                                        ///intitution name cookies,
                                        foreach (var inst in _data.GetInstitutionNameUsingInstitutionCode(inx))
                                        {
                                            
                                            if(inst.NAME.ToString() != null){Response.Cookies.Append("institutionName", inst.NAME.ToString(), cookieOptions);}
                                            if(inst.Tax_ID.ToString() != null){Response.Cookies.Append("rnc", inst.Tax_ID.ToString(), cookieOptions);}
                                        }
                                        
                                        var claims = new List<Claim>

                                        {
                                            new Claim(ClaimTypes.Actor, id.ToString()),
                                            new Claim(ClaimTypes.Hash, ins.ToString()),
                                            // new Claim(ClaimTypes.Role, role.ToString()),
                                            // new Claim(ClaimTypes.UserData, pswx.ToString())

                                        };

                                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                                        var authProperties = new AuthenticationProperties
                                        {
                                            // IsPersistent = ModelInput.REMEMBERME, // Set as needed
                                            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12) // Set expiration time as needed
                                        };

                                        await HttpContext.SignInAsync(
                                            CookieAuthenticationDefaults.AuthenticationScheme,
                                            new ClaimsPrincipal(claimsIdentity),
                                            authProperties);

                                        return LocalRedirect("/Index");
                                      
                                    
                                    // return RedirectToPage("./Login");
                                
                        }



        }
        return Page();
    }

    
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostAsync()
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddHours(12), // Cookie expiration
            Path = "/", // Limit cookie to the root path
            HttpOnly = true, // Cookie not accessible via JavaScript
            Secure = true, // Send cookie only over HTTPS (if HTTPS is used)
            SameSite = SameSiteMode.None // Adjust based on your requirements
        };

        var user = _data.userLogin(ModelInput);
            int code =0;
            string ins = "";
            string role = "";
            string insemail = "";
            string pswx = "";

        if(user.Count()>0){
            try{
                 
                    foreach (var i in user)
                    {
                        
                        if(i.EMAIL.ToString() != null){ Response.Cookies.Append("email", i.EMAIL.ToString(), cookieOptions);}
                        if(i.ROLE.ToString() != null){ role =  i.ROLE.ToString();}
                        // if(i.PASSWORD.ToString() != null){ pswx =  i.PASSWORD.ToString();}
                        
                        var psw = _data.LoadInstitutionDetails(i.INSTIITUTION);
                        if (psw.Count() > 0)
                        {
                            foreach (var name in psw)
                            {
                                if (name.DEF_PASS == ModelInput.PASSWORD)
                                {
                                    return RedirectToPage("/Account/NewPswd");
                                }
                            }

                                foreach (var its in _data.SystemAccessPolicy(i.INSTIITUTION, i.CODE))
                                {
                                    if (its.STATE == "disabled")
                                    {
                                        return RedirectToPage("/Account/AccessDenied");
                                    }
                                    else
                                    {
                                        foreach (var usr in _data.ILoadAllUserByUID(i.INSTIITUTION, i.CODE))
                                        {
                                            // SetCookies("CODE", usr.CODE.ToString(), 12);
                                            // SetCookies("INSTITUTION", usr.INSTITUTION, 12);
                                            code = usr.CODE;
                                            ins = usr.INSTITUTION;
                                            
                                            if(usr.F_NAME.ToString() != null){ Response.Cookies.Append("fname", usr.F_NAME.ToString(), cookieOptions);}
                                            if(usr.S_NAME.ToString() != null){ Response.Cookies.Append("sname", usr.S_NAME.ToString(), cookieOptions);}
                                            if(usr.F_LASTN.ToString() != null){ Response.Cookies.Append("flastname", usr.F_LASTN.ToString(), cookieOptions);}
                                            if(usr.S_LASTN.ToString() != null){ Response.Cookies.Append("slastname", usr.S_LASTN.ToString(), cookieOptions);}
                                            if(usr.PHONE.ToString() != null){ Response.Cookies.Append("phone", usr.PHONE.ToString(), cookieOptions);}
                                            if(usr.ADMITION_DATE.ToString() != null){ Response.Cookies.Append("admitionDate", usr.ADMITION_DATE.ToString(), cookieOptions);}
                                            if(usr.TEAM_ID.ToString() != null){ Response.Cookies.Append("team", usr.TEAM_ID.ToString(), cookieOptions);}
                                            if(usr.POSITION_KEY.ToString() != null){ 
                                                foreach(var item in _data.GetPositionById(i.INSTIITUTION, usr.POSITION_KEY)){
                                                    Response.Cookies.Append("positionName", item.POSITION_NAME.ToString(), cookieOptions);
                                                }
                                                Response.Cookies.Append("positionId", usr.POSITION_KEY.ToString(), cookieOptions);
                                                
                                            }
                                            if(usr.PERSONAL_EMAIL.ToString() != null){Response.Cookies.Append("personalEmail", usr.PERSONAL_EMAIL.ToString(), cookieOptions);}


                                            
                                            
                                        }
                        
                                        ///intitution name cookies,
                                        foreach (var inst in _data.GetInstitutionNameUsingInstitutionCode(i.INSTIITUTION))
                                        {
                                            
                                            if(inst.NAME.ToString() != null){Response.Cookies.Append("institutionName", inst.NAME.ToString(), cookieOptions);}
                                            if(inst.RNC.ToString() != null){Response.Cookies.Append("rnc", inst.RNC.ToString(), cookieOptions);}
                                        }
                                        
                                        


                                        var claims = new List<Claim>

                                        {
                                            new Claim(ClaimTypes.Actor, code.ToString()),
                                            new Claim(ClaimTypes.Hash, ins.ToString()),
                                            new Claim(ClaimTypes.Role, role.ToString()),
                                            new Claim(ClaimTypes.UserData, pswx.ToString())

                                        };

                                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                                        var authProperties = new AuthenticationProperties
                                        {
                                            IsPersistent = ModelInput.REMEMBERME, // Set as needed
                                            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12) // Set expiration time as needed
                                        };

                                        await HttpContext.SignInAsync(
                                            CookieAuthenticationDefaults.AuthenticationScheme,
                                            new ClaimsPrincipal(claimsIdentity),
                                            authProperties);

                                        
                                        
                                        return LocalRedirect("/Index");   
                                        

                                        
                                      
                                    }
                                    // return RedirectToPage("./Login");
                                }
                            
                        }
                    }


            }catch(Exception e){
                // Console.WriteLine(e);
            }
                   

        }else{
            ModelState.AddModelError(string.Empty, "Forgot my password? Please communicate with your organization's IT team");
                    
        }


        return Page();
    }


}
