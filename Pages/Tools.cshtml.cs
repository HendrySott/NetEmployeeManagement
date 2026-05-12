using System.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using NetEmployee.Model;

namespace NetEmployee.Pages;

public class Tools : PageModel
{
    [BindProperty]public string Command { get; set; }
    private readonly DataServices _data;
    private readonly AccountController _ctr;
    private readonly IHttpContextAccessor _httpContextAccessor;
    [BindProperty]public Communication communication{get;set;}
    [BindProperty]public PersonalTodoTask personalTk {get;set;}


    public Tools(DataServices data, AccountController ctr,IHttpContextAccessor httpContextAccessor)
    {
        _data= data;
        _ctr = ctr;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize]
    public async Task<IActionResult> OnGetAsync(){
        if (!User.Identity.IsAuthenticated){
            return RedirectToPage("Index");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var institution = User.FindFirst(ClaimTypes.Hash)?.Value;
        var code = User.FindFirst(ClaimTypes.Actor)?.Value;
        

        

        switch(communication.Command){
            case "PersonalTask":
                personalTk.VALUE = "n";
                personalTk.INSTITUTION = institution;
                personalTk.CODE =  int.Parse(code);
                _data.SavePersonalTaskProd(personalTk);
            break;
            case "timeOnTracker":
                
                communication.code = int.Parse(code);
                communication.ins = institution;
                _ctr.SetTimeOnTracker(communication);
            break;
        }
        
        return Page();
    }



}
