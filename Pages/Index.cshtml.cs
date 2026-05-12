using System.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using NetEmployee.Model;

namespace NetEmployee.Pages;

public class IndexModel : PageModel
{
    [BindProperty]public string Command { get; set; }
    private readonly DataServices _data;
    private readonly AccountController _ctr;
    private readonly IHttpContextAccessor _httpContextAccessor;
    [BindProperty]public Communication communication{get;set;}
    [BindProperty]public PersonalTodoTask personalTk {get;set;}
    [BindProperty]public VacationPlan vacationPlan {get;set;}
    [BindProperty]public MedicalLicenceRegitry medicalLicence {get;set;}
    [BindProperty]public Notifications notifications {get;set;}

    [BindProperty]public Absence absence {get;set;}


    public IndexModel(DataServices data, AccountController ctr,IHttpContextAccessor httpContextAccessor)
    {
        _data= data;
        _ctr = ctr;
        _httpContextAccessor = httpContextAccessor;
    }

    [Authorize]
    public async Task<IActionResult> OnGetAsync()
    {

        if (!User.Identity.IsAuthenticated){
            return RedirectToPage("/Account/Login");
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
            case "authVacation":
                vacationPlan.INSTITUTION = institution;
                vacationPlan.AUTHORIZED_BY = int.Parse(code);
                vacationPlan.ID = Convert.ToInt32(communication.Data);
                //normal =0, aproved = 1, refused = 3 
                vacationPlan.STATE = 1;
                _data.AuthorizeVacation(vacationPlan);

                notifications.CODE = int.Parse(code);
                notifications.INSTITUTION = institution;
                notifications.DATA = "Your vacation has been aproved";
                notifications.STATUS = false;
                _data.AddItemToNotification(notifications);
            break;
            case "VacationDecline":
                vacationPlan.INSTITUTION = institution;
                vacationPlan.AUTHORIZED_BY = int.Parse(code);
                vacationPlan.ID = Convert.ToInt32(communication.Data);
                //normal =0, aproved = 1, refused = 3 
                vacationPlan.STATE = 3;
                _data.AuthorizeVacation(vacationPlan);
            break;
             case "LicenseDecline":
                medicalLicence.INSTITUTION = institution;
                medicalLicence.AUTHORIZED_BY = int.Parse(code);
                medicalLicence.ID = Convert.ToInt32(communication.Data);
                //normal =0, aproved = 1, refused = 3 
                medicalLicence.STATE = 3;
                _data.AuthorizeMedicalRegister(medicalLicence);
            break;
            case "authLicense":
                medicalLicence.INSTITUTION = institution;
                medicalLicence.AUTHORIZED_BY = int.Parse(code);
                medicalLicence.ID = Convert.ToInt32(communication.Data);
                //normal =0, aproved = 1, refused = 3 
                medicalLicence.STATE = 1;
                _data.AuthorizeMedicalRegister(medicalLicence);
            break;
            case "ReportAbsence":
                absence.REPORTED_BY = int.Parse(code);
                absence.INSTITUTION = institution;
                absence.STATUS = false;
                _data.ReportAbsence(absence);
                
            break;
        }
        
        return Page();
    }



}
