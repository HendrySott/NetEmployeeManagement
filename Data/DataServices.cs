using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using NetEmployee.Model;



namespace NetEmployee.Data;
public class DataServices{
    public string ConnectionString { get; set; }
    static List<Department> _department;
    public DataServices(string connectionString)
    {
        this.ConnectionString = connectionString;
    }
    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }

           
        private static Random random = new Random();

        public static string RandomStringGenerator(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //Random INT 
        public static string RandomInt32Generator(int length)
        {
            const string chars = "123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    
    public IList<AuthenticationModel>? userLogin(AuthenticationModel data)
    {
        
        List<AuthenticationModel> list = new List<AuthenticationModel>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {
                string query = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE  EMAIL = '{data.EMAIL}' and PASSWORD = '{data.PASSWORD}' ";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        list.Add(new AuthenticationModel()
                        {

                            CODE = reader.GetInt32("CODE"),
                            INSTIITUTION = reader.GetString("INSTITUTION"),
                            ROLE = reader.GetString("ROLE"),

                            EMAIL = reader.GetString("EMAIL"),
                            // PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),

                        });

                    }
                }

                int result = cmd.ExecuteNonQuery();
                if(list.Count()<0){
                    string query1 = $"SELECT * FROM `admbasic`.`AUTHENTICATION` WHERE PERSONAL_EMAIL = '{data.EMAIL}' and PASSWORD = '{data.PASSWORD}'";
                
                    MySqlCommand cmd2 = new MySqlCommand(query1, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            list.Add(new AuthenticationModel()
                            {

                                CODE = reader.GetInt32("CODE"),
                                INSTIITUTION = reader.GetString("INSTITUTION"),
                                PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),
                                ROLE = reader.GetString("ROLE"),
                                // EMAIL = reader.GetString("EMAIL"),
                                // PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),

                            });

                        }
                    }

                int result1 = cmd2.ExecuteNonQuery();
                //lblError.Text = "Data Saved";                         
                }
                //lblError.Text = "Data Saved";
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
            }
        }
       
        return list;

        

    }


        //Load User Just Using The ID
        public List<PersonProfile> LoadUserByID(int? ID,string institution)
        {
            List<PersonProfile> list = new List<PersonProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE `ID` = '{ID}' and INSTITUTION = '{institution}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonProfile()
                        {
                       
                            CODE = reader.GetInt32("CODE"),
                             F_NAME = reader.GetString("F_NAME"),
                             S_NAME = reader.GetString("S_NAME"),
                             F_LASTN = reader.GetString("F_LASTN"),
                             S_LASTN = reader.GetString("S_LASTN"),
                             GRADE = reader.GetString("GRADE"),
                             ADDRESS = reader.GetString("ADDRESS"),
                             PHONE = reader.GetString("PHONE"),
                             IDENTIFICATION = reader.GetString("IDENTIFICATION"),
                            PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),
                      
                            POSITION_KEY = reader.GetInt32("POSITION_KEY"),


                        });
                    }
                }
            }

            return list;
        }


        public List<PersonProfile> LoadTeamMembers(string institution,int? ID)
        {
            List<PersonProfile> list = new List<PersonProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE INSTITUTION = '{institution}' and `TEAM_ID` = '{ID}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonProfile()
                        {
                       
                            CODE = reader.GetInt32("CODE"),
                             F_NAME = reader.GetString("F_NAME"),
                             S_NAME = reader.GetString("S_NAME"),
                             F_LASTN = reader.GetString("F_LASTN"),
                             S_LASTN = reader.GetString("S_LASTN"),
                             GRADE = reader.GetString("GRADE"),
                             ADDRESS = reader.GetString("ADDRESS"),
                             PHONE = reader.GetString("PHONE"),
                             IDENTIFICATION = reader.GetString("IDENTIFICATION"),
                            PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),
                            
                            POSITION_KEY = reader.GetInt32("POSITION_KEY"),


                        });
                    }
                }
            }

            return list;
        }

    public IList<PersonProfile> GetUserById(string institution,int id)
    {
        List<PersonProfile> list = new List<PersonProfile>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE CODE = '{id}' and INSTITUTION = '{institution}'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new PersonProfile()
                    {

                        
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


        //Update User Information


        public List<PersonProfile> LoadAllEmployee(string institution)
        {
            List<PersonProfile> list = new List<PersonProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON` where INSTITUTION = '{institution}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetInt32("ID") != null)
                        {
                            list.Add(new PersonProfile()
                            {
                                ID = reader.GetInt32("ID"),
                                INSTITUTION = reader.GetString("INSTITUTION"),
                                CODE = reader.GetInt32("CODE"),
                                F_NAME = reader.GetString("F_NAME"),
                                S_NAME = reader.GetString("S_NAME"),
                                F_LASTN = reader.GetString("F_LASTN"),
                                S_LASTN = reader.GetString("S_LASTN"),
                                GRADE = reader.GetString("GRADE"),
                                PHONE = reader.GetString("PHONE"),
                                IDENTIFICATION = reader.GetString("IDENTIFICATION"),
                                PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),
                                TEAM_ID = reader.GetInt32("TEAM_ID"),
                                POSITION_KEY = reader.GetInt32("POSITION_KEY"),

                                
                            });
                        }
                        


                    }
                }
            }

            return list;
        }



        //department


        public void CreateNewDepartment(string name, string description, string functionCode)
        {

           
            var deptoKey = RandomStringGenerator(16);

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                try
                {
                    string query = $"INSERT INTO `HRSS`.`DEPARTMENT` (`DEPARTMENT_KEY`, `NAME`, `DESCRIPTION`) VALUES('{deptoKey}','{name}','{description}');";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    int result = cmd.ExecuteNonQuery();
                    //lblError.Text = "Data Saved";
                    CreateNewPosition(deptoKey, functionCode);

                }
                catch (Exception)
                {
                    System.Console.WriteLine("not entered");
                    //lblError.Text = ex.Message;
                }
            }
        }

        public void CreateNewPosition(string deptoKey, string functionCode)
        {


            

            switch (functionCode)
            {
                case "Basic":
                    ExecuteQuereFunctions(deptoKey, 1, "Gerente");
                    ExecuteQuereFunctions(deptoKey,2,"Sepervisor");
                    ExecuteQuereFunctions(deptoKey, 3, "Empleado");
                    break;
                case "Middium":
                    ExecuteQuereFunctions(deptoKey, 1, "Sepervisor");
                    ExecuteQuereFunctions(deptoKey, 2, "Empleado");
                    break;
            }

        }



        public void ExecuteQuereFunctions(string deptoKey,int herarchy, string name)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                try
                {
                    var functionkey = RandomStringGenerator(8);
                    string query = $"INSERT INTO `HRSS`.`POSITION`(`POSITION_KEY`,`DEPARTMENT_KEY`,`POSITION_NAME`)VALUES('{functionkey}','{deptoKey}','{name}');";
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










        public List<Department> LoadAllDepartment( string institution)
        {
            List<Department> list = new List<Department>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`DEPARTMENT` WHERE INSTITUTINO = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Department()
                        {
                            ID = reader.GetInt32("ID"),
                            Name = reader.GetString("Name"),
                            Description = reader.GetString("Description")

                        });
                    }
                }
            }

            return list;
        }





       


       



     


        public List<Department> LoadDepartmentByPositionKey(string institutino , int id)
        {
            List<Department> list = new List<Department>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`Department` WHERE  DEPARTMENT = '{institutino}' and ID ={id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Department()
                        {
                            Name = reader.GetString("Name"),


                        });
                    }
                }
            }

            return list;
        }



        public List<DepartmentProfile> LoadDepartmentByID(string institution, int key)
        {
            List<DepartmentProfile> list = new List<DepartmentProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`DEPARTMENT` WHERE INSTITUTION = '{institution}' and ID = {key};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DepartmentProfile()
                        {
                            ID = reader.GetInt32("ID"),
                            NAME = reader.GetString("NAME"),
                            CODE = reader.GetInt32("CODE"),
                            

                        });
                    }
                }
            }

            return list;
        }



        public void UpdateDepartment(string name, string description, int ID)
        {

           
            var deptoKey = RandomStringGenerator(16);

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                try
                {
                    string query = $"UPDATE `HRSS`.`Department` SET   `NAME` = '{name}', `DESCRIPTION` = '{description}' WHERE `ID` = {ID};";
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
        

        //smart query framework
        public void Delete(string data, string model, int key)
        {

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                try
                {
                    var functionkey = RandomStringGenerator(8);
                    string query = $"DELETE FROM `HRSS`.`{model}` WHERE ID = {key} ";
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





      
 

        public Department GetDepartment(int id)
        {
            var data =  _department.FirstOrDefault(e => e.ID == id);
            return data;
        }

        public Department Update(Department UpdateDepartment)
        {
            Department department = _department.FirstOrDefault(e => e.ID == UpdateDepartment.ID);
            if (department != null)
            {
                

                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    try
                    {
                        string query = $"UPDATE `HRSS`.`Department` SET   `NAME` = '{UpdateDepartment.Name}', `DESCRIPTION` = '{UpdateDepartment.Description}' WHERE `ID` = {UpdateDepartment.ID};";
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
            return UpdateDepartment;
        }

       

        public LevelWages SaveWage(LevelWages data)
        {
           
                        string query = $"INSERT INTO `admbasic`.`WAGES`(`INSTITUTION`,`PAYMENT_INTERVAL`,`AMOUNT`,`CURRENCY`,`POSITION_KEY`,`TEAM_ID`,`PAYMENT_METHOD`,`CR_ACCOUNT`,`AFP`)VALUES('{data.INSTITUTION}',{data.PAYMENT_INTERVAL},{data.AMOUNT},'{data.CURRENCY}',{data.POSITION_KEY},{data.TEAM_ID},{data.PAYMENT_METHOD},{data.CR_ACCOUNT},{data.AFP});";
           Executor(query);
            return data;
        }
        
        public IEnumerable<LevelWages> loadAllWages(string institution)
        {
            List<LevelWages> list = new List<LevelWages>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`WAGES` where INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LevelWages()
                        {
                            ID =reader.GetInt32("ID"),
		
                            INSTITUTION = reader.GetString("INSTITUTION"),

                            PAYMENT_INTERVAL =reader.GetInt32("PAYMENT_INTERVAL"),

                            POSITION_KEY =reader.GetInt32("POSITION_KEY"),
		
                            TEAM_ID =reader.GetInt32("TEAM_ID"),

                            AMOUNT = reader.GetFloat("AMOUNT"),

                            CURRENCY = reader.GetString("CURRENCY"),
                            CR_ACCOUNT = reader.GetInt32("CR_ACCOUNT")

                        });
                    }
                }
            }

            return list;
        }

 public PersonProfile UpdatePersonProfileGrade(PersonProfile data)
        {
           
            string query = $"UPDATE `admbasic`.`PERSON`SET `GRADE` = '{data.GRADE}' WHERE INSTITUTION = '{data.INSTITUTION}' and CODE = {data.CODE};";

            Executor(query);
            return data;


        }
        public IEnumerable<LevelWages> GetWageByPositionId(string institution,int id)
        {
            List<LevelWages> list = new List<LevelWages>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`WAGES` where INSTITUTION = '{institution}' and POSITION_KEY = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LevelWages()
                        {
                            AFP =reader.GetInt32("AFP"),
                            ID =reader.GetInt32("ID"),
		
                            INSTITUTION = reader.GetString("INSTITUTION"),

                            PAYMENT_INTERVAL =reader.GetInt32("PAYMENT_INTERVAL"),

                            POSITION_KEY =reader.GetInt32("POSITION_KEY"),
		
                            TEAM_ID =reader.GetInt32("TEAM_ID"),

                            AMOUNT = reader.GetFloat("AMOUNT"),

                            CURRENCY = reader.GetString("CURRENCY")
                            
                            

                        });
                    }
                }
            }

            return list;
        }


        public TitleModel SaveTitle(TitleModel data)
        {
            var code = RandomStringGenerator(8);
            string query = $"INSERT INTO `admbasic`.`TITLE`(`INSTITUTION`,`NAME`,`DESCRIPTION`,`CODE`,`LEVEL`)VALUES('{data.INSTITUTION}','{data.NAME}','{data.DESCRIPTION}','{code}',{data.LEVEL});";
            Executor(query);
            return data;
        }

        public IEnumerable<TitleModel> loadAllTitles(string institution)
        {
            List<TitleModel> list = new List<TitleModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`TITLE` where INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TitleModel()
                        {
                            ID = reader.GetInt32("ID"),
                            NAME = reader.GetString("NAME"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            CODE  = reader.GetString("CODE")


                        });
                    }
                }
            }

            return list;
        }
        
        public IList<SetupModel> SetupState(string institution)
        {
            List<SetupModel> list = new List<SetupModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM admbasic.SETUPPROGRESS where INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SetupModel()
                        {
                            
                            INSTRITUTION_NAME =reader.GetInt32("INSTRITUTION_NAME"),
                            INSTITUTION_ADDRESS =reader.GetInt32("INSTITUTION_ADDRESS"),
                            INSTITUTION_FDEPARTMENT =reader.GetInt32("INSTITUTION_FDEPARTMENT"),
                            INSTITUTION_FPOSITIONS =reader.GetInt32("INSTITUTION_FPOSITIONS"),
                            INSTITUTION_RB =reader.GetInt32("INSTITUTION_RB"),
                            INSTITUTION_RB_CONCILIATOR =reader.GetInt32("INSTITUTION_RB_CONCILIATOR"),
                            INSTITUTION_HR =reader.GetInt32("INSTITUTION_HR"),
                            INSTITUTION_ACCOUNTING =reader.GetInt32("INSTITUTION_ACCOUNTING"),
                            INSTITUTION_INV =reader.GetInt32("INSTITUTION_INV"),
                            INSTITUTION_SU_SETUP =reader.GetInt32("INSTITUTION_SU_SETUP"),


                        });
                    }
                }
            }

            return list;
        }

        public IEnumerable<TitleModel> getTitlesByProfile(string institution,int id)
        {
            List<TitleModel> list = new List<TitleModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`TITLE` where INSTITUTION = '{institution}' and ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TitleModel()
                        {
                            ID = reader.GetInt32("ID"),
                            LEVEL = reader.GetInt32("LEVEL"),
                            NAME = reader.GetString("NAME"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            CODE = reader.GetString("CODE")
                            
                            


                        });
                    }
                }
            }

            return list;
        }




      

  

        public IEnumerable<DepartmentProfile> loadAllDepartment(string institution)
        {
            List<DepartmentProfile> list = new List<DepartmentProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`DEPARTMENT` WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DepartmentProfile()
                        {

                            ID = reader.GetInt32("ID"),
                            NAME = reader.GetString("NAME"),
                            INITIALS = reader.GetString("INITIALS"),
                            CODE = reader.GetInt32("CODE"),
                            LOCATION = reader.GetString("LOCATION"),
                            JUSTIFICATION = reader.GetString("JUSTIFICATION"),
                            OBJETIVE = reader.GetString("OBJETIVE"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            CREATED_BY = reader.GetInt32("CREATED_BY"),
                            


                        });
                    }
                }
            }

            return list;
        }
        
        
        
        public IEnumerable<DepartmentProfile> GetDepartmentNameById(string institution,int id)
        {
            List<DepartmentProfile> list = new List<DepartmentProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`DEPARTMENT` WHERE INSTITUTION = '{institution}' and ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DepartmentProfile()
                        {

                            ID = reader.GetInt32("ID"),
                            NAME = reader.GetString("NAME"),
                            INITIALS = reader.GetString("INITIALS"),
                           


                        });
                    }
                }
            }

            return list;
        }
/// <summary>
/// add position to the company
/// </summary>
/// <param name="data"></param>
/// <returns></returns>
        public PositionProfile SaveNewPosition(PositionProfile data)
        {
            if (!string.IsNullOrEmpty(data.INSTITUTION))
            {
                var code = RandomInt32Generator(8);
                string query = $"INSERT INTO `admbasic`.`POSITIONS`(`INSTITUTION`,`POSITION_NAME`,`TEAM_ID`,`REASON`,`JOB_PROFILE`,`JOB_QUANTITY`,`LOCATION_PROFILE`,`JOB_TYPE`,`CONTRACT_TYPE`,`ACADEMIC_GRADE`,`SHIFT`,`SKILLS_REQUIRED`,`ROLL`,`AVAILABLE_FROM`,`CODE`,`OBJETIVE`)VALUES('{data.INSTITUTION}','{data.POSITION_NAME}','{data.TEAM_ID}','{data.REASON}','{data.JOB_PROFILE}','{data.JOB_QUANTITY}',{data.LOCATION_PROFILE},{data.JOB_TYPE},{data.CONTRACT_TYPE},{data.ACADEMIC_GRADE},{data.SHIFT},'{data.SKILLS_REQUIRED}','{data.POSITION_NAME}',current_date(),{code},'{data.OBJETIVE}')";
                Executor(query);
            }
            return data;
        }

        public IEnumerable<PositionProfile> loadAllPosition(string institution)
        {
            List<PositionProfile> list = new List<PositionProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`POSITIONS` WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PositionProfile()
                        {

                            ID = reader.GetInt32("ID"),
                            REASON = reader.GetString("REASON"),
                            JOB_PROFILE = reader.GetInt32("JOB_PROFILE"),
                            JOB_QUANTITY = reader.GetInt32("JOB_QUANTITY"),
                            POSITION_NAME = reader.GetString("POSITION_NAME"),
                            LOCATION_PROFILE = reader.GetInt32("LOCATION_PROFILE"),
                            JOB_TYPE = reader.GetInt32("JOB_TYPE"),
                            CONTRACT_TYPE = reader.GetInt32("CONTRACT_TYPE"),
                            ACADEMIC_GRADE = reader.GetInt32("ACADEMIC_GRADE"),
                            SHIFT = reader.GetInt32("SHIFT"),
                            SKILLS_REQUIRED = reader.GetString("SKILLS_REQUIRED"),
                            TEAM_ID = reader.GetInt32("TEAM_ID"),
                            
                            


                        });
                    }
                }
            }

            return list;
        }
        
        
        public IEnumerable<PositionProfile> GetPositionById(string institution, int id)
        {
            List<PositionProfile> list = new List<PositionProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`POSITIONS` WHERE INSTITUTION = '{institution}' and ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PositionProfile()
                        {

                            ID = reader.GetInt32("ID"),
                            
                            POSITION_NAME = reader.GetString("POSITION_NAME"),
                            REASON = reader.GetString("REASON"),
                            JOB_PROFILE = reader.GetInt32("JOB_PROFILE"),
                            JOB_QUANTITY = reader.GetInt32("JOB_QUANTITY"),
                            
                            LOCATION_PROFILE = reader.GetInt32("LOCATION_PROFILE"),
                            JOB_TYPE = reader.GetInt32("JOB_TYPE"),
                            CONTRACT_TYPE = reader.GetInt32("CONTRACT_TYPE"),
                            ACADEMIC_GRADE = reader.GetInt32("ACADEMIC_GRADE"),
                            SHIFT = reader.GetInt32("SHIFT"),
                            SKILLS_REQUIRED = reader.GetString("SKILLS_REQUIRED"),
                            TEAM_ID = reader.GetInt32("TEAM_ID"),
                            
                            
                            
                            


                        });
                    }
                }
            }

            return list;
        }

        

        

        public IEnumerable<PositionProfile> getPosition(int id, string institution)
        {
            List<PositionProfile> list = new List<PositionProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`POSITIONS` where ID = {id} and WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PositionProfile()
                        {

                            
                            POSITION_NAME = reader.GetString("POSITION_NAME"),
                            REASON = reader.GetString("REASON"),
                            JOB_PROFILE = reader.GetInt32("JOB_PROFILE"),
                            JOB_QUANTITY = reader.GetInt32("JOB_CUANTITY"),

                            LOCATION_PROFILE = reader.GetInt32("LOCATION_PROFILE"),
                            JOB_TYPE = reader.GetInt32("JOB_TYPE"),
                            CONTRACT_TYPE = reader.GetInt32("CONTRACT_TYPE"),
                            ACADEMIC_GRADE = reader.GetInt32("ACADEMIC_GRADE"),
                            SHIFT = reader.GetInt32("SHIFT"),
                            SKILLS_REQUIRED = reader.GetString("SKILLS_REQUIRED"),
                            TEAM_ID = reader.GetInt32("TEAM_ID"),
                            


                        });
                    }
                }
            }

            return list;
        }
        
        
        
        public IEnumerable<PositionProfile> getAvailablePosition(string institution)
        {
            List<PositionProfile> list = new List<PositionProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`POSITIONS` WHERE INSTITUTION = '{institution}' and JOB_QUANTITY > 0;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PositionProfile()
                        {

                            ID = reader.GetInt32("ID"),
                          
                            REASON = reader.GetString("REASON"),
                            JOB_PROFILE = reader.GetInt32("JOB_PROFILE"),
                            JOB_QUANTITY = reader.GetInt32("JOB_QUANTITY"),
                            POSITION_NAME = reader.GetString("POSITION_NAME"),
                            LOCATION_PROFILE = reader.GetInt32("LOCATION_PROFILE"),
                            JOB_TYPE = reader.GetInt32("JOB_TYPE"),
                            CONTRACT_TYPE = reader.GetInt32("CONTRACT_TYPE"),
                            ACADEMIC_GRADE = reader.GetInt32("ACADEMIC_GRADE"),
                            SHIFT = reader.GetInt32("SHIFT"),
                            SKILLS_REQUIRED = reader.GetString("SKILLS_REQUIRED"),
                            TEAM_ID = reader.GetInt32("TEAM_ID"),
                            AVAILABLE_FROM = reader.GetString("AVAILABLE_FROM"),
                            
                            


                        });
                    }
                }
            }

            return list;
        }


     




        public IEnumerable<PositionProfile> getPositionByDepartment(int id,string institution)
        {
            List<PositionProfile> list = new List<PositionProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`POSITIONS` where institution = '{institution}' and DEPARTMENT_PROFILE = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PositionProfile()
                        {
                            ID = reader.GetInt32("ID"),
                            
                            REASON = reader.GetString("REASON"),
                            JOB_PROFILE = reader.GetInt32("JOB_PROFILE"),
                            JOB_QUANTITY = reader.GetInt32("JOB_QUANTITY"),

                            LOCATION_PROFILE = reader.GetInt32("LOCATION_PROFILE"),
                            JOB_TYPE = reader.GetInt32("JOB_TYPE"),
                            CONTRACT_TYPE = reader.GetInt32("CONTRACT_TYPE"),
                            ACADEMIC_GRADE = reader.GetInt32("ACADEMIC_GRADE"),
                            SHIFT = reader.GetInt32("SHIFT"),
                            SKILLS_REQUIRED = reader.GetString("SKILLS_REQUIRED"),
                            TEAM_ID = reader.GetInt32("TEAM_ID"),
                            POSITION_NAME = reader.GetString("POSITION_NAME")
                      

                        });
                    }
                }
            }

            return list;
        }

        public PersonProfile UpdatePersonRoleAndPositionn(PersonProfile data)
        {
            string uery = $"UPDATE `admbasic`.`PERSON`SET`DEPARTMENT_KEY` = {data.TEAM_ID},`POSITION_KEY` = {data.POSITION_KEY} WHERE CODE = {data.CODE} and INSTITUTION ='{data.INSTITUTION}'";
            Executor(uery);
            return data;

        }


        public PersonProfile SaveNewAplicant(PersonProfile data)
        {
            string email = null;
            string defpawd = null;
            if (!string.IsNullOrEmpty(data.F_NAME))
            {
                var code = RandomInt32Generator(8);
                string query = $"INSERT INTO `admbasic`.`PERSON`(`INSTITUTION`,`CODE`,`F_NAME`,`S_NAME`,`F_LASTN`,`S_LASTN`,`GRADE`,`PHONE`,`IDENTIFICATION`,`PERSONAL_EMAIL`,`DEPARTMENT_KEY`,`POSITION_KEY`,`STREET`,`HOME`, `COUNTY`, `COUNTRY`,`ADMITION_DATE`) VALUE('{data.INSTITUTION}',{data.CODE},'{data.F_NAME}','{data.S_NAME}','{data.F_LASTN}','{data.S_LASTN}',{data.GRADE},'{data.PHONE}','{data.IDENTIFICATION}','{data.PERSONAL_EMAIL}',{data.TEAM_ID},{data.POSITION_KEY},'{data.STREET}',{data.HOME},'{data.COUNTY}','{data.COUNTY}'current_date());";

                Executor(query);


                var em = LoadInstitutionDetails(data.INSTITUTION);
                if (em.Count()>0)
                {
                    foreach (var item in em)
                    {
                        email = item.INSTITUTIONALEMAIL.ToLower();
                        defpawd = item.DEF_PASS;
                    }
                }
                string InstitutionalEmail = data.F_NAME + data.S_NAME + "." + data.F_LASTN + data.S_LASTN + email;
                string DefaultPassword = defpawd;
                //Todo add data and time, set the name for the institution create method to load institUTION CODE
                string auth = $" INSERT INTO `admbasic`.`AUTHENTICATION`(`INSTITUTION`,`CODE`,`EMAIL`,`PASSWORD`,`STATE`,`AlT_EMAIL`)VALUES ('{data.INSTITUTION}',{code}, '{InstitutionalEmail}', '{DefaultPassword}',771,'-');";

                Executor(auth);
                IncertSystemAccessPolicy(code, data.INSTITUTION);
                
                
            }    
            return data;
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
                            NAME =reader.GetString("NAME"),
                            INSTITUTIONALEMAIL = reader.GetString("INSTITUTIONALEMAIL"),
                            DEF_PASS = reader.GetString("DEF_PASS"),
                            

                        });
                    }
                }
            }
            return list;
        }
        

        

        public IEnumerable<PersonProfile> loadPeople(string institution)
        {
            List<PersonProfile> list = new List<PersonProfile>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonProfile()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            F_NAME = reader.GetString("F_NAME"),
                            S_NAME = reader.GetString("S_NAME"),
                            F_LASTN = reader.GetString("F_LASTN"),
                            S_LASTN = reader.GetString("S_LASTN"),
                            GRADE = reader.GetString("GRADE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            PHONE = reader.GetString("PHONE"),
                            IDENTIFICATION = reader.GetString("IDENTIFICATION"),
                            PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),
                            POSITION_KEY = reader.GetInt32("POSITION_KEY"),
                          TEAM_ID = reader.GetInt32("TEAM_ID"),
                           
                        });
                    }
                }
            }
            return list;
        }
        
        
        

        
        



            public IEnumerable<PersonProfile> GetPerson(string institution, int id)
        {
            List<PersonProfile> list = new List<PersonProfile>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE INSTITUTION = '{institution}' and CODE = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonProfile()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            F_NAME = reader.GetString("F_NAME"),
                            S_NAME = reader.GetString("S_NAME"),
                            F_LASTN = reader.GetString("F_LASTN"),
                            S_LASTN = reader.GetString("S_LASTN"),
                            GRADE = reader.GetString("GRADE"),
                          HOME = reader.GetInt32("HOME"),
                          COUNTRY = reader.GetString("COUNTRY"),
                          COUNTY = reader.GetString("COUNTY"),
                            PHONE = reader.GetString("PHONE"),
                            STREET = reader.GetString("STREET"),
                            IDENTIFICATION = reader.GetString("IDENTIFICATION"),
                            PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),
                            POSITION_KEY = reader.GetInt32("POSITION_KEY"),
                            TEAM_ID = reader.GetInt32("TEAM_ID"),                           
                        });
                    }
                }
            }
            return list;
        }
    




        
        


//DEPARTMENT
        
        public DepartmentProfile SaveDepartment(DepartmentProfile data)
        {
          
            var code = RandomInt32Generator(5);
            string query = $"INSERT INTO `admbasic`.`DEPARTMENT`(`INSTITUTION`,`NAME`,`INITIALS`,`CODE`,`LOCATION`,`JUSTIFICATION`,`OBJETIVE`,`DESCRIPTION`,`CREATED_BY`)VALUES('{data.INSTITUTION}','{data.NAME}','{data.INITIALS}',{code},'{data.LOCATION}','{data.JUSTIFICATION}','{data.OBJETIVE}','{data.DESCRIPTION}',{data.CREATED_BY} );";
            Executor(query);
                        
            return data;
        }

        
        /// <summary>
        /// pay to employee
        /// </summary>
        /// <returns></returns>
        public PayrollModel SavePayroll(PayrollModel data)
        {
          
            string query = $"INSERT INTO `admbasic`.`PAYROLL`(`INSTITUTION`,`CODE`,`AMOUNT`,`BONUS`,`CURRENCY`,`HOURS`,`PAYMENT_METHOD`,`TAXES`,`TAXES_ID`,`AFP`,`AFP_ID`,`ENSURANCE`,`ENSURANCE_ID`,`STATE`,`DATE`)VALUES('{data.INSTITUTION}',{data.CODE},{data.AMOUNT},{data.BONUS},'{data.CURRENCY}',{data.HOURS},'{data.PAYMENT_METHOD}',{data.TAXES},{data.TAXES_ID},{data.AFP},{data.AFP_ID},{data.ENSURANCE},{data.ENSURANCE_ID},{data.STATE},'{data.DATE}',{data.APROVED_BY},{data.DR_ACC},'{data.CR_ACC}');";
            Executor(query);
                        
            return data;
        }
        
        
        

        public IList<PayrollModel> LoadCurrentPayroll(string institution, string date)
        {
            List<PayrollModel> list = new List<PayrollModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PAYROLL` WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PayrollModel()
                        {
                            
                            SERIAL = reader.GetInt32("SERIAL"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            CODE = reader.GetInt32("CODE"),
                            AMOUNT =reader.GetFloat("AMOUNT"),
                            BONUS =reader.GetFloat("BONUS"),
                            CURRENCY =reader.GetString("CURRENCY"),
                            HOURS =reader.GetFloat("HOURS"),
                            PAYMENT_METHOD =reader.GetString("PAYMENT_METHOD"),
                            
                            AFP =reader.GetFloat("AFP"),
                            AFP_ID = reader.GetInt32("AFP_ID"),
                            ENSURANCE =reader.GetFloat("ENSURANCE"),
                            ENSURANCE_ID = reader.GetInt32("ENSURANCE_ID"),
                            STATE = reader.GetInt32("STATE"),
                            PERIOD = reader.GetInt32("PERIOD"),
                            DATE =reader.GetString("DATE"),
                            APROVED_BY= reader.GetInt32("APROVED_BY"),
                            DR_ACC = reader.GetInt32("DR_ACC"),


                        });
                    }
                }
            }

            return list;
        }
        public IList<PayrollModel> LoadCurrentPayment(string institution)
        {
            List<PayrollModel> list = new List<PayrollModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PAYROLL` WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PayrollModel()
                        {
                            
                            SERIAL = reader.GetInt32("SERIAL"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            CODE = reader.GetInt32("CODE"),
                            AMOUNT =reader.GetFloat("AMOUNT"),
                            BONUS =reader.GetFloat("BONUS"),
                            CURRENCY =reader.GetString("CURRENCY"),
                            HOURS =reader.GetFloat("HOURS"),
                            PAYMENT_METHOD =reader.GetString("PAYMENT_METHOD"),
                            
                            AFP =reader.GetFloat("AFP"),
                            AFP_ID = reader.GetInt32("AFP_ID"),
                            ENSURANCE =reader.GetFloat("ENSURANCE"),
                            ENSURANCE_ID = reader.GetInt32("ENSURANCE_ID"),
                            STATE = reader.GetInt32("STATE"),
                            PERIOD = reader.GetInt32("PERIOD"),
                            DATE =reader.GetString("DATE"),
                            APROVED_BY= reader.GetInt32("APROVED_BY"),
                            DR_ACC = reader.GetInt32("DR_ACC"),


                        });
                    }
                }
            }

            return list;
        }
        
        public IList<PayrollModel> LoadSpecificPayroll(string institution,int code, int serial)
        {
            List<PayrollModel> list = new List<PayrollModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PAYROLL` WHERE INSTITUTION = '{institution}' and(CODE = {code}) and (SERIAL = {serial});";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PayrollModel()
                        {
                            
                            SERIAL = reader.GetInt32("SERIAL"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            CODE = reader.GetInt32("CODE"),
                            AMOUNT =reader.GetFloat("AMOUNT"),
                            BONUS =reader.GetFloat("BONUS"),
                            CURRENCY =reader.GetString("CURRENCY"),
                            HOURS =reader.GetFloat("HOURS"),
                            PAYMENT_METHOD =reader.GetString("PAYMENT_METHOD"),
                            
                            AFP =reader.GetFloat("AFP"),
                            AFP_ID = reader.GetInt32("AFP_ID"),
                            ENSURANCE =reader.GetFloat("ENSURANCE"),
                            ENSURANCE_ID = reader.GetInt32("ENSURANCE_ID"),
                            STATE = reader.GetInt32("STATE"),
                            PERIOD = reader.GetInt32("PERIOD"),
                            DATE =reader.GetString("DATE"),
                            APROVED_BY= reader.GetInt32("APROVED_BY"),
                            DR_ACC = reader.GetInt32("DR_ACC"),


                        });
                    }
                }
            }

            return list;
        }
        
        
        public IList<PayrollModel> LoadPreviousPayrollTest(string institution, int code, int period)
        {
            List<PayrollModel> list = new List<PayrollModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PAYROLL` WHERE INSTITUTION = '{institution}' and (CODE = {code}) and (PERIOD = {period}) ORDER BY serial DESC LIMIT 1;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PayrollModel()
                        {
                            
                            SERIAL = reader.GetInt32("SERIAL"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            CODE = reader.GetInt32("CODE"),
                            AMOUNT =reader.GetFloat("AMOUNT"),
                            BONUS =reader.GetFloat("BONUS"),
                            CURRENCY =reader.GetString("CURRENCY"),
                            HOURS =reader.GetFloat("HOURS"),
                            PAYMENT_METHOD =reader.GetString("PAYMENT_METHOD"),
                            
                            AFP =reader.GetFloat("AFP"),
                            AFP_ID = reader.GetInt32("AFP_ID"),
                            ENSURANCE =reader.GetFloat("ENSURANCE"),
                            ENSURANCE_ID = reader.GetInt32("ENSURANCE_ID"),
                            STATE = reader.GetInt32("STATE"),
                            PERIOD = reader.GetInt32("PERIOD"),
                            DATE =reader.GetString("DATE"),
                            APROVED_BY= reader.GetInt32("APROVED_BY"),
                            DR_ACC = reader.GetInt32("DR_ACC"),


                        });
                    }
                }
            }

            return list;
        }

        public IEnumerable<DepartmentProfile> LoadAllDepartmentData(string institution)
        {
            List<DepartmentProfile> list = new List<DepartmentProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`DEPARTMENT` WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DepartmentProfile()
                        {
                            
                            ID = reader.GetInt32("ID"),
                            NAME = reader.GetString("NAME"),
                            INITIALS = reader.GetString("INITIALS"),
                            LOCATION = reader.GetString("LOCATION"),
                            JUSTIFICATION = reader.GetString("JUSTIFICATION"),
                            OBJETIVE = reader.GetString("OBJETIVE"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            CREATED_BY = reader.GetInt32("CREATED_BY"),


                        });
                    }
                }
            }

            return list;
        }

        public ShiftProfile SaveNewSchedule(ShiftProfile data)
        {
            if (data.PERIOD != null)
            {
                
                        string query = $"INSERT INTO `admbasic`.`SHIFT`(`INSTITUTION`,`PERIOD`,`START`,`HOURS`,`LUNCH`,`BREAK`,`WEEKEND`,`QUANTITY`)VALUES('{data.INSTITUTION}',{data.PERIOD},'{data.START}',{data.HOURS},'{data.LUNCH}',{data.BREAK},{data.WEEKEND},{data.QUANTITY});";
                        Executor(query);
            }
            return data;
        }


        public IEnumerable<ShiftProfile> GetShiftUsingID(string institutino, int id)
        {
            List<ShiftProfile> list = new List<ShiftProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`SHIFT` WHERE INSTITUTION = '{institutino}' and ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ShiftProfile()
                        {
                            ID = reader.GetInt32("ID"),
                            PERIOD = reader.GetInt32("PERIOD"),
                            START = reader.GetString("START"),
                            HOURS = reader.GetInt32("HOURS"),
                            LUNCH = reader.GetFloat("LUNCH"),
                            BREAK = reader.GetInt32("BREAK"),
                            WEEKEND = reader.GetInt32("WEEKEND"),
                            QUANTITY = reader.GetInt32("QUANTITY"),



                        });
                    }
                }
            }

            return list;
        }
        public IEnumerable<ShiftProfile> ILoadAllShifts()
        {
            List<ShiftProfile> list = new List<ShiftProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`SHIFT`;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ShiftProfile()
                        {
                            ID = reader.GetInt32("ID"),
                            PERIOD = reader.GetInt32("PERIOD"),
                            START = reader.GetString("START"),
                            HOURS = reader.GetInt32("HOURS"),
                            LUNCH = reader.GetFloat("LUNCH"),
                            BREAK = reader.GetInt32("BREAK"),
                            WEEKEND = reader.GetInt32("WEEKEND"),
                            QUANTITY = reader.GetInt32("QUANTITY"),


                        });
                    }
                }
            }

            return list;
        }
        

        public IEnumerable<PersonProfile> TestIfCodeOrInstitutinoExist(string code)
        {
            List<PersonProfile> list = new List<PersonProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON`";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonProfile()
                        {
                            
                            CODE = reader.GetInt32("CODE"),


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
                
               
                    string query = $"INSERT INTO `admbasic`.`PERSON`(`INSTITUTION`,`CODE`,`F_NAME`,`S_NAME`,`F_LASTN`,`S_LASTN`,`GRADE`,`ADDRESS`,`PHONE`,`IDENTIFICATION`,`PERSONAL_EMAIL`,`TEAM_ID`,`POSITION_KEY`,`STREET`,`HOME`,`COUNTY`,`COUNTRY`,`ADMITION_DATE`) VALUE" +
                                   $"('{data.INSTITUTION}',{data.CODE},'{data.F_NAME}','{data.S_LASTN}','{data.F_LASTN}','{data.S_LASTN}','{data.GRADE}','{data.ADDRESS}','{data.PHONE}','{data.IDENTIFICATION}','{data.PERSONAL_EMAIL}',{data.TEAM_ID},{data.POSITION_KEY},'{data.STREET}',{data.HOME},'{data.COUNTY}','{data.COUNTY}',current_date());";

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
        
        public PersonProfile UpdatePersonProfile(PersonProfile data)
        {
           
            string query = $"UPDATE `admbasic`.`PERSON`SET " +
                           
                           $"`PHONE` = '{data.PHONE}'," +
                           $"`IDENTIFICATION` = '{data.IDENTIFICATION}'," +
                           $"`PERSONAL_EMAIL` = '{data.PERSONAL_EMAIL}'," +
                          
                           $"`STREET` = '{data.STREET}'," +
                           $"`HOME` = {data.HOME}," +
                           $"`COUNTY` = '{data.COUNTY}'," +
                           $"`COUNTRY` = '{data.COUNTRY}' WHERE INSTITUTION = '{data.INSTITUTION}' and CODE = {data.CODE};";

            Executor(query);
            return data;


        }

        public PersonProfile UpdateSkills(PersonProfile data)
        {
           
            string query = $"UPDATE `admbasic`.`PERSON` SET `SKILLS` = '{data.SKILLS}' WHERE INSTITUTION = '{data.INSTITUTION}' and CODE = {data.CODE};";

            Executor(query);
            return data;


        }

        public IEnumerable<PersonProfile> IGetPersonByCODE(string inst,int id)
        {
            List<PersonProfile> list = new List<PersonProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON` where INSTITUTION = '{inst}'and CODE = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonProfile()
                        {

                           
                            CODE = reader.GetInt32("CODE"),
                            F_NAME = reader.GetString("F_NAME"),
                            S_NAME = reader.GetString("S_NAME"),
                            F_LASTN = reader.GetString("F_LASTN"),
                            S_LASTN = reader.GetString("S_LASTN"),
                            GRADE = reader.GetString("GRADE"),
                            //ADDRESS = reader.GetString("ADDRESS"),
                            PHONE = reader.GetString("PHONE"),
                            IDENTIFICATION = reader.GetString("IDENTIFICATION"),
                            PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),
                            POSITION_KEY = reader.GetInt32("POSITION_KEY"),
                            TEAM_ID = reader.GetInt32("TEAM_ID")



                        });
                    }
                }
            }

            return list;
        }
        public List<PersonProfile> getSkills(string ins,int code)
        {
            List<PersonProfile> list = new List<PersonProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE `INSTITUTION` = '{ins}' and CODE = {code}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonProfile()
                        {
                            SKILLS = reader.GetString("SKILLS"),
                        });
                    }
                }
            }

            return list;
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
        
        


        

        public IEnumerable<PersonProfile> IGetPersonGlobalSearch(string institution, string name)
        {
            List<PersonProfile> list = new List<PersonProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`Person` where INSTITUTION = '{institution}' and (F_NAME = '{name}') or (CODE = '{name}') or(IDENTIFICATION = '{name}');";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonProfile()
                        {


                            CODE = reader.GetInt32("CODE"),
                            F_NAME = reader.GetString("F_NAME"),
                            S_NAME = reader.GetString("S_NAME"),
                            F_LASTN = reader.GetString("F_LASTN"),
                            S_LASTN = reader.GetString("S_LASTN"),
                            
                            PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL"),
                            
                            //IMG = reader.GetString("IMG"),


                        });
                    }
                }
            }

            return list;
        }
        
        
        public IEnumerable<PaymentHistorys> VerifyStateOfAuth(string institution, int code, string date)
        {
            List<PaymentHistorys> list = new List<PaymentHistorys>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PAYROLL` where CODE = {code} and (INSTITUTIon = '{institution}') and(DATE = '{date}');";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PaymentHistorys()
                        {


                            STATE = reader.GetInt32("STATE")
                            
                            //IMG = reader.GetString("IMG"),


                        });
                    }
                }
            }

            return list;
        }
        public IEnumerable<LevelWages> GetWageById(string institution,int id)
        {
            List<LevelWages> list = new List<LevelWages>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`WAGES` where INSTITUTION = '{institution}' and POSITION_KEY = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LevelWages()
                        {
                            AFP = reader.GetFloat("AFP"),

                            ID = reader.GetInt32("ID"),
		
                            INSTITUTION = reader.GetString("INSTITUTION"),

                            PAYMENT_INTERVAL =reader.GetInt32("PAYMENT_INTERVAL"),

                            POSITION_KEY =reader.GetInt32("POSITION_KEY"),
		
                            TEAM_ID =reader.GetInt32("TEAM_ID"),

                            AMOUNT = reader.GetFloat("AMOUNT"),

                            CURRENCY = reader.GetString("CURRENCY"),

                            
                            
                            

                        });
                    }
                }
            }

            return list;
        }
        
        public IEnumerable<PaymentHistorys> GetPaymentHistoryByCode(string institution, int code)
        {
            List<PaymentHistorys> list = new List<PaymentHistorys>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM admbasic.PAYROLL WHERE CODE = {code} and (INSTITUTION = '{institution}');";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PaymentHistorys()
                        {


                            STATE = reader.GetInt32("STATE"),
                            AMOUNT = reader.GetDouble("AMOUNT"),
                            DATE = reader.GetString("DATE"),
                            PERIOD = reader.GetInt32("PERIOD"),
                            HOURS = reader.GetDouble("HOURS"),
                            BONUS = reader.GetFloat("BONUS"),
                            AFP = reader.GetDouble("AFP"),
                            SERIAL = reader.GetInt32("SERIAL"),
                            CODE = reader.GetInt32("CODE")




                        });
                    }
                }
            }

            return list;
        }
        
        public IEnumerable<PayrollBonus> GetBonusByUserID(string institution, int code)
        {
            List<PayrollBonus> list = new List<PayrollBonus>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`BONUS` WHERE CODE = {code} and (INSTITUTION = '{institution}');";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PayrollBonus()
                        {

                            AMOUNT = reader.GetFloat("AMOUNT"),
                            
                         
                        });
                    }
                }
            }

            return list;
        }
        
        public IEnumerable<EsuranceModel> GetEnsuranceByID(string institution, int code)
        {
            List<EsuranceModel> list = new List<EsuranceModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`ENSURANCE` WHERE CODE = {code} and (INSTITUTION = '{institution}');";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new EsuranceModel()
                        {

                            ID = reader.GetInt32("ID"),
                            
                         
                        });
                    }
                }
            }

            return list;
        }
        
        public IEnumerable<PaymentHistorys> TestPaymentPeriod(string institution, int code)
        {
            List<PaymentHistorys> list = new List<PaymentHistorys>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PAYROLL` WHERE CODE = {code} and (INSTITUTION = '{institution}')LIMIT 1;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PaymentHistorys()
                        {

                            PERIOD = reader.GetInt32("PERIOD"),
                            DATE = reader.GetString("DATE"),
                            
                         
                        });
                    }
                }
            }

            return list;
        }
        public IEnumerable<AFPModel> GetAfpById(string institution, int code)
        {
            List<AFPModel> list = new List<AFPModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`AFP` WHERE CODE = {code} and (INSTITUTION = '{institution}');";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AFPModel()
                        {

                            ID = reader.GetInt32("ID"),
                            
                         
                        });
                    }
                }
            }

            return list;
        }
        

        public PaymentHistorys RegisterPayment(PaymentHistorys data)
        {
            int afp_id = 0;
            int ens_id = 0;
            foreach (var item in GetEnsuranceByID(data.INSTITUTION,data.CODE))
            {
                ens_id = item.ID;
            }
            foreach (var item in GetAfpById(data.INSTITUTION,data.CODE))
            {
                afp_id = item.ID;
            }
            var serial = RandomInt32Generator(8);
                string query = $"INSERT INTO `admbasic`.`PAYROLL`(`INSTITUTION`,`CODE`,`AMOUNT`," +
                               $"`BONUS`,`CURRENCY`,`HOURS`," +
                               $"`PAYMENT_METHOD`," +
                               $"`AFP`,`AFP_ID`,`ENSURANCE`,`ENSURANCE_ID`," +
                               $"`STATE`,`DATE`,`APROVED_BY`,`DR_ACC`,`PERIOD`)VALUES('{data.INSTITUTION}',{data.CODE},{data.AMOUNT}," +
                               $"{data.BONUS},'{data.CURRENCY}','{data.HOURS}',{data.PAYMENT_METHOD},{data.AFP},{afp_id},{data.ENSURANCE},{ens_id},{data.STATE},'{data.DATE}',{data.APROVED_BY},{data.DR_ACC},{data.PERIOD})";
                Executor(query);




                double amount = data.AMOUNT + data.BONUS;
               // RegisterOnAcc(data.INSTITUTION,data.DR_ACC,amount,data.DR_ACC,data.APROVED_BY,data.PERIOD);
            
            return data;
        }
    
        public IList<ACCModel> LoadAccountDateById(string institution, int id)
        {
            List<ACCModel> list = new List<ACCModel>();

            using (MySqlConnection conn = GetConnection())
            {
                string search = $"SELECT * FROM `admbasic`.`ACCOUNTS` WHERE ID = {id} and(INSTITUTION = '{institution}');";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(search, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new ACCModel()
                        {
                            ID = reader.GetInt32("ID"),
                            CURRENT_VALANCE = reader.GetFloat("CURRENT_VALANCE"),
                            INITIAL_VALANCE = reader.GetFloat("INITIAL_VALANCE"),
                        });
                       
                        
                        
                    }
                }
            }

            return list;
        }
        
        /// <summary>
        /// updata acchistory and sum all to account
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public void RegisterOnAcc(string ins, int accd,double amount,int DR_ACC,int id,int period)
        {
            try
            {

                var loadaccountinfo = LoadAccountDateById(ins , accd);
                foreach (var item in loadaccountinfo)
                {
                    float currentv = item.CURRENT_VALANCE;
                    //update account valance cr
                    string updacc = $"UPDATE `admbasic`.`ACCOUNTS` SET `CURRENT_VALANCE` = {currentv-amount} WHERE `ID` = {DR_ACC} and(INSTITUTION = '{ins}');";

                    Executor(updacc);
                    
                    
                    //registe on history
                    string inc = $"INSERT INTO `admbasic`.`ACCHISTORY`(`INSTITUTION`,`TYPE`,`CATEGORY`,`DATE`,`DETAILS`,`AMOUNT`,`ACC_CODE`,`POW_CODE`,`CODE`)VALUES('{ins}','PAYMENT','PAYROLL',curdate(),'PERIOD{period}',{amount},{DR_ACC},'N/A',{id});";

                    Executor(inc);
                
                    
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
           
        }
    
    
        public AFPModel AFPRegister(AFPModel data)
        {
            string query = $"INSERT INTO `admbasic`.`AFP`(`INSTITUTION`,`CODE`,`AFP_AMOUNT`,`DATE`,`STATE`)VALUES('{data.INSTITUTION}',{data.CODE},{data.AFP_AMOUNT},current_date,{data.STATE})";
                Executor(query);
                return data;
        }
        public EsuranceModel ENSURANCERegister(EsuranceModel data)
        {
            string query = $"INSERT INTO `admbasic`.`ENSURANCE`(`INSTITUTION`,`CODE`,`ENSURANCE_AMOUNT`,`DATE`,`STATE`)VALUES('{data.INSTITUTION}',{data.CODE},{data.ENSURANCE_AMOUNT},current_date,{data.STATE})";
            Executor(query);
            return data;
        }
        
        
        public PayrollBonus CreateTempPaymenBONUS(PayrollBonus data)
        {
            string query = $" INSERT INTO `admbasic`.`BONUS`(`INSTITUTION`,`CODE`,`AMOUNT`,`AUTHORIZER`,`DATE`)VALUES('{data.INSTITUTION}',{data.CODE},{data.AMOUNT},{data.AUTHORIZER},current_date())ON DUPLICATE KEY UPDATE AMOUNT = {data.AMOUNT}";
            Executor(query);
            return data;
        }
        public PaymentHistorys TempCeanPaymenBONUS(string institution, int code)
        {
            string query = $"DELETE FROM `admbasic`.`BONUS` WHERE CODE = {code} and INSTITUTION = '{institution}'";
            Executor(query);
            return null;
        }
      
        
        

// Human resources
// TO Separate INV
// =================================================================================================================================================
// =================================================================================================================================================



    

        public IEnumerable<StockModel> IGetByID(int id)
        {
            List<StockModel> list = new List<StockModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `INV`.`STOCK` WHERE ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StockModel()
                        {
                            ID = reader.GetInt32("ID"),

                            PROVIDER = reader.GetInt32("PROVIDER"),

                            GROUPOF = reader.GetInt32("GROUPOF"),

                            BARCODE = reader.GetString("BARCODE"),

                            NAME = reader.GetString("NAME"),

                            BRAND = reader.GetString("BRAND"),

                            DESCRIPTION = reader.GetString("DESCRIPTION"),

                            IMAGE = reader.GetString("IMAGE"),

                            VALUE = reader.GetFloat("VALUE"),

                            STOCK = reader.GetInt32("STOCK"),

                            TAX = reader.GetString("TAX"),
                            PRICE = reader.GetFloat("PRICE"),


                        });
                    }
                }
            }

            return list;
        }

        public IEnumerable<StockModel> IGetGroup(int g)
        {
            List<StockModel> list = new List<StockModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `INV`.`STOCK` WHERE GROUPOF = {g};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StockModel()
                        {
                            ID = reader.GetInt32("ID"),

                            PROVIDER = reader.GetInt32("PROVIDER"),

                            GROUPOF = reader.GetInt32("GROUPOF"),

                            BARCODE = reader.GetString("BARCODE"),

                            NAME = reader.GetString("NAME"),

                            BRAND = reader.GetString("BRAND"),

                            DESCRIPTION = reader.GetString("DESCRIPTION"),

                            IMAGE = reader.GetString("IMAGE"),

                            VALUE = reader.GetFloat("VALUE"),

                            STOCK = reader.GetInt32("STOCK"),
                            TAX = reader.GetString("TAX"),
                            PRICE = reader.GetFloat("PRICE"),


                        });
                    }
                }
            }

            return list;
        }

        public IEnumerable<StockModel> IGetVol(int vol)
        {
            List<StockModel> list = new List<StockModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM INV.STOCK WHERE STOCK = {vol};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StockModel()
                        {
                            ID = reader.GetInt32("ID"),

                            PROVIDER = reader.GetInt32("PROVIDER"),

                            GROUPOF = reader.GetInt32("GROUPOF"),

                            BARCODE = reader.GetString("BARCODE"),

                            NAME = reader.GetString("NAME"),

                            BRAND = reader.GetString("BRAND"),

                            DESCRIPTION = reader.GetString("DESCRIPTION"),

                            IMAGE = reader.GetString("IMAGE"),

                            VALUE = reader.GetFloat("VALUE"),

                            STOCK = reader.GetInt32("STOCK"),

                            TAX = reader.GetString("TAX"),
                            PRICE = reader.GetFloat("PRICE"),
                        });
                    }
                }
            }

            return list;
        }

        public IEnumerable<StockModel> ILoadAll()
        {
            List<StockModel> list = new List<StockModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM INV.STOCK WHERE;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StockModel()
                        {
                            ID = reader.GetInt32("ID"),
                            PROVIDER = reader.GetInt32("PROVIDER"),
                            GROUPOF = reader.GetInt32("GROUPOF"),
                            BARCODE = reader.GetString("BARCODE"),
                            NAME = reader.GetString("NAME"),
                            BRAND = reader.GetString("BRAND"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            IMAGE = reader.GetString("IMAGE"),
                            VALUE = reader.GetFloat("VALUE"),
                            STOCK = reader.GetInt32("STOCK"),
                            TAX = reader.GetString("TAX"),
                            PRICE = reader.GetFloat("PRICE"),
                        });
                    }
                }
            }
            return list;
        }


        public StockModel ISave(StockModel data)
        {
            if (!string.IsNullOrEmpty(data.BARCODE))
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    try
                    {
                        string query = $"INSERT INTO `INV`.`STOCK`(`PROVIDER`,`GROUPOF`,`BARCODE`,`NAME`,`BRAND`,`DESCRIPTION`,`IMAGE`,`VALUE`,`STOCK`,`TAX`,`PRICE`)PRICES({data.PROVIDER},{data.GROUPOF},'{data.BARCODE}','{data.NAME}','{data.BRAND}','{data.DESCRIPTION}','{data.PROVIDER}','{data.IMAGE}',{data.VALUE}, {data.STOCK},{data.TAX},{data.PRICE}) ON DUPLICATE KEY UPDATE STOCK = STOCK+{data.STOCK};";
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        int result = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("not entered");
                        //lblError.Text = ex.Message;
                    }
                }
            }
            return data;

        }

        

        public StockModel IUpdate(StockModel data)
        {
            if (!string.IsNullOrEmpty(data.BARCODE))
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    try
                    {
                        string query = $"UPDATE `INV`.`STOCK` SET `PROVIDER` = {data.PROVIDER},`GROUPOF` = {data.GROUPOF},`BARCODE` = '{data.BARCODE}',`NAME` = '{data.NAME}',`BRAND` = '{data.BRAND}',`DESCRIPTION` = '{data.DESCRIPTION}',`IMAGE` = '{data.IMAGE}',`VALUE` = {data.VALUE},`STOCK` = {data.STOCK} `TAX` = {data.TAX}, `PRICE` = {data.PRICE} WHERE ID = {data.ID};";
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        int result = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("not entered");
                        //lblError.Text = ex.Message;
                    }
                }
            }
            return data;
        }

        public StockCategory ISave(StockCategory data)
        {
            if (!string.IsNullOrEmpty(data.NAME))
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    try
                    {
                        string query = $"INSERT INTO `INV`.`STOCKCATEGORY`(`NAME`)PRICES('{data.NAME}');";
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        int result = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("not entered");
                        //lblError.Text = ex.Message;
                    }
                }
            }
            return data;
        }

        public IEnumerable<StockCategory> ILoadAllCategorys()
        {
            List<StockCategory> list = new List<StockCategory>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM INV.STOCKCATEGORY WHERE;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StockCategory()
                        {
                            ID = reader.GetInt32("ID"),
                            NAME = reader.GetString("NAME"),
                        });
                    }
                }
            }
            return list;
        }

        //FONT
        public FontsEXModel ISave(FontsEXModel data)
        {
            if (!string.IsNullOrEmpty(data.NAME))
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    try
                    {
                        string query = $"INSERT INTO `admbasic`.`VALUEPROFILE`(`NAME`,`DESCRIPTION`,`AMOUNT`,`RELATION`)PRICES('{data.NAME}','{data.DESCRIPTION}',{data.AMOUNT},{data.RELATION});";
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        int result = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("not entered");
                        //lblError.Text = ex.Message;
                    }
                }
            }
            return data;
        }

        public IEnumerable<FontsEXModel> ILoadAllVALUE()
        {
            List<FontsEXModel> list = new List<FontsEXModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`VALUEPROFILE`;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new FontsEXModel()
                        {
                            ID = reader.GetInt32("ID"),
                            
                            NAME = reader.GetString("NAME"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            AMOUNT = reader.GetFloat("AMOUNT"),
                            
                        });
                    }
                }
            }
            return (IEnumerable<FontsEXModel>)list;
        }

        public IEnumerable<FontsEXModel> GetById(int id)
        {
            List<FontsEXModel> list = new List<FontsEXModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`VALUEPROFILE` WHERE ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new FontsEXModel()
                        {
                            ID = reader.GetInt32("ID"),

                            NAME = reader.GetString("NAME"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            AMOUNT = reader.GetFloat("AMOUNT"),

                        });
                    }
                }
            }
            return (IEnumerable<FontsEXModel>)list;
        }
// Human resources
// TO Separate INV
// =================================================================================================================================================
// =================================================================================================================================================
// Human resources
// TO Separate USER
// =================================================================================================================================================
// =================================================================================================================================================
public IList<PersonProfile> ILoadAllUserByUID(string ins,int id)
        {
            List<PersonProfile> list = new List<PersonProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSON` WHERE INSTITUTION = '{ins}' and CODE = {id}";
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
                            PHONE = reader.GetString("PHONE"),
                            POSITION_KEY = reader.GetInt32("POSITION_KEY"),
                            ADMITION_DATE = reader.GetString("ADMITION_DATE"),
                            TEAM_ID = reader.GetInt32("TEAM_ID"),
                            PERSONAL_EMAIL = reader.GetString("PERSONAL_EMAIL")



                        }) ;
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



                        }) ;
                    }
                }
            }

            return list;
        }
