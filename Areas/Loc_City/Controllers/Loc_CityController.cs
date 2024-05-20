using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using database.Areas.Loc_State.Models;
using database.Areas.Loc_City.Models;
using database.Areas.Loc_Country.Models;

namespace database.Areas.Loc_City.Controllers
{
    [Area("Loc_City")]
    [Route("Loc_City/[controller]/[action]")]
    public class Loc_CityController : Controller
    {
        private IConfiguration configuration;

        #region con
        public Loc_CityController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion
        public IActionResult Index()
        {
            FillCountryDDL();
            FillStateDDL();
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Pro_City_Selectall";
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            return View("Loc_CityList", dt);
        }
        public IActionResult Loc_CityDelete(int CityID)
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
                cmd.CommandText = "Pro_City_delete";
                cmd.Parameters.AddWithValue("@CityID", CityID);
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

        public IActionResult Loc_CityAddedit(int? CityID)
        {
            FillCountryDDL();
            FillStateDDL();
            if (CityID != null)
            {
                String ConnString = this.configuration.GetConnectionString("Mystring");
                DataTable dt = new DataTable();
                SqlConnection sqlConn = new SqlConnection(ConnString);
                /* SqlDatabase db=new SqlDatabase(ConnString);*/
                sqlConn.Open();
                SqlCommand cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Pro_City_SelectByPk";
                cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);

                Loc_CityModel modelLOC_City = new Loc_CityModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLOC_City.CityName = dr["CityName"].ToString();
                    modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_City.CityCode = dr["CityCode"].ToString();
                    modelLOC_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_City.Modified = Convert.ToDateTime(dr["Modified"]);

                }
                return View("LOC_CityAddEdit", modelLOC_City);

            }
            return View();
        }
        /*        private IActionResult _WrongFilled(Loc_CityModel model) {
                    FillCountryDDL();
                    FillStateDDL();
                    return View("Loc_CityAddedit",model);
         if (!TryValidateModel(modelCity))
                        return _WrongFilled(modelCity);
                }*/
        public IActionResult Save(Loc_CityModel modelCity)
        {
           
;            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelCity.CityID == null)
            {
                cmd.CommandText = "Pro_City_insert";
                //cmd.Parameters.Add("@CountryAddDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            else
            {
                cmd.CommandText = "Pro_City_Update";
                cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelCity.CityID;

            }
            cmd.Parameters.Add("@CityName", SqlDbType.VarChar).Value = modelCity.CityName;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelCity.CountryID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelCity.StateID;
            cmd.Parameters.Add("@CityCode", SqlDbType.VarChar).Value = modelCity.CityCode;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelCity.CityID == null)
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
        public void FillStateDDL()
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            List<LOC_StateDropDownModel> loc_State = new
           List<LOC_StateDropDownModel>();
            SqlConnection objConn = new SqlConnection(ConnString);
            objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_SelectAll_Dropdown";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    LOC_StateDropDownModel state = new
               LOC_StateDropDownModel()
                    {
                        StateID = Convert.ToInt32(objSDR["StateID"]),
                        StateName = objSDR["StateName"].ToString()
                    };
                    loc_State.Add(state);
                }
                objSDR.Close();
            }
            objConn.Close();
            ViewBag.StateList = loc_State;

        }


        #region DropDownByCountry
        [HttpPost]
        public IActionResult DropDownByCountry(int? CountryID)
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Statedropdownbycountry";
            cmd.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader objSDR = cmd.ExecuteReader();
            dt.Load(objSDR);
            sqlConn.Close();
            List<LOC_StateDropDownModel> list1 = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_StateDropDownModel vlst = new LOC_StateDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = dr["StateName"].ToString();
                list1.Add(vlst);
            }
            var vModel = list1;
            return Json(vModel);
        }
        #endregion

        private DataTable _fetchData(String? CityName="", int CountryID = -1, int StateID = -1)
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Pro_City_Selectall";
            /*cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = CountryName;
            cmd.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = StateName;*/
            if (CityName != "") cmd.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = CityName;
            if (CountryID != -1) cmd.Parameters.AddWithValue("CountryID", CountryID);
            if (StateID != -1) cmd.Parameters.AddWithValue("StateID", StateID);
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            sqlConn.Close();
            return dt;
        }
        public IActionResult Search(String? CityName="", int CountryID = -1, int StateID = -1)
        {
            FillCountryDDL();
            FillStateDDL();
            return View("Loc_CityList", _fetchData(CityName, CountryID, StateID));
        }
            
    }
}
