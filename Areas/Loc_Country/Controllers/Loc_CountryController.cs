using database.Areas.Loc_Country.Models;
using database.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace database.Areas.Loc_Country.Controllers
{
    [Area("Loc_Country")]
    [Route("Loc_Country/[controller]/[action]")]
    public class Loc_CountryController : Controller
    {
        private IConfiguration configuration;
       
        #region con 
        public Loc_CountryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        Loc_CountryDal loc_CountryDal = new Loc_CountryDal();
        #region index
        public IActionResult Index()
        {
           // string ConnString = this.configuration.GetConnectionString("Mystring");
            
            //Loc_CountryDal loc_CountryDal = new Loc_CountryDal();
            DataTable dt = loc_CountryDal.Loc_Countrygetall();
            /*DataTable dt= new DataTable();
            SqlConnection sqlConn=new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd=sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Country_SelectAll";
            SqlDataReader reader = cmd.ExecuteReader(); 
            dt.Load(reader);*/
            return View("Loc_CountryList",dt);
        }
        #endregion
        public IActionResult Loc_CountryAddedit(int? CountryID)
        {
            
            if(CountryID != null)
            {
                DataTable dt = loc_CountryDal.Loc_Countrygetbypk(CountryID);

             /*   String ConnString = this.configuration.GetConnectionString("Mystring");
                DataTable dt = new DataTable();
                SqlConnection sqlConn = new SqlConnection(ConnString);
                *//* SqlDatabase db=new SqlDatabase(ConnString);*//*
                sqlConn.Open();
                SqlCommand cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_Country_SelectAllByPk";
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);*/
                if(dt.Rows.Count>0) 
                {
                    Loc_CountryModel modelLOC_Country = new Loc_CountryModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                        modelLOC_Country.CountryName = dr["CountryName"].ToString();
                        modelLOC_Country.CountryCode = dr["CountryCode"].ToString();
                        modelLOC_Country.Created = Convert.ToDateTime(dr["Created"]);
                        modelLOC_Country.Modified = Convert.ToDateTime(dr["Modified"]);

                    }
                    return View("LOC_CountryAddEdit", modelLOC_Country);
                }
            }
            return View();
        }
        public IActionResult Loc_CountryDelete(int CountryID)
        {

           // Loc_CountryDal loc_CountryDal = new Loc_CountryDal();
            if(Convert.ToBoolean(loc_CountryDal.Loc_Countrydelete(CountryID)))
            {
                return RedirectToAction("Index");
            }
            return View("Index");

/*            try
            {
              
             *//*   String ConnString = this.configuration.GetConnectionString("Mystring");
                DataTable dt = new DataTable();
                SqlConnection sqlConn = new SqlConnection(ConnString);
                *//* SqlDatabase db=new SqlDatabase(ConnString);*//*
                sqlConn.Open();
                SqlCommand cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_Country_DeleteByPK";
                cmd.Parameters.AddWithValue("@CountryID", CountryID);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                sqlConn.Close();*//*
                
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }*/
        }

        public IActionResult Save(Loc_CountryModel modelcountry)
        {
            if(modelcountry.CountryID==null)
            {
                if (Convert.ToBoolean(loc_CountryDal.Loc_Countryinsert(modelcountry))) 
                {
                    TempData["CountryInsertMsg"] = "Record Inserted Successfully";
                }
            }
            else
            {
                if (Convert.ToBoolean(loc_CountryDal.Loc_Countryupdate(modelcountry)))
                {
                    TempData["CountryInsertMsg"] = "Record updated Successfully";
                }
                return RedirectToAction("Index");
            }
          /*  String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelcountry.CountryID == null)
            {
                cmd.CommandText = "PR_Country_Insert";
                //cmd.Parameters.Add("@CountryAddDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            else
            {
                cmd.CommandText = "PR_Country_UpdateByPK";
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelcountry.CountryID;

            }
            cmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = modelcountry.CountryName;
            cmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = modelcountry.CountryCode;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelcountry.CountryID == null)
                {
                    TempData["CountryInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["CountryInsertMsg"] = "Record updated Successfully";
                }
            }
            sqlConn.Close();*/
            return RedirectToAction("Index");
        }
      
        public IActionResult Search(String? searchdata)
        {
            return View("Loc_CountryList",loc_CountryDal._fetchData(searchdata));
        }
           //View("Loc_CountryList", loc_CountryDal. fetchData(searchdata));
    }
}