public IList<PersonProfile> ILoadAllUserByUIDINSandAuthInformation(int uid, string institution)
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
                    TEAM_ID = reader.GetInt32("TEAM_ID"),
                    POSITION_KEY = reader.GetInt32("POSITION_KEY")



                }) ;
            }
        }
    }

    return list;
}
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



                        }) ;
                    }
                }
            }

            return list;
        }




// Human resources
// TO Separate INV
// =================================================================================================================================================
// =================================================================================================================================================
// Human resources
// TO Separate USER
// =================================================================================================================================================
// =================================================================================================================================================

        public SetupModel ISetup(SetupModel data)
        {
            string auth = $"INSERT INTO `admbasic`.`SETUPPROGRESS`(`INSTITUTION`) VALUES ('{data.INSTITUTION}');";

            Executor(auth);
            return data;
            
            
        }
        
        public SetupModel SetupDepartments(SetupModel data)
        {
            string auth = $"INSERT INTO `admbasic`.`SETUPPROGRESS`(`INSTITUTION`) VALUES ('{data.INSTITUTION}');";

            Executor(auth);
            return data;
            
            
        }
        
        
        

      
        
        
        
        
        public IList<TimeTrackerModel> LoadAllTimeFromTracker(string institution, string data)
        {
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = $"SELECT * FROM `admbasic`.`TIMETRACKER` WHERE `date` BETWEEN DATE_SUB( CURDATE( ) ,INTERVAL 15 DAY ) AND CURDATE( ) and(INSTITUTION = '{institution}')  and (DATE != curdate());";                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new TimeTrackerModel()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            TOTAL_OFFLINE =reader.GetString("TOTAL_OFFLINE"),
                            TOTAL_ONLINE =reader.GetString("TOTAL_ONLINE"),
                            START =reader.GetString("START"),
                            LUNCH =reader.GetString("LUNCH"),
                            LUNCH_ =reader.GetString("LUNCH_"),
                            LUNCH_S =reader.GetString("LUNCH_S"),
                            BREAK =reader.GetString("BREAK"),
                            BREAK_ =reader.GetString("BREAK_"),
                            BREAK_S =reader.GetString("BREAK_S"),
                
                            BREAKL =reader.GetString("BREAKL"),
                            BREAKL_ =reader.GetString("BREAKL_"),
                            BREAK_SL =reader.GetString("BREAK_SL"),
                            OTHER =reader.GetString("OTHER"),
                            OTHER_ =reader.GetString("OTHER_"),
                            OTHER_S =reader.GetString("OTHER_S"),
                            END =reader.GetString("END"),
                           
                            TOTAL =reader.GetString("TOTAL"),
                            STATE =reader.GetString("STATE"),
                            
                            DATESH = reader.GetString("DATESH")
                        });
                       
                        
                        
                    }
                }
            }

            return list;
        }
        
        
        public IList<TimeTrackerModel> LoadTimeHistoriesFromTrackerOnTheCurrentDate(string institution,string from, string to, int datax)
        {
            
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();
            DateTime date_ = DateTime.Now;
            string date = date_.ToString("MM/dd/yyyy");
            string query = null;
            using (MySqlConnection conn = GetConnection())
            {
                query = $"SELECT * FROM `admbasic`.`TIMETRACKER`  WHERE date between  DATE_FORMAT(CURDATE() ,'%Y-%m-01') AND  CURDATE() and(INSTITUTION = '{institution}') and(DATESH != CURDATE())";
                
                       // query = $"SELECT * FROM `admbasic`.`ACCHISTORY`  WHERE `date` BETWEEN CAST(current_date() AS DATE) AND CAST(current_date() AS DATE) and(INSTITUTION = '{institution}') and( DATE != CURDATE( ))";

            
                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new TimeTrackerModel()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            TOTAL_OFFLINE =reader.GetString("TOTAL_OFFLINE"),
                            TOTAL_ONLINE =reader.GetString("TOTAL_ONLINE"),
                            START =reader.GetString("START"),
                            LUNCH =reader.GetString("LUNCH"),
                            LUNCH_ =reader.GetString("LUNCH_"),
                            LUNCH_S =reader.GetString("LUNCH_S"),
                            BREAK =reader.GetString("BREAK"),
                            BREAK_ =reader.GetString("BREAK_"),
                            BREAK_S =reader.GetString("BREAK_S"),
                
                            BREAKL =reader.GetString("BREAKL"),
                            BREAKL_ =reader.GetString("BREAKL_"),
                            BREAK_SL =reader.GetString("BREAK_SL"),
                            OTHER =reader.GetString("OTHER"),
                            OTHER_ =reader.GetString("OTHER_"),
                            OTHER_S =reader.GetString("OTHER_S"),
                            END =reader.GetString("END"),
                           
                            TOTAL =reader.GetString("TOTAL"),
                            STATE =reader.GetString("STATE"),
                            
                            DATESH = reader.GetString("DATESH")
                        });
                       
                        
                        
                    }
                }
            }

            return list;
        }
        
        
        public IList<TimeTrackerModel> LoadTimeHistoriesFromTrackerOnTheCurrentDateUsingCode(string institution,int datax)
        {
            
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();
            DateTime date_ = DateTime.Now;
            string date = date_.ToString("MM/dd/yyyy");
            string query = null;
            using (MySqlConnection conn = GetConnection())
            {
                query = $" SELECT * FROM `admbasic`.`TIMETRACKER` WHERE  date between  DATE_FORMAT(CURDATE() ,'%Y-%m-01') AND  CURDATE() and(INSTITUTION = '{institution}') and(DATESH != CURDATE()) and(CODE = {datax})";
                
                       // query = $"SELECT * FROM `admbasic`.`ACCHISTORY`  WHERE `date` BETWEEN CAST(current_date() AS DATE) AND CAST(current_date() AS DATE) and(INSTITUTION = '{institution}') and( DATE != CURDATE( ))";

            
                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new TimeTrackerModel()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            
                            TOTAL =reader.GetString("TOTAL"),
                            
                            DATESH = reader.GetString("DATESH")
                        });
                       
                        
                        
                    }
                }
            }

            return list;
        }
        
        public IList<TimeTrackerModel> GetFirstDateOfTImeTracker(string institution, string data)
        {
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = $"SELECT * FROM `admbasic`.`TIMETRACKER` WHERE  INSTITUTION = '{institution}'  and (DATE != curdate()) ;";                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new TimeTrackerModel()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                           
                            TOTAL =reader.GetString("TOTAL"),
                            DATESH = reader.GetString("DATESH")
                        });
                       
                        
                        
                    }
                }
            }

            return list;
        }
        
        
        
        
         public IList<TimeTrackerModel> LoadAllTimeFromTrackerDataAndId(string institution,int code, string from, string to )
        {
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = $"SELECT * FROM `admbasic`.`TIMETRACKER` WHERE `date` BETWEEN CAST('{from}' AS DATE) AND CAST('{to}' AS DATE) and(INSTITUTION = '{institution}')  and(CODE = {code});";                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new TimeTrackerModel()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            TOTAL_OFFLINE =reader.GetString("TOTAL_OFFLINE"),
                            TOTAL_ONLINE =reader.GetString("TOTAL_ONLINE"),
                            START =reader.GetString("START"),
                            LUNCH =reader.GetString("LUNCH"),
                            LUNCH_ =reader.GetString("LUNCH_"),
                            LUNCH_S =reader.GetString("LUNCH_S"),
                            BREAK =reader.GetString("BREAK"),
                            BREAK_ =reader.GetString("BREAK_"),
                            BREAK_S =reader.GetString("BREAK_S"),
                
                            BREAKL =reader.GetString("BREAKL"),
                            BREAKL_ =reader.GetString("BREAKL_"),
                            BREAK_SL =reader.GetString("BREAK_SL"),
                            OTHER =reader.GetString("OTHER"),
                            OTHER_ =reader.GetString("OTHER_"),
                            OTHER_S =reader.GetString("OTHER_S"),
                            END =reader.GetString("END"),
                            //DATE =reader.GetString("DATE"),
                            TOTAL =reader.GetString("TOTAL"),
                            STATE =reader.GetString("STATE"),
                            
                            DATESH = reader.GetString("DATESH")
                        });
                       
                        
                        
                    }
                }
            }

            return list;
        }
        
        public IList<TimeTrackerModel> GetTimeFromTrackerbyId(string institution, string code)
        {
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = $"SELECT * FROM `admbasic`.`TIMETRACKER` WHERE `date` BETWEEN DATE_SUB( CURDATE( ) ,INTERVAL 15 DAY ) AND CURDATE( ) and(INSTITUTION = '{institution}') and (AUTHORIZED_BY = 0) and (CODE = {code}) and( DATE != CURDATE( ));";                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new TimeTrackerModel()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            TOTAL_OFFLINE =reader.GetString("TOTAL_OFFLINE"),
                            TOTAL_ONLINE =reader.GetString("TOTAL_ONLINE"),
                            START =reader.GetString("START"),
                            LUNCH =reader.GetString("LUNCH"),
                            LUNCH_ =reader.GetString("LUNCH_"),
                            LUNCH_S =reader.GetString("LUNCH_S"),
                            BREAK =reader.GetString("BREAK"),
                            BREAK_ =reader.GetString("BREAK_"),
                            BREAK_S =reader.GetString("BREAK_S"),
                
                            BREAKL =reader.GetString("BREAKL"),
                            BREAKL_ =reader.GetString("BREAKL_"),
                            BREAK_SL =reader.GetString("BREAK_SL"),
                            OTHER =reader.GetString("OTHER"),
                            OTHER_ =reader.GetString("OTHER_"),
                            OTHER_S =reader.GetString("OTHER_S"),
                            END =reader.GetString("END"),
                            //DATE =reader.GetString("DATE"),
                            TOTAL =reader.GetString("TOTAL"),
                            STATE =reader.GetString("STATE"),
                            DATESH = reader.GetString("DATESH"),
                        });
                       
                        
                        
                    }
                }
            }

            return list;
        }
        
        
        public IList<TimeTrackerModel> GetTimeFromTrackerPeriods(string institution, string code)
        {
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = $"SELECT * FROM `admbasic`.`TIMETRACKER` WHERE `date` BETWEEN DATE_SUB( CURDATE( ) ,INTERVAL 15 DAY ) AND CURDATE( ) and(INSTITUTION = '{institution}') and (AUTHORIZED_BY = 0) and (CODE = {code}) and( DATE != CURDATE( ));";                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new TimeTrackerModel()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            TOTAL_OFFLINE =reader.GetString("TOTAL_OFFLINE"),
                            TOTAL_ONLINE =reader.GetString("TOTAL_ONLINE"),
                            START =reader.GetString("START"),
                            LUNCH =reader.GetString("LUNCH"),
                            LUNCH_ =reader.GetString("LUNCH_"),
                            LUNCH_S =reader.GetString("LUNCH_S"),
                            BREAK =reader.GetString("BREAK"),
                            BREAK_ =reader.GetString("BREAK_"),
                            BREAK_S =reader.GetString("BREAK_S"),
                
                            BREAKL =reader.GetString("BREAKL"),
                            BREAKL_ =reader.GetString("BREAKL_"),
                            BREAK_SL =reader.GetString("BREAK_SL"),
                            OTHER =reader.GetString("OTHER"),
                            OTHER_ =reader.GetString("OTHER_"),
                            OTHER_S =reader.GetString("OTHER_S"),
                            END =reader.GetString("END"),
                            //DATE =reader.GetString("DATE"),
                            TOTAL =reader.GetString("TOTAL"),
                            STATE =reader.GetString("STATE"),
                            DATESH = reader.GetString("DATESH"),
                        });
                       
                        
                        
                    }
                }
            }

            return list;
        }
        
        
        
         public IList<TimeTrackerModel> LoadAllTimeFromTrackerSpecificDate(string institution, string from,string to)
        {
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`TIMETRACKER` WHERE  date between  DATE_FORMAT(CURDATE() ,'%Y-%m-01') AND  CURDATE() and (INSTITUTION = '{institution}') and (DATESH!= current_date());";                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TimeTrackerModel()
                        {

                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            TOTAL_OFFLINE =reader.GetString("TOTAL_OFFLINE"),
                            TOTAL_ONLINE =reader.GetString("TOTAL_ONLINE"),
                            START =reader.GetString("START"),
                            LUNCH =reader.GetString("LUNCH"),
                            LUNCH_ =reader.GetString("LUNCH_"),
                            LUNCH_S =reader.GetString("LUNCH_S"),
                            BREAK =reader.GetString("BREAK"),
                            BREAK_ =reader.GetString("BREAK_"),
                            BREAK_S =reader.GetString("BREAK_S"),
                
                            BREAKL =reader.GetString("BREAKL"),
                            BREAKL_ =reader.GetString("BREAKL_"),
                            BREAK_SL =reader.GetString("BREAK_SL"),
                            OTHER =reader.GetString("OTHER"),
                            OTHER_ =reader.GetString("OTHER_"),
                            OTHER_S =reader.GetString("OTHER_S"),
                            END =reader.GetString("END"),
                            //DATE =reader.GetString("DATE"),
                            TOTAL =reader.GetString("TOTAL"),
                            STATE =reader.GetString("STATE"),
                            DATESH = reader.GetString("DATESH")



                        });
                    }
                }
            }

            return list;
        }
         
         
         public IList<TimeTrackerModel> LoadAllTimeFromTrackerSpecificDateAndCode(string institution, string from,string to, int code)
        {
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT   * FROM `admbasic`.`TIMETRACKER` WHERE `date` BETWEEN CAST('{from}' AS DATE) AND CAST('{to}' AS DATE) and(INSTITUTION = '{institution}') and( DATE != CURDATE( )) and(CODE = {code});";                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TimeTrackerModel()
                        {

                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION =reader.GetString("INSTITUTION"),
                            TOTAL_OFFLINE =reader.GetString("TOTAL_OFFLINE"),
                            TOTAL_ONLINE =reader.GetString("TOTAL_ONLINE"),
                            START =reader.GetString("START"),
                            LUNCH =reader.GetString("LUNCH"),
                            LUNCH_ =reader.GetString("LUNCH_"),
                            LUNCH_S =reader.GetString("LUNCH_S"),
                            BREAK =reader.GetString("BREAK"),
                            BREAK_ =reader.GetString("BREAK_"),
                            BREAK_S =reader.GetString("BREAK_S"),
                
                            BREAKL =reader.GetString("BREAKL"),
                            BREAKL_ =reader.GetString("BREAKL_"),
                            BREAK_SL =reader.GetString("BREAK_SL"),
                            OTHER =reader.GetString("OTHER"),
                            OTHER_ =reader.GetString("OTHER_"),
                            OTHER_S =reader.GetString("OTHER_S"),
                            END =reader.GetString("END"),
                            //DATE =reader.GetString("DATE"),
                            TOTAL =reader.GetString("TOTAL"),
                            STATE =reader.GetString("STATE"),
                            DATESH = reader.GetString("DATESH")



                        });
                    }
                }
            }

            return list;
        }
         
        
          public IEnumerable<TaxOffice> LoadTaxOfficeByLocation()
        {
            List<TaxOffice> list = new List<TaxOffice>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`TAXOFFICE`;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TaxOffice()
                        {
                            ID = reader.GetInt32("ID"),
                            LOCATION = reader.GetString("LOCATION"),
                            NAME = reader.GetString("NAME"),
                            PHONE = reader.GetString("PHONE"),
                            ADDRESS = reader.GetString("ADDRESS"),
 
                        });
                    }
                }
            }
            return list;
        }
        
        public IEnumerable<TaxOffice> LoadTaxOfficeByLocationID(int id)
        {
            List<TaxOffice> list = new List<TaxOffice>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`TAXOFFICE` WHERE ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TaxOffice()
                        {
                            ID = reader.GetInt32("ID"),
                            LOCATION = reader.GetString("LOCATION"),
                            NAME = reader.GetString("NAME"),
                            PHONE = reader.GetString("PHONE"),
                            ADDRESS = reader.GetString("ADDRESS"),
 
                        });
                    }
                }
            }
            return list;
        }
        
        public TaxOffice SetTaxOffice(TaxOffice data)
        {
            string auth = $"UPDATE `admbasic`.`SYSTAX` SET `OFFICE` = {data.ID} WHERE INSTITUTION = '{data.INSTITUTION}';";

            Executor(auth);
            return data;
        }
        
        public IList<InstitutionModel> GetInstitutionNameUsingInstitutionCode(string data)
        {
            

            List<InstitutionModel> list = new List<InstitutionModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT NAME,RNC FROM `admbasic`.`INSTITUTIONPROFILE` WHERE INSTITUTION = '{data}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new InstitutionModel()
                        {

                       
                            NAME = reader.GetString("NAME"),
                            RNC = reader.GetInt32("RNC")
                            



                        });
                    }
                }
            }

            return list;
        }
        
        /// <summary>
        /// get time from trackee
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IList<TimeTrackerModel> GetTimeDataFromTracker(TimeTrackerModel data)
        {
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`TIMETRACKER` WHERE INSTITUTION = '{data.INSTITUTION}' and CODE = {data.CODE} and (DATE = current_date());";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            list.Add(new TimeTrackerModel()
                            {
                                CODE = reader.GetInt32("CODE"),
                                INSTITUTION = reader.GetString("INSTITUTION"),
                                TOTAL_OFFLINE = reader.GetString("TOTAL_OFFLINE"),
                                TOTAL_ONLINE = reader.GetString("TOTAL_ONLINE"),
                                START = reader.GetString("START"),
                                LUNCH = reader.GetString("LUNCH"),
                                LUNCH_ = reader.GetString("LUNCH_"),
                                LUNCH_S = reader.GetString("LUNCH_S"),
                                BREAK = reader.GetString("BREAK"),
                                BREAK_ = reader.GetString("BREAK_"),
                                BREAK_S = reader.GetString("BREAK_S"),
                                BREAKL = reader.GetString("BREAKL"),
                                BREAKL_ = reader.GetString("BREAKL_"),
                                BREAK_SL = reader.GetString("BREAK_SL"),
                                OTHER = reader.GetString("OTHER"),
                                OTHER_ = reader.GetString("OTHER_"),
                                OTHER_S = reader.GetString("OTHER_S"),
                                END = reader.GetString("END"),
                                
                            });
                        }
                        catch (Exception e)
                        {
                            
                        }
                        
                    }
                }
            }

            return list;
        }
        
        
        public IList<TimeTrackerModel> TrackUserHavailability(string institution, string dt)
        {
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`TIMETRACKER` WHERE INSTITUTION = '{institution}' and (DATE = curdate());";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            list.Add(new TimeTrackerModel()
                            {
                                CODE = reader.GetInt32("CODE"),
                                INSTITUTION = reader.GetString("INSTITUTION"),
                                TOTAL_OFFLINE = reader.GetString("TOTAL_OFFLINE"),
                                TOTAL_ONLINE = reader.GetString("TOTAL_ONLINE"),
                                START = reader.GetString("START"),
                                LUNCH = reader.GetString("LUNCH"),
                                LUNCH_ = reader.GetString("LUNCH_"),
                                LUNCH_S = reader.GetString("LUNCH_S"),
                                BREAK = reader.GetString("BREAK"),
                                BREAK_ = reader.GetString("BREAK_"),
                                BREAK_S = reader.GetString("BREAK_S"),
                                BREAKL = reader.GetString("BREAKL"),
                                BREAKL_ = reader.GetString("BREAKL_"),
                                BREAK_SL = reader.GetString("BREAK_SL"),
                                OTHER = reader.GetString("OTHER"),
                                OTHER_ = reader.GetString("OTHER_"),
                                OTHER_S = reader.GetString("OTHER_S"),
                                END = reader.GetString("END"),
                                //DATE = reader.("DATE"),
                                STATE= reader.GetString("STATE"),
                            });
                        }
                        catch (Exception e)
                        {
                            
                        }
                        
                    }
                }
            }

            return list;
        }
        
        public IList<TimeTrackerModel> LookUpUserTimeTracker(string institution, string date,int code)
        {
            List<TimeTrackerModel> list = new List<TimeTrackerModel>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`TIMETRACKER` WHERE INSTITUTION = '{institution}' and (DATE = current_date() and(CODE = {code}));";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            list.Add(new TimeTrackerModel()
                            {
                                CODE = reader.GetInt32("CODE"),
                                INSTITUTION = reader.GetString("INSTITUTION"),
                                TOTAL_OFFLINE = reader.GetString("TOTAL_OFFLINE"),
                                TOTAL_ONLINE = reader.GetString("TOTAL_ONLINE"),
                                START = reader.GetString("START"),
                                LUNCH = reader.GetString("LUNCH"),
                                LUNCH_ = reader.GetString("LUNCH_"),
                                LUNCH_S = reader.GetString("LUNCH_S"),
                                BREAK = reader.GetString("BREAK"),
                                BREAK_ = reader.GetString("BREAK_"),
                                BREAK_S = reader.GetString("BREAK_S"),
                                BREAKL = reader.GetString("BREAKL"),
                                BREAKL_ = reader.GetString("BREAKL_"),
                                BREAK_SL = reader.GetString("BREAK_SL"),
                                OTHER = reader.GetString("OTHER"),
                                OTHER_ = reader.GetString("OTHER_"),
                                OTHER_S = reader.GetString("OTHER_S"),
                                END = reader.GetString("END"),
                                //DATE= reader.GetDateOnly("DATE"),
                                STATE= reader.GetString("STATE"),
                            });
                        }
                        catch (Exception e)
                        {
                            
                        }
                        
                    }
                }
            }

            return list;
        }
        
        public IList<PagesDirectoryModel> SysDirectoryAcessPolicy(string institutiom, int id)
        {
            

            List<PagesDirectoryModel> list = new List<PagesDirectoryModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`SYSTEMDIRACCESS` WHERE INSTITUTION = '{institutiom}' AND CODE = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            list.Add(new PagesDirectoryModel()
                            {
                                ID = reader.GetInt32("ID"),
                                CODE = reader.GetInt32("CODE"),
                                INSTITUTION = reader.GetString("INSTITUTION"),
                                CASHREGISTER = reader.GetString("CASHREGISTER"),
                                ACCOUNTING = reader.GetString("ACCOUNTING"),
                                HHRR = reader.GetString("HHRR"),
                                SETTINGSAC = reader.GetString("SETTINGSAC"),
                                SETTINGSMAIN = reader.GetString("SETTINGSMAIN"),
                                MAININTERFACE = reader.GetString("MAININTERFACE"),
                                STATE = reader.GetString("STATE"),
                                INVENTORY = reader.GetString("INVENTORY"),


                            });
                        }
                        catch (Exception e)
                        {
                            
                        }
                        
                    }
                }
            }

            return list;
        }
        
        
        
            
            
        public IList<PagesDirectoryModel> SystemAccessPolicy(string data, int code)
        {
            

            List<PagesDirectoryModel> list = new List<PagesDirectoryModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`SYSTEMDIRACCESS` WHERE INSTITUTION = '{data}' and CODE = {code};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            list.Add(new PagesDirectoryModel()
                            {
                                CASHREGISTER = reader.GetString("CASHREGISTER"),
                                ACCOUNTING = reader.GetString("ACCOUNTING"),
                                HHRR = reader.GetString("HHRR"),
                                SETTINGSAC = reader.GetString("SETTINGSAC"),
                                SETTINGSMAIN = reader.GetString("SETTINGSMAIN"),
                                MAININTERFACE = reader.GetString("MAININTERFACE"),
                                STATE = reader.GetString("STATE"),
                                INVENTORY = reader.GetString("INVENTORY")
                                

                            });
                        }
                        catch (Exception e)
                        {
                            
                        }
                        
                    }
                }
            }

            return list;
        }
     public IList<SysTax> SystemConfigTax(SysTax data)
        {
            

            List<SysTax> list = new List<SysTax>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`SYSTAX` WHERE INSTITUTION = '{data.INSTITUTION}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            list.Add(new SysTax()
                            {
                                ID = reader.GetInt32("ID"),
                                OFFICE = reader.GetInt32("OFFICE"),
                                STATE = reader.GetInt32("STATE"),
                                INSTITUTION = reader.GetString("INSTITUTION")


                            });
                        }
                        catch (Exception e)
                        {
                            
                        }
                        
                    }
                }
            }

            return list;
        }
     
   
      
        public SysTax SetSysAccountingSettingTax(SysTax data)
        {
            string auth = $"INSERT INTO `admbasic`.`SYSTAX` (`INSTITUTION`,`STATE`)VALUES('{data.INSTITUTION}',{data.STATE}) ON DUPLICATE KEY UPDATE STATE = STATE";

            Executor(auth);
            return data;
        }
        public SysTax SetSysAccountingSettingUpdateTax(SysTax data)
        {
            string auth = $"UPDATE `admbasic`.`SYSTAX`SET  `STATE` = {data.STATE} WHERE INSTITUTION = '{data.INSTITUTION}'";

            Executor(auth);
            return data;
        }
        public TimeTrackerModel SetStartTimeOnTracker(TimeTrackerModel data)
        {
            string auth = $"INSERT INTO `admbasic`.`TIMETRACKER`(`CODE`,`INSTITUTION`,`TOTAL_OFFLINE`,`TOTAL_ONLINE`,`START`,`LUNCH`,`LUNCH_`,`LUNCH_S`,`BREAK`,`BREAK_`,`BREAK_S`,`OTHER`,`OTHER_`,`OTHER_S`,`END`,`DATE`,`TOTAL`,`BREAKL`,`BREAKL_`,`BREAK_SL`,`STATE`,`AUTHORIZED_BY`,`DATESH`) VALUE " +
                                                                $"({data.CODE},'{data.INSTITUTION}','{data.TOTAL_OFFLINE}','{data.TOTAL_ONLINE}','{data.START}','{data.LUNCH}','{data.LUNCH_}','{data.LUNCH_S}','{data.BREAK}','{data.BREAK_}','{data.BREAK_S}','{data.OTHER}','{data.OTHER_}','{data.OTHER_S}','{data.END}',current_date(),'{data.TOTAL}','{data.BREAKL}','{data.BREAKL_}','{data.BREAK_SL}','READY',{data.AUTHORIZED_BY},'{data.DATESH}')";

            Executor(auth);
            return data;
        }


        
        /// <summary>
        /// register end of jornal and sum total
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TimeTrackerModel SetENDTimeOnTracker(TimeTrackerModel data)
        {
            
            
            
            
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `END` = '{data.END}' , `STATE` = 'END' WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";

            Executor(auth);

            EndJornal(data);
            return data;
        }

        public TimeTrackerModel SetBreakTimeOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `BREAK` = '{data.BREAK}' , `STATE` = 'BREAK' WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";

            Executor(auth);
            return data;
        }
        
        public TimeTrackerModel SetSecondBreakTimeOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `BREAKL` = '{data.BREAKL}'  , `STATE` = 'BREAK' WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";

            Executor(auth);
            return data;
        }
        
        
        public TimeTrackerModel SetBreakTimeEndOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `BREAK_` = '{data.BREAK_}' , `STATE` = 'READY'  WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE =current_date())";

            Executor(auth);

            SumGlobalBreakTime(data);
                
            return data;
 
        }
        
        public TimeTrackerModel SetSecondBreakTimeEndOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `BREAKL_` = '{data.BREAKL_}' , `STATE` = 'BREAK' WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";

            Executor(auth);

            SumGlobalSecondBreakTime(data);
                
            return data;
 
        }
        
        public TimeTrackerModel SetSumOfBreakTimeOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `BREAK_S` = '{data.BREAK_S}'  WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";
            Executor(auth);
            return data;
        }
        public TimeTrackerModel SetSumOfSecondBreakTimeOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `BREAK_SL` = '{data.BREAK_SL}'  WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";
            Executor(auth);
            return data;
        }
        public TimeTrackerModel SetSumOfLunchTimeOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `LUNCH_S` = '{data.LUNCH_S}'  WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";
            Executor(auth);
            return data;
        }
        public TimeTrackerModel SetSumOfOtherTimeOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `OTHER_S` = '{data.OTHER_S}'  WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";
            Executor(auth);
            return data;
        }
        public TimeTrackerModel sumJornal(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `TOTAL` = '{data.TOTAL}', `TOTAL_OFFLINE`= '{data.TOTAL_OFFLINE}'  WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";
            Executor(auth);
            return data;
        }
        
        
