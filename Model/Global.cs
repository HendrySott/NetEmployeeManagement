using System;
using System.ComponentModel.DataAnnotations;
namespace NetEmployee.Model;

public class PersonProfile
{
    public int ID { get; set; }
    public int CODE { get; set; }
    public string INSTITUTION { get; set; }
    public string F_NAME { get; set; }
    public string S_NAME { get; set; }
    public string F_LASTN { get; set; }
    public string S_LASTN { get; set; }
    public string GRADE { get; set; }
    public string ADDRESS { get; set; }
    public string PHONE { get; set; }
    public string IDENTIFICATION { get; set; }
    public string PERSONAL_EMAIL { get; set; }
    public int POSITION_KEY { get; set; }
    public int TEAM_ID { get; set; }
    public string STREET { get; set; }
    public int HOME { get; set; }
    public string COUNTY { get; set; }
    public string COUNTRY { get; set; }
    public string ADMITION_DATE{get;set;}
    public string ALT_PHONE{get;set;}
    public string RESUME{get;set;}
    public string COVERLETTER{get;set;}
    public string SKILLS{get;set;}
   
    public string COMPANY_NAME{get;set;}
    public string REPORT_TO{get;set;}
    public string DATE_TIME_START{get;set;}
    public string DATE_TIME_FINISH{get;set;}
    public string LEAVE_REASON{get;set;}
    public float LAST_WAGE{get;set;}
    public int WORKING_EX{get;set;}
    public string CARRER_NAME{get;set;}
    public int IS_GRADUATED{get;set;}
    public float DESIRED_WAGE{get;set;}
    public int CURRENT_STATUS{get;set;}
    public string DATE_TIME{get;set;}
    public int POSITION_ID{get;set;}

 

}




public class PositionProfile
{
    public int ID { get; set; }
    public string POSITION_NAME { get; set; }
    public int TEAM_ID { get; set; }
    public string REASON { get; set; }
    public int JOB_PROFILE { get; set; }
    public int JOB_QUANTITY { get; set; }
    public int LOCATION_PROFILE { get; set; }
    public int JOB_TYPE { get; set; }
    public int CONTRACT_TYPE { get; set; }
    public int ACADEMIC_GRADE { get; set; }
    public int SHIFT { get; set; }
    public string SKILLS_REQUIRED { get; set; }
    
    public string AVAILABLE_FROM { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }

    public string SKILS { get; set; }
    public string OBJETIVE { get; set; }
    public float PRICE { get; set; }
    public int WAGE_PORSENT { get; set; }
    public int REPORT_TO { get; set; }

    public float PAYMENTCALCTYPE { get; set; }

}

public class TitleModel
{
    public int ID { get; set; }
    public int LEVEL { get; set; }
    public string NAME { get; set; }
    public string DESCRIPTION { get; set; }
    public string CODE { get; set; }

    public string INSTITUTION { get; set; }
}

public class EsuranceModel
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }
    public double ENSURANCE_AMOUNT { get; set; }
    public string DATE { get; set; }
    public int STATE { get; set; }
}





public class AFPModel
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }
    public double AFP_AMOUNT { get; set; }
    public string DATE { get; set; }
    public int STATE { get; set; }
    
}



public class TimeTrackerModel
{
    public int ID { get; set; }
    public int CODE { get; set; }
    public string INSTITUTION { get; set; }
    public string TOTAL_OFFLINE { get; set; }
    public string TOTAL_ONLINE { get; set; }
    public string START { get; set; }
    public string LUNCH { get; set; }
    public string LUNCH_ { get; set; }
    public string LUNCH_S { get; set; }
    public string BREAK { get; set; }
    public string BREAK_ { get; set; }
    public string BREAK_S { get; set; }

    public string BREAKL { get; set; }
    public string BREAKL_ { get; set; }
    public string BREAK_SL { get; set; }
    public string OTHER { get; set; }
    public string OTHER_ { get; set; }
    public string OTHER_S { get; set; }
    public string END { get; set; }
    public string DATE { get; set; }
    public string TOTAL { get; set; }
    public string STATE { get; set; }

