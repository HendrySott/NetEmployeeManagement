using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Web;
using NetEmployee.Data;
using NetEmployee.Model;

namespace NetEmployee.Pages;
public class MedicalLicence : PageModel{

    private readonly DataServices _data;
    private readonly AccountController _ctr;
    private readonly IWebHostEnvironment _environment;

    [BindProperty]public Communication communication{get;set;}
    [BindProperty]public MedicalLicenceRegitry medical{get;set;}
    [BindProperty]public IFormFile File { get; set; }
    public MedicalLicence(DataServices data, AccountController ctr,IWebHostEnvironment environment){
        _data = data;
        _ctr = ctr;
        _environment = environment;
    }

    public async Task<IActionResult> OnGetAsync(){
        if (!User.Identity.IsAuthenticated){
            return RedirectToPage("Index");
        }
        return Page();
    }
    public async Task<IActionResult> OnPostAsync(){

        var institution = User.FindFirst(ClaimTypes.Hash)?.Value;
        var code = User.FindFirst(ClaimTypes.Actor)?.Value;
        switch(communication.Command){
            case "lisence":

                if (File != null && File.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Private/Files/License", File.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await File.CopyToAsync(stream);
                    }
                }
                
            
                medical.INSTITUTION = institution;
                medical.CODE = int.Parse(code);
                medical.AUTHORIZED_BY = 0;
                medical.STATE = 0;
                medical.BACKUP = File.FileName;
                _data.RegisterMedicalLicense(medical);
                return Page();

            break;
            case "timeOnTracker":
                communication.code = int.Parse(code);
                communication.ins = institution;
                _ctr.SetTimeOnTracker(communication);
                return Page();
            break;

        }
        

  
        return Page();
    }
}