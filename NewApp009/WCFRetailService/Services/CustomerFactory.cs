using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Media.Imaging;
using NewApp.BusinessTier.Models;
using WCFRetailService;


namespace WCFRetailService
{
    public class CustomerFactory : Customer, ICustomerFactory
    {
        SQLDataAccess DBObj;

        //Constructor
        public CustomerFactory()
        {
        }

        //Member Methods  
        #region MemberMethods

        //    private void GetCustomerFromFile()
        //{
        //        try
        //        {
        //            // Create an instance of StreamReader to read from a file.
        //            // The using statement also closes the StreamReader.
        //            using (StreamReader sr = new StreamReader("d:\\test.txt"))
        //            {
        //                String line;
        //                int count = 0;
        //                // Read and display lines from the file until the end of
        //                // the file is reached.
        //                while ((line = sr.ReadLine()) != null)
        //                {
        //                    if (count == 0)
        //                        this._custGender = line;
        //                    else if (count == 1)
        //                            this.Age = Convert.ToInt32(line);
        //                    else if (count == 2)
        //                            this.Gender = line;

        //                    count = count + 1;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Let the user know what went wrong.
        //            throw ex.InnerException;
        //        }
        //}

        //    public void GetCustObject()
        //{
        //    FilePickerThread = new Thread(new ThreadStart(GetCustomerFromFile));
        //    FilePickerThread.Start();

        //    while (!FilePickerThread.IsAlive) ;

        //    // Put the Main thread to sleep for 1 millisecond to allow oThread
        //    // to do some work:
        //    Thread.Sleep(100);

        //    // Request that oThread be suspended
        //    //FilePickerThread.Suspend();

        //    // Wait until oThread finishes. Join also has overloads
        //    // that take a millisecond interval or a TimeSpan object.
        //    FilePickerThread.Join();
        //}

        public bool GetCustomers(string CustomerIdOrName, out string errorMessage, out Customer[] CustObjArray)
            {
                DataSet Ds = new DataSet();
                DataRow Dr;
                Customer CustObj;

                bool retval = false;

                errorMessage = null;

                //CustObjList = new List<Customer>();

                CustObjArray = null; 

                SqlParameter[] SQLParamArray = new SqlParameter[2];

                SqlParameter parameter;

                parameter = new SqlParameter("@Cust_Id", SqlDbType.VarChar);
                parameter.Value = CustomerIdOrName;
                SQLParamArray[0] = parameter;

                parameter = new SqlParameter("@Cust_Name", SqlDbType.VarChar);
                parameter.Value = CustomerIdOrName;
                SQLParamArray[1] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;


                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetCustomers", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {

                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            CustObjArray = new Customer[Ds.Tables[0].Rows.Count];

                            for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                            {
                                CustObj = new Customer();

                                Dr = Ds.Tables[0].Rows[i];

                                CustObj.CustomerId = Dr[0].ToString();
                                CustObj.CustomerName = Dr[1].ToString();

                                CustomerStatusFactory CustomerStatusFactory = new CustomerStatusFactory();
                                CustomerStatus CustStatus = new CustomerStatus();
                                if (CustomerStatusFactory.GetCustomerStatus(Convert.ToInt32(Dr[2].ToString()), out CustStatus, out errorMessage) == true)
                                    CustObj.Status = CustStatus;
                                else
                                    return retval;

                                if ((Dr[3].ToString() != null) && (Dr[3].ToString() != ""))
                                    CustObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                                GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                                GenderType GenderType = new GenderType();
                                if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                    CustObj.GenderType = GenderType;
                                else
                                    return retval;

                                CustObj.PresentAddress = Dr[5].ToString();
                                CustObj.PresentCity = Dr[6].ToString();
                                CustObj.PresentPinCode = Dr[7].ToString();
                                CustObj.PresentPhoneNo = Dr[8].ToString();
                                CustObj.Mobile = Dr[9].ToString();
                                CustObj.EMailId = Dr[10].ToString();
                                if (Dr[11].ToString() == "0")
                                    CustObj.IsPresentPermAddressSame = false;
                                else if (Dr[11].ToString() == "1")
                                    CustObj.IsPresentPermAddressSame = true;
                                CustObj.PermanentAddress = Dr[12].ToString();
                                CustObj.PermanentCity = Dr[13].ToString();
                                CustObj.PermanentPinCode = Dr[14].ToString();
                                CustObj.PermanentPhoneNo = Dr[15].ToString();

                                IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                                IDProofType IDProofType = new IDProofType();
                                if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                    CustObj.IDProofType = IDProofType;
                                else
                                    return retval;

                                CustObj.IDProofValue = Dr[17].ToString();
                                if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                                    CustObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                                if ((Dr[19].ToString() != null) && (Dr[19].ToString() != ""))
                                    CustObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());

                                if (Dr[20] != null)
                                    CustObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());