    public string DATESH { get; set; }
    public int AUTHORIZED_BY { get; set; }

}

public class ShiftProfile
{
    public int ID { get; set; }
    public int PERIOD { get; set; }
    public string START { get; set; }
    public int HOURS { get; set; }
    public float LUNCH { get; set; }
    public int BREAK { get; set; }

    public int QUANTITY { get; set; }
    public int WEEKEND { get; set; }
    public string INSTITUTION { get; set; }

    public double END { get; set; }
}



public class PagesDirectoryModel
{
    public int ID { get; set; }
    public int CODE { get; set; }
    public string INSTITUTION { get; set; }
    public string CASHREGISTER { get; set; }
    public string ACCOUNTING { get; set; }

    public string INVENTORY { get; set; }
    public string HHRR { get; set; }
    public string SETTINGSAC { get; set; }
    public string SETTINGSMAIN { get; set; }
    public string MAININTERFACE { get; set; }
    public string STATE { get; set; }
}




public class LicenceModel
{
    public int ID { get; set; }
    public string SERIAL { get; set; }

    public string DATE { get; set; }
    public string INSTITUTION { get; set; }
    public string EMAIL { get; set; }

    public int PERIOD { get; set; }
    public string TOCKEN { get; set; }
    public int STATE { get; set; }

}



public class PersonalTodoList{
    public int ID{get;set;}
    public int CODE{get;set;}
    public string INSTITUTION{get;set;}
    public string TASK{get;set;}
    public string DATE_TIME{get;set;}
    public string CREATION_DATE{get;set;}
    public int STATE {get;set;}

}

public class InteractionHistory{
    public int ID{get;set;}
    public int HOST_CODE{get;set;}
    public int USER_CODE{get;set;}
    public string INSTITUTION{get;set;}
    public string HISTORY_DATA{get;set;}
    public string DATE_TIME{get;set;}

}



public class PayrollModel
{
    public int SERIAL { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }
    public float AMOUNT { get; set; }
    public float BONUS { get; set; }
    public string CURRENCY { get; set; }
    public float HOURS { get; set; }
    public string PAYMENT_METHOD { get; set; }
    public float TAXES { get; set; }
    public int TAXES_ID { get; set; }
    public float AFP { get; set; }
    public int AFP_ID { get; set; }
    public float ENSURANCE { get; set; }
    public int ENSURANCE_ID { get; set; }
    public int STATE { get; set; }
    public int PERIOD { get; set; }
    public string DATE { get; set; }
    public int APROVED_BY { get; set; }
    public int DR_ACC { get; set; }
    public string CR_ACC { get; set; }
}


public class PayrollBonus
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }
    public float AMOUNT { get; set; }
    public int AUTHORIZER { get; set; }
    public string DATE { get; set; }
    public string CHAIN { get; set; }
}

public class PaymentHistorys
{
    public int SERIAL { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }
    public double AMOUNT { get; set; }
    public float BONUS { get; set; }
    public string CURRENCY { get; set; }
    public double HOURS { get; set; }
    public string PAYMENT_METHOD { get; set; }
    public float TAXES { get; set; }
    public int TAXES_ID { get; set; }
    public double AFP { get; set; }
    public int AFP_ID { get; set; }
    public float ENSURANCE { get; set; }
    public int ENSURANCE_ID { get; set; }
    public int STATE { get; set; }
    public int PERIOD { get; set; }
    public string DATE { get; set; }
    public int APROVED_BY { get; set; }
    public int DR_ACC { get; set; }
    public string CR_ACC { get; set; }
}


public class FontsEXModel
{
    public int ID { get; set; }
    public string TYPE { get; set; }
    public string CATEGORY { get; set; }
    public string TRACKING { get; set; }
    public string NAME { get; set; }
    public string DESCRIPTION { get; set; }
    public float AMOUNT { get; set; }
    public int RELATION { get; set; }
}

