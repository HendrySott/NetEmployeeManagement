using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using NetEmployee.Model;

namespace NetEmployee.Pages;
public class Payroll : PageModel{
    
    private readonly DataServices _data;
    private readonly AccountController _ctr;
    [BindProperty]public Communication communication{get;set;}
    public Payroll( DataServices data, AccountController ctr){
        _data = data;
        _ctr = ctr;
    }
    public async Task<IActionResult> OnGetAsync(){
        if (!User.Identity.IsAuthenticated){
            return RedirectToPage("Index");
        }
        return Page();
    }
    public async Task<IActionResult> OnPostAsync(){
        var institution =  @User.FindFirst(ClaimTypes.Hash)?.Value;
        var code =  @User.FindFirst(ClaimTypes.Actor)?.Value;
        if(communication.Command.Contains("timeOnTracker")){
            
           communication.code = int.Parse(code);
            communication.ins = institution;
            _ctr.SetTimeOnTracker(communication);
            
        }
        return Page();
    }
    
}