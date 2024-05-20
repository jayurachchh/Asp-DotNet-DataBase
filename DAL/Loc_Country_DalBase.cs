using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using database.Areas.Loc_Country.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
namespace database.DAL
{
    public class Loc_Country_DalBase :Dal_Helper
    {
        public DataTable _fetchData(String? searchdata = null)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(Constr);
                DbCommand cmd = sqldb.GetStoredProcCommand("PR_Country_SelectAll");
                sqldb.AddInParameter(cmd, "data", SqlDbType.VarChar, searchdata);
                DataTable dt = new DataTable();
                using (IDataReader reader = sqldb.ExecuteReader(cmd))
                {
                    dt.Load(reader);
                }
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
            /* String ConnString = this.configuration.GetConnectionString("Mystring");
             DataTable dt = new DataTable();
             SqlConnection sqlConn = new SqlConnection(ConnString);
             sqlConn.Open();
             SqlCommand cmd = sqlConn.CreateCommand();
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "PR_Country_SelectAll";
             cmd.Parameters.Add("@data", SqlDbType.VarChar).Value = searchdata;
             SqlDataReader reader = cmd.ExecuteReader();
             dt.Load(reader);
             sqlConn.Close();
             return dt;*/
        }
        public DataTable Loc_Countrygetall() 
        {
            try 
            {
                SqlDatabase sqldb = new SqlDatabase(Constr);
                
                DbCommand cmd = sqldb.GetStoredProcCommand("PR_Country_SelectAll");
                
                DataTable dt = new DataTable();
                using(IDataReader reader = sqldb.ExecuteReader(cmd))
                {
                    dt.Load(reader);
                }
                return dt;
            }
            catch(Exception e)  
            {
                return null;
            }
        }
        public bool  Loc_Countrydelete(int CountryID)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(Constr);

                DbCommand cmd = sqldb.GetStoredProcCommand("PR_Country_DeleteByPK");
                sqldb.AddInParameter(cmd, "CountryID",SqlDbType.Int, CountryID);
                int issuccess = sqldb.ExecuteNonQuery(cmd);
                return (issuccess == -1 ? false:true);

            }
            catch (Exception e) 
            {
                return false;
            }

        }
        public DataTable Loc_Countrygetbypk(int? CountryID)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(Constr);

                DbCommand cmd = sqldb.GetStoredProcCommand("PR_Country_SelectAllByPk");
                sqldb.AddInParameter(cmd,"CountryID", SqlDbType.Int,CountryID);

                DataTable dt = new DataTable();
                using (IDataReader reader = sqldb.ExecuteReader(cmd))
                {
                    dt.Load(reader);
                }
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Loc_Countryinsert(Loc_CountryModel modelcountry)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(Constr);
                DbCommand cmd = sqldb.GetStoredProcCommand("PR_Country_Insert");
                sqldb.AddInParameter(cmd, "CountryName", SqlDbType.VarChar, modelcountry.CountryName);
                sqldb.AddInParameter(cmd, "CountryCode", SqlDbType.VarChar, modelcountry.CountryCode);

                int vReturnValue = sqldb.ExecuteNonQuery(cmd);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Loc_Countryupdate(Loc_CountryModel modelcountry)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(Constr);
                DbCommand cmd = sqldb.GetStoredProcCommand("PR_Country_UpdateByPK");
                sqldb.AddInParameter(cmd, "CountryID", SqlDbType.Int, modelcountry.CountryID);
                sqldb.AddInParameter(cmd, "CountryName", SqlDbType.VarChar, modelcountry.CountryName);
                sqldb.AddInParameter(cmd, "CountryCode", SqlDbType.VarChar, modelcountry.CountryCode);

                int vReturnValue = sqldb.ExecuteNonQuery(cmd);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
       

    }
}