public class TaxOffice
{
    public int ID { get; set; }
    public string LOCATION { get; set; }
    public string NAME { get; set; }
    public string PHONE { get; set; }
    public string ADDRESS { get; set; }
    public int STATE { get; set; }
    public string INSTITUTION { get; set; }
}

public class SysTax
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int STATE { get; set; }
    public int OFFICE { get; set; }
    public string FILE { get; set; }
    public int AUTHORIZED_BY { get; set; }
    public int SERIAL { get; set; }
    public string PERIOD { get; set; }
    public string PAYED_AT { get; set; }
    public float AMOUNT { get; set; }
    public int ACCOUNT_DEBITED { get; set; }
}

public class Finantial
{
    public double DEBIT { get; set; }
    public double CREDIT { get; set; }
    public int ACCOUNT { get; set; }
    public int TRANSIT { get; set; }
    public int ACCOUNTINGBOOK { get; set; }
    public string DESCRIPTION { get; set; }
    public string NAME { get; set; }
    public string DATE { get; set; }
    public string TIME { get; set; }
    public int ID { get; set; }
    public string UID { get; set; }
    public string STATE { get; set; }
}


public class AccountHistory
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public string BATCH { get; set; }
    public int ACCOUNT { get; set; }

    public int GENERALBOOK { get; set; }
    public float AMOUNT { get; set; }

    public int TRANSIT { get; set; }
    public string DETAILS { get; set; }
    public int UID { get; set; }
    public int STATE { get; set; }
    public DateOnly DATE { get; set; }
    public string TYPE { get; set; }

    public int DR_ACCOUNNT { get; set; }

    public int CR_ACCOUNNT { get; set; }


}

public class Account
{
    public int ID { get; set; }

    public string INSTITUTION { get; set; }
    public string NAME { get; set; }

    public int LEAGERBOOK { get; set; }

    public int TRANSIT { get; set; }

    public int ONTRANSIT { get; set; }

    public float VALANCE { get; set; }

    public float OVERDRAFT { get; set; }

    public string TYPE { get; set; }

    public string CATEGORY { get; set; }
    public string TRACKING { get; set; }

    public int STATE { get; set; }
    public string DESCRIPTION { get; set; }
}

public class ACCModel
{
    public int ID { get; set; }
    public string TYPE { get; set; }
    public string CATEGORY { get; set; }
    public string TRACKING { get; set; }
    public string NAME { get; set; }
    public string DESCRIPTION { get; set; }
    public float CURRENT_VALANCE { get; set; }
    public int RELATION { get; set; }

    public int STATE { get; set; }
    public int ACCOUNT { get; set; }
    public string INSTITUTION { get; set; }
    public float INITIAL_VALANCE { get; set; }



}

public class ACCLogModel
{
    public int ID { get; set; }
    public int CODE { get; set; }
    public string INSTITUTION { get; set; }
    //curdate(),curtime()
    public DateOnly DATE { get; set; }

    public TimeOnly TIME { get; set; }
    public int DR_ACCOUNNT { get; set; }
    public int CR_ACCOUNNT { get; set; }
    public float AMOUNT { get; set; }

    public string STATE { get; set; }
    public string TRSERIAL { get; set; }

}

public class ACCHistory
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public string TYPE { get; set; }
    public string CATEGORY { get; set; }
    public string DATE { get; set; }
    public string DETAILS { get; set; }
    public float AMOUNT { get; set; }
    public int ACC_CODE { get; set; }
    public string POW_CODE { get; set; }
    public int CODE { get; set; }
    public float VAR { get; set; }

}


public class RecruitmentCampaign{
    public int ID {get;set;}
    public string CAMPAIG_NAME {get;set;}
    public string INSTITUTION {get;set;}
    public string CREATED_BY{get;set;}
    public string AUTHORIZED_BY{get;set;}
    public string DETAILS{get;set;}
    public string DATE_TIME_CREATION{get;set;}
    public string DATE_TIME_START{get;set;}
    public string DATE_TIME_FINISH{get;set;}
    public int STATUS{get;set;}


}

