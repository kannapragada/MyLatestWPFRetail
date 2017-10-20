using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Serialization;
using System.Xml;



namespace WCFRetailService
{
    public class SQLDataAccess : IDisposable, IDataAccessService
    {
        string ConnStr;
        SqlConnection Conn;
        SqlCommand Cmd;
        SqlDataAdapter Adptr;

        static private string ConStr;

        static SQLDataAccess SQLDataAccessObj = new SQLDataAccess();

        private bool disposed;


        ~SQLDataAccess()
    {
        this.Dispose(false);
    }

        public void Dispose()
    {
        if (!this.disposed)
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
            this.disposed = true;
        }
    }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // clean up managed resources
                Adptr.Dispose();
                Cmd.Dispose();
                Conn.Dispose();
                ConnStr = null;

            }

            // clean up unmanaged resources
        }


        public SQLDataAccess()
        {

        }
        
        public SQLDataAccess(string ConnectionString)
        {
            ConnStr = ConnectionString;
        }

        public static SQLDataAccess GetDBObj(out string errorMessage)
        {
            errorMessage = null;

            try
            {
                if (SQLDataAccessObj != null)
                    return SQLDataAccessObj;
                else
                {
                    try
                    {
                        string Option = null;

                        AppSettingsReader AppRdr = new AppSettingsReader();

                        Option = AppRdr.GetValue("HomeorOffice", typeof(string)).ToString();

                        if (Option == "O")
                            ConStr = AppRdr.GetValue("WPFRetailOfficeConnectionString", typeof(string)).ToString();
                        else if (Option == "H")
                            ConStr = AppRdr.GetValue("WPFRetailHomeConnectionString", typeof(string)).ToString();


                        SQLDataAccessObj = new SQLDataAccess();

                        SQLDataAccessObj.ConnectDB(out errorMessage);

                    }
                    catch (Exception ex1)
                    {
                        errorMessage = ex1.Message;
                    }
                }
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }


            return SQLDataAccessObj;
        }


        public bool ConnectDB(out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;

            try
            {
                string Option = null;

                AppSettingsReader AppRdr = new AppSettingsReader();

                Option = AppRdr.GetValue("HomeorOffice", typeof(string)).ToString();

                if (Option == "O")
                    ConStr = AppRdr.GetValue("WPFRetailOfficeConnectionString", typeof(string)).ToString();
                else if (Option == "H")
                    ConStr = AppRdr.GetValue("WPFRetailHomeConnectionString", typeof(string)).ToString();

                Conn = new SqlConnection(ConStr);
                Conn.Open();
                return true;
            }
            catch (SqlException ex1)
            {
                errorMessage = ex1.Message;
            }
            catch (Exception ex2)
            {
                errorMessage = ex2.Message;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            return retval;
        }

        public bool OpenDB(out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;

            try
            {
                if (Conn.State != ConnectionState.Open)
                    Conn.Open();

                if (Conn.State == ConnectionState.Open)
                    retval = true;
            }
            catch (SqlException ex1)
            {
                errorMessage = ex1.Message;
            }
            catch (Exception ex2)
            {
                errorMessage = ex2.Message;
            }

            return retval;
        }

        public bool CloseDB(string errorMessage, out string closeErrorMessage)
        {
            bool retval = false;

            closeErrorMessage = errorMessage;

             try
            {
                Conn.Close();
                if (Conn.State == ConnectionState.Closed)
                    retval = true;
            }
             catch (SqlException ex1)
             {
                 closeErrorMessage = closeErrorMessage + ex1.Message;
             }
             catch (Exception ex2)
             {
                 closeErrorMessage = closeErrorMessage + ex2.Message;
             }
            return retval;
        }

        public bool ExecuteSqlNonQry(out string errorMessage, string SqlStr)
        {
            int rescount;
            bool retval = false;

            errorMessage = null;


            if (ConnectDB(out errorMessage) == false)
            {
                errorMessage = "Could Not Connect to Database" + errorMessage;
                return retval;
            }
            
            Cmd = new SqlCommand();

            Cmd.Connection = Conn;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = SqlStr;

            rescount = 0;

            if (OpenDB(out errorMessage) == true)
            {
                try
                {
                    rescount = Cmd.ExecuteNonQuery();
                    if (rescount == -1)
                        errorMessage = "No Records effected!";

                    retval = true;
                }
                catch (SqlException ex1)
                {
                    errorMessage = ex1.Message;
                }
                catch (Exception ex2)
                {
                    errorMessage = ex2.Message;
                }
            }

            if (CloseDB(errorMessage, out errorMessage) == true)
            {
                if (errorMessage == null)
                    retval = true;
            }
             


            return retval;
        }


        public bool ExecuteSqlQry(string SqlStr, out DataSet Ds, out string errorMessage)
        {
            errorMessage = null;

            bool retval = false;

            Ds = new DataSet();

            if (ConnectDB(out errorMessage) == false)
            {
                errorMessage = "Could Not Connect to Database";
                return retval;
            }

            Cmd = new SqlCommand();

            Cmd.Connection = Conn;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = SqlStr;

            Adptr = new SqlDataAdapter(Cmd);

            if (OpenDB(out errorMessage) == true)
            {
                try
                {
                    Adptr.Fill(Ds);
                    if (Ds.Tables[0].DefaultView.Count == 0)
                        errorMessage = "No Records Found!";
                    retval = true;
                }
                catch (SqlException ex1)
                {
                    errorMessage = ex1.Message;
                }
                catch (Exception ex2)
                {
                    errorMessage = ex2.Message;
                }

                if (CloseDB(errorMessage, out errorMessage) == true)
                {
                    if (errorMessage == null)
                        retval = true;
                }

            }
            return retval;
        }

        public bool ExecuteSPWithParamsRespDS(out DataSet Ds, out string errorMessage, string StoredProcName, params SqlParameter[] SPParameters)
        {
            errorMessage = null;

            bool retval = false;  

            Ds = null;

            if (ConnectDB(out errorMessage) == false)
            {
                errorMessage = "Could Not Connect to Database";
                return retval;
            }

                Cmd = new SqlCommand();

                Cmd.Connection = Conn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = StoredProcName;

                Cmd.Parameters.AddRange(SPParameters);

                if (OpenDB(out errorMessage) == true)
                {
                    Adptr = new SqlDataAdapter(Cmd);

                    try
                    {
                        Ds = new DataSet();
                        Adptr.Fill(Ds);
                    }
                    catch (SqlException ex1)
                    {
                        errorMessage = ex1.Message;
                    }
                    catch (Exception ex2)
                    {
                        errorMessage = ex2.Message;
                    }

                    if (CloseDB(errorMessage, out errorMessage) == true)
                    {
                        if (errorMessage == null)
                            retval = true;
                    }
                }

            return retval;
        }

        public bool ExecuteSPWithParamsNoResp(out string errorMessage, string StoredProcName, params SqlParameter[] SPParameters)
        {
            errorMessage = null;

            bool retval = false;

            if (ConnectDB(out errorMessage) == false)
            {
                errorMessage = "Could Not Connect to Database";
                return retval;
            }

            Cmd = new SqlCommand();

            Cmd.Connection = Conn;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = StoredProcName;

            Cmd.Parameters.AddRange(SPParameters);

            try
            {
                if (OpenDB(out errorMessage) == true)
                {
                    Cmd.ExecuteNonQuery();
                    retval = true;
                }
            }
            catch (SqlException ex1)
            {
                errorMessage = ex1.Message;
            }
            catch (Exception ex2)
            {
                errorMessage = ex2.Message;
            }

            if (CloseDB(errorMessage, out errorMessage) == true)
            {
                if (errorMessage == null)
                    retval = true;
            }

            return retval;
        }

        public bool ExecuteSPForSearch(out DataSet Ds, out string errorMessage, string StoredProcName, string SearchStr)
        {
            errorMessage = null;

            bool retval = false;

            Ds = null;

            if (ConnectDB(out errorMessage) == false)
            {
                errorMessage = "Could Not Connect to Database";
                return retval;
            }

            Cmd = new SqlCommand();

            Cmd.Connection = Conn;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = StoredProcName;

            SqlParameter SPParam = new SqlParameter("@SearchStr", SearchStr);

            Cmd.Parameters.Add(SPParam);

            Adptr = new SqlDataAdapter(Cmd);

            if (OpenDB(out errorMessage) == true)
            {
                try
                {
                    Ds = new DataSet();
                    Adptr.Fill(Ds);
                }
                catch (SqlException ex1)
                {
                    errorMessage = ex1.Message;
                }
                catch (Exception ex2)
                {
                    errorMessage = ex2.Message;
                }

                if (CloseDB(errorMessage, out errorMessage) == true)
                {
                    if (errorMessage == null)
                        retval = true;
                }
            }
            return retval;
        }

        public bool ExecuteSPNoParamsWithRespDS(out DataSet Ds, out string errorMessage, string StoredProcName)
        {
            errorMessage = null;

            bool retval = false;

            Ds = null;

            if (ConnectDB(out errorMessage) == false)
            {
                errorMessage = "Could Not Connect to Database";
                return retval;
            }

            Cmd = new SqlCommand();

            Cmd.Connection = Conn;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = StoredProcName;

            Adptr = new SqlDataAdapter(Cmd);

            if (OpenDB(out errorMessage) == true)
            {
                try
                {
                    Ds = new DataSet();
                    Adptr.Fill(Ds);
                }
                catch (SqlException ex1)
                {
                    errorMessage = ex1.Message;
                }
                catch (Exception ex2)
                {
                    errorMessage = ex2.Message;
                }

                if (CloseDB(errorMessage, out errorMessage) == true)
                {
                    if (errorMessage == null)
                        retval = true;
                }
            }
            return retval;
        }


        public bool ExecuteSPToTestDataTables(out string Ds, out string errorMessage, string StoredProcName, params SqlParameter[] SPParameters)
        {
            errorMessage = null;

            bool retval = false;

            DataSet Ds1 = new DataSet();

            Ds = null;

            if (ConnectDB(out errorMessage) == false)
            {
                errorMessage = "Could Not Connect to Database";
                return retval;
            }

            Cmd = new SqlCommand();

            Cmd.Connection = Conn;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = StoredProcName;

            Cmd.Parameters.AddRange(SPParameters);

            if (OpenDB(out errorMessage) == true)
            {
                Adptr = new SqlDataAdapter(Cmd);

                try
                {
                    Adptr.Fill(Ds1);

                    Ds = Ds1.GetXml().ToString();
                }
                catch (SqlException ex1)
                {
                    errorMessage = ex1.Message;
                }
                catch (Exception ex2)
                {
                    errorMessage = ex2.Message;
                }

                if (CloseDB(errorMessage, out errorMessage) == true)
                {
                    if (errorMessage == null)
                        retval = true;
                }
            }

            return retval;
        }

        public bool CheckDataAccess(out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;

            try
            {
                string Option = null;

                AppSettingsReader AppRdr = new AppSettingsReader();

                Option = AppRdr.GetValue("HomeorOffice", typeof(string)).ToString();

                if (Option == "O")
                    ConStr = AppRdr.GetValue("WPFRetailOfficeConnectionString", typeof(string)).ToString();
                else if (Option == "H")
                    ConStr = AppRdr.GetValue("WPFRetailHomeConnectionString", typeof(string)).ToString();




                if (SQLDataAccessObj.ConnectDB(out errorMessage) == true)
                {
                    if (SQLDataAccessObj.OpenDB(out errorMessage) == true)
                        if (SQLDataAccessObj.CloseDB(errorMessage, out errorMessage) == true)
                            retval = true;
                }
                else
                    errorMessage = "Could Not Connect to Database";
            }
            catch (Exception ex1)
            {
                errorMessage = "Could Not Connect to Database" + ex1.Message;
            }

            return retval;
        }
        
        public System.Data.DataTable GetNormalTable()
        {
            DataTable dt = new DataTable("table1");
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(1, "av");
            dt.Rows.Add(2, "avkm");
            dt.Rows.Add(3, "avkmurthy");

            return dt;
        }

        public bool GetNormalDataSet(out System.Data.DataSet ds)
        {
            bool retval = false;

            DataTable dt = new DataTable("table1");
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(1, "av");
            dt.Rows.Add(2, "avkm");
            dt.Rows.Add(3, "avkmurthy");

            DataTable dt1 = new DataTable("table2");
            dt1.Columns.Add("id", typeof(int));
            dt1.Columns.Add("deptid", typeof(int));
            dt1.Columns.Add("salary", typeof(double));

            dt1.Rows.Add(1, 1, 1000);
            dt1.Rows.Add(2, 2, 2000);
            dt1.Rows.Add(3, 3, 3000);

            ds = new DataSet();

            ds.Tables.Add(dt);
            ds.Tables.Add(dt1);

            retval = true;


            return retval;
        }
    }
}