                                if (Dr[21] != null)
                                    CustObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                                if ((Dr[22].ToString() != null) && (Dr[22].ToString() != ""))
                                    CustObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());

                                if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) 
                                    CustObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());

                                if (Dr[24] != null)
                                {
                                    if (Dr[24].ToString().Length > 0)
                                    {
                                        byte[] bytes = (byte[])Dr[24];
                                        CustObj.Image = (byte[])bytes;
                                    }
                                }

                                CustObjArray[i] = CustObj;
                            }
                        }
                        else
                        {
                            errorMessage = "No Records Found!";
                        }
                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

        public bool GetCustomers(string CustId, string CustName, string Address, int IDProofTypeId, string IDProofValue, DateTime FromDateofBirth, DateTime ToDateofBirth, DateTime FromDateofStartRelationship, DateTime ToDateofStartRelationship, DateTime FromDateofExpiryRelationship, DateTime ToDateofExpiryRelationship, out string errorMessage, out Customer[] CustObjArray)
            {
                DataSet Ds = null;
                DataRow Dr;
                Customer CustObj;

                bool retval = false;

                errorMessage = null;

                //CustObjList = new List<Customer>();

                CustObjArray = null;

                SqlParameter[] SQLParamArray = new SqlParameter[11];

                SqlParameter parameter;

                try
                {
                    parameter = new SqlParameter("@Cust_Id", SqlDbType.VarChar);
                    parameter.Value = (object)CustId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Cust_Name", SqlDbType.VarChar);
                    parameter.Value = (object)CustName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Cust_Address", SqlDbType.VarChar);
                    parameter.Value = Address;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Cust_IDProofTypeId", SqlDbType.Int);
                    if ((IDProofTypeId.ToString().Length == 0) || (IDProofTypeId == -1))
                        parameter.Value = DBNull.Value;
                    else
                        parameter.Value = (object)IDProofTypeId;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Cust_IDProofValue", SqlDbType.VarChar);
                    parameter.Value = (object)IDProofValue ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@Cust_FromDateofBirth", SqlDbType.DateTime);
                    if ((FromDateofBirth > DateTime.MinValue) && (FromDateofBirth <  DateTime.MaxValue))
                        parameter.Value = (object)FromDateofBirth;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@Cust_ToDateofBirth", SqlDbType.DateTime);
                    if ((ToDateofBirth > DateTime.MinValue) && (ToDateofBirth < DateTime.MaxValue))
                        parameter.Value = (object)ToDateofBirth;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[6] = parameter;

                    parameter = new SqlParameter("@Cust_FromDateofStartRelationship", SqlDbType.DateTime);
                    if ((FromDateofStartRelationship > DateTime.MinValue) && (FromDateofStartRelationship < DateTime.MaxValue))
                        parameter.Value = (object)FromDateofStartRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[7] = parameter;

                    parameter = new SqlParameter("@Cust_ToDateofStartRelationship", SqlDbType.DateTime);
                    if ((ToDateofStartRelationship.Year > 1900) && (ToDateofStartRelationship.Year < 2500))
                        parameter.Value = (object)ToDateofStartRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[8] = parameter;

                    parameter = new SqlParameter("@Cust_FromDateofExpiryRelationship", SqlDbType.DateTime);
                    if ((FromDateofExpiryRelationship.Year > 1900) && (FromDateofExpiryRelationship.Year < 2500))
                        parameter.Value = (object)FromDateofExpiryRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[9] = parameter;

                    parameter = new SqlParameter("@Cust_ToDateofExpiryRelationship", SqlDbType.DateTime);
                    if ((ToDateofExpiryRelationship.Year > 1900) && (ToDateofExpiryRelationship.Year < 2500))
                        parameter.Value = (object)ToDateofExpiryRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[10] = parameter;

                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..SearchCustomers", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            CustObjArray = new Customer[Ds.Tables[0].Rows.Count];

                            #region Extract Data
                            for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                            {
                                CustObj = new Customer();

                                Dr = Ds.Tables[0].Rows[i];

                                CustObj.CustomerId = Dr[0].ToString();
                                CustObj.CustomerName = Dr[1].ToString();

                                CustomerStatusFactory CustomerStatusFactory = new CustomerStatusFactory();
                                CustomerStatus CustomerStatus = new CustomerStatus();
                                if (CustomerStatusFactory.GetCustomerStatus(Convert.ToInt32(Dr[2].ToString()), out CustomerStatus, out errorMessage) == true)
                                    CustObj.Status = CustomerStatus;
                                else
                                    return retval;

                                CustObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                                GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                                GenderType GenderType = new GenderType();
                                if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                    CustObj.GenderType = GenderType;
                                else
                                    return retval;

                                CustObj.PresentAddress = Dr[5].ToString();
                                CustObj.PresentCity = Dr[6].ToString();
                                CustObj.PresentPinCode = Dr[7].ToString();
                                CustObj.PresentPhoneNo = Dr[8].ToString();
                                CustObj.Mobile = Dr[9].ToString();
                                CustObj.EMailId = Dr[10].ToString();
                                CustObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                                CustObj.PermanentAddress = Dr[12].ToString();
                                CustObj.PermanentCity = Dr[13].ToString();
                                CustObj.PermanentPinCode = Dr[14].ToString();
                                CustObj.PermanentPhoneNo = Dr[15].ToString();

                                IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                                IDProofType IDProofType = new IDProofType();
                                if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                    CustObj.IDProofType = IDProofType;
                                else
                                    return retval;

                                CustObj.IDProofValue = Dr[17].ToString();
                                if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                                    CustObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                                if ((Dr[19].ToString() != null) && (Dr[19].ToString() != ""))
                                    CustObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());

                                if (Dr[20] != null)
                                    CustObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());

                                if (Dr[21] != null)
                                    CustObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                                if ((Dr[22].ToString() != null) && (Dr[22].ToString() != ""))
                                    CustObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());

                                if ((Dr[23].ToString() != null) && (Dr[23].ToString() != ""))
                                    CustObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());

                                if (Dr[24] != null)
                                {
                                    if (Dr[24].ToString().Length > 0)
                                    {
                                        byte[] bytes = (byte[])Dr[24];
                                        CustObj.Image = (byte[])bytes;
                                    }
                                }


                                //CustObjList.Add(CustObj);

                                CustObjArray[i] = CustObj;
                            }

                        #endregion
                        }
                        else
                        {
                            errorMessage = "No Records Found!";
                        }
                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }


                return retval;
            }

        public bool GetCustomerDetails(string CustIdOrName, out string errorMessage, out Customer CustObj)
            {
                DataSet Ds = null;
                DataRow Dr;


                bool retval = false;

                errorMessage = null;

                CustObj = null;


                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter;

                parameter = new SqlParameter("@Cust_Id", SqlDbType.VarChar);
                parameter.Value = CustIdOrName;
                SQLParamArray[0] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetCustomerDetails", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        if (Ds.Tables[0].Rows.Count == 1)
                        {
                            Dr = Ds.Tables[0].Rows[0];

                            CustObj = new Customer();

                            CustObj.CustomerId = Dr[0].ToString();
                            CustObj.CustomerName = Dr[1].ToString();

                            CustomerStatusFactory CustomerStatusFactory = new CustomerStatusFactory();
                            CustomerStatus CustomerStatus = new CustomerStatus();
                            if (CustomerStatusFactory.GetCustomerStatus(Convert.ToInt32(Dr[2].ToString()), out CustomerStatus, out errorMessage) == true)
                                CustObj.Status = CustomerStatus;
                            else
                                return retval;

                            CustObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                            GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                            GenderType GenderType = new GenderType();
                            if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                CustObj.GenderType = GenderType;
                            else
                                return retval;

                            CustObj.PresentAddress = Dr[5].ToString();
                            CustObj.PresentCity = Dr[6].ToString();
                            CustObj.PresentPinCode = Dr[7].ToString();
                            CustObj.PresentPhoneNo = Dr[8].ToString();
                            CustObj.Mobile = Dr[9].ToString();
                            CustObj.EMailId = Dr[10].ToString();
                            CustObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                            CustObj.PermanentAddress = Dr[12].ToString();
                            CustObj.PermanentCity = Dr[13].ToString();
                            CustObj.PermanentPinCode = Dr[14].ToString();
                            CustObj.PermanentPhoneNo = Dr[15].ToString();

                            IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                            IDProofType IDProofType = new IDProofType();
                            if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                CustObj.IDProofType = IDProofType;
                            else
                                return retval;

                            CustObj.IDProofValue = Dr[17].ToString();
                            if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                                CustObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                            if ((Dr[19].ToString() != null) && (Dr[19].ToString() != ""))
                                CustObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());

                            if (Dr[20] != null)
                                CustObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());

                            if (Dr[21] != null)
                                CustObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                            if ((Dr[22].ToString() != null) && (Dr[22].ToString() != ""))
                                CustObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());

                            if ((Dr[23].ToString() != null) && (Dr[23].ToString() != ""))
                                CustObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());

                            if (Dr[24] != null)
                            {
                                if (Dr[24].ToString().Length > 0)
                                {
                                    byte[] bytes = (byte[])Dr[24];
                                    CustObj.Image = (byte[])bytes;
                                }
                            }

                            SaleFactory SaleFactory = new SaleFactory();
                            List<Sale> SaleObjList = new List<Sale>();
                            Sale[] SaleObjArray = null;

                            if (SaleFactory.GetSaleDetails(null, CustObj.CustomerId, null, DateTime.MinValue, DateTime.MinValue, out SaleObjArray, out errorMessage) == true)
                            {
                                if (SaleObjArray != null)
                                    CustObj.SalesDetails = SaleObjArray.ToList<Sale>();
                            }
                            else
                                return retval;
                        }
                        else if (Ds.Tables[0].Rows.Count <= 0)
                        {
                            errorMessage = "Record Not Found!";
                            return retval;
                        }
                        else if (Ds.Tables[0].Rows.Count > 1)
                        {
                            errorMessage = "More Than One Customer Record Exists For Customer Id.  Please Correct Data!";
                            return retval;
                        }


                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

        public bool GetCustomerBasicDetails(string CustIdOrName, out string errorMessage, out Customer CustObj)
        {
            DataSet Ds = null;
            DataRow Dr;


            bool retval = false;

            errorMessage = null;

            CustObj = null;


            SqlParameter[] SQLParamArray = new SqlParameter[1];

            SqlParameter parameter;

            parameter = new SqlParameter("@Cust_Id", SqlDbType.VarChar);
            parameter.Value = CustIdOrName;
            SQLParamArray[0] = parameter;

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                if (errorMessage != null)
                    return retval;

                if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetCustomerDetails", SQLParamArray) == false)
                {
                    if (errorMessage != null)
                        return retval;
                }
                else
                {
                    if (Ds.Tables[0].Rows.Count == 1)
                    {
                        Dr = Ds.Tables[0].Rows[0];

                        CustObj = new Customer();

                        CustObj.CustomerId = Dr[0].ToString();
                        CustObj.CustomerName = Dr[1].ToString();

                        CustomerStatusFactory CustomerStatusFactory = new CustomerStatusFactory();
                        CustomerStatus CustomerStatus = new CustomerStatus();
                        if (CustomerStatusFactory.GetCustomerStatus(Convert.ToInt32(Dr[2].ToString()), out CustomerStatus, out errorMessage) == true)
                            CustObj.Status = CustomerStatus;
                        else
                            return retval;

                        CustObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                        GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                        GenderType GenderType = new GenderType();
                        if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                            CustObj.GenderType = GenderType;
                        else
                            return retval;

                        CustObj.PresentAddress = Dr[5].ToString();
                        CustObj.PresentCity = Dr[6].ToString();
                        CustObj.PresentPinCode = Dr[7].ToString();
                        CustObj.PresentPhoneNo = Dr[8].ToString();
                        CustObj.Mobile = Dr[9].ToString();
                        CustObj.EMailId = Dr[10].ToString();
                        CustObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                        CustObj.PermanentAddress = Dr[12].ToString();
                        CustObj.PermanentCity = Dr[13].ToString();
                        CustObj.PermanentPinCode = Dr[14].ToString();
                        CustObj.PermanentPhoneNo = Dr[15].ToString();

                        IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                        IDProofType IDProofType = new IDProofType();
                        if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                            CustObj.IDProofType = IDProofType;
                        else
                            return retval;

                        CustObj.IDProofValue = Dr[17].ToString();
                        if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                            CustObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                        if ((Dr[19].ToString() != null) && (Dr[19].ToString() != ""))
                            CustObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());

                        if (Dr[20] != null)
                            CustObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());

                        if (Dr[21] != null)
                            CustObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                        if ((Dr[22].ToString() != null) && (Dr[22].ToString() != ""))
                            CustObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());

                        if ((Dr[23].ToString() != null) && (Dr[23].ToString() != ""))
                            CustObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());

                        if (Dr[24] != null)
                        {
                            if (Dr[24].ToString().Length > 0)
                            {
                                byte[] bytes = (byte[])Dr[24];
                                CustObj.Image = (byte[])bytes;
                            }
                        }
                    }
                    else if (Ds.Tables[0].Rows.Count <= 0)
                    {
                        errorMessage = "Record Not Found!";
                        return retval;
                    }
                    else if (Ds.Tables[0].Rows.Count > 1)
                    {
                        errorMessage = "More Than One Customer Record Exists For Customer Id.  Please Correct Data!";
                        return retval;
                    }


                    retval = true;
                }
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            return retval;
        }

        public bool AddCustomerDetails(Customer CustObj, out string errorMessage)
            {
                bool retval = false;

                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@CustId", SqlDbType.VarChar, 30);
                    parameter.Value = "";
                    parameter.Direction = ParameterDirection.Output;
                    SQLParamArray[0] = parameter;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..GetNextCustId", SQLParamArray) == false)
                    {
                        errorMessage = "Error While Generating Customer Id";
                        return false;
                    }

                    CustObj.CustomerId = SQLParamArray[0].Value.ToString();

                    SQLParamArray = new SqlParameter[23];

                    parameter = new SqlParameter("@Cust_Id", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.CustomerId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Cust_Name", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.CustomerName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Cust_Status", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.Status.CustomerStatusId ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Cust_DateofBirth", SqlDbType.DateTime);
                    parameter.Value = (object)CustObj.DateofBirth ?? DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Cust_Gender", SqlDbType.Int);
                    parameter.Value = (object)CustObj.GenderType.GenderTypeId ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@Cust_PresentAddress", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.PresentAddress ?? DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@Cust_PresentCity", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.PresentCity ?? DBNull.Value;
                    SQLParamArray[6] = parameter;

                    parameter = new SqlParameter("@Cust_PresentPinCode", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.PresentPinCode ?? DBNull.Value;
                    SQLParamArray[7] = parameter;

                    parameter = new SqlParameter("@Cust_PresentPhone", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.PresentPhoneNo ?? DBNull.Value;
                    SQLParamArray[8] = parameter;

                    parameter = new SqlParameter("@Cust_Mobile", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.PresentPinCode ?? DBNull.Value;
                    SQLParamArray[9] = parameter;

                    parameter = new SqlParameter("@Cust_EMailId", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.EMailId ?? DBNull.Value;
                    SQLParamArray[10] = parameter;

                    parameter = new SqlParameter("@Cust_IsPresentPermAddressSame", SqlDbType.Int);
                    parameter.Value = (object)Convert.ToInt32(CustObj.IsPresentPermAddressSame) ?? DBNull.Value;
                    SQLParamArray[11] = parameter;

                    parameter = new SqlParameter("@Cust_PermanentAddress", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.PermanentAddress ?? DBNull.Value;
                    SQLParamArray[12] = parameter;

                    parameter = new SqlParameter("@Cust_PermanentCity", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.PermanentCity ?? DBNull.Value;
                    SQLParamArray[13] = parameter;

                    parameter = new SqlParameter("@Cust_PermanentPinCode", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.PermanentPinCode ?? DBNull.Value;
                    SQLParamArray[14] = parameter;

                    parameter = new SqlParameter("@Cust_PermanentPhone", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.PermanentPhoneNo ?? DBNull.Value;
                    SQLParamArray[15] = parameter;

                    parameter = new SqlParameter("@Cust_IDProofTypeId", SqlDbType.Int);
                    parameter.Value = (object)CustObj.IDProofType.IDProofTypeId ?? DBNull.Value;
                    SQLParamArray[16] = parameter;

                    parameter = new SqlParameter("@Cust_IDProofValue", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.IDProofValue ?? DBNull.Value;
                    SQLParamArray[17] = parameter;

                    parameter = new SqlParameter("@Cust_RelationshipStartDate", SqlDbType.DateTime);
                    parameter.Value = (object)CustObj.RelationshipStartDate ?? DBNull.Value;
                    SQLParamArray[18] = parameter;

                    parameter = new SqlParameter("@Cust_AmtTobePaid", SqlDbType.Decimal);
                    parameter.Value = (object)CustObj.AmountTobePaid ?? DBNull.Value;
                    SQLParamArray[19] = parameter;

                    parameter = new SqlParameter("@Cust_AmtPaidYTD", SqlDbType.Decimal);
                    parameter.Value = (object)CustObj.AmountPaidYTD ?? DBNull.Value;
                    SQLParamArray[20] = parameter;

                    parameter = new SqlParameter("@Cust_CreateDate", SqlDbType.DateTime);
                    parameter.Value = (object)CustObj.CreateDate ?? DBNull.Value;
                    SQLParamArray[21] = parameter;

                    parameter = new SqlParameter("@Cust_Picture", SqlDbType.Image);
                    parameter.Value = (object)CustObj.Image ?? DBNull.Value;
                    SQLParamArray[22] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddCustomerDetails", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                        retval = true;

                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }
                return retval;
            }

        public void ModifyCustomerDetails()
            {
                throw new System.NotSupportedException ();
            }

        public bool DeleteCustomerDetails(out string errorMessage, Customer CustObj)
            {

                bool retval = false;

                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@Cust_Id", SqlDbType.VarChar);
                    parameter.Value = (object)CustObj.CustomerId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..DeleteCustomerDetails", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                        retval = true;
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

        #endregion
    }
}