/// <summary>
/// Sum the time on the tracker 
/// </summary>
/// <param name="data"></param>
        internal void SumGlobalBreakTime(TimeTrackerModel data)
        {
            int h1 = 0;
            int h2 = 0;
            int m1 = 0;
            int m2 = 0;
            int s1 = 0;
            int s2 = 0;

            int hs = 0;
            int ms = 0;
            int ss = 0;
            int i = 0;
            string breakTime = null;

            int coeficiente = 60;
            foreach (var item in GetTimeDataFromTracker(data))
            {
                if (item.BREAK != null)
                {
                    if (data.BREAK_ != null)
                    {
                        string tmf = item.BREAK.ToString();
                        string tms = item.BREAK_.ToString();
                        string[] words1 = tmf.Split(':');
                        string[] words2 = tms.Split(':');

                        ///hour
                        h1 = Convert.ToInt32(words1[0]);
                        h2 = Convert.ToInt32(words2[0]);
                        m1 = Convert.ToInt32(words1[1]);
                        m2 = Convert.ToInt32(words2[1]);
                        s1 = Convert.ToInt32(words1[2]);
                        s2 = Convert.ToInt32(words2[2]);
                        try
                        {
                        ms = m2 % m1;
                        hs = h2 % h1;
                        ss = s2 % s1;

                        breakTime = $"{hs}:{ms}:{ss}";
            
                        data.BREAK_S = breakTime;
                        SetSumOfBreakTimeOnTracker(data);
                        }
                        catch (Exception e)
                        {
                            
                        }
                        
                    }
                }
            }
        }