public class PositionDetails{
    public int ID {get;set;}
    public string INSTITUTION {get;set;}
    public int POSITION_ID {get;set;}
    public string POSITION_DETAILS {get;set;}
    public string DESIRED_SKILLS{get;set;}
    public string MANDATORY_SKILLS{get;set;}
    public string ACADEMIC_GRADE{get;set;}
    public string CONTRACT_TYPE{get;set;}
    public float WAGE{get;set;}
    public string REPORT_TO{get;set;}
    public int PREVIOUS_EXPERIENCE{get;set;}
    public string DEPARMENT{get;set;}
    public string LOCATION{get;set;}
    public string CREATED_BY{get;set;}
    public int CURRENT_STATUS{get;set;}
    public string DATE_TIME_CREATION{get;set;}
    public string DATE_TIME_START{get;set;}
    public string DATE_TIME_FINISH{get;set;}
    public string EMAIL_TEMPLATE{get;set;}
    
}

public class ApplicantProfile{
    public int ID {get;set;}
    public string INSTITUTION{get;set;}
    public string NAME {get;set;}
    public string LASTNAME {get;set;}
    public string EMAIL{get;set;}
    public string PHONE{get;set;}
    public string ALT_PHONE{get;set;}
    public string ADDRESS{get;set;}
    public string RESUME{get;set;}
    public string COVERLETTER{get;set;}
    public string SKILLS{get;set;}
    public int IS_WORKING{get;set;}
    public string COMPANY_NAME{get;set;}
    public string REPORT_TO{get;set;}
    public string DATE_TIME_START{get;set;}
    public string DATE_TIME_FINISH{get;set;}
    public string LEAVE_REASON{get;set;}
    public float LAST_WAGE{get;set;}
    public int WORKING_EX{get;set;}
    public string ACADEMIC_GRADE{get;set;}
    public string CARRER_NAME{get;set;}
    public int IS_GRADUATED{get;set;}
    public float DESIRED_WAGE{get;set;}
    public int CURRENT_STATUS{get;set;}
    public string DATE_TIME{get;set;}
    public int POSITION_ID{get;set;}
    public string DESCARTING_REASON{get;set;}
    public string CUSTOM_DESCARTING_REASON{get;set;}
    public string NOTE{get;set;}
    public string APPROACH{get;set;}

    public int EVALUATION_SCORE{get;set;}


    
}

public class ApplicantNote{
    public int ID{get;set;}
    public int CODE{get;set;}
    public int ApplicantProfile{get;set;}
    public string INSTITUTION{get;set;}
    public string TITLE{get;set;}
    public string BODY{get;set;}
    public string DATE_TIME{get;set;}
    public string COLOR{get;set;}
}

public class RecruitmentCampaignEmailAuto{
    public int ID{get;set;}
    public string INSTITUTION{get;set;}
    public string FROM{get;set;}
    public string TO{get;set;}
    public string CC{get;set;}
    public string SUBJECT{get;set;}
    public string BODY{get;set;}
    public bool AUTO{get;set;}
    public int POSITION_ID{get;set;}
    public int CODE{get;set;}
    public string DATE_TIME{get;set;}

}

public class EmailOutbox{
    public int ID{get;set;}
    public string INSTITUTION{get;set;}
    public string FROM{get;set;}
    public string TO{get;set;}
    public string CC{get;set;}
    public string SUBJECT{get;set;}
    public string BODY{get;set;}
    public int  CODE{get;set;}
    public string DATE_TIME{get;set;}
    public int AUTO{get;set;}
    public int POSITION_ID{get;set;}

}

