using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using database.Areas.Loc_State.Models;
using database.Areas.MST_Student.Models;
using database.Areas.Loc_Country.Models;
using database.Areas.Loc_City.Models;
using database.Areas.MST_Branch.Models;
using Microsoft.CodeAnalysis.Operations;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;

namespace database.Areas.MST_Student.Controllers
{
    [Area("MST_Student")]
    [Route("MST_Student/[controller]/[action]")]
    public class Mst_StudentController : Controller
    {
        private IConfiguration configuration;

        #region con
        public Mst_StudentController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion
        public IActionResult Index()
        {
            FillBranchDDL();
            FillCityDDL();
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Pro_Student_select";
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            return View("Mst_StudentList",dt);
        }
        public IActionResult Mst_StudentDelete(int StudentID)
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
                cmd.CommandText = "Pro_Student_delete";
                cmd.Parameters.AddWithValue("@StudentID", StudentID);
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

        public IActionResult Mst_StudentAddEdit(int? StudentID)
        {
            FillCityDDL();
            FillBranchDDL();
            if (StudentID != null)
            {
                String ConnString = this.configuration.GetConnectionString("Mystring");
                DataTable dt = new DataTable();
                SqlConnection sqlConn = new SqlConnection(ConnString);
                /* SqlDatabase db=new SqlDatabase(ConnString);*/
                sqlConn.Open();
                SqlCommand cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Pro_Student_selectByPk";
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentID;
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);

                Mst_Student modelStudent = new Mst_Student();
                foreach (DataRow dr in dt.Rows)
                {
                    modelStudent.StudentID = Convert.ToInt32(dr["StudentID"]);
                    modelStudent.StudentName = dr["StudentName"].ToString();
                    modelStudent.CityID = Convert.ToInt32(dr["CityID"].ToString());
                    modelStudent.BranchID = Convert.ToInt32(dr["BranchID"].ToString());
                    modelStudent.MobileNoStudent = dr["MobileNoStudent"].ToString();
                    modelStudent.Email = dr["Email"].ToString();
                    modelStudent.MobileNoFather = dr["MobileNoFather"].ToString();
                    modelStudent.Address = dr["Address"].ToString();
                    modelStudent.BirthDate =Convert.ToDateTime(dr["BirthDate"]);
                    modelStudent.Age = Convert.ToInt32(dr["Age"].ToString());
                    modelStudent.IsActive =Convert.ToBoolean(dr["IsActive"]);
                    modelStudent.Gender = dr["Gender"].ToString();
                    modelStudent.Password = dr["Password"].ToString();
                 
                }
                return View("Mst_StudentAddEdit", modelStudent);

            }
            return View();
        }

        public IActionResult Save(Mst_Student modelStudent)
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelStudent.StudentID == null)
            {
                cmd.CommandText = "Pro_Student_insert";
                //cmd.Parameters.Add("@CountryAddDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            else
            {
                cmd.CommandText = "Pro_Student_update";
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = modelStudent.StudentID;

            }
            cmd.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = modelStudent.StudentName;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelStudent.CityID;
            cmd.Parameters.Add("@BranchID", SqlDbType.Int).Value = modelStudent.BranchID;
            cmd.Parameters.Add("@MobileNoStudent", SqlDbType.VarChar).Value = modelStudent.MobileNoStudent;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = modelStudent.Email;
            cmd.Parameters.Add("@MobileNoFather", SqlDbType.VarChar).Value = modelStudent.MobileNoFather;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = modelStudent.Address;
            cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = Convert.ToDateTime(modelStudent.BirthDate);
            cmd.Parameters.Add("@Age", SqlDbType.Int).Value = modelStudent.Age;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = modelStudent.IsActive;
            cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = modelStudent.Gender;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = modelStudent.Password;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelStudent.StudentID == null)
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
        
        public void FillCityDDL()
        {

            String ConnString = this.configuration.GetConnectionString("Mystring");
            List<LOC_CityDropDownModel> loc_City = new
           List<LOC_CityDropDownModel>();
            SqlConnection objConn = new SqlConnection(ConnString);
            objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_City_SelectAll_Dropdown";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    LOC_CityDropDownModel city = new
               LOC_CityDropDownModel()
                    {
                        CityID = Convert.ToInt32(objSDR["CityID"]),
                        CityName = objSDR["CityName"].ToString()
                    };
                    loc_City.Add(city);
                }
                objSDR.Close();
            }
            objConn.Close();
            ViewBag.CityList = loc_City;

        }
        public void FillBranchDDL()
        {

            String ConnString = this.configuration.GetConnectionString("Mystring");
            List<Mst_BranchDropDownModel> Mst_branch = new
           List<Mst_BranchDropDownModel>();
            SqlConnection objConn = new SqlConnection(ConnString);
            objConn.Open();
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_Branch_SelectAll_Dropdown";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    Mst_BranchDropDownModel branch = new
               Mst_BranchDropDownModel()
                    {
                        BranchID = Convert.ToInt32(objSDR["BranchID"]),
                        BranchName = objSDR["BranchName"].ToString()
                    };
                    Mst_branch.Add(branch);
                }
                objSDR.Close();
            }
            objConn.Close();
            ViewBag.branchList = Mst_branch;

        

        }


        private DataTable _fetchData(String? StudentName, String? Email, String? Address, String? Age, String? Gender, int BranchID = -1, int CityID = -1)
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Pro_Student_select";
            if(StudentName!="") cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = StudentName;
            /*            cmd.Parameters.Add("@BranchName", SqlDbType.NVarChar).Value = BranchName;
                        cmd.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = CityName;*/
            if (BranchID != -1) cmd.Parameters.AddWithValue("BranchID", BranchID);
            if (CityID != -1) cmd.Parameters.AddWithValue("CityID", CityID);
            if (Email != "") cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            if (Address != "") cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = Address;
            if (Age != "") cmd.Parameters.Add("@Age", SqlDbType.NVarChar).Value = Age;
            if (Gender != "") cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = Gender;
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            sqlConn.Close();
            return dt;
        }
        public IActionResult Search(String? StudentName, String? Email, String? Address, String? Age, String? Gender, int BranchID = -1, int CityID = -1)
        {
            FillBranchDDL();
            FillCityDDL();
            return View("Mst_StudentList", _fetchData(StudentName, Email, Address, Age, Gender, BranchID, CityID));
        }
    }
}
 