/// <summary>
/// Sum the time on the tracker 
/// </summary>
/// <param name="data"></param>
internal void SumGlobalSecondBreakTime(TimeTrackerModel data)
{
    int h1 = 0;
    int h2 = 0;
    int m1 = 0;
    int m2 = 0;
    int s1 = 0;
    int s2 = 0;

    int hs = 0;
    int ms = 0;
    int ss = 0;
    int i = 0;
    string breakTime = null;

    int coeficiente = 60;
    foreach (var item in GetTimeDataFromTracker(data))
    {
        if (item.BREAKL != null)
        {
            if (data.BREAKL_ != null)
            {
                string tmf = item.BREAKL.ToString();
                string tms = item.BREAKL_.ToString();
                string[] words1 = tmf.Split(':');
                string[] words2 = tms.Split(':');

                ///hour
                h1 = Convert.ToInt32(words1[0]);
                h2 = Convert.ToInt32(words2[0]);
                m1 = Convert.ToInt32(words1[1]);
                m2 = Convert.ToInt32(words2[1]);
                s1 = Convert.ToInt32(words1[2]);
                s2 = Convert.ToInt32(words2[2]);
                try
                {
                    ms = m2 % m1;
                    hs = h2 % h1;
                    ss = s2 % s1;

                    breakTime = $"{hs}:{ms}:{ss}";
            
                    data.BREAK_SL = breakTime;
                    SetSumOfSecondBreakTimeOnTracker(data);
                }
                catch (Exception e)
                {
                    
                }
                
            }
        }
    }
}