public class MedicalLicenceRegitry{
    public int ID{get;set;}
    public int CODE{get;set;}
    public string INSTITUTION{get;set;}
    public string FROM_DATE{get;set;}
    public string TO_DATE{get;set;}
    public int STATE{get;set;}
    public int AUTHORIZED_BY{get;set;}
    public int DAYS{get;set;}
    public string CONSEPT{get;set;}
    public string BACKUP{get;set;}
    public string DATE_CREATION{get;set;}


}

public class VacationPlan{
    public int ID{get;set;}
    public int CODE{get;set;}
    public string INSTITUTION{get;set;}
    public string FROM_DATE{get;set;}
    public string TO_DATE{get;set;}
    public int STATE{get;set;}
    public int AUTHORIZED_BY{get;set;}
    public int DAYS{get;set;}
    public string CONSEPT{get;set;}


}

public class VacationProfile{
    public int ID{get;set;}
    public string INSTITUTION{get;set;}
    public string CONCEPT{get;set;}
    public int POSITION_ID{get;set;}
    public int DAYS{get;set;}
    public int START_DATE{get;set;}
    public int END_DATE{get;set;}
    public int AFTER_DATE{get;set;}
    public int YEARS{get;set;}

}
public class SetupModel
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int INSTRITUTION_NAME { get; set; }
    public int INSTITUTION_ADDRESS { get; set; }


    public int INSTITUTION_FDEPARTMENT { get; set; }
    public int INSTITUTION_FPOSITIONS { get; set; }
    public int INSTITUTION_RB { get; set; }
    public int INSTITUTION_RB_CONCILIATOR { get; set; }
    public int INSTITUTION_HR { get; set; }
    public int INSTITUTION_ACCOUNTING { get; set; }
    public int INSTITUTION_INV { get; set; }
    public int INSTITUTION_SU_SETUP { get; set; }

}

public class InstitutionIndex{
    public int ID{get;set;}
    public int CODE{get;set;}
    public int INSTITUTION{get;set;}
}


public class LevelWages
{
    public int ID { get; set; }

    public string INSTITUTION { get; set; }

    public int PAYMENT_INTERVAL { get; set; }

    public int POSITION_KEY { get; set; }

    public int TEAM_ID { get; set; }
    public float AMOUNT { get; set; }
    public string CURRENCY { get; set; }
    public int PAYMENT_METHOD { get; set; }
    public int CR_ACCOUNT { get; set; }
    public int DR_ACCOUNT { get; set; }

    public float AFP { get; set; }
    public float TAX { get; set; }


}

public class InstitutionModel
{
    public int ID { get; set; }
    public int TAX_ID { get; set; }
    public string TAX_NAME { get; set; }
    public string NAME { get; set; }
    public string INSTITUTIONALEMAIL { get; set; }
    public string INSTITUTION { get; set; }
    public string DEF_PASS { get; set; }
    public int RNC { get; set; }
    public int CODE { get; set; }
}

public class DepartmentProfile
{
    public int ID { get; set; }
    public string NAME { get; set; }
    public string INITIALS { get; set; }
    public int CODE { get; set; }
    public string LOCATION { get; set; }
    public string JUSTIFICATION { get; set; }
    public string OBJETIVE { get; set; }
    public string DESCRIPTION { get; set; }
    public int CREATED_BY { get; set; }
    public string DATETIME { get; set; }
    public string INSTITUTION { get; set; }
}

public class TEAMPROFILE
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public string NAME { get; set; }
    public string INITIALS { get; set; }
    public string LOCATION { get; set; }
    public string JUSTIFICATION { get; set; }
    public string OBJETIVE { get; set; }
    public string DESCRIPTION { get; set; }
    public int CREATED_BY { get; set; }
    public string DATETIME { get; set; }
    public int REPORT_TO { get; set; }
    public int DEPARTMENT{get;set;}

}
public class Department
{
    public int ID { get; set; }
    public string Department_Key { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Limit { get; set; }
}


public class AddressProfile
{
    public int ID { get; set; }
    public int LOCATION_TYPE { get; set; }
    public string BUILDING_NAME { get; set; }
    public string STREET { get; set; }
    public string PROVINCE { get; set; }
    public int NUMBER { get; set; }
    public string STATE { get; set; }
    public string CITY { get; set; }
    public string CONTRY { get; set; }
    public int ZIP { get; set; }

