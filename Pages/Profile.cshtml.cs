
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using NetEmployee.Model;
using NetEmployee.Data;
using System.Security.Claims;



namespace NetEmployee.Pages;

public class Profile : PageModel
{
    private readonly DataServices _data;
    private readonly AccountController _ctr;

    public Profile(DataServices data, AccountController ctr)
    {
        _data= data;
        _ctr = ctr;
    }

    [BindProperty] public PersonProfile upd { get; set; } = default!;
    [BindProperty] public Communication communication { get; set; } = default!;
    public IEnumerable<PersonProfile> prs { get; set; } = default!;
    [BindProperty] public string comm { get; set; }

    [BindProperty] public int codex { get; set; }
    public async Task<IActionResult> OnGetAsync(){
        if (!User.Identity.IsAuthenticated){
            return RedirectToPage("Index");
        }
        return Page();
    }
    public async Task<IActionResult> OnPostAsync(int codex)
    {
        var ins =  @User.FindFirst(ClaimTypes.Hash)?.Value;
        var code =  @User.FindFirst(ClaimTypes.Actor)?.Value;
        switch(communication.Command){
            case "updC":
                upd.INSTITUTION = ins;
                upd.CODE = int.Parse(code);
                _data.UpdatePersonProfileGrade(upd);
                return Page();
            break;
            case "updAll":
                upd.INSTITUTION = ins;
                upd.CODE = int.Parse(code);
                _data.UpdatePersonProfile(upd);
                return Page();
            break;
            case "updSkils":
                upd.INSTITUTION = ins;
                upd.CODE = int.Parse(code);
                _data.UpdateSkills(upd);
                return Page();
            break;
            case "timeOnTracker":
                
                communication.code = int.Parse(code);
                communication.ins = ins;
                _ctr.SetTimeOnTracker(communication);
            break;
        }
        

        return Page();
    }


    
}