internal void SumGlobalLunchTime(TimeTrackerModel data)
{
    int h1 = 0;
    int h2 = 0;
    int m1 = 0;
    int m2 = 0;
    int s1 = 0;
    int s2 = 0;

    int hs = 0;
    int ms = 0;
    int ss = 0;
    int i = 0;
    string breakTime = null;

   
    foreach (var item in GetTimeDataFromTracker(data))
    {
        if (item.LUNCH != null)
        {
            if (data.LUNCH_ != null)
            {
                string tmf = item.LUNCH.ToString();
                string tms = item.LUNCH_.ToString();
                string[] words1 = tmf.Split(':');
                string[] words2 = tms.Split(':');

                ///hour
                h1 = Convert.ToInt32(words1[0]);
                h2 = Convert.ToInt32(words2[0]);
                m1 = Convert.ToInt32(words1[1]);
                m2 = Convert.ToInt32(words2[1]);
                s1 = Convert.ToInt32(words1[2]);
                s2 = Convert.ToInt32(words2[2]);
                try
                {
                    ms = m2 % m1;
                    hs = h2 % h1;
                    ss = s2 % s1;

                    breakTime = $"{hs}:{ms}:{ss}";
            
                    data.LUNCH_S = breakTime;
                    SetSumOfLunchTimeOnTracker(data);
                }
                catch (Exception e)
                {
                   
                }
                
            }
        }
    }
}

internal void SumGlobalOtherTime(TimeTrackerModel data)
{
    int h1 = 0;
    int h2 = 0;
    int m1 = 0;
    int m2 = 0;
    int s1 = 0;
    int s2 = 0;

    int hs = 0;
    int ms = 0;
    int ss = 0;
    int i = 0;
    string breakTime = null;

   
    foreach (var item in GetTimeDataFromTracker(data))
    {
        if (item.OTHER != null)
        {
            if (data.OTHER_ != null)
            {
                string tmf = item.OTHER.ToString();
                string tms = item.OTHER_.ToString();
                string[] words1 = tmf.Split(':');
                string[] words2 = tms.Split(':');

                ///hour
                h1 = Convert.ToInt32(words1[0]);
                h2 = Convert.ToInt32(words2[0]);
                m1 = Convert.ToInt32(words1[1]);
                m2 = Convert.ToInt32(words2[1]);
                s1 = Convert.ToInt32(words1[2]);
                s2 = Convert.ToInt32(words2[2]);

                try
                {
  ms = m2 % m1;
                hs = h2 % h1;
                ss = s2 % s1;

                breakTime = $"{hs}:{ms}:{ss}";
            
                data.OTHER_S = breakTime;
                SetSumOfOtherTimeOnTracker(data);
                }
                catch (Exception e)
                {
                   
                }
              
            }
        }
    }
}

internal void EndJornal(TimeTrackerModel data)
{
    int h1 = 0;
    int h2 = 0;
    int h3 = 0;
    int h4 = 0;
    int m1 = 0;
    int m2 = 0;
    int m3 = 0;
    int m4 = 0;
    int s1 = 0;
    int s2 = 0;
    int s3 = 0;
    int s4 = 0;

    int hs = 0;
    int ms = 0;
    int ss = 0;
    
    int hsf = 0;
    int msf = 0;
    int ssf = 0;
    int i = 0;
    string TotalTimeOnline = null;
    string TotalTimeOffline = null;
    string finalTime = null;
    string Totaltime = null;
    foreach (var item in GetTimeDataFromTracker(data))
    {
        if (item.START != null)
        {
            if (data.END != null)
            {
                string tmf = item.START.ToString();
                string tms = item.END.ToString();
                string[] words1 = tmf.Split(':');
                string[] words2 = tms.Split(':');

                ///hour
                h1 = Convert.ToInt32(words1[0]);
                h2 = Convert.ToInt32(words2[0]);
                m1 = Convert.ToInt32(words1[1]);
                m2 = Convert.ToInt32(words2[1]);
                s1 = Convert.ToInt32(words1[2]);
                s2 = Convert.ToInt32(words2[2]);
                try
                {
msf = m2 % m1;
                hsf = h2 % h1;
                ssf = s2 % s1;

                Totaltime = $"{hsf}:{msf}:{ssf}";
                }
                catch (Exception e)
                {
                    
                }
                
                
                
                if (true)
                {
                    string t1 = item.BREAK_S.ToString();
                    string t2 = item.LUNCH_S.ToString();
                    string t3 = item.OTHER_S.ToString();
                    string t4 = item.BREAK_SL.ToString();
                    string[] time = t1.Split(':');
                    string[] time2 = t2.Split(':');
                    string[] time3 = t3.Split(':');
                    string[] time4 = t4.Split(':');
                    ///time break down
                    if (item.BREAK_S != "-")
                    {
                        h1 = Convert.ToInt32(t1[0]);
                        m1 = Convert.ToInt32(t1[1]);
                        s1 = Convert.ToInt32(t1[2]);
                        
                        
                    }
                    else
                    {
                        h1 = 0;
                        m1 = 0;
                        s1 = 0;
                    }

                    if (item.LUNCH_S != "-")
                    {
                        h2 = Convert.ToInt32(t2[0]);
                        m2 = Convert.ToInt32(t2[1]);
                        s2 = Convert.ToInt32(t2[2]);
                    }
                    else
                    {
                        h2 = 0;
                        m2 = 0;
                        s2 = 0;
                    }

                    if (item.OTHER_S != "-")
                    {
                        h3 = Convert.ToInt32(t3[0]);
                        m3 = Convert.ToInt32(t3[1]);
                        s3 = Convert.ToInt32(t3[2]);
                    }
                    else
                    {
                        h3 = 0;
                        m3 = 0;
                        s3 = 0;
                    }
                    if (item.BREAK_SL != "-")
                    {
                        h4 = Convert.ToInt32(t4[0]);
                        m4 = Convert.ToInt32(t4[1]);
                        s4 = Convert.ToInt32(t4[2]);
                    }
                    else
                    {
                        h4 = 0;
                        m4 = 0;
                        s4 = 0;
                    }

                    try
                    {
                    ms = m1 + m2 + m3+m4;
                    hs = h1 + h2 + h3+m4;
                    ss = s1 + s2 +s3+m4;
                    
                    
                    TotalTimeOffline = $"{hs}:{ms}:{ss}";
                    }
                    catch (Exception e)
                    {
                       
                    }
                    
                }

                //data.TOTAL_ONLINE = Convert.ToInt32(Totaltime) - Convert.ToInt32(TotalTimeOffline);
                data.TOTAL = Totaltime;
                data.TOTAL_OFFLINE = TotalTimeOffline;
                sumJornal(data);
            }
        }
        
    }
    
}
        
        public TimeTrackerModel SetLunchTimeOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `LUNCH` = '{data.LUNCH}', `STATE` = 'LUNCH' WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";

            Executor(auth);
            return data;
        }
        
        
        public TimeTrackerModel SetLunchTimeEndOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `LUNCH_` = '{data.LUNCH_}',  `STATE` = 'READY' WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";

            Executor(auth);
            SumGlobalLunchTime(data);
            return data;
        }
        public TimeTrackerModel SetOtherTimeOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `OTHER` = '{data.OTHER}' , `STATE` = 'OTHER' WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";

            Executor(auth);
            return data;
        }
        
        public TimeTrackerModel SetOtherTimeEndOnTracker(TimeTrackerModel data)
        {
            string auth = $"UPDATE `admbasic`.`TIMETRACKER` SET `OTHER_` = '{data.OTHER_}' , `STATE` = 'READY' WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTITUTION}' and (DATE = current_date())";

            Executor(auth);
            return data;
        }
        

        public TimeTrackerModel AddOfflineTimeOnTracker(TimeTrackerModel data)
        {
            string auth = $"INSERT INTO `admbasic`.`TIMETRACKER`(`CODE`,`INSTITUTION`, `TOTAL_OFFLINE`,`DATE`) VALUE ({data.CODE},'{data.INSTITUTION}','{data.TOTAL_OFFLINE}','{data.TOTAL_ONLINE}','{data.START}','{data.END}',current_date()) ON DUPLICATE KEY UPDATE DATE = DATE+;";

            Executor(auth);
            return data;
        }

        public InstitutionModel AddInstitutionDetails(InstitutionModel data)
        {
            string auth = $"UPDATE `admbasic`.`INSTITUTIONPROFILE` SET `RNC` = {data.RNC},`NAME` = '{data.NAME}',`INSTITUTIONALEMAIL` = '{data.INSTITUTIONALEMAIL}',`DEF_PASS` = '{data.DEF_PASS}' where INSTITUTION = '{data.INSTITUTION}' ";

            Executor(auth);

            string InstitutionalEmail = null;
            
            var em = IGetPersonByCODE(data.INSTITUTION ,data.CODE);
            if (em.Count()>0)
            {
                foreach (var item in em)
                {
                    InstitutionalEmail = $"{item.F_NAME}{item.S_NAME}.{item.F_LASTN}{item.S_LASTN}{data.INSTITUTIONALEMAIL}";
                }
            }
            
            //Todo add data and time, set the name for the institution create method to load institUTION CODE
            string insemal = $"UPDATE `admbasic`.`AUTHENTICATION` SET `AlT_EMAIL` = '{InstitutionalEmail}' WHERE INSTITUTION = '{data.INSTITUTION}' and (CODE = {data.CODE});";

            Executor(insemal);
            
            return data;
        }
        
       
        
        public InstitutionModel UpdateInstitutionDetails(InstitutionModel data)
        {
            string auth = $"UPDATE `admbasic`.`INSTITUTIONPROFILE` SET `RNC` = {data.RNC},`NAME` = '{data.NAME}' where INSTITUTION = '{data.INSTITUTION}' ";

            Executor(auth);
            return data;
        }
        public InstitutionModel InstitutionIntance(string data)
        {
            string auth = $"INSERT INTO `admbasic`.`INSTITUTIONPROFILE`(`INSTITUTION`)VALUES('{data}')";

            Executor(auth);

            return null;
        }
        

        /// <summary>
        /// Licence activation query, get the tocken if is the same activate and move the serial to the basic data basse from admin databace
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IList<LicenceModel> GetActivateState(LicenceModel data)
        {


            List<LicenceModel> list = new List<LicenceModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`LICENCREGISTRY` WHERE INSTITUTION = '{data.INSTITUTION}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LicenceModel()
                        {

                            
                            TOCKEN = reader.GetString("TOKEN"),
                            STATE = reader.GetInt32("STATE"),
                            DATE = reader.GetString("DATE"),
                            PERIOD = reader.GetInt32("PERIOD"),
                            INSTITUTION = reader.GetString("INSTITUTION")
                            


                        });
                    }
                }
            }

            return list;
        }
        
       
        
          public  AddressProfile SaveAdress(AddressProfile data)
        {
       
            string query = $"INSERT INTO `admbasic`.`INSTITUTIONALADDRESS`(`INSTITUTION`,`LOCATION_TYPE`,`BUILDING_NAME`,`NUMBER`,`STREET`,`PROVINCE`,`CITY`,`CONTRY`,`PHONE`)VALUES('{data.INSTITUTION}',{data.LOCATION_TYPE},'{data.BUILDING_NAME}',{data.NUMBER},'{data.STREET}','{data.PROVINCE}','{data.CITY}','{data.CONTRY}','{data.PHONE}');";
             Executor(query);
             return data;
        }
          
          public  PagesDirectoryModel UpdateSystemAccess(PagesDirectoryModel data)
          {
       
              string query = $"UPDATE `admbasic`.`SYSTEMDIRACCESS` SET `CASHREGISTER` = '{data.CASHREGISTER}',`ACCOUNTING` = '{data.ACCOUNTING}',`HHRR` = '{data.HHRR}', `INVENTORY` = '{data.INVENTORY}' ,`STATE` = '{data.STATE}' WHERE CODE = {data.CODE} and INSTITUTION ='{data.INSTITUTION}';";
              Executor(query);
              return data;
          }

          public  AuthenticationModel UpdateUserPassword(AuthenticationModel data)
          {
       
              string query = $"UPDATE `admbasic`.`AUTHENTICATION` SET `PASSWORD` = '{data.PASSWORD}' WHERE `CODE` = {data.CODE} and INSTITUTION = '{data.INSTIITUTION}';";
              Executor(query);
              return data;
          }
      

        public IEnumerable<AddressProfile> getInstitutionalAddress(string institution)
        {
            List<AddressProfile> list = new List<AddressProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`INSTITUTIONALADDRESS` WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AddressProfile()
                        {

                            ID = reader.GetInt32("ID"),
                            BUILDING_NAME = reader.GetString("BUILDING_NAME"),
                            CONTRY = reader.GetString("CONTRY"),
                            NUMBER = reader.GetInt32("NUMBER"),
                            LOCATION_TYPE = reader.GetInt32("LOCATION_TYPE"),
                            PROVINCE = reader.GetString("PROVINCE"),
                            CITY = reader.GetString("CITY"),
                            PHONE = reader.GetString("PHONE"),
                            STREET = reader.GetString("STREET"),
                           



                        }) ;
                    }
                }
            }

            return list;
        }


        
      



       



