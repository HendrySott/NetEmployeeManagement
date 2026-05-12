using System.ComponentModel.DataAnnotations;

namespace NetEmployee.Model;


public class AuthenticationModel
{
    public int ID { get; set; }
    public string INSTIITUTION { get; set; }
    public int CODE { get; set; }
    
    [Required,EmailAddress]
    public string EMAIL { get; set; }= string.Empty;
    
    [Required, MinLength(5)]
    public string PASSWORD { get; set; }
    
  
    public string ROLE { get; set; }

    public int STATE { get; set; }

    public string PERSONAL_EMAIL { get; set; }

    public bool REMEMBERME{get;set;}

   

}

public class Autentication
{
    public int ID { get; set; }
    public int UID { get; set; }
    public string EMAIL { get; set; }
    public string PASSWORD { get; set; }
    public int STATE { get; set; }
}