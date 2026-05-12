using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetEmployee.Data;
using NetEmployee.Model;

namespace NetEmployee.Pages.Account;
public class SigninModel :PageModel{
    //private readonly ILogger<LoginModel> _logger;
        
       
        [BindProperty] public PersonProfile person { get; set; }
        [BindProperty] public AuthenticationModel auth { get; set; }
        [BindProperty] public SetupModel setup { get; set; }
        [BindProperty] public string psw { get; set; }
        public string ReturnUrl { get; private set; }
        
        private readonly DataServicesAuthentication _auth;
        private readonly AccountController _ctr;



        public SigninModel(DataServicesAuthentication auth,AccountController ctr)
        {
           
            _auth = auth;
            _ctr = ctr;
            
        }
        private static Random random = new Random();

        //todo get in this form just the email and password, the sistem send an email to the email registred

        public static string RandomInt32Generator(int length)
        {
            const string chars = "123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomStringGenerator(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<IActionResult> OnGetAsync()
        {
            
            try{
                _ctr.Remove("INSTITUTION");
                    _ctr.Remove("CODE");
                    _ctr.Remove("INSTITUTION_NAME");
                    _ctr.Remove("NAME");
                    _ctr.Remove("MACHINE");
            }catch(Exception e){

            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var code = RandomInt32Generator(8);
            var ins = RandomStringGenerator(32);
            try
            {
                var user = _auth.userLogin(auth);

                if (user.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, "Este email ya esta registrado, porfavor intente con otro email.");
                    return Page();
                }
                else
                {
                    person.INSTITUTION = ins;
                    person.CODE = Convert.ToInt32(code);;
                    if (string.IsNullOrEmpty(person.S_NAME))
                    {
                        person.S_NAME = " ";
                    }
                    
                  
                    if (string.IsNullOrEmpty(person.S_LASTN))
                    {
                        person.S_LASTN = " ";
                    }
                    
                    person.GRADE = "-";
                    person.ADDRESS = "-";
                    person.PHONE = "0";
                    person.IDENTIFICATION = "n/a";
                    person.PERSONAL_EMAIL = auth.EMAIL;
                    person.TEAM_ID = 0;
                    person.POSITION_KEY = 0;
                    person.STREET = "n/a";
                    person.HOME = 0;
                    person.COUNTY = "n/a";
                    person.COUNTRY = "n/a";
                    
                    
                    
                    auth.INSTIITUTION = person.INSTITUTION;
                    setup.INSTITUTION = person.INSTITUTION;
                    auth.CODE = person.CODE;
                    
                    _auth.IRegisterNewPerson(person);
                    //string InstitutionalEmail = person.F_NAME + person.S_NAME + "." + person.F_LASTN + person.S_LASTN + "@adm.com";
                    //auth.EMAIL = InstitutionalEmail.ToLower();

                    auth.STATE = 777;
                    _auth.IRegisterNewUser(auth);
                
                    setup.INSTITUTION = ins;
                    _auth.ISetup(setup);
                    
                    _auth.IncertSystemAccessPolicy(code, ins);
                    
                    
                    
                    ////////////
                    
                    
                                        foreach (var usr in _auth.ILoadAllUserByUID(Convert.ToInt32(code)))
                                        {
                                            _ctr.SetCookies("CODE", usr.CODE.ToString(), 5000);
                                            _ctr.SetCookies("NAME", usr.F_NAME, 5000);
                                            _ctr.SetCookies("INSTITUTION", usr.INSTITUTION, 5000);
                                            _ctr.SetCookies("USR_NAME", usr.F_NAME, 5000);
                                        }
                        
                                      
                    
                    ///////////

               
                    _auth.InstitutionIntance(ins);
                
                    
                    
                    ///Test if the cookies has beennnn savae
                    ///
                    return RedirectToPage("./Welcome");



                }
                
                
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Page();
        }
}