    public string PHONE { get; set; }
    public string FAX { get; set; }
    public string INSTITUTION { get; set; }
    
}

public class FeedBack
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public string DATE { get; set; }
    public int STATE { get; set; }
    public string DATA { get; set; }
}

public class SessionModel
{
    [Key]
    public int SESSION_KEY { get; set; }

    public string SESSION_ID { get; set; }

    public string USR_ID { get; set; }
    public string PROD_BARCODE_ARRAY { get; set; }

    public string NAME { get; set; }
    public float PROD_PRICE_ARRAY { get; set; }
    public float EARN_ARRAY { get; set; }
    public string BARCODE { get; set; }
    public float PRICE { get; set; }
    public int SESSION_PROD_COUNT_TOTAL { get; set; }

    public float SESSION_TOTAL_CASH_PAYED { get; set; }

    public float SESSION_TOTAL_CASH_RETURNED { get; set; }

    public string SESSION_TIME { get; set; }
}

public class TempSession
{
    public int ID { get; set; }

    public string INSTITUTION { get; set; }

    public int UID { get; set; }

    public string BARCODE { get; set; }

    public int VOLUME { get; set; }
    public int MACHINE { get; set; }


}

public class MachineModel
{
    public int ID { get; set; }
    public int NUMBER { get; set; }

    public int CODE { get; set; }

    public int STATE { get; set; }
    public string INSTITUTION { get; set; }
}

public class StockModel
{
    [Key]
    public int ID { get; set; }

    public int PROVIDER { get; set; }

    public int GROUPOF { get; set; }

    public string BARCODE { get; set; }

    public string NAME { get; set; }

    public string BRAND { get; set; }

    public string DESCRIPTION { get; set; }

    public string IMAGE { get; set; }

    public float VALUE { get; set; }

    public float STOCK { get; set; }

    public string MASSUNITY { get; set; }

    public string TAX { get; set; }

    public float PRICE { get; set; }

    public int CR_ACCOUNT { get; set; }

    public int DR_ACCOUNT { get; set; }

    public float SUMSTOCK { get; set; }

    public string INSTITUTION { get; set; }
    public int UID { get; set; }

    public string ACCOUNTING_SEAT { get; set; }



}

public class StockCategory
{
    public int ID { get; set; }
    public string NAME { get; set; }
    public string DESCRIPTION { get; set; }
    public string INSTITUTION { get; set; }


}



//inventory
public class RequisitionModel
{
    public int SERIAL { get; set; }
    public string INSTITUTION { get; set; }
    public string NAME { get; set; }
    public string BARCODE { get; set; }
    public int QUANTITY { get; set; }
    public string DATE { get; set; }
    public int CODE { get; set; }
}

//Provider
public class ProviderProfile
{
    [Key]
    public int ID { get; set; }

    public string INSTITUTION { get; set; }

    public string NAME { get; set; }

    public string PHONE { get; set; }

    public string EMAIL { get; set; }

    public string ADDRESS { get; set; }

    public int ORDER_LIMIT { get; set; }

    public float PREFERED_PRICE { get; set; }

    public string BARCODE_ARRAY { get; set; }

    public string PRODUCT_CATEGORY_ARRAY { get; set; }

    public string PRODUCT_ARRAY { get; set; }

    public string NOTE { get; set; }
    public string CATEGORY { get; set; }

}


public class ConciliationDailyModel
{
    [Key]
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int TOTAL_PROD { get; set; }
    public float TOTAL_SOLD { get; set; }
    public float TOTAL_MISSING { get; set; }
    public int CASH_STILL { get; set; }
    public int CODE { get; set; }
    public int AUTHORIZED_BY { get; set; }
    public DateOnly DATE { get; set; }
    public TimeOnly TIME { get; set; }

