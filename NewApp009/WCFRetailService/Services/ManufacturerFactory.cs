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
    public class ManufacturerFactory : Manufacturer, IManufacturerFactory
    {
        //Declarations
        # region Declarations



            SQLDataAccess DBObj;

        #endregion

        //Constructor
        public ManufacturerFactory()
        {
        }

        //Member Methods  
        #region MemberMethods

        //    private void GetManufacturerFromFile()
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
        //                        this._manufGender = line;
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

        //    public void GetManufacturerObject()
        //{
        //    FilePickerThread = new Thread(new ThreadStart(GetManufacturerFromFile));
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

            public bool GetManufacturers(string ManufIdOrName, out string errorMessage, out Manufacturer[] ManufObjArray)
            {
                DataSet Ds = null;
                DataRow Dr;
                Manufacturer ManufObj;

                bool retval = false;

                errorMessage = null;

                List<Manufacturer> ManufObjList = new List<Manufacturer>();
                ManufObjArray = null;

                SqlParameter[] SQLParamArray = new SqlParameter[2];

                SqlParameter parameter;

                parameter = new SqlParameter("@Manuf_Id", SqlDbType.VarChar);
                parameter.Value = ManufIdOrName;
                SQLParamArray[0] = parameter;

                parameter = new SqlParameter("@Manuf_Name", SqlDbType.VarChar);
                parameter.Value = ManufIdOrName;
                SQLParamArray[1] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetManufacturers", SQLParamArray) == false)
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
                                ManufObj = new Manufacturer();

                                Dr = Ds.Tables[0].Rows[i];

                                ManufObj.ManufacturerId = Dr[0].ToString();
                                ManufObj.ManufacturerName = Dr[1].ToString();

                                ManufacturerStatusFactory ManufStatusFactory = new ManufacturerStatusFactory();
                                ManufacturerStatus ManufStatus = new ManufacturerStatus();
                                if (ManufStatusFactory.GetManufacturerStatus(Convert.ToInt32(Dr[2].ToString()), out ManufStatus, out errorMessage) == true)
                                    ManufObj.Status = ManufStatus;
                                else
                                    return retval;

                                if ((Dr[3].ToString() != null) && (Dr[3].ToString() != ""))
                                    ManufObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                                GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                                GenderType GenderType = new GenderType();
                                if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                    ManufObj.GenderType = GenderType;
                                else
                                    return retval;

                                ManufObj.PresentAddress = Dr[5].ToString();
                                ManufObj.PresentCity = Dr[6].ToString();
                                ManufObj.PresentPinCode = Dr[7].ToString();
                                ManufObj.PresentPhoneNo = Dr[8].ToString();
                                ManufObj.Mobile = Dr[9].ToString();
                                ManufObj.EMailId = Dr[10].ToString();
                                ManufObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                                ManufObj.PermanentAddress = Dr[12].ToString();
                                ManufObj.PermanentCity = Dr[13].ToString();
                                ManufObj.PermanentPinCode = Dr[14].ToString();
                                ManufObj.PermanentPhoneNo = Dr[15].ToString();

                                IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                                IDProofType IDProofType = new IDProofType();
                                if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                    ManufObj.IDProofType = IDProofType;
                                else
                                    return retval;

                                ManufObj.IDProofValue = Dr[17].ToString();
                                if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                                    ManufObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                                if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) ManufObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());
                                if (Dr[20] != null)
                                    ManufObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());
                                if (Dr[21] != null)
                                    ManufObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());
                                if ((Dr[22].ToString() != null) && (Dr[22].ToString() != ""))
                                    ManufObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());
                                if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) ManufObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());
                                if (Dr[24] != null)
                                {
                                    if (Dr[24].ToString().Length > 0)
                                    {
                                        byte[] bytes = (byte[])Dr[24];
                                        ManufObj.Image = (byte[])bytes;
                                    }
                                }

                                ManufObjList.Add(ManufObj);
                            }
                            ManufObjArray = ManufObjList.ToArray<Manufacturer>();
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

            public bool GetManufacturers(string ManufId, string ManufName, string Address, string IDProofTypeId, string IDProofValue, DateTime FromDateofBirth, DateTime ToDateofBirth, DateTime FromDateofStartRelationship, DateTime ToDateofStartRelationship, DateTime FromDateofExpiryRelationship, DateTime ToDateofExpiryRelationship,out string errorMessage, out Manufacturer[] ManufObjArray)
            {
                DataSet Ds = null;
                DataRow Dr;
                Manufacturer ManufObj;

                bool retval = false;

                errorMessage = null;

                List<Manufacturer> ManufObjList = new List<Manufacturer>();
                ManufObjArray = null;

                SqlParameter[] SQLParamArray = new SqlParameter[11];

                SqlParameter parameter;

                try
                {
                    parameter = new SqlParameter("@Manuf_Id", SqlDbType.VarChar);
                    parameter.Value = (object)ManufId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Manuf_Name", SqlDbType.VarChar);
                    parameter.Value = (object)ManufName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Manuf_Address", SqlDbType.VarChar);
                    parameter.Value = Address;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Manuf_IDProofTypeId", SqlDbType.Int);
                    if (IDProofTypeId.ToString().Length == 0)
                        parameter.Value = DBNull.Value;
                    else
                        parameter.Value = (object)IDProofTypeId;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Manuf_IDProofValue", SqlDbType.VarChar);
                    parameter.Value = (object)IDProofValue ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@Manuf_FromDateofBirth", SqlDbType.DateTime);
                    if ((FromDateofBirth.Year > 1900) && (FromDateofBirth.Year < 2500))
                        parameter.Value = (object)FromDateofBirth;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@Manuf_ToDateofBirth", SqlDbType.DateTime);
                    if ((ToDateofBirth.Year > 1900) && (ToDateofBirth.Year < 2500))
                        parameter.Value = (object)ToDateofBirth;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[6] = parameter;

                    parameter = new SqlParameter("@Manuf_FromDateofStartRelationship", SqlDbType.DateTime);
                    if ((FromDateofStartRelationship.Year > 1900) && (FromDateofStartRelationship.Year < 2500))
                        parameter.Value = (object)FromDateofStartRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[7] = parameter;

                    parameter = new SqlParameter("@Manuf_ToDateofStartRelationship", SqlDbType.DateTime);
                    if ((ToDateofStartRelationship.Year > 1900) && (ToDateofStartRelationship.Year < 2500))
                        parameter.Value = (object)ToDateofStartRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[8] = parameter;

                    parameter = new SqlParameter("@Manuf_FromDateofExpiryRelationship", SqlDbType.DateTime);
                    if ((FromDateofExpiryRelationship.Year > 1900) && (FromDateofExpiryRelationship.Year < 2500))
                        parameter.Value = (object)FromDateofExpiryRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[9] = parameter;

                    parameter = new SqlParameter("@Manuf_ToDateofExpiryRelationship", SqlDbType.DateTime);
                    if ((ToDateofExpiryRelationship.Year > 1900) && (ToDateofExpiryRelationship.Year < 2500))
                        parameter.Value = (object)ToDateofExpiryRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[10] = parameter;

                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..SearchManufacturers", SQLParamArray) == false)
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
                                ManufObj = new Manufacturer();

                                Dr = Ds.Tables[0].Rows[i];

                                ManufObj.ManufacturerId = Dr[0].ToString();
                                ManufObj.ManufacturerName = Dr[1].ToString();

                                ManufacturerStatusFactory ManufStatusFactory = new ManufacturerStatusFactory();
                                ManufacturerStatus ManufStatus = new ManufacturerStatus();
                                if (ManufStatusFactory.GetManufacturerStatus(Convert.ToInt32(Dr[2].ToString()), out ManufStatus, out errorMessage) == true)
                                    ManufObj.Status = ManufStatus;
                                else
                                    return retval;

                                if ((Dr[3].ToString() != null) && (Dr[3].ToString() != ""))
                                    ManufObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                                GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                                GenderType GenderType = new GenderType();
                                if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                    ManufObj.GenderType = GenderType;
                                else
                                    return retval;

                                ManufObj.PresentAddress = Dr[5].ToString();
                                ManufObj.PresentCity = Dr[6].ToString();
                                ManufObj.PresentPinCode = Dr[7].ToString();
                                ManufObj.PresentPhoneNo = Dr[8].ToString();
                                ManufObj.Mobile = Dr[9].ToString();
                                ManufObj.EMailId = Dr[10].ToString();
                                ManufObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                                ManufObj.PermanentAddress = Dr[12].ToString();
                                ManufObj.PermanentCity = Dr[13].ToString();
                                ManufObj.PermanentPinCode = Dr[14].ToString();
                                ManufObj.PermanentPhoneNo = Dr[15].ToString();

                                IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                                IDProofType IDProofType = new IDProofType();
                                if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                    ManufObj.IDProofType = IDProofType;
                                else
                                    return retval;

                                ManufObj.IDProofValue = Dr[17].ToString();
                                if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                                    ManufObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                                if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) ManufObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());
                                if (Dr[20] != null)
                                    ManufObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());
                                if (Dr[21] != null)
                                    ManufObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());
                                if ((Dr[22].ToString() != null) && (Dr[22].ToString() != ""))
                                    ManufObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());
                                if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) ManufObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());
                                if (Dr[24] != null)
                                {
                                    if (Dr[24].ToString().Length > 0)
                                    {
                                        byte[] bytes = (byte[])Dr[24];
                                        ManufObj.Image = (byte[])bytes;
                                    }
                                }


                                ManufObjList.Add(ManufObj);
                            }

                            ManufObjArray = ManufObjList.ToArray<Manufacturer>();
                        }
                        else
                            errorMessage = "No Records Found!";

                        retval = true;
                    }
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

            public bool GetManufacturerDetails(string Manufacturer, out string errorMessage, out Manufacturer ManufObj)
            {
                DataSet Ds = null;
                DataRow Dr;


                bool retval = false;

                errorMessage = null;

                ManufObj = null;


                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter;

                parameter = new SqlParameter("@Manuf_Id", SqlDbType.VarChar);
                parameter.Value = Manufacturer;
                SQLParamArray[0] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetManufacturerDetails", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        if (Ds.Tables[0].Rows.Count == 1)
                        {
                            Dr = Ds.Tables[0].Rows[0];

                            ManufObj = new Manufacturer();

                            ManufObj.ManufacturerId = Dr[0].ToString();
                            ManufObj.ManufacturerName = Dr[1].ToString();

                            ManufacturerStatusFactory ManufStatusFactory = new ManufacturerStatusFactory();
                            ManufacturerStatus ManufStatus = new ManufacturerStatus();
                            if (ManufStatusFactory.GetManufacturerStatus(Convert.ToInt32(Dr[2].ToString()), out ManufStatus, out errorMessage) == true)
                                ManufObj.Status = ManufStatus;
                            else
                                return retval;

                            if ((Dr[3].ToString() != null) && (Dr[3].ToString() != ""))
                                ManufObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                            GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                            GenderType GenderType = new GenderType();
                            if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                ManufObj.GenderType = GenderType;
                            else
                                return retval;

                            ManufObj.PresentAddress = Dr[5].ToString();
                            ManufObj.PresentCity = Dr[6].ToString();
                            ManufObj.PresentPinCode = Dr[7].ToString();
                            ManufObj.PresentPhoneNo = Dr[8].ToString();
                            ManufObj.Mobile = Dr[9].ToString();
                            ManufObj.EMailId = Dr[10].ToString();
                            ManufObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                            ManufObj.PermanentAddress = Dr[12].ToString();
                            ManufObj.PermanentCity = Dr[13].ToString();
                            ManufObj.PermanentPinCode = Dr[14].ToString();
                            ManufObj.PermanentPhoneNo = Dr[15].ToString();

                            IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                            IDProofType IDProofType = new IDProofType();
                            if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                ManufObj.IDProofType = IDProofType;
                            else
                                return retval;

                            ManufObj.IDProofValue = Dr[17].ToString();
                            if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                                ManufObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                            if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) ManufObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());
                            if (Dr[20] != null)
                                ManufObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());
                            if (Dr[21] != null)
                                ManufObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());
                            if ((Dr[22].ToString() != null) && (Dr[22].ToString() != ""))
                                ManufObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());
                            if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) ManufObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());
                            if (Dr[24] != null)
                            {
                                if (Dr[24].ToString().Length > 0)
                                {
                                    byte[] bytes = (byte[])Dr[24];
                                    ManufObj.Image = (byte[])bytes;
                                }
                            }

                            SaleFactory SaleFactory = new SaleFactory();
                            List<Sale> SaleObjList = new List<Sale>();
                            Sale[] SaleObjArray = null;

                            DateTime DateTime = DateTime.MinValue;

                            if (SaleFactory.GetManufSaleDetails(null, ManufObj.ManufacturerId, null, DateTime, DateTime, out SaleObjArray, out errorMessage) == true)
                            {
                                if (SaleObjArray != null)
                                    ManufObj.SalesDetails = SaleObjArray.ToList<Sale>();
                            }
                        }
                        else if (Ds.Tables[0].Rows.Count <= 0)
                        {
                            errorMessage = "Manufacturer Record Not Found!";
                            return retval;
                        }
                        else if (Ds.Tables[0].Rows.Count > 1)
                        {
                            errorMessage = "More Than One Manufacturer Record Exists For Manufacturer Id.  Please Correct Data!";
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

            public bool GetManufacturerBasicDetails(string Manufacturer, out string errorMessage, out Manufacturer ManufObj)
            {
                DataSet Ds = null;
                DataRow Dr;


                bool retval = false;

                errorMessage = null;

                ManufObj = null;


                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter;

                parameter = new SqlParameter("@Manuf_Id", SqlDbType.VarChar);
                parameter.Value = Manufacturer;
                SQLParamArray[0] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetManufacturerDetails", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        if (Ds.Tables[0].Rows.Count == 1)
                        {
                            Dr = Ds.Tables[0].Rows[0];

                            ManufObj = new Manufacturer();

                            ManufObj.ManufacturerId = Dr[0].ToString();
                            ManufObj.ManufacturerName = Dr[1].ToString();

                            ManufacturerStatusFactory ManufStatusFactory = new ManufacturerStatusFactory();
                            ManufacturerStatus ManufStatus = new ManufacturerStatus();
                            if (ManufStatusFactory.GetManufacturerStatus(Convert.ToInt32(Dr[2].ToString()), out ManufStatus, out errorMessage) == true)
                                ManufObj.Status = ManufStatus;
                            else
                                return retval;

                            if ((Dr[3].ToString() != null) && (Dr[3].ToString() != ""))
                                ManufObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                            GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                            GenderType GenderType = new GenderType();
                            if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                ManufObj.GenderType = GenderType;
                            else
                                return retval;

                            ManufObj.PresentAddress = Dr[5].ToString();
                            ManufObj.PresentCity = Dr[6].ToString();
                            ManufObj.PresentPinCode = Dr[7].ToString();
                            ManufObj.PresentPhoneNo = Dr[8].ToString();
                            ManufObj.Mobile = Dr[9].ToString();
                            ManufObj.EMailId = Dr[10].ToString();
                            ManufObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                            ManufObj.PermanentAddress = Dr[12].ToString();
                            ManufObj.PermanentCity = Dr[13].ToString();
                            ManufObj.PermanentPinCode = Dr[14].ToString();
                            ManufObj.PermanentPhoneNo = Dr[15].ToString();

                            IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                            IDProofType IDProofType = new IDProofType();
                            if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                ManufObj.IDProofType = IDProofType;
                            else
                                return retval;

                            ManufObj.IDProofValue = Dr[17].ToString();
                            if ((Dr[18].ToString() != null) && (Dr[18].ToString() != ""))
                                ManufObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                            if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) ManufObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());
                            if (Dr[20] != null)
                                ManufObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());
                            if (Dr[21] != null)
                                ManufObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());
                            if ((Dr[22].ToString() != null) && (Dr[22].ToString() != ""))
                                ManufObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());
                            if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) ManufObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());
                            if (Dr[24] != null)
                            {
                                if (Dr[24].ToString().Length > 0)
                                {
                                    byte[] bytes = (byte[])Dr[24];
                                    ManufObj.Image = (byte[])bytes;
                                }
                            }
                        }
                        else if (Ds.Tables[0].Rows.Count <= 0)
                        {
                            errorMessage = "Manufacturer Record Not Found!";
                            return retval;
                        }
                        else if (Ds.Tables[0].Rows.Count > 1)
                        {
                            errorMessage = "More Than One Manufacturer Record Exists For Manufacturer Id.  Please Correct Data!";
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

            public bool AddManufacturerDetails(out string errorMessage, Manufacturer ManufObj)
            {
                bool retval = false;

                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@ManufId", SqlDbType.VarChar, 30);
                    parameter.Value = "";
                    parameter.Direction = ParameterDirection.Output;
                    SQLParamArray[0] = parameter;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..GetNextManufId", SQLParamArray) == false)
                    {
                        errorMessage = "Error While Generating Manufacturer Id";
                        return false;
                    }

                    ManufObj.ManufacturerId = SQLParamArray[0].Value.ToString();

                    SQLParamArray = new SqlParameter[23];

                    parameter = new SqlParameter("@Manuf_Id", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.ManufacturerId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Manuf_Name", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.ManufacturerName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Manuf_Status", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.Status.ManufacturerStatusId ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@Manuf_DateofBirth", SqlDbType.DateTime);
                    parameter.Value = (object)ManufObj.DateofBirth ?? DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@Manuf_Gender", SqlDbType.Int);
                    parameter.Value = (object)ManufObj.GenderType.GenderTypeId ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@Manuf_PresentAddress", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.PresentAddress ?? DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@Manuf_PresentCity", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.PresentCity ?? DBNull.Value;
                    SQLParamArray[6] = parameter;

                    parameter = new SqlParameter("@Manuf_PresentPinCode", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.PresentPinCode ?? DBNull.Value;
                    SQLParamArray[7] = parameter;

                    parameter = new SqlParameter("@Manuf_PresentPhone", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.PresentPhoneNo ?? DBNull.Value;
                    SQLParamArray[8] = parameter;

                    parameter = new SqlParameter("@Manuf_Mobile", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.PresentPinCode ?? DBNull.Value;
                    SQLParamArray[9] = parameter;

                    parameter = new SqlParameter("@Manuf_EMailId", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.EMailId ?? DBNull.Value;
                    SQLParamArray[10] = parameter;

                    parameter = new SqlParameter("@Manuf_IsPresentPermAddressSame", SqlDbType.Int);
                    parameter.Value = (object)Convert.ToInt32(ManufObj.IsPresentPermAddressSame) ?? DBNull.Value;
                    SQLParamArray[11] = parameter;

                    parameter = new SqlParameter("@Manuf_PermanentAddress", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.PermanentAddress ?? DBNull.Value;
                    SQLParamArray[12] = parameter;

                    parameter = new SqlParameter("@Manuf_PermanentCity", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.PermanentCity ?? DBNull.Value;
                    SQLParamArray[13] = parameter;

                    parameter = new SqlParameter("@Manuf_PermanentPinCode", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.PermanentPinCode ?? DBNull.Value;
                    SQLParamArray[14] = parameter;

                    parameter = new SqlParameter("@Manuf_PermanentPhone", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.PermanentPhoneNo ?? DBNull.Value;
                    SQLParamArray[15] = parameter;

                    parameter = new SqlParameter("@Manuf_IDProofTypeId", SqlDbType.Int);
                    parameter.Value = (object)ManufObj.IDProofType.IDProofTypeId ?? DBNull.Value;
                    SQLParamArray[16] = parameter;

                    parameter = new SqlParameter("@Manuf_IDProofValue", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.IDProofValue ?? DBNull.Value;
                    SQLParamArray[17] = parameter;

                    parameter = new SqlParameter("@Manuf_RelationshipStartDate", SqlDbType.DateTime);
                    parameter.Value = (object)ManufObj.RelationshipStartDate ?? DBNull.Value;
                    SQLParamArray[18] = parameter;

                    parameter = new SqlParameter("@Manuf_AmtTobePaid", SqlDbType.Decimal);
                    parameter.Value = (object)ManufObj.AmountTobePaid ?? DBNull.Value;
                    SQLParamArray[19] = parameter;

                    parameter = new SqlParameter("@Manuf_AmtPaidYTD", SqlDbType.Decimal);
                    parameter.Value = (object)ManufObj.AmountPaidYTD ?? DBNull.Value;
                    SQLParamArray[20] = parameter;

                    parameter = new SqlParameter("@Manuf_CreateDate", SqlDbType.DateTime);
                    parameter.Value = (object)ManufObj.CreateDate ?? DBNull.Value;
                    SQLParamArray[21] = parameter;

                    parameter = new SqlParameter("@Manuf_Picture", SqlDbType.Image);
                    parameter.Value = (object)ManufObj.Image ?? DBNull.Value;
                    SQLParamArray[22] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddManufacturerDetails", SQLParamArray) == false)
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

            public void ModifyManufacturerDetails()
            {
                throw new System.NotSupportedException ();
            }

            public bool DeleteManufacturerDetails(out string errorMessage, Manufacturer ManufObj)
            {

                bool retval = false;

                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@Manuf_Id", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.ManufacturerId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..DeleteManufacturerDetails", SQLParamArray) == false)
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

            public bool AddManufacturerPhoto(int Manufid, out string errorMessage, Manufacturer ManufObj)
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
                    parameter.Value = Manufid;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Name", SqlDbType.VarChar);
                    parameter.Value = (object)ManufObj.ManufacturerName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Picture", SqlDbType.Image);
                    parameter.Value = (object)ManufObj.Image ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddManufacturerImg", SQLParamArray) == false)
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

            public bool GetManufacturerPhoto(int ManufId, out byte[] imgBytes, out string errorMessage)
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

                    SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int);
                    parameter.Value = ManufId;
                    SQLParamArray[0] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetManufacturerImg", SQLParamArray) == false)
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

            public bool GetManufSaleDetails(string ManufId, out string errorMessage, out Sale[] SaleObjArray)
            {
                bool retval = false;
                errorMessage = null;

                SaleFactory SaleFactory = new SaleFactory();
                SaleObjArray = null;

                try
                {
                    if (SaleFactory.GetManufSaleDetails(null, ManufId, null, DateTime.MinValue, DateTime.MinValue, out SaleObjArray, out errorMessage) == true)
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



