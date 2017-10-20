using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using WCFRetailService;
using NewApp.BusinessTier.Models;



namespace WCFRetailService
{
    public class UserTypeFactory : UserType, IUserTypeFactory
    {
        SQLDataAccess DBObj;


        public UserTypeFactory()
        {
        }

        public bool GetAllUserTypes(out UserType[] UserTypeObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            UserTypeObjArray = null;
            List<UserType> UserTypeListObj = null;

            if (GetAllUserTypesFromDB(out UserTypeListObj, out errorMessage) == true)
            {
                UserTypeObjArray = UserTypeListObj.ToArray<UserType>();
                retval = true;
            }

            return retval;
        }

        public bool GetUserType(int UserTypeId, out UserType UserType, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            UserType = null;

            List<UserType> UserTypeListObj;

            try
            {
                if (GetAllUserTypesFromDB(out UserTypeListObj, out errorMessage) == true)
                {
                    if (UserTypeListObj != null)
                        if (UserTypeListObj.Count > 0)
                        {
                            foreach (UserType TmpUserType in UserTypeListObj)
                            {
                                if (UserTypeId == TmpUserType.UserTypeId)
                                {
                                    UserType = TmpUserType;
                                    retval = true;
                                }
                            }
                        }
                }
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            return retval;
        }

        private bool GetAllUserTypesFromDB(out List<UserType> AllUserTypeListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllUserTypeListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllUserTypes") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                UserType UserType;

                AllUserTypeListObj = new List<UserType>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    UserType = new UserType();

                    UserType.UserTypeId = Convert.ToInt32(dr[0].ToString());
                    UserType.UserTypeValue = dr[1].ToString();
                    UserType.UserTypeDescription = dr[2].ToString();

                    AllUserTypeListObj.Add(UserType);
                }
                retval = true;
            }

            return retval;
        }
    }
}
