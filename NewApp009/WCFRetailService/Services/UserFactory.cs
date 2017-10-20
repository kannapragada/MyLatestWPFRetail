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
using NewApp.BusinessTier.Models;
using WCFRetailService;




namespace WCFRetailService
{
    public class UserFactory : User, IUserFactory
    {
        //Declarations
        # region Declarations


        SQLDataAccess DBObj;

        #endregion

        //Constructor
        public UserFactory()
        {
        }

        //Member Methods  
        #region MemberMethods

        //    private void GetUserFromFile()
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
        //                        this._userGender = line;
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

        //    public void GetUserObject()
        //{
        //    FilePickerThread = new Thread(new ThreadStart(GetUserFromFile));
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

            public bool GetUsers(string User, out string errorMessage, out User[] UserObjArray)
            {
                DataSet Ds = null;
                DataRow Dr;
                User UserObj;

                bool retval = false;

                errorMessage = null;

                List<User> UserObjList = new List<User>();
                UserObjArray = null;

                SqlParameter[] SQLParamArray = new SqlParameter[2];

                SqlParameter parameter;

                parameter = new SqlParameter("@User_Id", SqlDbType.VarChar);
                parameter.Value = User;
                SQLParamArray[0] = parameter;

                parameter = new SqlParameter("@User_Name", SqlDbType.VarChar);
                parameter.Value = User;
                SQLParamArray[1] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetUsers", SQLParamArray) == false)
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
                                UserObj = new User();

                                Dr = Ds.Tables[0].Rows[i];

                                UserObj.UserId = Dr[0].ToString();
                                UserObj.UserName = Dr[1].ToString();

                                UserStatusFactory UserStatusFactory = new UserStatusFactory();
                                UserStatus UserStatus = new UserStatus();
                                if (UserStatusFactory.GetUserStatus(Convert.ToInt32(Dr[2].ToString()), out UserStatus, out errorMessage) == true)
                                    UserObj.Status = UserStatus;
                                else
                                    return retval;

                                if ((Dr[3].ToString() != null) && (Dr[3].ToString() != ""))
                                    UserObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                                GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                                GenderType GenderType = new GenderType();
                                if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                    UserObj.GenderType = GenderType;
                                else
                                    return retval;

                                UserObj.PresentAddress = Dr[5].ToString();
                                UserObj.PresentCity = Dr[6].ToString();
                                UserObj.PresentPinCode = Dr[7].ToString();
                                UserObj.PresentPhoneNo = Dr[8].ToString();
                                UserObj.Mobile = Dr[9].ToString();
                                UserObj.EMailId = Dr[10].ToString();
                                UserObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                                UserObj.PermanentAddress = Dr[12].ToString();
                                UserObj.PermanentCity = Dr[13].ToString();
                                UserObj.PermanentPinCode = Dr[14].ToString();
                                UserObj.PermanentPhoneNo = Dr[15].ToString();

                                IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                                IDProofType IDProofType = new IDProofType();
                                if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                    UserObj.IDProofType = IDProofType;
                                else
                                    return retval;

                                UserObj.IDProofValue = Dr[17].ToString();
                                UserObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                                if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) UserObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());

