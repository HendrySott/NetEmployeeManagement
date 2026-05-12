
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NetEmployee.Model;
using System.Security.Claims;


namespace NetEmployee.Data;

[Route("[controller]/[action]")]
public class AccountController : Controller
{
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataServices _data;
        public AccountController(ILogger<AccountController> logger,  IHttpContextAccessor httpContextAccessor,DataServices data)
        {
           _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _data= data;
        }

        [Authorize]
        public IActionResult Secure()
        {
            return View();
        }


    [HttpPost]
    public IActionResult ProcessForm([FromBody] Communication model)
    {
        if (ModelState.IsValid)
        {
            // Process the form data (e.g., save to database, send email, etc.)
            // Example: Save to a database
            // YourDatabaseContext.Save(model);

            return Json(new { success = true, message = "Form submitted successfully!" });
        }

        // If model state is not valid, return error response
        return Json(new { success = false, message = "Invalid input. Please check the form and try again." });
    }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home"); // Redirect to home page or any other page after logout
        }


        public IActionResult ReadSession(string key)
        {
            var sessionValue = HttpContext.Session.GetString(key);
            ViewData[key] = sessionValue;
            return View();
        }


        /// <summary>
        /// Delete cookie from cash register
        /// </summary>
        public void DeleteCookieFromCashRegister()
        {
            //Delete the Cookie from Browser.
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("MACHINE");

            _httpContextAccessor.HttpContext.Response.Cookies.Append("MACHINE", "", new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(-1)
            });
            //Thread.Sleep(5000);

            //foreach (var cookie in HttpContext.Request.Cookies)
            //{
            //    Response.Cookies.Delete(cookie.Key);
            //}

        }

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void SetCookies(string name,string value, int expireTime)
        {
            
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddHours(expireTime), // Cookie expiration
                Path = "/", // Limit cookie to the root path
                HttpOnly = true, // Cookie not accessible via JavaScript
                Secure = true, // Send cookie only over HTTPS (if HTTPS is used)
                SameSite = SameSiteMode.None // Adjust based on your requirements
            };

            Response.Cookies.Append(name, value, cookieOptions);
            
        }
        

        /// <summary>
        /// get cookies stored by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string? GetCookieUsingKey(string key)
        {
            var cookieValue = "";
            if (Request.Cookies[key] != null)
            {
                cookieValue = Request.Cookies[key];
            }
            return cookieValue; 
        }
         public string? GetCookie(string key)
        {
            var cookieValue = "";
            if (Request.Cookies[key] != null)
            {
                cookieValue = Request.Cookies[key];
            }
            return cookieValue; 
        }

    public string GetClaimRole()
    {
        // Retrieve the Email claim value
        string role = "";

        if (!string.IsNullOrEmpty(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value))
        {
            role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            
        }

        // Proceed with the action logic if Email claim exists and is not empty
        ViewData["role"] = role;
        return role;
    }
    // [Authorize]

    public string GetSession(string key)
    {
        
        return Request.Cookies[key];
       
    }
    public string GetClaimIns()
    {
        // Retrieve the Email claim value
        string Ins = "";

        if (!string.IsNullOrEmpty(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Hash)?.Value))
        {
            Ins = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Hash)?.Value;
            
        }

        // Proceed with the action logic if Email claim exists and is not empty
        ViewData["Ins"] = Ins;
        return Ins;
    }
  

        /// <summary>  
        /// Delete the key stored
        /// </summary>  
        /// <param name="key">Key</param>  
        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }

        public void PurgeCookie()
        {
            //Delete the Cookie from Browser.
            try
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("MACHINE");
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("INSTITUTION");
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("INSTITUTION_NAME");
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("NAME");
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("CODE");
            }
            catch (Exception e)
            {
                
            }
            
        }

        public void CurrentTime()
        {
            
        }

        public void CurrentMonth()
        {
            
        }
        public void CurrentYear()
        {
            
        }
        public string CurrenDate()
        {
            DateTime dt = DateTime.Now;
            
            return Convert.ToString(dt);
        }

        
        public TimeTrackerModel tm { get; set; }
        [HttpPost]
        public void SetTimeOnTracker(Communication data)
        {
            var cl = data.Aux;

            tm = new TimeTrackerModel();
      


            DateTime dt = DateTime.Now;
            if (!string.IsNullOrEmpty(cl))
            {
                        string date_ = dt.ToString("MM/dd/yyyy");
                        string time_ = dt.ToString("HH:mm:ss");

                        tm.INSTITUTION = data.ins;
                        tm.CODE = data.code;
                        //tm.DATE = date_;
                        
                        tm.TOTAL_OFFLINE = "-";
                        tm.TOTAL_ONLINE = "-";
                        
                        tm.LUNCH = "-";
                        tm.LUNCH_ = "-";
                        tm.LUNCH_S = "-";
                        tm.BREAK = "-";
                        tm.BREAK_ = "-";
                        tm.BREAK_S = "-";
                        tm.BREAKL = "-";
                        tm.BREAKL_ = "-";
                        tm.BREAK_SL = "-";
                        tm.OTHER = "-";
                        tm.OTHER_ = "-";
                        tm.OTHER_S = "-";
                        tm.END = "-";
                        tm.TOTAL = "0:0:0";
                        tm.AUTHORIZED_BY = 0;
                        tm.DATESH = date_;
                        tm.START = time_;
                        
            
                        var trackerData = this._data.GetTimeDataFromTracker(tm);
                        
                        if (cl == "available")
                        {
                            if (trackerData.Count() <= 0)
                            {
                                 _data.SetStartTimeOnTracker(tm);
                            }
                            else
                            {
                                foreach (var item in this._data.GetTimeDataFromTracker(tm))
                                {
                                    if (item.DATE == tm.DATE)
                                    {
                                        
                                    }
                                    else
                                    {
                                        _data.SetStartTimeOnTracker(tm);
                                    }

                                    if (item.BREAK != "-")
                                    {
                                        if (item.BREAK_ == "-")
                                        {
                                            tm.BREAK_ = time_;
                                            _data.SetBreakTimeEndOnTracker(tm);
                                        }
                                        else
                                        {
                                            if (item.BREAKL != "-")
                                            {
                                                if (item.BREAKL_ == "-")
                                                {
                                                    tm.BREAKL_ = time_;
                                                    _data.SetSecondBreakTimeEndOnTracker(tm);
                                                }
                                            }
                                        }
                                        
                                        
                                    }
                                    
                                    if (item.LUNCH != "-")
                                    {
                                        if(item.LUNCH_ == "-"){}
                                        tm.LUNCH_ = time_;
                                        _data.SetLunchTimeEndOnTracker(tm);
                                    }
                                    if (item.OTHER != "-")
                                    {
                                        if (item.OTHER_ == "-")
                                        {
                                            tm.OTHER_ = time_;
                                            _data.SetOtherTimeEndOnTracker(tm);
                                        }
                                        
                                    }
                                }
                            }
                            
                            
                            
                        }

                        if (cl == "break")
                        {
                            foreach (var item in this._data.GetTimeDataFromTracker(tm))
                            {
                                if (item.BREAK == "-")
                                {
                                    if (item.BREAK_ == "-")
                                    {
                                        tm.BREAK = time_;
                                        _data.SetBreakTimeOnTracker(tm);
                                    }
                                    
                                }
                                else
                                {
                                    if (item.BREAKL_ == "-")
                                    {
                                        tm.BREAKL = time_;
                                        _data.SetSecondBreakTimeOnTracker(tm);
                                    }
                                }
                                

                            }


                        }

                        if (cl == "lunch")
                        {
                            foreach (var item in this._data.GetTimeDataFromTracker(tm))
                            {
                                
                                if (item.LUNCH == "-")
                                {
                                    if (item.LUNCH_ == "-")
                                    {
                                        tm.LUNCH = time_;
                                        _data.SetLunchTimeOnTracker(tm);
                                    }
                                    
                                }

                            }


                        }
                        if (cl == "other")
                        {
                            foreach (var item in this._data.GetTimeDataFromTracker(tm))
                            {
                                if (item.OTHER == "-")
                                {
                                    if (item.OTHER_ == "-")
                                    {
                                        tm.OTHER = time_;
                                        _data.SetOtherTimeOnTracker(tm);
                                    }
                                    
                                }

                            }


                        }
                        if (cl == "end")
                        {
                            foreach (var item in this._data.GetTimeDataFromTracker(tm))
                            {
                                if (item.START != null)
                                {
                                    tm.END = time_;
                                    _data.SetENDTimeOnTracker(tm);

                                }
                                

                            }


                        }
                        
                        
                            
                        //_data.SetStartTimeOnTracker(time);
                       
                    
                
            }



    }
    }