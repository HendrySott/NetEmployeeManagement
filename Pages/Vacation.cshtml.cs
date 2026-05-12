using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using NetEmployee.Model;

namespace NetEmployee.Pages;
public class Vacation : PageModel{
    private readonly DataServices _data;
    private readonly AccountController _ctr;
    [BindProperty]public Communication communication{get;set;}
    public Vacation(DataServices data, AccountController ctr){
        _data = data;
        _ctr = ctr;
    }

    [BindProperty]public VacationPlan vacationP{get;set;}
    public async Task<IActionResult> OnGetAsync(){
        if (!User.Identity.IsAuthenticated){
            return RedirectToPage("Index");
        }
        return Page();
    }
    

    public async Task<IActionResult> OnPostAsync(){

        var institution = User.FindFirst(ClaimTypes.Hash)?.Value;
        var code = User.FindFirst(ClaimTypes.Actor)?.Value;
        

        

        if(communication.Command.Contains("timeOnTracker")){
            
           communication.code = int.Parse(code);
            communication.ins = institution;
            _ctr.SetTimeOnTracker(communication);
            
        }
        
        vacationP.INSTITUTION = institution;
        vacationP.CODE = int.Parse(code);
        vacationP.AUTHORIZED_BY = 0;
        
        vacationP.STATE = 0;

        _data.BookVacation(vacationP);
        return Page();
    }
}