    public string BILL_ID { get; set; }

    public float TAXES { get; set; }

    public float TOTALEARNED { get; set; }
}

public class BillModel
{
    public int ID { get; set; }

    public int UID { get; set; }

    public string BARCODE_ARRAY { get; set; }

    public string PRICE_ARRAY { get; set; }

    public int SELLING_AMOUNT { get; set; }

    public int PAYED { get; set; }

    public int RETURNED { get; set; }

    public string TIME { get; set; }

    public string DATE { get; set; }

    public int MACHINE { get; set; }

    public int STATE { get; set; }

    public float ITBIS { get; set; }
    public int PRICE_ARRAY_TOTAL { get; set; }

    public string TOTAL_EARNED_ARRAY { get; set; }
}

public class PersonalTodoTask{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }
    public string TASK { get; set; }
    public string VALUE{get;set;}
    public bool STATUS{get;set;}
    public string RANKING { get; set; }
    public string CREATION_DATE{get;set;}
    public string EXPIRATION_DATE{get;set;}
    public string EXPIRATION_TIME{get;set;}
    public bool REPEAT{get;set;}

}


public class Notifications{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }
    public string DATA { get; set; }
    public bool STATUS{get;set;}
    public string DATE { get; set; }


}

public class Absence{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }
    public int REPORTED_BY{get;set;}
    public string REASON { get; set; }
    public bool STATUS{get;set;}
    public string DATE { get; set; }


}

public class ProductibityVolume{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }
    public string TASK { get; set; }
    public int VOLUME { get; set; }
    public float VALUE{get;set;}
    public string DATE { get; set; }
    public int TEAM_ID {get;set;}


}

public class AuthenticationProcedure{
    public string Institution{get;set;}
    public int Code {get;set;}
    public string Date{get;set;}
    public string time{get;set;}
    public string Ip{get;set;}
    public string sha {get;set;}
    public bool State {get;set;}
    public string Value {get;set;}
}

public class CalendarEventModel
{
    public int ID { get; set; }
    public int CODE { get; set; }
    public string INSTITUTION { get; set; }
    public string TITLE { get; set; }
    public string START { get; set; }
    public string END { get; set; }
    public string CREATION { get; set; }
    public string DESCRIPTION { get; set; }
}

public class ClosingDay
{
    public int ID { get; set; }
    public string DATE { get; set; }
    public float TOTAL { get; set; }
    public string CURRENCY { get; set; }
    public string INSTITUTION { get; set; }
    public int UID { get; set; }
}

public class DirectoryModel
{
    public int ID { get; set; }
    public int CODE { get; set; }
    public string INSTITUTION { get; set; }
    public string LOCATION { get; set; }
    public string DIR_NAME { get; set; }
    public string FOLDER_TYPE { get; set; }
    public string DATE_CREATION { get; set; }
    public bool STATE { get; set; }
}

public class LinksModel
{
    public int id { get; set; }
    public string ADDRESS { get; set; }
    public int CODE { get; set; }
    public string INSTITUTION { get; set; }
    public string DESCRIPTION { get; set; }
}

public class PositionTitleModel
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public string NAME { get; set; }
    public string DESCRIPTION { get; set; }
    public string CODE { get; set; }
}

public class SequenceModel
{
    public string SEQ_NAME { get; set; }
    public long SEQ_VALUE { get; set; }
}

public class UserStateModel
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public string BREAK { get; set; }
    public string LUNCH { get; set; }
    public string OTHER { get; set; }
    public string READY { get; set; }
    public int CODE { get; set; }
}

public class WageTaxModel
{
    public int ID { get; set; }
    public string INSTITUTION { get; set; }
    public int CODE { get; set; }
    public float TAX_PERCENT { get; set; }
    public float OVER_AMOUNT { get; set; }
    public string DATE { get; set; }
    public int STATE { get; set; }
}