internal void Executor(string query)
        {
            if (!string.IsNullOrEmpty(query))
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
            
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++Accounting+++++++++++++++++
        public IEnumerable<StockModel> IGetByIDAndInstitutionalCode(string institution)
        {
            List<StockModel> list = new List<StockModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`STOCKS` WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StockModel()
                        {
                          ID = reader.GetInt32("ID"),
                            GROUPOF =reader.GetInt32("GROUPOF"),

                            BARCODE = reader.GetString("BARCODE"),

                            NAME = reader.GetString("NAME"),

                            BRAND = reader.GetString("BRAND"),

                            DESCRIPTION = reader.GetString("DESCRIPTION"),

                            

                            VALUE = reader.GetFloat("VALUE"),

                            STOCK = reader.GetFloat("STOCK"),
PROVIDER = reader.GetInt32("PROVIDER"),
                            

                            TAX = reader.GetString("TAX"),

                            PRICE = reader.GetFloat("PRICE"),

DR_ACCOUNT = reader.GetInt32("DR_ACCOUNT"),
CR_ACCOUNT = reader.GetInt32("CR_ACCOUNT"),
ACCOUNTING_SEAT = reader.GetString("ACCOUNTING_SEAT"),
                            

                            INSTITUTION = reader.GetString("INSTITUTION"),


                        });
                    }
                }
            }

            return list;
        }
        
        public IEnumerable<StockModel> IGetStockByIDAndInstitutionalCode(string institution,int id)
        {
            List<StockModel> list = new List<StockModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`STOCKS` WHERE INSTITUTION = '{institution}' and ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StockModel()
                        {
                          
                            ID =reader.GetInt32("ID"),
                            
                            GROUPOF =reader.GetInt32("GROUPOF"),

                            BARCODE = reader.GetString("BARCODE"),

                            NAME = reader.GetString("NAME"),

                            BRAND = reader.GetString("BRAND"),

                            DESCRIPTION = reader.GetString("DESCRIPTION"),

                            PROVIDER = reader.GetInt32("PROVIDER"),

                            VALUE = reader.GetFloat("VALUE"),

                            STOCK = reader.GetFloat("STOCK"),
                            
                            TAX = reader.GetString("TAX"),

                            PRICE = reader.GetFloat("PRICE"),
                            
                            DR_ACCOUNT = reader.GetInt32("DR_ACCOUNT"),
                            
                            CR_ACCOUNT = reader.GetInt32("DR_ACCOUNT"),
                            
                            ACCOUNTING_SEAT = reader.GetString("ACCOUNTING_SEAT"),

                            



                        });
                    }
                }
            }

            return list;
        }
        




        public StockModel SaveProductInStock(StockModel data)
        {
            
            
                        
            string query = $"INSERT INTO `admbasic`.`STOCKS`(`PROVIDER`,`GROUPOF`,`BARCODE`,`NAME`,`BRAND`,`DESCRIPTION`,`VALUE`,`STOCK`,`MASSUNITY`,`TAX`,`PRICE`,`INSTITUTION`,`UID`,`DR_ACCOUNT`,`CR_ACCOUNT`, `ACCOUNTING_SEAT`)VALUES({data.PROVIDER},{data.GROUPOF},'{data.BARCODE}','{data.NAME}','{data.BRAND}','{data.DESCRIPTION}',{data.VALUE},{data.STOCK},'{data.MASSUNITY}', '{data.TAX}',{data.PRICE},'{data.INSTITUTION}',{data.UID},{data.DR_ACCOUNT},{data.CR_ACCOUNT},'{data.ACCOUNTING_SEAT}') ;";
          Executor(query);
            return data;

        }
        
        public ProviderProfile UpdateProvider(ProviderProfile data)
        {
            
            
                        
            string query = $"UPDATE `admbasic`.`PROVIDER`SET `NAME` = '{data.NAME}',`PHONE` = '{data.PHONE}',`EMAIL` = '{data.EMAIL}',`ADDRESS` = '{data.ADDRESS}',`ORDER_LIMIT` = {data.ORDER_LIMIT} WHERE ID = {data.ID} and INSTITUTION = '{data.INSTITUTION}' ;";
            Executor(query);
            return data;

        }


        
        public ProviderProfile AddNewProvider(ProviderProfile data)
        {
            if (!string.IsNullOrEmpty(data.NAME))
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    try
                    {
                        string query = $"INSERT INTO `admbasic`.`PROVIDER`(`INSTITUTION`,`NAME`,`PHONE`,`EMAIL`,`ADDRESS`,`ORDER_LIMIT`,`NOTE`,`CATEGORY`)VALUES('{data.INSTITUTION}','{data.NAME}','{data.PHONE}','{data.EMAIL}','{data.ADDRESS}','{data.ORDER_LIMIT}','{data.NOTE}','{data.CATEGORY}');";
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        int result = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("not entered");
                        //lblError.Text = ex.Message;
                    }
                }
            }
            return data;
        }

        public StockModel UpdataStock(StockModel data)
        {
            if (!string.IsNullOrEmpty(data.BARCODE))
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    try
                    {
                        string query = $"UPDATE `admbasic`.`STOCKS`SET `PROVIDER` = {data.PROVIDER},`GROUPOF` = {data.GROUPOF},`BARCODE` = '{data.BARCODE}',`NAME` = '{data.NAME}',`BRAND` = '{data.BRAND}',`DESCRIPTION` = '{data.DESCRIPTION}',`VALUE` = {data.VALUE},`STOCK` = {data.STOCK},`MASSUNITY` = '{data.MASSUNITY}',`TAX` = '{data.TAX}',`PRICE` = {data.PRICE} WHERE `ID` = {data.ID} and INSTITUTION = '{data.INSTITUTION}';";
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        int result = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("not entered");
                        //lblError.Text = ex.Message;
                    }
                }
            }
            return data;
        }




      








        public FontsEXModel IUpdate(FontsEXModel data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ACCModel> ILoadAllAccounts(string institution)
        {
            List<ACCModel> list = new List<ACCModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`ACCOUNTS` where INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ACCModel()
                        {
                            ID = reader.GetInt32("ID"),
                            TYPE = reader.GetString("TYPE"),
                            CATEGORY = reader.GetString("CATEGORY"),
                            TRACKING = reader.GetString("TRACKING"),
                            NAME = reader.GetString("NAME"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            CURRENT_VALANCE = reader.GetFloat("CURRENT_VALANCE"),
                            INITIAL_VALANCE =  reader.GetFloat("INITIAL_VALANCE"),
                            RELATION = reader.GetInt32("RELATION"),
                        });
                    }
                }
            }
            return list;
        }
        
        public IEnumerable<ACCModel> ILoadAllAccountsById(string institution, int id)
        {
            List<ACCModel> list = new List<ACCModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`ACCOUNTS` where INSTITUTION = '{institution}' and ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ACCModel()
                        {
                            ID = reader.GetInt32("ID"),
                            TYPE = reader.GetString("TYPE"),
                            CATEGORY = reader.GetString("CATEGORY"),
                            TRACKING = reader.GetString("TRACKING"),
                            NAME = reader.GetString("NAME"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            CURRENT_VALANCE = reader.GetFloat("CURRENT_VALANCE"),
                            INITIAL_VALANCE =  reader.GetFloat("INITIAL_VALANCE"),
                            RELATION = reader.GetInt32("RELATION"),
                        });
                    }
                }
            }
            return list;
        }


        public ACCModel UpdateAccountData(ACCModel data)
        {

            string query = $"UPDATE `admbasic`.`ACCOUNTS`SET `NAME` = '{data.NAME}',`DESCRIPTION` = '{data.DESCRIPTION}',`CURRENT_VALANCE` = {data.CURRENT_VALANCE} WHERE INSTITUTION = '{data.INSTITUTION}' and ID = {data.ID};";
            Executor(query);
            return data;
        }
        
        
        public ACCHistory RegisterOnAcc(ACCHistory data)
        {
            try
            {
                var loadaccountinfo = ILoadAllAccountsById(data.INSTITUTION , data.ACC_CODE);
                foreach (var item in loadaccountinfo)
                {
                  
                    //update account valance cr
                    string updacc = $"UPDATE `admbasic`.`ACCOUNTS` SET `CURRENT_VALANCE` = {data.AMOUNT} WHERE `ID` = {data.ACC_CODE} and(INSTITUTION = '{data.INSTITUTION}');";

                    Executor(updacc);
                    
                    
                    //registe on history
                    string inc = $"INSERT INTO `admbasic`.`ACCHISTORY`(`INSTITUTION`,`TYPE`,`CATEGORY`,`DATE`,`DETAILS`,`AMOUNT`,`ACC_CODE`,`POW_CODE`,`CODE`)VALUES('{data.INSTITUTION}','{data.TYPE}','{data.CATEGORY}',curdate(),'{data.DETAILS}',{data.VAR},{data.ACC_CODE},'{data.POW_CODE}',{data.CODE});";

                    Executor(inc);
                
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return data;
        }
        
        
        
        public IList<ACCModel> LoadAccountData(string institution, int id)
        {
            List<ACCModel> list = new List<ACCModel>();

            using (MySqlConnection conn = GetConnection())
            {
                string search = $"SELECT * FROM `admbasic`.`ACCOUNTS` WHERE ID = {id} and(INSTITUTION = '{institution}');";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(search, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new ACCModel()
                        {
                            ID = reader.GetInt32("ID"),
                            NAME = reader.GetString("NAME"),
                            TYPE =  reader.GetString("TYPE"),
                            CATEGORY = reader.GetString("CATEGORY"),
                            CURRENT_VALANCE = reader.GetFloat("CURRENT_VALANCE"),
                            INITIAL_VALANCE = reader.GetFloat("INITIAL_VALANCE"),
                        });
                       
                        
                        
                    }
                }
            }

            return list;
        }
        
        
        public IEnumerable<ProviderProfile> GetProvidersByID(int id, string institution)
        {
            List<ProviderProfile> list = new List<ProviderProfile>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PROVIDER` where INSTITUTION = '{institution}' and ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ProviderProfile()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            NAME = reader.GetString("NAME"),
                            PHONE = reader.GetString("PHONE"),
                            EMAIL = reader.GetString("EMAIL"),
                            ADDRESS = reader.GetString("ADDRESS"),
                            NOTE = reader.GetString("NOTE"),
                            CATEGORY = reader.GetString("CATEGORY"),
                        });
                    }
                }
            }
            return list;
        }
        public ACCModel SaveAccount(ACCModel data)
        {
            if (!string.IsNullOrEmpty(data.NAME))
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    try
                    {
                        string query = $"INSERT INTO `admbasic`.`ACCOUNTS`(`TYPE`,`CATEGORY`,`TRACKING`,`NAME`,`DESCRIPTION`,`CURRENT_VALANCE`,`RELATION`,`INSTITUTION`,`INITIAL_VALANCE`)VALUES('{data.CATEGORY}','{data.TYPE}','{data.TRACKING}','{data.NAME}','{data.DESCRIPTION}',{data.INITIAL_VALANCE},{data.RELATION},'{data.INSTITUTION}',{data.INITIAL_VALANCE});";
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        int result = cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("not entered");
                        //lblError.Text = ex.Message;
                    }
                }
            }
            return data;
        }

        public StockModel UpdateStock(StockModel data)
        {
            if (!string.IsNullOrEmpty(data.NAME))
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    try
                    {
                        string query = $"UPDATE `admbasic`.`STOCKS` `SET` `VALUE` = {data.VALUE},`STOCK` = {data.STOCK},`PRICE` = {data.PRICE},WHERE `INSTITUTION` = '{data.INSTITUTION}'";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        int result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("not entered");
                        //lblError.Text = ex.Message;
                    }
                }
            }
            return data;
        }
        
        
        public IList<ACCHistory> LoadAllAccountHistory(string institution)
        {
            List<ACCHistory> list = new List<ACCHistory>();

            using (MySqlConnection conn = GetConnection())
            {
                string search = $"SELECT * FROM `admbasic`.`ACCHISTORY`  WHERE  INSTITUTION = '{institution}' and( DATE = CURDATE( ))";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(search, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new ACCHistory()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            TYPE = reader.GetString("TYPE"),
                            CATEGORY = reader.GetString("CATEGORY"),
                            DATE = reader.GetString("DATE"),
                            DETAILS = reader.GetString("DETAILS"),
                            AMOUNT = reader.GetFloat("AMOUNT"),
                        });
                       
                        
                        
                    }
                }
            }

            return list;
        }
        
        
        public IList<ACCHistory> LoadAllAccountTransferHistory(string institution, string data)
        {
            List<ACCHistory> list = new List<ACCHistory>();

            using (MySqlConnection conn = GetConnection())
            {
                string search = $"SELECT * FROM `admbasic`.`ACCHISTORY` WHERE TYPE = '{data}' and INSTITUTION = '{institution}';";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(search, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new ACCHistory()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            TYPE = reader.GetString("TYPE"),
                            CATEGORY = reader.GetString("CATEGORY"),
                            DATE = reader.GetString("DATE"),
                            DETAILS = reader.GetString("DETAILS"),
                            AMOUNT = reader.GetFloat("AMOUNT"),
                        });
                    }
                }
            }

            return list;
        }
        
        //https://www.plus2net.com/sql_tutorial/date-lastweek.php
        public IList<ACCHistory> LoadAllAccountHistorySpecificDate(string institution,string from, string to, string account, string data)
        {
            
            List<ACCHistory> list = new List<ACCHistory>();
            DateTime date_ = DateTime.Now;
            string date = date_.ToString("MM/dd/yyyy");
            string query = null;
            using (MySqlConnection conn = GetConnection())
            {
                if (account == "ALL")
                {
                    switch (data)
                    {
                        case"MONTHPREVIOUS":
                            query = $"SELECT * FROM `admbasic`.`ACCHISTORY`  WHERE `date` >= DATE_SUB(CURDATE(), INTERVAL 1 MONTH) and(INSTITUTION = '{institution}')";
                            break;
                        case"MONTH":
                            query = $"SELECT * FROM `admbasic`.`ACCHISTORY`  WHERE date between  DATE_FORMAT(CURDATE() ,'%Y-%m-01') AND CURDATE() and(INSTITUTION = '{institution}')";
                            break;
                        case "15 DAY":
                            query = $"SELECT * FROM `admbasic`.`ACCHISTORY`  WHERE `date` BETWEEN DATE_SUB( CURDATE( ) ,INTERVAL 15 DAY ) AND CURDATE( ) and(INSTITUTION = '{institution}')";
                            break;
                        case"TODAY":
                            query = $"SELECT * FROM `admbasic`.`ACCHISTORY`  WHERE  INSTITUTION = '{institution}' and( DATE = CURDATE( ))";
                            break;
                    }
                }
                else
                {
                    switch (data)
                    {
                        case"MONTHPREVIOUS":
                            query = $"SELECT * FROM `admbasic`.`ACCHISTORY`  WHERE `date` >= DATE_SUB(CURDATE(), INTERVAL 1 MONTH) and(INSTITUTION = '{institution}')";
                            break;
                        case"MONTH":
                            query = $"SELECT * FROM `admbasic`.`ACCHISTORY`  WHERE date between  DATE_FORMAT(CURDATE() ,'%Y-%m-01') AND CURDATE() and(INSTITUTION = '{institution}') and (TYPE = '{account}')";
                            break;
                        case "15 DAY":
                            query = $"SELECT * FROM `admbasic`.`ACCHISTORY`  WHERE `date` BETWEEN DATE_SUB( CURDATE( ) ,INTERVAL 15 DAY ) AND CURDATE( ) and(INSTITUTION = '{institution}') and (TYPE = '{account}')";
                            break;
                        case"TODAY":
                            query = $"SELECT * FROM `admbasic`.`ACCHISTORY`  WHERE  INSTITUTION = '{institution}' and( DATE = CURDATE( )) and (TYPE = '{account}')";
                            break;
                    }
                }
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        list.Add(new ACCHistory()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            TYPE = reader.GetString("TYPE"),
                            CATEGORY = reader.GetString("CATEGORY"),
                            DATE = reader.GetString("DATE"),
                            DETAILS = reader.GetString("DETAILS"),
                            AMOUNT = reader.GetFloat("AMOUNT"),
                        });
                    }
                }
            }

            return list;
        }

        public StockModel UpdateStockVolume(StockModel data)
        {
            string query = $"UPDATE `admbasic`.`STOCKS` SET `VALUE` = {data.VALUE},`STOCK` = {data.STOCK},`PRICE` = {data.PRICE}, `MASSUNITY` = '{data.MASSUNITY}'WHERE `INSTITUTION` = '{data.INSTITUTION}' and ID = {data.ID}";
            Executor(query);
            return data;
        }
        
        /// <summary>
        /// Transfer fornt, 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ACCLogModel TransferFonts(ACCLogModel data)
        {
            float combine = 0;

            var TRSERIAL = RandomStringGenerator(16);
            try
            {
                //load cr account
                foreach (var cr in ILoadAllAccountsById(data.INSTITUTION,data.CR_ACCOUNNT))
                {
                    //update cr
                    
                    combine = cr.CURRENT_VALANCE + data.AMOUNT;
                    string updacc = $"UPDATE `admbasic`.`ACCOUNTS`SET `CURRENT_VALANCE` = {combine} WHERE `ID` = {data.CR_ACCOUNNT}";
                    Executor(updacc);
                    //UPdate account history
                    string acch = $"INSERT INTO `admbasic`.`ACCHISTORY`(`INSTITUTION`,`TYPE`,`CATEGORY`,`DATE`,`DETAILS`,`AMOUNT`,`ACC_CODE`,`POW_CODE`,`CODE`)VALUES('{data.INSTITUTION}','TRANSFER','CR',curdate(),'{cr.NAME}',{data.AMOUNT},{data.CR_ACCOUNNT},'{TRSERIAL}',{data.CODE});";

                    Executor(acch);
                }
                //load dr account
                foreach (var dr in ILoadAllAccountsById(data.INSTITUTION,data.DR_ACCOUNNT))
                {
                    //update dr
                    combine = dr.CURRENT_VALANCE - data.AMOUNT;
                    string updacc = $"UPDATE `admbasic`.`ACCOUNTS`SET `CURRENT_VALANCE` = {combine} WHERE `ID` = {data.DR_ACCOUNNT}";
                    Executor(updacc);
                    //UPdate account history
                    string acch = $"INSERT INTO `admbasic`.`ACCHISTORY`(`INSTITUTION`,`TYPE`,`CATEGORY`,`DATE`,`DETAILS`,`AMOUNT`,`ACC_CODE`,`POW_CODE`,`CODE`)VALUES('{data.INSTITUTION}','TRANSFER','DR',curdate(),'{dr.NAME}',{data.AMOUNT},{data.DR_ACCOUNNT},'{TRSERIAL}',{data.CODE});";

                    Executor(acch);
                }
                
                string qs = $"INSERT INTO `admbasic`.`ACCTRANSACTIONLOG`(`CODE`,`INSTITUTION`,`DATE`,`TIME`,`DR_ACCOUNNT`,`CR_ACCOUNNT`,`AMOUNT`,`STATE`,`TRSERIAL`)VALUES({data.CODE},'{data.INSTITUTION}',curdate(),curtime(),{data.DR_ACCOUNNT},{data.CR_ACCOUNNT},{data.AMOUNT},'APROVED','{TRSERIAL}')";
                Executor(qs);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return data;
        }
            
        public StockCategory AddStockCategory(StockCategory data)
        {
            string query = $"INSERT INTO  `admbasic`.`STOCKCATEGORY`(`NAME`,`DESCRIPTION`,`INSTITUTION`) VALUE('{data.NAME}','{data.DESCRIPTION}','{data.INSTITUTION}')";
            Executor(query);
            return data;
        }

        public SysTax SaveTaxesPayment(SysTax data)
        {
            string query = $"INSERT INTO `admbasic`.`SYSTAX`(`INSTITUTION`,`STATE`,`AUTHORIZED_BY`,`SERIAL`,`PAYED_AT`,`AMOUNT`,`ACCOUNT_DEBITED`)VALUES('{data.INSTITUTION}',7,{data.AUTHORIZED_BY},{data.SERIAL},'{data.PAYED_AT}',{data.AMOUNT},{data.ACCOUNT_DEBITED})";
            Executor(query);
            return data;
        }
        
        public IEnumerable<SysTax> LoadTaxHistory(string institution)
        {
            List<SysTax> list = new List<SysTax>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`SYSTAX` WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SysTax()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            STATE = reader.GetInt32("STATE"),
                           
                            AUTHORIZED_BY = reader.GetInt32("AUTHORIZED_BY"),
                            SERIAL = reader.GetInt32("SERIAL"),
                            PAYED_AT = reader.GetString("PAYED_AT"),
                            AMOUNT = reader.GetFloat("AMOUNT"),
                            ACCOUNT_DEBITED = reader.GetInt32("ACCOUNT_DEBITED"),
                        });
                    }
                }
            }
            return list;
        }

        
        public IEnumerable<StockCategory> LoadStockCategory(string institution)
        {
            List<StockCategory> list = new List<StockCategory>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM admbasic.STOCKCATEGORY WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StockCategory()
                        {
                            ID = reader.GetInt32("ID"),
                            NAME = reader.GetString("NAME"),
                            DESCRIPTION = reader.GetString("DESCRIPTION")
                        });
                    }
                }
            }
            return list;
        }

        public ACCModel UpdataAccounBalance(ACCModel data)
        {
            string query =$"UPDATE `admbasic`.`ACCOUNTS` SET `INITIAL_VALANCE` = {data.INITIAL_VALANCE}, `CURRENT_VALANCE` = {data.CURRENT_VALANCE}WHERE INSTITUTION = '{data.INSTITUTION}'";
            Executor(query);
            return data;
        }

        public IEnumerable<StockModel> ISumAll(string institution)
        {
            List<StockModel> list = new List<StockModel>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT SUM(STOCK) AS SUMOFSTUCK FROM admbasic.STOCKS WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StockModel()
                        {
                            SUMSTOCK = reader.GetFloat("SUMOFSTUCK"),
                        });
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// if a stock is less than 10, 
        /// </summary>
        /// <param name="institution"></param>
        /// <returns></returns>
        public IEnumerable<StockModel> AlertVolume(string institution)
        {
            List<StockModel> list = new List<StockModel>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM admbasic.STOCKS WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new StockModel()
                        {
                            ID = reader.GetInt32("ID"),
                            STOCK = reader.GetFloat("STOCK"),
                            NAME = reader.GetString("NAME"),
                            MASSUNITY = reader.GetString("MASSUNITY")
                        });
                    }
                }
            }
            return list;
        }
        
        
        public IEnumerable<ProviderProfile> LoadAllProviders(string institution)
        {
            List<ProviderProfile> list = new List<ProviderProfile>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM admbasic.PROVIDER WHERE INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ProviderProfile()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            NAME = reader.GetString("NAME"),
                            PHONE = reader.GetString("PHONE"),
                            EMAIL = reader.GetString("EMAIL"),
                            ADDRESS = reader.GetString("ADDRESS"),
                            ORDER_LIMIT = reader.GetInt32("ORDER_LIMIT"),
                            NOTE = reader.GetString("NOTE"),
                            CATEGORY = reader.GetString("CATEGORY"),
                        });
                    }
                }
            }
            return list;
        }
        public IEnumerable<ProviderProfile> GetProviderById(string institution,int id)
        {
            List<ProviderProfile> list = new List<ProviderProfile>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM admbasic.PROVIDER WHERE INSTITUTION = '{institution}' and ID = {id};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ProviderProfile()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            NAME = reader.GetString("NAME"),
                            PHONE = reader.GetString("PHONE"),
                            EMAIL = reader.GetString("EMAIL"),
                            ADDRESS = reader.GetString("ADDRESS"),
                            ORDER_LIMIT = reader.GetInt32("ORDER_LIMIT"),
                            NOTE = reader.GetString("NOTE"),
                            CATEGORY = reader.GetString("CATEGORY"),
                        });
                    }
                }
            }
            return list;
        }
        
  
        /// <summary>
        /// load data from accounnt all taxable accounts
        /// load data from employee payment all employee
        /// </summary>
        public void CompileCurrentTaxes()
        {
           //string query = $"SELECT * FROM `admbasic`.`CLOSINGDAY` where INSTITUTION = '{institution}'";
          // Executor(query);
                
        }
        
        public IEnumerable<ACCModel> CompileAccount(string institution)
        {
           
            List<ACCModel> list = new List<ACCModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`ACCOUNTS` where INSTITUTION = '{institution}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ACCModel()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            TYPE = reader.GetString("TYPE"),
                            CATEGORY = reader.GetString("CATEGORY"),
                            TRACKING = reader.GetString("TRACKING"),
                            NAME = reader.GetString("NAME"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            CURRENT_VALANCE = reader.GetFloat("CURRENT_VALANCE"),
                            INITIAL_VALANCE =  reader.GetFloat("INITIAL_VALANCE"),
                            RELATION = reader.GetInt32("RELATION"),
                            
                        });
                    }
                }
            }
            return list;
        }


        public RecruitmentCampaign CreateRecruitmentCampaign(RecruitmentCampaign data)
        {
            string query =$"INSERT INTO `admbasic`.`RecruitmentCampaign` (INSTITUTION, CAMPAIG_NAME, CREATED_BY, DETAILS,DATE_TIME_CREATION, DATE_TIME_START,DATE_TIME_FINISH, STATUS) VALUE ('{data.INSTITUTION}','{data.CAMPAIG_NAME}',{data.CREATED_BY},'{data.DETAILS}',current_date(),'{data.DATE_TIME_START}','{data.DATE_TIME_FINISH}',{data.STATUS})";
            Executor(query);
            return data;
        }

        public IList<RecruitmentCampaign> LoadAllRecruitmentCampaignByInstitution(string data)
        {
            List<RecruitmentCampaign> list = new List<RecruitmentCampaign>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`RecruitmentCampaign` WHERE INSTITUTION = '{data}';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new RecruitmentCampaign()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            CAMPAIG_NAME = reader.GetString("CAMPAIG_NAME"),
                            CREATED_BY = reader.GetString("CREATED_BY"),
                            DETAILS = reader.GetString("DETAILS"),
                            DATE_TIME_CREATION = reader.GetString("DATE_TIME_CREATION"),
                            DATE_TIME_START = reader.GetString("DATE_TIME_START"),
                            DATE_TIME_FINISH = reader.GetString("DATE_TIME_FINISH"),
                            STATUS = reader.GetInt32("STATUS"),
                        });
                    }
                }
            }
            return list;
        }

        public IEnumerable<RecruitmentCampaign> LoadAllRecruitmentCampaignByInstitutionAndUser(RecruitmentCampaign data)
        {
            List<RecruitmentCampaign> list = new List<RecruitmentCampaign>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`RecruitmentCampaign` WHERE INSTITUTION = '{data.INSTITUTION}' and CREATED_BY = {data.CREATED_BY};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new RecruitmentCampaign()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            CAMPAIG_NAME = reader.GetString("CAMPAIG_NAME"),
                            CREATED_BY = reader.GetString("CREATED_BY"),
                            DETAILS = reader.GetString("DETAILS"),
                            DATE_TIME_CREATION = reader.GetString("DATE_TIME"),
                            DATE_TIME_START = reader.GetString("DATE_TIME"),
                            DATE_TIME_FINISH = reader.GetString("DATE_TIME"),
                            STATUS = reader.GetInt32("STATUS"),
                        });
                    }
                }
            }
            return list;
        }
        public PositionDetails CreateNewPositionForApplicant(PositionDetails data)
        {
            string query =$"INSERT INTO `admbasic`.`PositionDetails` (INSTITUTION , POSITION_ID , POSITION_DETAILS , DESIRED_SKILLS, MANDATORY_SKILLS, ACADEMIC_GRADE, CONTRACT_TYPE, WAGE, REPORT_TO, PREVIOUS_EXPERIENCE, DEPARMENT, LOCATION, CREATED_BY, CURRENT_STATUS, DATE_TIME_CREATION, DATE_TIME_START, DATE_TIME_FINISH) VALUE ('{data.INSTITUTION}', {data.POSITION_ID}, '{data.POSITION_DETAILS}', '{data.DESIRED_SKILLS}', '{data.MANDATORY_SKILLS}', '{data.ACADEMIC_GRADE}', '{data.CONTRACT_TYPE}', {data.WAGE}, '{data.REPORT_TO}', {data.PREVIOUS_EXPERIENCE}, '{data.DEPARMENT}', '{data.LOCATION}', '{data.CREATED_BY}', {data.CURRENT_STATUS}, current_date(), '{data.DATE_TIME_START}', '{data.DATE_TIME_FINISH}')";
            Executor(query);
            return data;
        }

        public IEnumerable<PositionDetails> LoadAllPositionDetailsForApplicantByInstitutionAndCode(int code)
        {
            List<PositionDetails> list = new List<PositionDetails>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PositionDetails` WHERE ID = {code}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PositionDetails()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            POSITION_ID = reader.GetInt32("POSITION_ID"),
                            POSITION_DETAILS = reader.GetString("POSITION_DETAILS"),
                            DESIRED_SKILLS = reader.GetString("DESIRED_SKILLS"),
                            MANDATORY_SKILLS = reader.GetString("MANDATORY_SKILLS"),
                            ACADEMIC_GRADE = reader.GetString("ACADEMIC_GRADE"),
                            CONTRACT_TYPE = reader.GetString("CONTRACT_TYPE"),
                            WAGE = reader.GetFloat("WAGE"),
                            REPORT_TO = reader.GetString("REPORT_TO"),
                            PREVIOUS_EXPERIENCE = reader.GetInt32("PREVIOUS_EXPERIENCE"),
                            DEPARMENT = reader.GetString("DEPARMENT"),
                            LOCATION = reader.GetString("LOCATION"),
                            CREATED_BY = reader.GetString("CREATED_BY"),
                            CURRENT_STATUS = reader.GetInt32("CURRENT_STATUS"),
                            DATE_TIME_CREATION = reader.GetString("DATE_TIME_CREATION"),
                            

                        });
                    }
                }
            }
            return list;
        }


        public IEnumerable<PositionDetails> GetEmailTemplateCampaign(string ins,int code)
        {
            List<PositionDetails> list = new List<PositionDetails>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PositionDetails` WHERE INSTITUTION = '{ins}' and ID = {code} ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PositionDetails()
                        {
                            EMAIL_TEMPLATE = reader.GetString("EMAIL_TEMPLATE"),
                        
                        });
                    }
                }
            }
            return list;
        }

        public IEnumerable<PersonalTodoList> LoadAllTodoTaskList(string ins,int code)
        {
            List<PersonalTodoList> list = new List<PersonalTodoList>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PERSONALTODOLIST` WHERE INSTITUTION = '{ins}' and CODE = {code} ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonalTodoList()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            TASK = reader.GetString("TASK"),
                            DATE_TIME = reader.GetString("DATE_TIME"),
                            STATE = reader.GetInt32("STATE"),

                        
                        });
                    }
                }
            }
            return list;
        }

        public PersonalTodoList SaveTaskOnPersonalProfile(PersonalTodoList data)
        {
            string query =$"INSERT into `admbasic`.`PERSONALTODOLIST` (CODE ,INSTITUTION ,TASK ,DATE_TIME, CREATION_DATE,STATE ) value ({data.CODE},'{data.INSTITUTION}','{data.TASK}','{data.DATE_TIME}',current_date(),{data.STATE})";
            Executor(query);
            return data;
        }

        public IEnumerable<ApplicantNote> LoadNoteOfApplicantProfile(string ins,int applicant)
        {
            List<ApplicantNote> list = new List<ApplicantNote>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`APPLICANTNOTE` WHERE INSTITUTION = '{ins}' and ApplicantProfile = {applicant} ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ApplicantNote()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            ApplicantProfile = reader.GetInt32("ApplicantProfile"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            TITLE = reader.GetString("TITLE"),
                            BODY = reader.GetString("BODY"),
                            DATE_TIME = reader.GetString("DATE_TIME"),
                            COLOR = reader.GetString("COLOR"),

                        
                        });
                    }
                }
            }
            return list;
        }
        public ApplicantNote UpdateNoteIntoApplicantProfile(ApplicantNote data)
        {
            string query =$"UPDATE `admbasic`.`APPLICANTNOTE` set BODY = '{data.BODY}', TITLE = '{data.TITLE}' where INSTITUTION = '{data.INSTITUTION}' ID = {data.ID}";
            Executor(query);
            return data;
        }

        public TEAMPROFILE SaveNewTeamProfile(TEAMPROFILE data)
        {
            string query =$"INSERT INTO `admbasic`.`TEAMPROFILE` (INSTITUTION , NAME , INITIALS , LOCATION , JUSTIFICATION , OBJETIVE , DESCRIPTION , CREATED_BY , DATETIME,DEPARTMENT) VALUE ('{data.INSTITUTION}'  ,'{data.NAME}'  ,'{data.INITIALS}'  ,'{data.LOCATION}'  ,'{data.JUSTIFICATION}'  ,'{data.OBJETIVE}'  ,'{data.DESCRIPTION}'  ,{data.CREATED_BY}  , current_date(),{data.DEPARTMENT})";
            Executor(query);
            return data;
        }

        public IEnumerable<TEAMPROFILE> LoadAllTeams(string ins)
        {
            List<TEAMPROFILE> list = new List<TEAMPROFILE>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`TEAMPROFILE` WHERE INSTITUTION = '{ins}' ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TEAMPROFILE()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            NAME = reader.GetString("NAME"),
                            INITIALS = reader.GetString("INITIALS"),
                            LOCATION = reader.GetString("LOCATION"),
                            JUSTIFICATION = reader.GetString("JUSTIFICATION"),
                            OBJETIVE = reader.GetString("OBJETIVE"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            CREATED_BY = reader.GetInt32("CREATED_BY"),
                            DATETIME = reader.GetString("DATETIME"),
                            DEPARTMENT = reader.GetInt32("DEPARTMENT")

                        
                        });
                    }
                }
            }
            return list;
        }
        public IEnumerable<TEAMPROFILE> GetTeamFromSupervisor(string ins)
        {
            List<TEAMPROFILE> list = new List<TEAMPROFILE>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`TEAMPROFILE` WHERE INSTITUTION = '{ins}' ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TEAMPROFILE()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            NAME = reader.GetString("NAME"),
                            INITIALS = reader.GetString("INITIALS"),
                            LOCATION = reader.GetString("LOCATION"),
                            JUSTIFICATION = reader.GetString("JUSTIFICATION"),
                            OBJETIVE = reader.GetString("OBJETIVE"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            CREATED_BY = reader.GetInt32("CREATED_BY"),
                            DATETIME = reader.GetString("DATETIME"),
                            DEPARTMENT = reader.GetInt32("DEPARTMENT")

                        
                        });
                    }
                }
            }
            return list;
        }
        public IEnumerable<TEAMPROFILE> GetTeamFromSupervisorId(string ins, int id)
        {
            List<TEAMPROFILE> list = new List<TEAMPROFILE>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`TEAMPROFILE` WHERE INSTITUTION = '{ins}' and ID = {id}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TEAMPROFILE()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            NAME = reader.GetString("NAME"),
                            INITIALS = reader.GetString("INITIALS"),
                            LOCATION = reader.GetString("LOCATION"),
                            JUSTIFICATION = reader.GetString("JUSTIFICATION"),
                            OBJETIVE = reader.GetString("OBJETIVE"),
                            DESCRIPTION = reader.GetString("DESCRIPTION"),
                            CREATED_BY = reader.GetInt32("CREATED_BY"),
                            DATETIME = reader.GetString("DATETIME"),
                            DEPARTMENT = reader.GetInt32("DEPARTMENT")

                        
                        });
                    }
                }
            }
            return list;
        }



        public ApplicantNote SaveNoteIntoApplicantProfile(ApplicantNote data)
        {
            string query =$"INSERT INTO `admbasic`.`APPLICANTNOTE` (CODE,ApplicantProfile,INSTITUTION,TITLE,BODY,DATE_TIME,COLOR) value({data.CODE},{data.ApplicantProfile},'{data.INSTITUTION}','{data.TITLE}','{data.BODY}', current_date() ,'{data.COLOR}')";
            Executor(query);
            return data;
        }
        public PositionDetails SetEmailTemplateForCampaign(PositionDetails data)
        {
            string query =$"UPDATE `admbasic`.`RecruitmentCampaign` set EMAIL_TEMPLATE where ID = {data.ID}";
            Executor(query);
            return data;
        }


        public IEnumerable<PositionDetails> LoadAllPositionDetailsForApplicantByInstitution(string ins)
        {
            List<PositionDetails> list = new List<PositionDetails>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PositionDetails` WHERE INSTITUTION = '{ins}' ";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PositionDetails()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            POSITION_ID = reader.GetInt32("POSITION_ID"),
                            POSITION_DETAILS = reader.GetString("POSITION_DETAILS"),
                            DESIRED_SKILLS = reader.GetString("DESIRED_SKILLS"),
                            MANDATORY_SKILLS = reader.GetString("MANDATORY_SKILLS"),
                            ACADEMIC_GRADE = reader.GetString("ACADEMIC_GRADE"),
                            CONTRACT_TYPE = reader.GetString("CONTRACT_TYPE"),
                            WAGE = reader.GetFloat("WAGE"),
                            REPORT_TO = reader.GetString("REPORT_TO"),
                            PREVIOUS_EXPERIENCE = reader.GetInt32("PREVIOUS_EXPERIENCE"),
                            DEPARMENT = reader.GetString("DEPARMENT"),
                            LOCATION = reader.GetString("LOCATION"),
                            CREATED_BY = reader.GetString("CREATED_BY"),
                            CURRENT_STATUS = reader.GetInt32("CURRENT_STATUS"),
                            DATE_TIME_CREATION = reader.GetString("DATE_TIME_CREATION"),

                        });
                    }
                }
            }
            return list;
        }



        public ApplicantProfile SaveApplicantProfile(ApplicantProfile data)
        {
            string query =$"INSERT INTO `admbasic`.`APLICANTPROFILE` (INSTITUTION,NAME,LASTNAME,EMAIL,PHONE,ADDRESS,RESUME,SKILLS,IS_WORKING,COMPANY_NAME,REPORT_TO,DATE_TIME_START,DATE_TIME_FINISH,LEAVE_REASON,LAST_WAGE,WORKING_EX,ACADEMIC_GRADE,CARRER_NAME,IS_GRADUATED,DESIRED_WAGE,CURRENT_STATUS,DATE_TIME,POSITION_ID) VALUE ('{data.INSTITUTION}','{data.NAME}','{data.LASTNAME}','{data.EMAIL}','{data.PHONE}','{data.ADDRESS}','{data.RESUME}','{data.SKILLS}',{data.IS_WORKING},'{data.COMPANY_NAME}','{data.REPORT_TO}','{data.DATE_TIME_START}','{data.DATE_TIME_FINISH}','{data.LEAVE_REASON}',{data.LAST_WAGE},{data.WORKING_EX},'{data.ACADEMIC_GRADE}','{data.CARRER_NAME}',{data.IS_GRADUATED},{data.DESIRED_WAGE},{data.CURRENT_STATUS},current_date(),{data.POSITION_ID})";
            Executor(query);
            return data;
        }

        public IEnumerable<ApplicantProfile> LoadAllApplicantByInstitution(string data)
        {
            List<ApplicantProfile> list = new List<ApplicantProfile>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`APLICANTPROFILE` WHERE INSTITUTION = '{data}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ApplicantProfile()
                        {
                            ID  = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            NAME  = reader.GetString("NAME"),
                            LASTNAME  = reader.GetString("LASTNAME"),
                            EMAIL = reader.GetString("EMAIL"),
                            PHONE = reader.GetString("PHONE"),
                            ADDRESS = reader.GetString("ADDRESS"),
                            RESUME = reader.GetString("RESUME"),
                            SKILLS = reader.GetString("SKILLS"),
                            IS_WORKING = reader.GetInt32("IS_WORKING"),
                            COMPANY_NAME = reader.GetString("COMPANY_NAME"),
                            REPORT_TO = reader.GetString("REPORT_TO"),
                            DATE_TIME_START = reader.GetString("DATE_TIME_START"),
                            DATE_TIME_FINISH = reader.GetString("DATE_TIME_FINISH"),
                            LEAVE_REASON = reader.GetString("LEAVE_REASON"),
                            LAST_WAGE = reader.GetFloat("LAST_WAGE"),
                            WORKING_EX = reader.GetInt32("WORKING_EX"),
                            ACADEMIC_GRADE = reader.GetString("ACADEMIC_GRADE"),
                            CARRER_NAME = reader.GetString("CARRER_NAME"),
                            IS_GRADUATED = reader.GetInt32("IS_GRADUATED"),
                            DESIRED_WAGE = reader.GetFloat("DESIRED_WAGE"),
                            CURRENT_STATUS = reader.GetInt32("CURRENT_STATUS"),
                            DATE_TIME = reader.GetString("DATE_TIME"),

                        });
                    }
                }
            }
            return list;
        }


        public IEnumerable<ApplicantProfile> LoadAllApplicantByInstitutionAndPositionId(string Institition,int id,int currentStatus)
        {
            List<ApplicantProfile> list = new List<ApplicantProfile>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`APLICANTPROFILE` WHERE INSTITUTION = '{Institition}' and POSITION_ID = {id} and (CURRENT_STATUS = {currentStatus})";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ApplicantProfile()
                        {
                            ID  = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            NAME  = reader.GetString("NAME"),
                            LASTNAME  = reader.GetString("LASTNAME"),
                            EMAIL = reader.GetString("EMAIL"),
                            PHONE = reader.GetString("PHONE"),
                            ADDRESS = reader.GetString("ADDRESS"),
                            RESUME = reader.GetString("RESUME"),
                            SKILLS = reader.GetString("SKILLS"),
                            IS_WORKING = reader.GetInt32("IS_WORKING"),
                            COMPANY_NAME = reader.GetString("COMPANY_NAME"),
                            REPORT_TO = reader.GetString("REPORT_TO"),
                            DATE_TIME_START = reader.GetString("DATE_TIME_START"),
                            DATE_TIME_FINISH = reader.GetString("DATE_TIME_FINISH"),
                            LEAVE_REASON = reader.GetString("LEAVE_REASON"),
                            LAST_WAGE = reader.GetFloat("LAST_WAGE"),
                            WORKING_EX = reader.GetInt32("WORKING_EX"),
                            ACADEMIC_GRADE = reader.GetString("ACADEMIC_GRADE"),
                            CARRER_NAME = reader.GetString("CARRER_NAME"),
                            IS_GRADUATED = reader.GetInt32("IS_GRADUATED"),
                            DESIRED_WAGE = reader.GetFloat("DESIRED_WAGE"),
                            CURRENT_STATUS = reader.GetInt32("CURRENT_STATUS"),
                            DATE_TIME = reader.GetString("DATE_TIME"),
                            POSITION_ID = reader.GetInt32("POSITION_ID"),

                        });
                    }
                }
            }
            return list;
        }


        public IEnumerable<ApplicantProfile> LoadAllApplicantOnInductionOnInstitution(string Institition,int currentStatus)
        {
            List<ApplicantProfile> list = new List<ApplicantProfile>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`APLICANTPROFILE` WHERE INSTITUTION = '{Institition}' and (CURRENT_STATUS = {currentStatus})";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ApplicantProfile()
                        {
                            ID  = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            NAME  = reader.GetString("NAME"),
                            LASTNAME  = reader.GetString("LASTNAME"),
                            EMAIL = reader.GetString("EMAIL"),
                            PHONE = reader.GetString("PHONE"),
                            ADDRESS = reader.GetString("ADDRESS"),
                            RESUME = reader.GetString("RESUME"),
                            SKILLS = reader.GetString("SKILLS"),
                            IS_WORKING = reader.GetInt32("IS_WORKING"),
                            COMPANY_NAME = reader.GetString("COMPANY_NAME"),
                            REPORT_TO = reader.GetString("REPORT_TO"),
                            DATE_TIME_START = reader.GetString("DATE_TIME_START"),
                            DATE_TIME_FINISH = reader.GetString("DATE_TIME_FINISH"),
                            LEAVE_REASON = reader.GetString("LEAVE_REASON"),
                            LAST_WAGE = reader.GetFloat("LAST_WAGE"),
                            WORKING_EX = reader.GetInt32("WORKING_EX"),
                            ACADEMIC_GRADE = reader.GetString("ACADEMIC_GRADE"),
                            CARRER_NAME = reader.GetString("CARRER_NAME"),
                            IS_GRADUATED = reader.GetInt32("IS_GRADUATED"),
                            DESIRED_WAGE = reader.GetFloat("DESIRED_WAGE"),
                            CURRENT_STATUS = reader.GetInt32("CURRENT_STATUS"),
                            DATE_TIME = reader.GetString("DATE_TIME"),
                            POSITION_ID = reader.GetInt32("POSITION_ID"),

                        });
                    }
                }
            }
            return list;
        }


         public IEnumerable<ApplicantProfile> GetApplicantByCode(string data,int code)
        {
            List<ApplicantProfile> list = new List<ApplicantProfile>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`APLICANTPROFILE` WHERE INSTITUTION = '{data}' and ID = {code}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ApplicantProfile()
                        {
                            ID  = reader.GetInt32("ID"),
                            NAME  = reader.GetString("NAME"),
                            LASTNAME  = reader.GetString("LASTNAME"),
                            EMAIL = reader.GetString("EMAIL"),
                            PHONE = reader.GetString("PHONE"),
                            ADDRESS = reader.GetString("ADDRESS"),
                            RESUME = reader.GetString("RESUME"),
                            SKILLS = reader.GetString("SKILLS"),
                            IS_WORKING = reader.GetInt32("IS_WORKING"),
                            COMPANY_NAME = reader.GetString("COMPANY_NAME"),
                            REPORT_TO = reader.GetString("REPORT_TO"),
                            DATE_TIME_START = reader.GetString("DATE_TIME_START"),
                            DATE_TIME_FINISH = reader.GetString("DATE_TIME_FINISH"),
                            LEAVE_REASON = reader.GetString("LEAVE_REASON"),
                            LAST_WAGE = reader.GetFloat("LAST_WAGE"),
                            WORKING_EX = reader.GetInt32("WORKING_EX"),
                            ACADEMIC_GRADE = reader.GetString("ACADEMIC_GRADE"),
                            CARRER_NAME = reader.GetString("CARRER_NAME"),
                            IS_GRADUATED = reader.GetInt32("IS_GRADUATED"),
                            DESIRED_WAGE = reader.GetFloat("DESIRED_WAGE"),
                            CURRENT_STATUS = reader.GetInt32("CURRENT_STATUS"),
                            DATE_TIME = reader.GetString("DATE_TIME"),

                        });
                    }
                }
            }
            return list;
        }
        
        public IEnumerable<ApplicantProfile> GetApplicantStatus(string data,int code)
        {
            List<ApplicantProfile> list = new List<ApplicantProfile>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`APLICANTPROFILE` WHERE INSTITUTION = '{data}' and ID = {code}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ApplicantProfile()
                        {
                            
                            CURRENT_STATUS = reader.GetInt32("CURRENT_STATUS"),

                        });
                    }
                }
            }

            return list;
        
        }

        public ApplicantProfile UpdataCurrentApplicantStatus(ApplicantProfile data)
        {
            string query =$"UPDATE `admbasic`.`APLICANTPROFILE` set CURRENT_STATUS = {data.CURRENT_STATUS}  where ID = {data.ID} and INSTITUTION = '{data.INSTITUTION}'";
            Executor(query);
            return data;
        }

        public ApplicantProfile DescarteApplicantAndUpdateProfile(ApplicantProfile data)
        {
            string query =$"UPDATE `admbasic`.`APLICANTPROFILE` set CURRENT_STATUS = {data.CURRENT_STATUS}, DESCARTING_REASON = '{data.DESCARTING_REASON}',CUSTOM_DESCARTING_REASON = '{data.CUSTOM_DESCARTING_REASON}'  where ID = {data.ID} and INSTITUTION = '{data.INSTITUTION}'";
            Executor(query);
            return data;
        }
         

    public RecruitmentCampaignEmailAuto SaveEmailtemplate(RecruitmentCampaignEmailAuto data)
        {
            string query =$"INSERT INTO `admbasic`.`CAMPAUTOSEND` (INSTITUTION,FROM,TO,CC,SUBJECT,BODY,AUTO,POSITION_ID,DATE_TIME) VALUE ('{data.INSTITUTION}','{data.FROM}','{data.TO}','{data.CC}','{data.SUBJECT}','{data.BODY}',{data.AUTO},{data.POSITION_ID},current_date())";
            Executor(query);
            return data;
        }

    public IEnumerable<PositionDetails> GetPositionDetails(string data,int code)
        {
            List<PositionDetails> list = new List<PositionDetails>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`PositionDetails` WHERE INSTITUTION = '{data}' and ID = {code}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PositionDetails()
                        {
                            
                            
                            POSITION_DETAILS  = reader.GetString("POSITION_DETAILS"),
                            DESIRED_SKILLS = reader.GetString("DESIRED_SKILLS"),
                            MANDATORY_SKILLS = reader.GetString("MANDATORY_SKILLS"),
                            ACADEMIC_GRADE = reader.GetString("ACADEMIC_GRADE"),
                            CONTRACT_TYPE = reader.GetString("CONTRACT_TYPE"),
                            // WAGE = reader.GetFloat("WAGE"),
                            // REPORT_TO = reader.GetString("REPORT_TO"),
                            // PREVIOUS_EXPERIENCE = reader.GetInt32("PREVIOUS_EXPERIENCE"),
                            // DEPARMENT = reader.GetString("DEPARMENT"),
                            // LOCATION = reader.GetString("LOCATION"),
                            // CREATED_BY = reader.GetString("CREATED_BY"),
                            // CURRENT_STATUS = reader.GetInt32("CURRENT_STATUS"),
                            // DATE_TIME_CREATION = reader.GetString("DATE_TIME_CREATION"),
                            // DATE_TIME_START = reader.GetString("DATE_TIME_START"),
                            // DATE_TIME_FINISH = reader.GetString("DATE_TIME_FINISH"),
                            // EMAIL_TEMPLATE = reader.GetString("EMAIL_TEMPLATE"),

                        });
                    }
                }
            }

            return list;
        
        }



        public ApplicantProfile SetScoreToApplicant(ApplicantProfile data)
        {
            string query =$"UPDATE `admbasic`.`APLICANTPROFILE` SET EVALUATION_SCORE = {data.EVALUATION_SCORE} WHERE INSTITUTION = '{data.INSTITUTION}' and ID = {data.ID}";
            Executor(query);
            return data;
        }

    public IEnumerable<ApplicantProfile> GetApplicantScore(string data,int code)
        {
            List<ApplicantProfile> list = new List<ApplicantProfile>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT EVALUATION_SCORE FROM `admbasic`.`APLICANTPROFILE` WHERE INSTITUTION = '{data}' and ID = {code}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ApplicantProfile()
                        {
                            
                            EVALUATION_SCORE  = reader.GetInt32("EVALUATION_SCORE"),
                            
                        });
                    }
                }
            }

            return list;
        
        }



    public InteractionHistory SaveInteraction(InteractionHistory data)
    {
        string query =$"INSERT INTO `admbasic`.`INTERACTIONHISTORY` (INSTITUTION, HOST_CODE,USER_CODE, HISTORY_DATA, DATA_TIME)VALUE('{data.INSTITUTION}',{data.HOST_CODE}, {data.USER_CODE}, '{data.HISTORY_DATA}', '{data.DATE_TIME}')";
        Executor(query);
        return data;
    }

    public IEnumerable<InteractionHistory> LoadInteractionHistory(string data,int code)
    {
        List<InteractionHistory> list = new List<InteractionHistory>();
        using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT EVALUATION_SCORE FROM `admbasic`.`APLICANTPROFILE` WHERE INSTITUTION = '{data}' and ID = {code}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new InteractionHistory()
                        {
                            
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            HOST_CODE = reader.GetInt32("HOST_CODE"),
                            USER_CODE = reader.GetInt32("USER_CODE"),
                            HISTORY_DATA = reader.GetString("HISTORY_DATA"),
                            DATE_TIME = reader.GetString("DATE_TIME"),
                            
                        });
                    }
                }
            }

        return list;
        
    }

    public VacationPlan BookVacation(VacationPlan data)
    {
        string query =$"INSERT INTO `admbasic`.`VACATIONPLAN` (CODE,INSTITUTION,FROM_DATE,TO_DATE,STATE,AUTHORIZED_BY,DAYS,CONSEPT)VALUE({data.CODE},'{data.INSTITUTION}','{data.FROM_DATE}','{data.TO_DATE}',{data.STATE},{data.AUTHORIZED_BY},{data.DAYS},'{data.CONSEPT}')";
        Executor(query);
        return data;
    }
    public VacationPlan AuthorizeVacation(VacationPlan data)
    {
        //normal =0, aproved = 1, refused = 3
        string query =$"UPDATE `admbasic`.`VACATIONPLAN` set STATE = {data.STATE}, AUTHORIZED_BY = {data.AUTHORIZED_BY} where INSTITUTION = '{data.INSTITUTION}' and ID = {data.ID}";
        Executor(query);
        return data;
    }


    public IEnumerable<VacationPlan> GetBoockedVacationOfEmployee(string data, int code, int s, int s2)
    {
        List<VacationPlan> list = new List<VacationPlan>();
        using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`VACATIONPLAN` WHERE INSTITUTION = '{data}' and CODE = {code} and (state = {s} OR state = {s2})";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new VacationPlan()
                        {
                            
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            FROM_DATE = reader.GetString("FROM_DATE"),
                            TO_DATE = reader.GetString("TO_DATE"),
                            STATE = reader.GetInt32("STATE"),
                            AUTHORIZED_BY = reader.GetInt32("AUTHORIZED_BY"),
                            DAYS = reader.GetInt32("DAYS"),
                            CONSEPT = reader.GetString("CONSEPT"),
                            
                        });
                    }
                }
            }

        return list;
        
    }


    public IEnumerable<VacationPlan> LoadAllBoockedVacationNotAuthorized(string data, int state, int aut)
    {
        List<VacationPlan> list = new List<VacationPlan>();
        using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`VACATIONPLAN` WHERE INSTITUTION = '{data}' and STATE = {state} and(AUTHORIZED_BY = {aut})";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new VacationPlan()
                        {
                            
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            FROM_DATE = reader.GetString("FROM_DATE"),
                            TO_DATE = reader.GetString("TO_DATE"),
                            STATE = reader.GetInt32("STATE"),
                            AUTHORIZED_BY = reader.GetInt32("AUTHORIZED_BY"),
                            DAYS = reader.GetInt32("DAYS"),
                            CONSEPT = reader.GetString("CONSEPT"),
                            
                        });
                    }
                }
            }

        return list;
        
    }
        public IEnumerable<VacationProfile> GetVacationProfile(string data)
    {
        List<VacationProfile> list = new List<VacationProfile>();
        using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`VACATIONPROFILE` WHERE INSTITUTION = '{data}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new VacationProfile()
                        {
                            
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            CONCEPT = reader.GetString("CONCEPT"),
                            POSITION_ID = reader.GetInt32("POSITION_ID"),
                            DAYS = reader.GetInt32("DAYS"),
                            START_DATE = reader.GetInt32("START_DATE"),
                            END_DATE = reader.GetInt32("END_DATE"),
                            AFTER_DATE = reader.GetInt32("AFTER_DATE"),
                            YEARS = reader.GetInt32("YEARS"),
                            
                        });
                    }
                }
            }

        return list;
        
    }
// VacationPlan

//  MedicalLicenceRegitry
    public IEnumerable<MedicalLicenceRegitry> LoadMedicalLicenseRegistry(string data, int code)
    {
        List<MedicalLicenceRegitry> list = new List<MedicalLicenceRegitry>();
        using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`MEDICALLICENSE` WHERE INSTITUTION = '{data}' and CODE = {code}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new MedicalLicenceRegitry()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            FROM_DATE = reader.GetString("FROM_DATE"),
                            TO_DATE = reader.GetString("TO_DATE"),
                            STATE = reader.GetInt32("STATE"),
                            AUTHORIZED_BY = reader.GetInt32("AUTHORIZED_BY"),
                            DAYS = reader.GetInt32("DAYS"),
                            CONSEPT = reader.GetString("CONSEPT"),
                            BACKUP = reader.GetString("BACKUP"),
                            DATE_CREATION = reader.GetString("DATE_CREATION"),
                            
                        });
                    }
                }
            }

        return list; 
    }
        public IEnumerable<MedicalLicenceRegitry> LoadMedicalLicenseRegistryByTeam(string data,  int team)
    {
        List<MedicalLicenceRegitry> list = new List<MedicalLicenceRegitry>();
        using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT SUM(CODE) FROM `admbasic`.`MEDICALLICENSE` WHERE INSTITUTION = '{data}'  and TEAM_ID = {team} and STATE = 1";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new MedicalLicenceRegitry()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            FROM_DATE = reader.GetString("FROM_DATE"),
                            TO_DATE = reader.GetString("TO_DATE"),
                            STATE = reader.GetInt32("STATE"),
                            AUTHORIZED_BY = reader.GetInt32("AUTHORIZED_BY"),
                            DAYS = reader.GetInt32("DAYS"),
                            CONSEPT = reader.GetString("CONSEPT"),
                            BACKUP = reader.GetString("BACKUP"),
                            DATE_CREATION = reader.GetString("DATE_CREATION"),
                            
                        });
                    }
                }
            }

        return list; 
    }

        public IEnumerable<MedicalLicenceRegitry> LoadNewMedicalLicenseRegistryData(string data,int state, int auth)
    {
        List<MedicalLicenceRegitry> list = new List<MedicalLicenceRegitry>();
        using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`MEDICALLICENSE` WHERE INSTITUTION = '{data}' and AUTHORIZED_BY = {auth} and ( STATE = {state})";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new MedicalLicenceRegitry()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            FROM_DATE = reader.GetString("FROM_DATE"),
                            TO_DATE = reader.GetString("TO_DATE"),
                            STATE = reader.GetInt32("STATE"),
                            AUTHORIZED_BY = reader.GetInt32("AUTHORIZED_BY"),
                            DAYS = reader.GetInt32("DAYS"),
                            CONSEPT = reader.GetString("CONSEPT"),
                            BACKUP = reader.GetString("BACKUP"),
                            DATE_CREATION = reader.GetString("DATE_CREATION"),
                            
                        });
                    }
                }
            }

        return list; 
    }

    public IEnumerable<MedicalLicenceRegitry> LoadGlobalMedicalLicenseRegistry(string data, int code)
    {
        List<MedicalLicenceRegitry> list = new List<MedicalLicenceRegitry>();
        using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM `admbasic`.`MEDICALLICENSE` WHERE INSTITUTION = '{data}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new MedicalLicenceRegitry()
                        {
                            ID = reader.GetInt32("ID"),
                            CODE = reader.GetInt32("CODE"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            FROM_DATE = reader.GetString("FROM_DATE"),
                            TO_DATE = reader.GetString("TO_DATE"),
                            STATE = reader.GetInt32("STATE"),
                            AUTHORIZED_BY = reader.GetInt32("AUTHORIZED_BY"),
                            DAYS = reader.GetInt32("DAYS"),
                            CONSEPT = reader.GetString("CONSEPT"),
                            BACKUP = reader.GetString("BACKUP"),
                            DATE_CREATION = reader.GetString("DATE_CREATION"),
                            
                        });
                    }
                }
            }

        return list; 
    }

        public MedicalLicenceRegitry RegisterMedicalLicense(MedicalLicenceRegitry data)
    {
        string query =$"INSERT INTO `admbasic`.`MEDICALLICENSE` (CODE,INSTITUTION,FROM_DATE,TO_DATE,STATE,AUTHORIZED_BY,DAYS,CONSEPT,BACKUP,DATE_CREATION)VALUE({data.CODE},'{data.INSTITUTION}','{data.FROM_DATE}','{data.TO_DATE}',{data.STATE},{data.AUTHORIZED_BY},{data.DAYS},'{data.CONSEPT}','{data.BACKUP}',current_date())";
        Executor(query);
        return data;
    }
    public MedicalLicenceRegitry AuthorizeMedicalRegister(MedicalLicenceRegitry data)
    {
        //normal =0, aproved = 1, refused = 3
        string query =$"Update `admbasic`.`MEDICALLICENSE` set STATE = {data.STATE}, AUTHORIZED_BY = {data.AUTHORIZED_BY} where INSTITUTION = '{data.INSTITUTION}' and ID = {data.ID}";
        Executor(query);
        return data;
    }

//  MedicalLicenceRegitry


// PersonalTodoTask
    public PersonalTodoTask SavePersonalTaskProd(PersonalTodoTask data)
    {
        
        string query =$"INSERT INTO `admbasic`.`PERSONALTASKPROD` (INSTITUTION,CODE,TASK,VALUE,STATUS,RANKING,CREATION_DATE,EXPIRATION_DATE,EXPIRATION_TIME) value('{data.INSTITUTION}',{data.CODE},'{data.TASK}','{data.VALUE}',{data.STATUS},'{data.RANKING}',current_date(),'{data.EXPIRATION_DATE}','{data.EXPIRATION_TIME}')";
        Executor(query);
        return data;
    }

        public PersonalTodoTask DELETEPersonalTaskProd(PersonalTodoTask data)
    {
        
        string query =$"UPDATE `admbasic`.`PERSONALTASKPROD` set STATUS = {data.STATUS} where INSTITUTION = '{data.INSTITUTION}' and CODE = {data.CODE} and ID = {data.ID}";
        Executor(query);
        return data;
    }
    public PersonalTodoTask CheckSinglePersonalTaskProd(PersonalTodoTask data)
    {
        
        string query =$"UPDATE `admbasic`.`PERSONALTASKPROD` set VALUE = '{data.VALUE}' where INSTITUTION = '{data.INSTITUTION}' and CODE = {data.CODE} and ID = {data.ID}";
        Executor(query);
        return data;
    }


    public IList<PersonalTodoTask> GetPersonalTaskProd(string institution, int code,string rank)
    {
        List<PersonalTodoTask> list = new List<PersonalTodoTask>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {

                string query = $"SELECT * FROM `admbasic`.`PERSONALTASKPROD` WHERE CODE = {code} and INSTITUTION = '{institution}' and RANKING = '{rank}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PersonalTodoTask()
                        {
                            ID = reader.GetInt32("ID"),
                            TASK = reader.GetString("TASK"),
                            VALUE = reader.GetString("VALUE"),
                            STATUS = reader.GetBoolean("STATUS"),
                            RANKING = reader.GetString("RANKING"),
                            CREATION_DATE = reader.GetString("RANKING"),
                            EXPIRATION_DATE = reader.GetString("EXPIRATION_DATE"),
                            EXPIRATION_TIME = reader.GetString("EXPIRATION_TIME"),


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

    // PersonalTodoTask

        
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
    
    public Notifications AddItemToNotification(Notifications data){
        
        string query =$"INSERT INTO `admbasic`.`NOTIFICATIONS` (INSTITUTION, CODE, DATA, STATUS, DATE) value('{data.INSTITUTION}', {data.CODE}, '{data.DATA}', {data.STATUS}, current_date())";
        Executor(query);
        return data;
    }

    public Absence ReportAbsence(Absence data){
        
        string query =$"INSERT INTO `admbasic`.`ABSENCE` (INSTITUTION, CODE,REPORTED_BY, REASON, STATUS, DATE) value('{data.INSTITUTION}',{data.REPORTED_BY}, {data.CODE}, '{data.REASON}', {data.STATUS}, current_date())";
        Executor(query);
        return data;
    }

    public IList<Notifications> LoadAbsences(string institution, int code, string date)
    {
        List<Notifications> list = new List<Notifications>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {

                string query = $"SELECT * FROM `admbasic`.`ABSENCE` WHERE INSTITUTION = '{institution}' DATE = '{date}' and CODE = {code}";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Notifications()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            CODE = reader.GetInt32("CODE"),
                            DATA = reader.GetString("DATA"),
                            STATUS = reader.GetBoolean("STATUS"),
                            DATE = reader.GetString("DATE")

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

    public IList<Notifications> LoadPersonalNotifications(string institution, int code, bool stat)
    {
        List<Notifications> list = new List<Notifications>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {

                string query = $"SELECT * FROM `admbasic`.`NOTIFICATIONS` WHERE CODE = {code} and INSTITUTION = '{institution}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Notifications()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            CODE = reader.GetInt32("CODE"),
                            DATA = reader.GetString("DATA"),
                            STATUS = reader.GetBoolean("STATUS"),
                            DATE = reader.GetString("DATE")

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


// ProductibityVolume
    public ProductibityVolume SaveTaskOnPersonalProdVolume(ProductibityVolume data)
    {
        
        string query =$"INSERT INTO `admbasic`.`PRODVOLUME` (INSTITUTION,CODE,TASK,VOLUME,VALUE,DATE,TEAM_ID) value('{data.INSTITUTION}',{data.CODE},'{data.TASK}',{data.VOLUME},{data.VALUE},currentdate(),{data.TEAM_ID})";
        Executor(query);
        return data;
    }

    public ProductibityVolume UpdateTaskOnProdVolume(ProductibityVolume data)
    {
        
        string query =$"UPDATE `admbasic`.`PRODVOLUME` set VOLUME = {data.VOLUME} where INSTITUTION = '{data.INSTITUTION}' and CODE = {data.CODE} and ID = {data.ID}";
        Executor(query);
        return data;
    }

    public IList<ProductibityVolume> LoadPersonalProductibityVolume(string institution, int code, bool stat)
    {
        List<ProductibityVolume> list = new List<ProductibityVolume>();
        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            try
            {

                string query = $"SELECT * FROM `admbasic`.`PRODVOLUME` WHERE CODE = {code} and INSTITUTION = '{institution}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ProductibityVolume()
                        {
                            ID = reader.GetInt32("ID"),
                            INSTITUTION = reader.GetString("INSTITUTION"),
                            CODE = reader.GetInt32("CODE"),
                            VOLUME = reader.GetInt32("VOLUME"),
                            VALUE = reader.GetFloat("VALUE"),
                            TASK = reader.GetString("TASK"),
                            TEAM_ID = reader.GetInt32("TEAM_ID")

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


        public IEnumerable<AuthenticationProcedure> validateUser(string sha)
        {
            List<AuthenticationProcedure> list = new List<AuthenticationProcedure>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = $"SELECT sha FROM `admbasic`.`AuthenticationProcedure` where sha = '{sha}'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AuthenticationProcedure()
                        {

                           
                            sha = reader.GetString("sha"),
                            Institution = reader.GetString("Institution"),
                            Code = reader.GetInt32("Code"),
                             
                            
                            



                        });
                    }
                }
            }

            return list;
        }



}