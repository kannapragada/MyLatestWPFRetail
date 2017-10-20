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
    public class VendorFactory : Vendor, IVendorFactory
    {
        //Declarations
        # region Declarations


            SQLDataAccess DBObj;

        #endregion

        //Constructor
        public VendorFactory()
        {
        }


        //Member Methods  
        #region MemberMethods

        //    private void GetVendorFromFile()
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
        //                        this._vendorGender = line;
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

        //    public void GetVendorObject()
        //{
        //    FilePickerThread = new Thread(new ThreadStart(GetVendorFromFile));
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

            public bool GetVendors(string Vendor, out string errorMessage, out Vendor[] VendorObjArray)
            {
                DataSet Ds = null;
                DataRow Dr;
                Vendor VendorObj;

                bool retval = false;

                errorMessage = null;

                List<Vendor> VendorObjList = new List<Vendor>();
                VendorObjArray = null;

                SqlParameter[] SQLParamArray = new SqlParameter[2];

                SqlParameter parameter;

                parameter = new SqlParameter("@Vendor_Id", SqlDbType.VarChar);
                parameter.Value = Vendor;
                SQLParamArray[0] = parameter;

                parameter = new SqlParameter("@Vendor_Name", SqlDbType.VarChar);
                parameter.Value = Vendor;
                SQLParamArray[1] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetVendors", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                            {
                                VendorObj = new Vendor();

                                Dr = Ds.Tables[0].Rows[i];

                                VendorObj.VendorId = Dr[0].ToString();
                                VendorObj.VendorName = Dr[1].ToString();

                                VendorStatusFactory VendorStatusFactory = new VendorStatusFactory();
                                VendorStatus VendorStatus = new VendorStatus();
                                if (VendorStatusFactory.GetVendorStatus(Convert.ToInt32(Dr[2].ToString()), out VendorStatus, out errorMessage) == true)
                                    VendorObj.Status = VendorStatus;
                                else
                                    return retval;

                                if ((Dr[3].ToString() != null) && (Dr[3].ToString() != ""))
                                    VendorObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                                GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                                GenderType GenderType = new GenderType();
                                if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                    VendorObj.GenderType = GenderType;
                                else
                                    return retval;

                                VendorObj.PresentAddress = Dr[5].ToString();
                                VendorObj.PresentCity = Dr[6].ToString();
                                VendorObj.PresentPinCode = Dr[7].ToString();
                                VendorObj.PresentPhoneNo = Dr[8].ToString();
                                VendorObj.Mobile = Dr[9].ToString();
                                VendorObj.EMailId = Dr[10].ToString();
                                VendorObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                                VendorObj.PermanentAddress = Dr[12].ToString();
                                VendorObj.PermanentCity = Dr[13].ToString();
                                VendorObj.PermanentPinCode = Dr[14].ToString();
                                VendorObj.PermanentPhoneNo = Dr[15].ToString();

                                IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                                IDProofType IDProofType = new IDProofType();
                                if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                    VendorObj.IDProofType = IDProofType;
                                else
                                    return retval;

                                VendorObj.IDProofValue = Dr[17].ToString();
                                VendorObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                                if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) VendorObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());

                                if (Dr[20] != null)
                                    VendorObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());

                                if (Dr[21] != null)
                                    VendorObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                                VendorObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());

                                if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) VendorObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());

                                if (Dr[24] != null)
                                {
                                    if (Dr[24].ToString().Length > 0)
                                    {
                                        byte[] bytes = (byte[])Dr[24];
                                        VendorObj.Image = (byte[])bytes;
                                    }
                                }

                                VendorObjList.Add(VendorObj);
                            }

                            VendorObjArray = VendorObjList.ToArray<Vendor>();
                            retval = true;
                        }
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

            public bool GetVendors(string VendorId, string VendorName, string Address, string IDProofTypeId, string IDProofValue, DateTime FromDateofBirth, DateTime ToDateofBirth, DateTime FromDateofStartRelationship, DateTime ToDateofStartRelationship, DateTime FromDateofExpiryRelationship, DateTime ToDateofExpiryRelationship,out string errorMessage, out Vendor[] VendorObjArray)
            {
                DataSet Ds = null;
                DataRow Dr;
                Vendor VendorObj;

                bool retval = false;

                errorMessage = null;

                List<Vendor> VendorObjList = new List<Vendor>();
                VendorObjArray = null;

                SqlParameter[] SQLParamArray = new SqlParameter[11];

                SqlParameter parameter;

                try
                {
                    parameter = new SqlParameter("@Vendor_Id", SqlDbType.VarChar);
                    parameter.Value = (object)VendorId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Vendor_Name", SqlDbType.VarChar);
                    parameter.Value = (object)VendorName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Vendor_Address", SqlDbType.VarChar);
                    parameter.Value = Address;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Vendor_IDProofTypeId", SqlDbType.Int);
                    if (IDProofTypeId.ToString().Length == 0)
                        parameter.Value = DBNull.Value;
                    else
                        parameter.Value = (object)IDProofTypeId;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Vendor_IDProofValue", SqlDbType.VarChar);
                    parameter.Value = (object)IDProofValue ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@Vendor_FromDateofBirth", SqlDbType.DateTime);
                    if ((FromDateofBirth.Year > 1900) && (FromDateofBirth.Year < 2500))
                        parameter.Value = (object)FromDateofBirth;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@Vendor_ToDateofBirth", SqlDbType.DateTime);
                    if ((ToDateofBirth.Year > 1900) && (ToDateofBirth.Year < 2500))
                        parameter.Value = (object)ToDateofBirth;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[6] = parameter;

                    parameter = new SqlParameter("@Vendor_FromDateofStartRelationship", SqlDbType.DateTime);
                    if ((FromDateofStartRelationship.Year > 1900) && (FromDateofStartRelationship.Year < 2500))
                        parameter.Value = (object)FromDateofStartRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[7] = parameter;

                    parameter = new SqlParameter("@Vendor_ToDateofStartRelationship", SqlDbType.DateTime);
                    if ((ToDateofStartRelationship.Year > 1900) && (ToDateofStartRelationship.Year < 2500))
                        parameter.Value = (object)ToDateofStartRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[8] = parameter;

                    parameter = new SqlParameter("@Vendor_FromDateofExpiryRelationship", SqlDbType.DateTime);
                    if ((FromDateofExpiryRelationship.Year > 1900) && (FromDateofExpiryRelationship.Year < 2500))
                        parameter.Value = (object)FromDateofExpiryRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[9] = parameter;

                    parameter = new SqlParameter("@Vendor_ToDateofExpiryRelationship", SqlDbType.DateTime);
                    if ((ToDateofExpiryRelationship.Year > 1900) && (ToDateofExpiryRelationship.Year < 2500))
                        parameter.Value = (object)ToDateofExpiryRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[10] = parameter;

                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..SearchVendors", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        if (Ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                            {
                                VendorObj = new Vendor();

                                Dr = Ds.Tables[0].Rows[i];

                                VendorObj.VendorId = Dr[0].ToString();
                                VendorObj.VendorName = Dr[1].ToString();

                                VendorStatusFactory VendorStatusFactory = new VendorStatusFactory();
                                VendorStatus VendorStatus = new VendorStatus();
                                if (VendorStatusFactory.GetVendorStatus(Convert.ToInt32(Dr[2].ToString()), out VendorStatus, out errorMessage) == true)
                                    VendorObj.Status = VendorStatus;
                                else
                                    return retval;

                                VendorObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                                GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                                GenderType GenderType = new GenderType();
                                if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                    VendorObj.GenderType = GenderType;
                                else
                                    return retval;

                                VendorObj.PresentAddress = Dr[5].ToString();
                                VendorObj.PresentCity = Dr[6].ToString();
                                VendorObj.PresentPinCode = Dr[7].ToString();
                                VendorObj.PresentPhoneNo = Dr[8].ToString();
                                VendorObj.Mobile = Dr[9].ToString();
                                VendorObj.EMailId = Dr[10].ToString();
                                VendorObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                                VendorObj.PermanentAddress = Dr[12].ToString();
                                VendorObj.PermanentCity = Dr[13].ToString();
                                VendorObj.PermanentPinCode = Dr[14].ToString();
                                VendorObj.PermanentPhoneNo = Dr[15].ToString();

                                IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                                IDProofType IDProofType = new IDProofType();
                                if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                    VendorObj.IDProofType = IDProofType;
                                else
                                    return retval;

                                VendorObj.IDProofValue = Dr[17].ToString();
                                VendorObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                                if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) VendorObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());

                                if (Dr[20] != null)
                                    VendorObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());

                                if (Dr[21] != null)
                                    VendorObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                                VendorObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());

                                if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) VendorObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());

                                if (Dr[24] != null)
                                {
                                    if (Dr[24].ToString().Length > 0)
                                    {
                                        byte[] bytes = (byte[])Dr[24];
                                        VendorObj.Image = (byte[])bytes;
                                    }
                                }


                                VendorObjList.Add(VendorObj);
                            }
                            VendorObjArray = VendorObjList.ToArray<Vendor>();
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

            public bool GetVendorDetails(string Vendor, out string errorMessage, out Vendor VendorObj)
            {
                DataSet Ds = null;
                DataRow Dr;


                bool retval = false;

                errorMessage = null;

                VendorObj = null;

                #region Extract Data1


                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter;

                parameter = new SqlParameter("@Vendor_Id", SqlDbType.VarChar);
                parameter.Value = Vendor;
                SQLParamArray[0] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetVendorDetails", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        #region Extract Data
                        if (Ds.Tables[0].Rows.Count == 1)
                        {
                            Dr = Ds.Tables[0].Rows[0];

                            VendorObj = new Vendor();

                            VendorObj.VendorId = Dr[0].ToString();
                            VendorObj.VendorName = Dr[1].ToString();

                            VendorStatusFactory VendorStatusFactory = new VendorStatusFactory();
                            VendorStatus VendorStatus = new VendorStatus();
                            if (VendorStatusFactory.GetVendorStatus(Convert.ToInt32(Dr[2].ToString()), out VendorStatus, out errorMessage) == true)
                                VendorObj.Status = VendorStatus;
                            else
                                return retval;

                            VendorObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                            GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                            GenderType GenderType = new GenderType();
                            if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                VendorObj.GenderType = GenderType;
                            else
                                return retval;

                            VendorObj.PresentAddress = Dr[5].ToString();
                            VendorObj.PresentCity = Dr[6].ToString();
                            VendorObj.PresentPinCode = Dr[7].ToString();
                            VendorObj.PresentPhoneNo = Dr[8].ToString();
                            VendorObj.Mobile = Dr[9].ToString();
                            VendorObj.EMailId = Dr[10].ToString();
                            VendorObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                            VendorObj.PermanentAddress = Dr[12].ToString();
                            VendorObj.PermanentCity = Dr[13].ToString();
                            VendorObj.PermanentPinCode = Dr[14].ToString();
                            VendorObj.PermanentPhoneNo = Dr[15].ToString();

                            IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                            IDProofType IDProofType = new IDProofType();
                            if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                VendorObj.IDProofType = IDProofType;
                            else
                                return retval;

                            VendorObj.IDProofValue = Dr[17].ToString();
                            VendorObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                            if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) VendorObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());

                            if (Dr[20] != null)
                                VendorObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());

                            if (Dr[21] != null)
                                VendorObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                            VendorObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());

                            if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) VendorObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());

                            if (Dr[24] != null)
                            {
                                if (Dr[24].ToString().Length > 0)
                                {
                                    byte[] bytes = (byte[])Dr[24];
                                    VendorObj.Image = (byte[])bytes;
                                }
                            }

                            SaleFactory SaleFactory = new SaleFactory();
                            List<Sale> SaleObjList = new List<Sale>();
                            Sale[] SaleObjArray = null;

                            DateTime DateTime = DateTime.MinValue;

                            if (SaleFactory.GetVendorSaleDetails(null, VendorObj.VendorId, null, DateTime, DateTime, out SaleObjArray, out errorMessage) == true)
                            {
                                if (SaleObjArray != null)
                                    VendorObj.SalesDetails = SaleObjArray.ToList<Sale>();;
                            }
                            if (errorMessage != null)
                                return retval;

                            retval = true;
                        }
                        else if (Ds.Tables[0].Rows.Count <= 0)
                                errorMessage = "Vendor Record Not Found!";
                        else if (Ds.Tables[0].Rows.Count > 1)
                                errorMessage = "More Than One Vendor Record Exists For Vendor Id.  Please Correct Data!";
                        #endregion
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;

                #endregion
            }

            public bool GetVendorBasicDetails(string Vendor, out string errorMessage, out Vendor VendorObj)
            {
                DataSet Ds = null;
                DataRow Dr;


                bool retval = false;

                errorMessage = null;

                VendorObj = null;

                #region Extract Data1


                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter;

                parameter = new SqlParameter("@Vendor_Id", SqlDbType.VarChar);
                parameter.Value = Vendor;
                SQLParamArray[0] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetVendorDetails", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        #region Extract Data
                        if (Ds.Tables[0].Rows.Count == 1)
                        {
                            Dr = Ds.Tables[0].Rows[0];

                            VendorObj = new Vendor();

                            VendorObj.VendorId = Dr[0].ToString();
                            VendorObj.VendorName = Dr[1].ToString();

                            VendorStatusFactory VendorStatusFactory = new VendorStatusFactory();
                            VendorStatus VendorStatus = new VendorStatus();
                            if (VendorStatusFactory.GetVendorStatus(Convert.ToInt32(Dr[2].ToString()), out VendorStatus, out errorMessage) == true)
                                VendorObj.Status = VendorStatus;
                            else
                                return retval;

                            VendorObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                            GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                            GenderType GenderType = new GenderType();
                            if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                VendorObj.GenderType = GenderType;
                            else
                                return retval;

                            VendorObj.PresentAddress = Dr[5].ToString();
                            VendorObj.PresentCity = Dr[6].ToString();
                            VendorObj.PresentPinCode = Dr[7].ToString();
                            VendorObj.PresentPhoneNo = Dr[8].ToString();
                            VendorObj.Mobile = Dr[9].ToString();
                            VendorObj.EMailId = Dr[10].ToString();
                            VendorObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                            VendorObj.PermanentAddress = Dr[12].ToString();
                            VendorObj.PermanentCity = Dr[13].ToString();
                            VendorObj.PermanentPinCode = Dr[14].ToString();
                            VendorObj.PermanentPhoneNo = Dr[15].ToString();

                            IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                            IDProofType IDProofType = new IDProofType();
                            if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                VendorObj.IDProofType = IDProofType;
                            else
                                return retval;

                            VendorObj.IDProofValue = Dr[17].ToString();
                            VendorObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                            if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) VendorObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());

                            if (Dr[20] != null)
                                VendorObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());

                            if (Dr[21] != null)
                                VendorObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                            VendorObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());

                            if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) VendorObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());

                            if (Dr[24] != null)
                            {
                                if (Dr[24].ToString().Length > 0)
                                {
                                    byte[] bytes = (byte[])Dr[24];
                                    VendorObj.Image = (byte[])bytes;
                                }
                            }

                            retval = true;
                        }
                        else if (Ds.Tables[0].Rows.Count <= 0)
                            errorMessage = "Vendor Record Not Found!";
                        else if (Ds.Tables[0].Rows.Count > 1)
                            errorMessage = "More Than One Vendor Record Exists For Vendor Id.  Please Correct Data!";
                        #endregion
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;

                #endregion
            }

            public bool AddVendorDetails(out string errorMessage, Vendor VendorObj)
            {
                bool retval = false;

                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@VendorId", SqlDbType.VarChar, 30);
                    parameter.Value = "";
                    parameter.Direction = ParameterDirection.Output;
                    SQLParamArray[0] = parameter;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..GetNextVendorId", SQLParamArray) == false)
                    {
                        errorMessage = "Error While Generating Vendor Id";
                        return false;
                    }

                    VendorObj.VendorId = SQLParamArray[0].Value.ToString();

                    SQLParamArray = new SqlParameter[23];

                    parameter = new SqlParameter("@Vendor_Id", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.VendorId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Vendor_Name", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.VendorName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Vendor_Status", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.Status.VendorStatusId ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Vendor_DateofBirth", SqlDbType.DateTime);
                    parameter.Value = (object)VendorObj.DateofBirth ?? DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Vendor_Gender", SqlDbType.Int);
                    parameter.Value = (object)VendorObj.GenderType.GenderTypeId ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@Vendor_PresentAddress", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.PresentAddress ?? DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@Vendor_PresentCity", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.PresentCity ?? DBNull.Value;
                    SQLParamArray[6] = parameter;

                    parameter = new SqlParameter("@Vendor_PresentPinCode", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.PresentPinCode ?? DBNull.Value;
                    SQLParamArray[7] = parameter;

                    parameter = new SqlParameter("@Vendor_PresentPhone", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.PresentPhoneNo ?? DBNull.Value;
                    SQLParamArray[8] = parameter;

                    parameter = new SqlParameter("@Vendor_Mobile", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.PresentPinCode ?? DBNull.Value;
                    SQLParamArray[9] = parameter;

                    parameter = new SqlParameter("@Vendor_EMailId", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.EMailId ?? DBNull.Value;
                    SQLParamArray[10] = parameter;

                    parameter = new SqlParameter("@Vendor_IsPresentPermAddressSame", SqlDbType.Int);
                    parameter.Value = (object)Convert.ToInt32(VendorObj.IsPresentPermAddressSame) ?? DBNull.Value;
                    SQLParamArray[11] = parameter;

                    parameter = new SqlParameter("@Vendor_PermanentAddress", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.PermanentAddress ?? DBNull.Value;
                    SQLParamArray[12] = parameter;

                    parameter = new SqlParameter("@Vendor_PermanentCity", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.PermanentCity ?? DBNull.Value;
                    SQLParamArray[13] = parameter;

                    parameter = new SqlParameter("@Vendor_PermanentPinCode", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.PermanentPinCode ?? DBNull.Value;
                    SQLParamArray[14] = parameter;

                    parameter = new SqlParameter("@Vendor_PermanentPhone", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.PermanentPhoneNo ?? DBNull.Value;
                    SQLParamArray[15] = parameter;

                    parameter = new SqlParameter("@Vendor_IDProofTypeId", SqlDbType.Int);
                    parameter.Value = (object)VendorObj.IDProofType.IDProofTypeId ?? DBNull.Value;
                    SQLParamArray[16] = parameter;

                    parameter = new SqlParameter("@Vendor_IDProofValue", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.IDProofValue ?? DBNull.Value;
                    SQLParamArray[17] = parameter;

                    parameter = new SqlParameter("@Vendor_RelationshipStartDate", SqlDbType.DateTime);
                    parameter.Value = (object)VendorObj.RelationshipStartDate ?? DBNull.Value;
                    SQLParamArray[18] = parameter;

                    parameter = new SqlParameter("@Vendor_AmtTobePaid", SqlDbType.Decimal);
                    parameter.Value = (object)VendorObj.AmountTobePaid ?? DBNull.Value;
                    SQLParamArray[19] = parameter;

                    parameter = new SqlParameter("@Vendor_AmtPaidYTD", SqlDbType.Decimal);
                    parameter.Value = (object)VendorObj.AmountPaidYTD ?? DBNull.Value;
                    SQLParamArray[20] = parameter;

                    parameter = new SqlParameter("@Vendor_CreateDate", SqlDbType.DateTime);
                    parameter.Value = (object)VendorObj.CreateDate ?? DBNull.Value;
                    SQLParamArray[21] = parameter;

                    parameter = new SqlParameter("@Vendor_Picture", SqlDbType.Image);
                    parameter.Value = (object)VendorObj.Image ?? DBNull.Value;
                    SQLParamArray[22] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddVendorDetails", SQLParamArray) == false)
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

            public void ModifyVendorDetails()
            {
                throw new System.NotSupportedException ();
            }

            public bool DeleteVendorDetails(out string errorMessage, Vendor VendorObj)
            {

                bool retval = false;

                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@Vendor_Id", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.VendorId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..DeleteVendorDetails", SQLParamArray) == false)
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

            public bool AddVendorPhoto(int Vendorid, out string errorMessage, Vendor VendorObj)
            {
                bool retval = false;

                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[3];

                    SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int);
                    parameter.Value = Vendorid;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Name", SqlDbType.VarChar);
                    parameter.Value = (object)VendorObj.VendorName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Picture", SqlDbType.Image);
                    parameter.Value = (object)VendorObj.Image ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddVendorImg", SQLParamArray) == false)
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

            public bool GetVendorPhoto(int Vendorid, out byte[] imgBytes, out string errorMessage)
            {
                bool retval = false;

                errorMessage = null;

                DataSet Ds = new DataSet();

                imgBytes = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    int a11 = Vendorid;

                    SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int);
                    parameter.Value = a11;
                    SQLParamArray[0] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetVendorImg", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }


                    DataRow dr;

                    dr = Ds.Tables[0].Rows[0];

                    imgBytes = (byte[])dr[2];

                    if (Ds != null)
                        Ds.Dispose();

                    retval = true;
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                    if (Ds != null)
                        Ds.Dispose();
                }

                return retval;
            }

            public bool GetVendorSaleDetails(string VendorId, out string errorMessage, out Sale[] SaleObjArray)
            {
                bool retval = false;
                errorMessage = null;

                SaleFactory SaleFactory = new SaleFactory();
                SaleObjArray = null;

                try
                {
                    if (SaleFactory.GetVendorSaleDetails(null, VendorId, null, DateTime.MinValue, DateTime.MinValue, out SaleObjArray, out errorMessage) == true)
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



