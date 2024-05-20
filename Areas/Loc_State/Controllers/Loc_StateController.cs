using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using database.Areas.Loc_Country.Models;
using database.Areas.Loc_State.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace database.Areas.Loc_State.Controllers
{
    [Area("Loc_State")]
    [Route("Loc_State/[controller]/[action]")]
    public class Loc_StateController : Controller
    {
        private IConfiguration configuration;

        #region con
        public Loc_StateController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion
        public IActionResult Index()
        {
            FillCountryDDL();
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Pro_State_Selectall";
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            return View("Loc_StateList",dt);
        }

        public IActionResult Loc_StateDelete(int StateID)
        {
            try
            {
                String ConnString = this.configuration.GetConnectionString("Mystring");
                DataTable dt = new DataTable();
                SqlConnection sqlConn = new SqlConnection(ConnString);
                /* SqlDatabase db=new SqlDatabase(ConnString);*/
                sqlConn.Open();
                SqlCommand cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Pro_State_Delete";
                cmd.Parameters.AddWithValue("@StateID", StateID);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                sqlConn.Close();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult Loc_StateAddedit(int? StateID)
        {
            FillCountryDDL();
            if (StateID != null)
            {
                String ConnString = this.configuration.GetConnectionString("Mystring");
                DataTable dt = new DataTable();
                SqlConnection sqlConn = new SqlConnection(ConnString);
                /* SqlDatabase db=new SqlDatabase(ConnString);*/
                sqlConn.Open();
                SqlCommand cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Pro_State_SelectallByPk";
                cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);

                Loc_StateModel modelState = new Loc_StateModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelState.StateID = Convert.ToInt32(dr["StateID"]);
                    modelState.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelState.StateName = dr["StateName"].ToString();
                    modelState.StateCode = dr["StateCode"].ToString();
                    modelState.Created = Convert.ToDateTime(dr["Created"]);
                    modelState.Modified = Convert.ToDateTime(dr["Modified"]);

                }
                return View("LOC_StateAddEdit", modelState);

            }
            return View();
        }

        public IActionResult Save(Loc_StateModel modelState)
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelState.StateID == null)
            {
                cmd.CommandText = "Pro_State_insert";
                //cmd.Parameters.Add("@CountryAddDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            else
            {
                cmd.CommandText = "Pro_State_Update";
                cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelState.StateID;

            }
            cmd.Parameters.Add("@StateName", SqlDbType.VarChar).Value = modelState.StateName;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelState.CountryID;
            cmd.Parameters.Add("@StateCode", SqlDbType.VarChar).Value = modelState.StateCode;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelState.StateID == null)
                {
                    TempData["StateInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["StateInsertMsg"] = "Record updated Successfully";
                }
            }
            sqlConn.Close();
            return RedirectToAction("Index");

        }

        public void FillCountryDDL()
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            List<LOC_CountryDropDownModel> loc_Country = new
           List<LOC_CountryDropDownModel>();
            SqlConnection objConn = new SqlConnection(ConnString);
            objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Country_SelectAll_Dropdown";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    LOC_CountryDropDownModel country = new
               LOC_CountryDropDownModel()
                {
                        CountryID = Convert.ToInt32(objSDR["CountryID"]),
                        CountryName = objSDR["CountryName"].ToString()
                };
                    loc_Country.Add(country);
                }
                objSDR.Close();
            }
            objConn.Close();
            ViewBag.CountryList = loc_Country;
        }

        private DataTable _fetchData(String StateName="",int CountryID=-1)
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Pro_State_Selectall";
            if(StateName!="") cmd.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = StateName;
            if (CountryID != -1) cmd.Parameters.AddWithValue("CountryID",CountryID);
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            sqlConn.Close();
            return dt;
        }
        public IActionResult Search(String StateName="",int CountryID=-1)
        {
            FillCountryDDL();
            return View("Loc_StateList", _fetchData(StateName, CountryID));
        }
      /*  public IActionResult Search1(String? CountryName,String? StateName)
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Pro_State_SelectallByname";
            if (CountryName == null)
            {
                CountryName = "";
            }
            if (StateName == null)
            {
                StateName = "";
            }
            cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = CountryName;
            cmd.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = StateName;
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            sqlConn.Close();
            return View("LOC_StateList", dt);
        }*/



    }
}

