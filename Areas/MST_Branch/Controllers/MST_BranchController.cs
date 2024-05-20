using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using database.Areas.Loc_Country.Models;
using database.Areas.MST_Branch.Models;
using database.Areas.Loc_State.Models;

namespace database.Areas.MST_Branch.Controllers
{
    [Area("MST_Branch")]
    [Route("MST_Branch/[controller]/[action]")]
    public class MST_BranchController : Controller
    {
        private IConfiguration configuration;

        #region con
        public MST_BranchController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion
        public IActionResult Index()
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PRO_Branch_SelectAll";
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            return View("MST_Branch",dt);
        }
        public IActionResult MST_BranchAddedit(int? BranchID)
        {
            if (BranchID != null)
            {
                String ConnString = this.configuration.GetConnectionString("Mystring");
                DataTable dt = new DataTable();
                SqlConnection sqlConn = new SqlConnection(ConnString);
                /* SqlDatabase db=new SqlDatabase(ConnString);*/
                sqlConn.Open();
                SqlCommand cmd = sqlConn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PRO_Branch_SelectByPk";
                cmd.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);

                Mst_BranchModel modelMst_Branch = new Mst_BranchModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelMst_Branch.BranchID = Convert.ToInt32(dr["BranchID"]);
                    modelMst_Branch.BranchName = dr["BranchName"].ToString();
                    modelMst_Branch.BranchCode = dr["BranchCode"].ToString();
                    modelMst_Branch.Created = Convert.ToDateTime(dr["Created"]);
                    modelMst_Branch.Modified = Convert.ToDateTime(dr["Modified"]);

                }
                return View("MST_BranchAddedit", modelMst_Branch);

            }
            return View();
        }
        public IActionResult MST_BranchDelete(int BranchID)
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
                cmd.CommandText = "Pro_Branch_delete";
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
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

        public IActionResult Save(Mst_BranchModel modelMst_Branch)
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelMst_Branch.BranchID == null)
            {
                cmd.CommandText = "Pro_branch_insert";
                //cmd.Parameters.Add("@CountryAddDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            else
            {
                cmd.CommandText = "Pro_Branch_Update";
                cmd.Parameters.Add("@BranchID", SqlDbType.Int).Value = modelMst_Branch.BranchID;

            }
            cmd.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = modelMst_Branch.BranchName;
            cmd.Parameters.Add("@BranchCode", SqlDbType.VarChar).Value = modelMst_Branch.BranchCode;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelMst_Branch.BranchID == null)
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

        public IActionResult Save1(Mst_BranchModel modelMst_Branch)
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelMst_Branch.BranchID == null)
            {
                cmd.CommandText = "Pro_branch_insert";
                //cmd.Parameters.Add("@CountryAddDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            else
            {
                cmd.CommandText = "Pro_Branch_Update";
                cmd.Parameters.Add("@BranchID", SqlDbType.Int).Value = modelMst_Branch.BranchID;

            }
            cmd.Parameters.Add("@BranchName", SqlDbType.VarChar).Value = modelMst_Branch.BranchName;
            cmd.Parameters.Add("@BranchCode", SqlDbType.VarChar).Value = modelMst_Branch.BranchCode;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelMst_Branch.BranchID == null)
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
        private DataTable _fetchData(String? searchdata = null)
        {
            String ConnString = this.configuration.GetConnectionString("Mystring");
            DataTable dt = new DataTable();
            SqlConnection sqlConn = new SqlConnection(ConnString);
            sqlConn.Open();
            SqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PRO_Branch_SelectAll";
            cmd.Parameters.Add("@data", SqlDbType.VarChar).Value = searchdata;
            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            sqlConn.Close();
            return dt;
        }
        public IActionResult Search(String? searchdata)
            => View("MST_Branch", _fetchData(searchdata));

    }
}
