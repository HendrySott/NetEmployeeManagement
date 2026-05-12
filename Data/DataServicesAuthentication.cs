using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using NetEmployee.Model;

namespace NetEmployee.Data;


public class DataServicesAuthentication
{


    public string ConnectionString { get; set; }

    public DataServicesAuthentication(string connectionString)
    {
        ConnectionString = connectionString;
    }


    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }
   

    private static Random random = new Random();

    public static string RandomStringGenerator(int length)
    {
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public IList<AuthenticationModel>? userLogin(AuthenticationModel data)
    {
        int a = 0;

        List<AuthenticationModel> list = new List<AuthenticationModel>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE EMAIL = '{data.EMAIL}' or (AlT_EMAIL = '{data.EMAIL}') and (PASSWORD = '{data.PASSWORD}')";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        list.Add(new AuthenticationModel()
                        {

                            CODE = reader.GetInt32("CODE"),
                            INSTIITUTION = reader.GetString("INSTITUTION"),
                            // ROLE = reader.GetInt32("ROLE"),
                            // EMAIL = reader.GetString("EMAIL"),
                            // PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),

                        });

                    }
                }

                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
       
        return list;

        

    }

     

    public IList<AuthenticationModel> ValidateEmail(AuthenticationModel data)
    {
        List<AuthenticationModel> list = new List<AuthenticationModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE EMAIL = '{data.EMAIL}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationModel()
                        {
                            CODE = reader.GetInt32("CODE"),
                            INSTIITUTION = reader.GetString("INSTITUTION"),
                        });
                    }
                }
                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
        return list;
    }
    public IList<AuthenticationModel> ValidatePSWD(AuthenticationModel data)
    {
        List<AuthenticationModel> list = new List<AuthenticationModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE PASSWORD = '{data.PASSWORD}' and CODE = {data.CODE} and (INSTITUTION = '{data.INSTIITUTION}')";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationModel()
                        {
                            STATE = reader.GetInt32("STATE"),

                        });
                    }
                }
                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
        return list;
    }

    public IEnumerable<InstitutionModel> LoadInstitutionDetails(string institution)
    {
        List<InstitutionModel> list = new List<InstitutionModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string query = $"SELECT * FROM `admbasic`.`INSTITUTIONPROFILE` WHERE INSTITUTION = '{institution}';";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new InstitutionModel()
                    {
                        NAME = reader.GetString("NAME"),
                        INSTITUTIONALEMAIL = reader.GetString("INSTITUTIONALEMAIL"),
                        DEF_PASS = reader.GetString("DEF_PASS")


                    });
                }
            }
        }
        return list;
    }




    public IList<AuthenticationModel> AuthCashRegister(AuthenticationModel data)
    {
        List<AuthenticationModel> list = new List<AuthenticationModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE CODE = {data.CODE} and PASSWORD = '{data.PASSWORD}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationModel()
                        {
                            ID = reader.GetInt32("ID"),
                        });
                    }
                }

                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
        return list;
    }

    public IList<AuthenticationModel> GetAuthData(string institution, int code)
    {
        List<AuthenticationModel> list = new List<AuthenticationModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {

                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE CODE = {code} and INSTITUTION = '{institution}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationModel()
                        {
                            ID = reader.GetInt32("ID"),
                            EMAIL = reader.GetString("EMAIL"),
                            STATE = reader.GetInt32("STATE"),

                            PERSONAL_EMAIL = reader.GetString("AlT_EMAIL")

                        });
                    }
                }

                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
        return list;
    }
    public IList<AuthenticationModel> AuthWagePayment(AuthenticationModel data)
    {
        List<AuthenticationModel> list = new List<AuthenticationModel>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE CODE = {data.CODE} and (PASSWORD = '{data.PASSWORD}') and INSTITUTION = '{data.INSTIITUTION}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationModel()
                        {

                            CODE = reader.GetInt32("CODE"),
                        });
                    }
                }

                int result = cmd.ExecuteNonQuery();
                //lblError.Text = "Data Saved";
            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }
        return list;
    }






    public AuthenticationModel IUpdatePassword(string npaww, int code, string institution)
    {
        string query = $"UPDATE `admbasic`.`AUTHENTICATION`SET `PASSWORD` = '{npaww}' WHERE CODE = {code} and( INSTITUTION = '{institution}');";
        Executor(query);
        return null;
    }

    

    public AuthenticationModel IRegisterNewUser(AuthenticationModel data)
    {
        string auth = $"INSERT INTO `admbasic`.`AUTHENTICATION`(`INSTITUTION`,`CODE`,`EMAIL`,`PASSWORD`,`STATE`)VALUES ('{data.INSTIITUTION}',{data.CODE}, '{data.EMAIL}', '{data.PASSWORD}',{data.STATE});";

        Executor(auth);
        return data;
    }


    /// <summary>
    /// usless 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    public IList<PersonProfile> ILoadAllUserByUID(int id)
    {
        List<PersonProfile> list = new List<PersonProfile>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE CODE = '{id}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new PersonProfile()
                    {

                        CODE = reader.GetInt32("CODE"),
                        INSTITUTION = reader.GetString("INSTITUTION"),
                        F_NAME = reader.GetString("F_NAME"),
                        S_NAME = reader.GetString("S_NAME"),
                        F_LASTN = reader.GetString("F_LASTN"),
                        S_LASTN = reader.GetString("S_LASTN"),



                    });
                }
            }
        }

        return list;
    }
    
    /// <summary>
    /// load speficic user by id and institution, just take one
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="institution"></param>
    /// <returns></returns>
    public IList<PersonProfile> ILoadAllUserByUIDINS(int uid, string institution)
    {
        List<PersonProfile> list = new List<PersonProfile>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE CODE = {uid} and INSTITUTION = '{institution}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new PersonProfile()
                    {

                        CODE = reader.GetInt32("CODE"),
                        INSTITUTION = reader.GetString("INSTITUTION"),
                        F_NAME = reader.GetString("F_NAME"),
                        S_NAME = reader.GetString("S_NAME"),
                        F_LASTN = reader.GetString("F_LASTN"),
                        S_LASTN = reader.GetString("S_LASTN"),



                    });
                }
            }
        }

        return list;
    }
    // public IList<PersonProfile> ILoadAllUserByUIDINSandAuthInformation(int uid, string institution)
    // {
    //     List<PersonProfile> list = new List<PersonProfile>();

    //     using (MySqlConnection conn = GetConnection())
    //     {
    //         conn.Open();
    //         string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE CODE = {uid} and INSTITUTION = '{institution}'";
    //         MySqlCommand cmd = new MySqlCommand(query, conn);
    //         using (MySqlDataReader reader = cmd.ExecuteReader())
    //         {
    //             while (reader.Read())
    //             {
    //                 list.Add(new PersonProfile()
    //                 {

    //                     CODE = reader.GetInt32("CODE"),
    //                     INSTITUTION = reader.GetString("INSTITUTION"),
    //                     F_NAME = reader.GetString("F_NAME"),
    //                     S_NAME = reader.GetString("S_NAME"),
    //                     F_LASTN = reader.GetString("F_LASTN"),
    //                     S_LASTN = reader.GetString("S_LASTN"),
    //                     te = reader.GetInt32("DEPARTMENT_KEY"),
    //                     POSITION_KEY = reader.GetInt32("POSITION_KEY")



    //                 });
    //             }
    //         }
    //     }

    //     return list;
    // }
    /// <summary>
    /// Load all user by institution code
    /// </summary>
    /// <param name="institution"></param>
    /// <returns></returns>
    public IList<PersonProfile> ILoadAllUserOnTheInstitutionByUIDINS(string institution)
    {
        List<PersonProfile> list = new List<PersonProfile>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE INSTITUTION = '{institution}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new PersonProfile()
                    {

                        CODE = reader.GetInt32("CODE"),
                        INSTITUTION = reader.GetString("INSTITUTION"),
                        F_NAME = reader.GetString("F_NAME"),
                        S_NAME = reader.GetString("S_NAME"),
                        F_LASTN = reader.GetString("F_LASTN"),
                        S_LASTN = reader.GetString("S_LASTN"),



                    });
                }
            }
        }

        return list;
    }


    internal void Executor(string query)
    {
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                int result = cmd.ExecuteNonQuery();

                //lblError.Text = "Data Saved";

            }
            catch (Exception)
            {
                System.Console.WriteLine("not entered");
                //lblError.Text = ex.Message;
            }
        }

    }

    public List<PersonProfile> TestUserExistanceByID(string ID,string EMAIL)
        {
            List<PersonProfile> list = new List<PersonProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE `IDENTIFICATION` = '{ID}' and PERSONAL_EMAIL = '{EMAIL}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonProfile()
                        {
                       
                            CODE = reader.GetInt32("CODE"),
                           
                            INSTITUTION = reader.GetString("INSTITUTION"),


                        });
                    }
                }
            }

            return list;
        }

        public PersonProfile IRegisterNewPerson(PersonProfile data)
        {
            if (!string.IsNullOrEmpty(data.F_NAME))
            {
                ///TODO if code exist get a new random
                
               
                    string query = $"INSERT INTO `admbasic`.`PERSON`(`INSTITUTION`,`CODE`,`F_NAME`,`S_NAME`,`F_LASTN`,`S_LASTN`,`GRADE`,`ADDRESS`,`PHONE`,`IDENTIFICATION`,`PERSONAL_EMAIL`,`TEAM_ID`,`POSITION_KEY`,`STREET`,`HOME`,`COUNTY`,`COUNTRY`) VALUE" +
                                   $"('{data.INSTITUTION}',{data.CODE},'{data.F_NAME}','{data.S_LASTN}','{data.F_LASTN}','{data.S_LASTN}','{data.GRADE}','{data.ADDRESS}','{data.PHONE}','{data.IDENTIFICATION}','{data.PERSONAL_EMAIL}',{data.TEAM_ID},{data.POSITION_KEY},'{data.STREET}',{data.HOME},'{data.COUNTY}','{data.COUNTY}');";

                    Executor(query);
                    IncertSystemAccessPolicy(data.CODE.ToString(), data.INSTITUTION);
                    
                //string InstitutionalEmail = data.F_NAME + data.S_NAME + "" + data.F_LASTN + data.S_LASTN + "@adm.com";
                //string DefaultPassword = "adm123";
                ////Todo add data and time, set the name for the institution create method to load institUTION CODE
                //string auth = $" INSERT INTO `admbasic`.`LOGIN`(`INSTITUTION`,`CODE`,`EMAIL`,`PASSWORD`,`STATE`)VALUES ({code}, '{InstitutionalEmail}', '{DefaultPassword}',1);";

                //Executor(auth);
            }
            return data;
        }
        public void IncertSystemAccessPolicy(string code,string institution)
        {
           
                string query = $"INSERT INTO `admbasic`.`SYSTEMDIRACCESS`(`CODE`,`INSTITUTION`,`CASHREGISTER`,`ACCOUNTING`,`HHRR`,`SETTINGSAC`,`SETTINGSMAIN`,`MAININTERFACE`,`STATE`,`INVENTORY`) VALUES ({code},'{institution}','garanted','garanted','garanted','garanted','garanted','garanted','enabled','garanted');";

                Executor(query);
                
           
        }
        public SetupModel ISetup(SetupModel data)
        {
            string auth = $"INSERT INTO `admbasic`.`SETUPPROGRESS`(`INSTITUTION`) VALUES ('{data.INSTITUTION}');";

            Executor(auth);
            return data;
            
            
        }
    public InstitutionModel InstitutionIntance(string data)
        {
            string auth = $"INSERT INTO `admbasic`.`INSTITUTIONPROFILE`(`INSTITUTION`)VALUES('{data}')";

            Executor(auth);

            return null;
        }

}