                                if (Dr[20] != null)
                                    UserObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());

                                if (Dr[21] != null)
                                    UserObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                                UserObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());

                                if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) UserObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());

                                if (Dr[24] != null)
                                {
                                    if (Dr[24].ToString().Length > 0)
                                    {
                                        byte[] bytes = (byte[])Dr[24];
                                        UserObj.Image = (byte[])bytes;
                                    }
                                }

                                if (Dr[25] != null)
                                    UserObj.UserThemeId = Convert.ToInt32(Dr[25].ToString());

                                if (Dr[26] != null)
                                    UserObj.UserPassword = Convert.ToString(Dr[26].ToString());

                                if (Dr[27] != null)
                                    UserObj.UserSecretQueryId = Convert.ToInt32(Dr[27].ToString());

                                if (Dr[28] != null)
                                    UserObj.UserSecretAnswer = Convert.ToString(Dr[28].ToString());

                                UserObjList.Add(UserObj);
                            }

                            UserObjArray = UserObjList.ToArray<User>();
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

            public bool GetUsers(string UserId, string UserName, string Address, string IDProofTypeId, string IDProofValue, DateTime FromDateofBirth, DateTime ToDateofBirth, DateTime FromDateofStartRelationship, DateTime ToDateofStartRelationship, DateTime FromDateofExpiryRelationship, DateTime ToDateofExpiryRelationship,out string errorMessage, out User[] UserObjArray)
            {
                DataSet Ds = null;
                DataRow Dr;
                User UserObj;

                bool retval = false;

                errorMessage = null;

                UserObjArray = null;
                List<User> UserObjList = new List<User>();

                SqlParameter[] SQLParamArray = new SqlParameter[11];

                SqlParameter parameter;

                try
                {
                    parameter = new SqlParameter("@User_Id", SqlDbType.VarChar);
                    parameter.Value = (object)UserId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@User_Name", SqlDbType.VarChar);
                    parameter.Value = (object)UserName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@User_Address", SqlDbType.VarChar);
                    parameter.Value = Address;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@User_IDProofTypeId", SqlDbType.Int);
                    if (IDProofTypeId.ToString().Length == 0)
                        parameter.Value = DBNull.Value;
                    else
                        parameter.Value = (object)IDProofTypeId;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@User_IDProofValue", SqlDbType.VarChar);
                    parameter.Value = (object)IDProofValue ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@User_FromDateofBirth", SqlDbType.DateTime);
                    if ((FromDateofBirth.Year > 1900) && (FromDateofBirth.Year < 2500))
                        parameter.Value = (object)FromDateofBirth;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@User_ToDateofBirth", SqlDbType.DateTime);
                    if ((ToDateofBirth.Year > 1900) && (ToDateofBirth.Year < 2500))
                        parameter.Value = (object)ToDateofBirth;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[6] = parameter;

                    parameter = new SqlParameter("@User_FromDateofStartRelationship", SqlDbType.DateTime);
                    if ((FromDateofStartRelationship.Year > 1900) && (FromDateofStartRelationship.Year < 2500))
                        parameter.Value = (object)FromDateofStartRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[7] = parameter;

                    parameter = new SqlParameter("@User_ToDateofStartRelationship", SqlDbType.DateTime);
                    if ((ToDateofStartRelationship.Year > 1900) && (ToDateofStartRelationship.Year < 2500))
                        parameter.Value = (object)ToDateofStartRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[8] = parameter;

                    parameter = new SqlParameter("@User_FromDateofExpiryRelationship", SqlDbType.DateTime);
                    if ((FromDateofExpiryRelationship.Year > 1900) && (FromDateofExpiryRelationship.Year < 2500))
                        parameter.Value = (object)FromDateofExpiryRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[9] = parameter;

                    parameter = new SqlParameter("@User_ToDateofExpiryRelationship", SqlDbType.DateTime);
                    if ((ToDateofExpiryRelationship.Year > 1900) && (ToDateofExpiryRelationship.Year < 2500))
                        parameter.Value = (object)ToDateofExpiryRelationship;
                    else
                        parameter.Value = DBNull.Value;
                    SQLParamArray[10] = parameter;

                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..SearchUsers", SQLParamArray) == false)
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
                                UserObj = new User();

                                Dr = Ds.Tables[0].Rows[i];

                                UserObj.UserId = Dr[0].ToString();
                                UserObj.UserName = Dr[1].ToString();

                                UserStatusFactory UserStatusFactory = new UserStatusFactory();
                                UserStatus UserStatus = new UserStatus();
                                if (UserStatusFactory.GetUserStatus(Convert.ToInt32(Dr[2].ToString()), out UserStatus, out errorMessage) == true)
                                    UserObj.Status = UserStatus;
                                else
                                    return retval;

                                UserObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                                GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                                GenderType GenderType = new GenderType();
                                if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                    UserObj.GenderType = GenderType;
                                else
                                    return retval;

                                UserObj.PresentAddress = Dr[5].ToString();
                                UserObj.PresentCity = Dr[6].ToString();
                                UserObj.PresentPinCode = Dr[7].ToString();
                                UserObj.PresentPhoneNo = Dr[8].ToString();
                                UserObj.Mobile = Dr[9].ToString();
                                UserObj.EMailId = Dr[10].ToString();
                                UserObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                                UserObj.PermanentAddress = Dr[12].ToString();
                                UserObj.PermanentCity = Dr[13].ToString();
                                UserObj.PermanentPinCode = Dr[14].ToString();
                                UserObj.PermanentPhoneNo = Dr[15].ToString();

                                IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                                IDProofType IDProofType = new IDProofType();
                                if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                    UserObj.IDProofType = IDProofType;
                                else
                                    return retval;

                                UserObj.IDProofValue = Dr[17].ToString();
                                UserObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                                if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) UserObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());

                                if (Dr[20] != null)
                                    UserObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());

                                if (Dr[21] != null)
                                    UserObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                                UserObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());

                                if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) UserObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());

                                if (Dr[24] != null)
                                {
                                    if (Dr[24].ToString().Length > 0)
                                    {
                                        byte[] bytes = (byte[])Dr[24];
                                        UserObj.Image = (byte[])bytes;
                                    }
                                }

                                if (Dr[25] != null)
                                    UserObj.UserThemeId = Convert.ToInt32(Dr[25].ToString());

                                if (Dr[26] != null)
                                    UserObj.UserPassword = Convert.ToString(Dr[26].ToString());

                                if (Dr[27] != null)
                                    UserObj.UserSecretQueryId = Convert.ToInt32(Dr[27].ToString());

                                if (Dr[28] != null)
                                    UserObj.UserSecretAnswer = Convert.ToString(Dr[28].ToString());

                                UserObjList.Add(UserObj);
                            }
                            UserObjArray = UserObjList.ToArray<User>();
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

            public bool GetUserSaleDetails(string UserId, out string errorMessage, out Sale[] SaleObjArray)
            {
                bool retval = false;
                errorMessage = null;

                SaleFactory SaleFactory = new SaleFactory();
                SaleObjArray = null;

                try
                {
                    if (SaleFactory.GetUserSaleDetails(null, UserId, null, DateTime.MinValue, DateTime.MinValue, out SaleObjArray, out errorMessage) == true)
                        retval = true;
                }
                catch (Exception ex1)
                {
                    errorMessage = ex1.Message;
                }

                return retval;
            }

            public bool GetUserBasicDetails(string User, out string errorMessage, out User UserObj)
            {
                DataSet Ds = null;
                DataRow Dr;


                bool retval = false;

                errorMessage = null;

                UserObj = null;


                SqlParameter[] SQLParamArray = new SqlParameter[1];

                SqlParameter parameter;

                parameter = new SqlParameter("@User_Id", SqlDbType.VarChar);
                parameter.Value = User;
                SQLParamArray[0] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetUserBasicDetails", SQLParamArray) == false)
                    {
                        if (errorMessage != null)
                            return retval;
                    }
                    else
                    {
                        if (Ds.Tables[0].Rows.Count == 1)
                        {
                            Dr = Ds.Tables[0].Rows[0];

                            UserObj = new User();

                            UserObj.UserId = Dr[0].ToString();
                            UserObj.UserName = Dr[1].ToString();

                            UserStatusFactory UserStatusFactory = new UserStatusFactory();
                            UserStatus UserStatus = new UserStatus();
                            if (UserStatusFactory.GetUserStatus(Convert.ToInt32(Dr[2].ToString()), out UserStatus, out errorMessage) == true)
                                UserObj.Status = UserStatus;
                            else
                                return retval;

                            UserObj.DateofBirth = Convert.ToDateTime(Dr[3].ToString());

                            GenderTypeFactory GenderTypeFactory = new GenderTypeFactory();
                            GenderType GenderType = new GenderType();
                            if (GenderTypeFactory.GetGenderType(Convert.ToInt32(Dr[4].ToString()), out GenderType, out errorMessage) == true)
                                UserObj.GenderType = GenderType;
                            else
                                return retval;

                            UserObj.PresentAddress = Dr[5].ToString();
                            UserObj.PresentCity = Dr[6].ToString();
                            UserObj.PresentPinCode = Dr[7].ToString();
                            UserObj.PresentPhoneNo = Dr[8].ToString();
                            UserObj.Mobile = Dr[9].ToString();
                            UserObj.EMailId = Dr[10].ToString();
                            UserObj.IsPresentPermAddressSame = Convert.ToBoolean(Dr[11]);
                            UserObj.PermanentAddress = Dr[12].ToString();
                            UserObj.PermanentCity = Dr[13].ToString();
                            UserObj.PermanentPinCode = Dr[14].ToString();
                            UserObj.PermanentPhoneNo = Dr[15].ToString();

                            IDProofTypeFactory IDProofTypeFactory = new IDProofTypeFactory();
                            IDProofType IDProofType = new IDProofType();
                            if (IDProofTypeFactory.GetIDProofType(Convert.ToInt32(Dr[16].ToString()), out IDProofType, out errorMessage) == true)
                                UserObj.IDProofType = IDProofType;
                            else
                                return retval;

                            UserObj.IDProofValue = Dr[17].ToString();
                            UserObj.RelationshipStartDate = Convert.ToDateTime(Dr[18].ToString());
                            if ((Dr[19].ToString() != null) && (Dr[19].ToString() != "")) UserObj.RelationshipExpiryDate = Convert.ToDateTime(Dr[19].ToString());

                            if (Dr[20] != null)
                                UserObj.AmountTobePaid = Convert.ToDouble(Dr[20].ToString());

                            if (Dr[21] != null)
                                UserObj.AmountPaidYTD = Convert.ToDouble(Dr[21].ToString());

                            UserObj.CreateDate = Convert.ToDateTime(Dr[22].ToString());

                            if ((Dr[23].ToString() != null) && (Dr[23].ToString() != "")) UserObj.LastModDate = Convert.ToDateTime(Dr[23].ToString());

                            if (Dr[24] != null)
                            {
                                if (Dr[24].ToString().Length > 0)
                                {
                                    byte[] bytes = (byte[])Dr[24];
                                    UserObj.Image = (byte[])bytes;
                                }
                            }

                            if (Dr[25] != null)
                                UserObj.UserThemeId = Convert.ToInt32(Dr[25].ToString());

                            if (Dr[26] != null)
                                UserObj.UserPassword = Convert.ToString(Dr[26].ToString());

                            if (Dr[27] != null)
                                UserObj.UserSecretQueryId = Convert.ToInt32(Dr[27].ToString());

                            if (Dr[28] != null)
                                UserObj.UserSecretAnswer = Convert.ToString(Dr[28].ToString());


                            SaleFactory SaleFactory = new SaleFactory();
                            List<Sale> SaleObjList = new List<Sale>();
                            Sale[] SaleObjArray = null;

                            DateTime DateTime = DateTime.MinValue;

                            if (SaleFactory.GetUserSaleDetails(null, UserObj.UserId, null, DateTime, DateTime, out SaleObjArray, out errorMessage) == true)
                            {
                                if (SaleObjList != null)
                                    UserObj.SalesDetails = SaleObjArray.ToList<Sale>();
                            }
                        }
                        else if (Ds.Tables[0].Rows.Count <= 0)
                        {
                            errorMessage = "User Record Not Found!";
                            return retval;
                        }
                        else if (Ds.Tables[0].Rows.Count > 1)
                        {
                            errorMessage = "More Than One User Record Exists For User Id.  Please Correct Data!";
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

            public bool AddUserDetails(out string errorMessage, User UserObj)
            {
                bool retval = false;

                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@UserId", SqlDbType.VarChar, 30);
                    parameter.Value = "";
                    parameter.Direction = ParameterDirection.Output;
                    SQLParamArray[0] = parameter;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..GetNextUserId", SQLParamArray) == false)
                    {
                        errorMessage = "Error While Generating User Id";
                        return retval;
                    }

                    UserObj.UserId = SQLParamArray[0].Value.ToString();

                    SQLParamArray = new SqlParameter[27];

                    parameter = new SqlParameter("@User_Id", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.UserId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@User_Name", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.UserName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@User_Status", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.Status.UserStatusId ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    parameter = new SqlParameter("@User_DateofBirth", SqlDbType.DateTime);
                    parameter.Value = (object)UserObj.DateofBirth ?? DBNull.Value;
                    SQLParamArray[3] = parameter;

                    parameter = new SqlParameter("@User_Gender", SqlDbType.Int);
                    parameter.Value = (object)UserObj.GenderType.GenderTypeId ?? DBNull.Value;
                    SQLParamArray[4] = parameter;

                    parameter = new SqlParameter("@User_PresentAddress", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.PresentAddress ?? DBNull.Value;
                    SQLParamArray[5] = parameter;

                    parameter = new SqlParameter("@User_PresentCity", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.PresentCity ?? DBNull.Value;
                    SQLParamArray[6] = parameter;

                    parameter = new SqlParameter("@User_PresentPinCode", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.PresentPinCode ?? DBNull.Value;
                    SQLParamArray[7] = parameter;

                    parameter = new SqlParameter("@User_PresentPhone", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.PresentPhoneNo ?? DBNull.Value;
                    SQLParamArray[8] = parameter;

                    parameter = new SqlParameter("@User_Mobile", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.PresentPinCode ?? DBNull.Value;
                    SQLParamArray[9] = parameter;

                    parameter = new SqlParameter("@User_EMailId", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.EMailId ?? DBNull.Value;
                    SQLParamArray[10] = parameter;

                    parameter = new SqlParameter("@User_IsPresentPermAddressSame", SqlDbType.Int);
                    parameter.Value = (object)Convert.ToInt32(UserObj.IsPresentPermAddressSame) ?? DBNull.Value;
                    SQLParamArray[11] = parameter;

                    parameter = new SqlParameter("@User_PermanentAddress", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.PermanentAddress ?? DBNull.Value;
                    SQLParamArray[12] = parameter;

                    parameter = new SqlParameter("@User_PermanentCity", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.PermanentCity ?? DBNull.Value;
                    SQLParamArray[13] = parameter;

                    parameter = new SqlParameter("@User_PermanentPinCode", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.PermanentPinCode ?? DBNull.Value;
                    SQLParamArray[14] = parameter;

                    parameter = new SqlParameter("@User_PermanentPhone", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.PermanentPhoneNo ?? DBNull.Value;
                    SQLParamArray[15] = parameter;

                    parameter = new SqlParameter("@User_IDProofTypeId", SqlDbType.Int);
                    parameter.Value = (object)UserObj.IDProofType.IDProofTypeId ?? DBNull.Value;
                    SQLParamArray[16] = parameter;

                    parameter = new SqlParameter("@User_IDProofValue", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.IDProofValue ?? DBNull.Value;
                    SQLParamArray[17] = parameter;

                    parameter = new SqlParameter("@User_RelationshipStartDate", SqlDbType.DateTime);
                    parameter.Value = (object)UserObj.RelationshipStartDate ?? DBNull.Value;
                    SQLParamArray[18] = parameter;

                    parameter = new SqlParameter("@User_AmtTobePaid", SqlDbType.Decimal);
                    parameter.Value = (object)UserObj.AmountTobePaid ?? DBNull.Value;
                    SQLParamArray[19] = parameter;

                    parameter = new SqlParameter("@User_AmtPaidYTD", SqlDbType.Decimal);
                    parameter.Value = (object)UserObj.AmountPaidYTD ?? DBNull.Value;
                    SQLParamArray[20] = parameter;

                    parameter = new SqlParameter("@User_CreateDate", SqlDbType.DateTime);
                    parameter.Value = (object)UserObj.CreateDate ?? DBNull.Value;
                    SQLParamArray[21] = parameter;

                    parameter = new SqlParameter("@User_Picture", SqlDbType.Image);
                    parameter.Value = (object)UserObj.Image ?? DBNull.Value;
                    SQLParamArray[22] = parameter;

                    parameter = new SqlParameter("@User_ThemeId", SqlDbType.Int);
                    parameter.Value = (object)UserObj.UserThemeId ?? DBNull.Value;
                    SQLParamArray[23] = parameter;

                    parameter = new SqlParameter("@User_Pwd", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.UserPassword ?? DBNull.Value;
                    SQLParamArray[24] = parameter;

                    parameter = new SqlParameter("@User_SecretQueryId", SqlDbType.Int);
                    parameter.Value = (object)UserObj.UserSecretQueryId ?? DBNull.Value;
                    SQLParamArray[25] = parameter;

                    parameter = new SqlParameter("@User_SecretAnswer", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.UserSecretAnswer ?? DBNull.Value;
                    SQLParamArray[26] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddUserDetails", SQLParamArray) == false)
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

            public void ModifyUserDetails()
            {
                throw new System.NotSupportedException ();
            }

            public bool DeleteUserDetails(out string errorMessage, User UserObj)
            {

                bool retval = false;

                errorMessage = null;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    SqlParameter[] SQLParamArray = new SqlParameter[1];

                    SqlParameter parameter = new SqlParameter("@User_Id", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.UserId ?? DBNull.Value;
                    SQLParamArray[0] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..DeleteUserDetails", SQLParamArray) == false)
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

            public bool AddUserPhoto(int Userid, out string errorMessage, User UserObj)
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
                    parameter.Value = Userid;
                    SQLParamArray[0] = parameter;

                    parameter = new SqlParameter("@Name", SqlDbType.VarChar);
                    parameter.Value = (object)UserObj.UserName ?? DBNull.Value;
                    SQLParamArray[1] = parameter;

                    parameter = new SqlParameter("@Picture", SqlDbType.Image);
                    parameter.Value = (object)UserObj.Image ?? DBNull.Value;
                    SQLParamArray[2] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..AddUserImg", SQLParamArray) == false)
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

            public bool GetUserPhoto(int Userid, out byte[] imgBytes, out string errorMessage)
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

                    int a11 = Userid;

                    SqlParameter parameter = new SqlParameter("@ID", SqlDbType.Int);
                    parameter.Value = a11;
                    SQLParamArray[0] = parameter;

                    errorMessage = null;

                    if (DBObj.ExecuteSPWithParamsRespDS(out Ds, out errorMessage, "NewAppDb..GetUserImg", SQLParamArray) == false)
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

            public bool AuthenticateUser(string User, string Password, out string errorMessage)
            {
                bool retval = false;

                errorMessage = null;


                SqlParameter[] SQLParamArray = new SqlParameter[2];

                SqlParameter parameter;

                parameter = new SqlParameter("@User_Id", SqlDbType.VarChar);
                parameter.Value = User;
                SQLParamArray[0] = parameter;

                parameter = new SqlParameter("@Password", SqlDbType.VarChar);
                parameter.Value = Password;
                SQLParamArray[1] = parameter;

                try
                {
                    DBObj = SQLDataAccess.GetDBObj(out errorMessage);

                    if (errorMessage != null)
                        return retval;

                    if (DBObj.ExecuteSPWithParamsNoResp(out errorMessage, "NewAppDb..CheckUser", SQLParamArray) == false)
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



