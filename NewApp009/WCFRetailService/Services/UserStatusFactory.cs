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
    public class UserStatusFactory : UserStatus, IUserStatusFactory
    {
        List<UserStatus> UserStatusListObj;

        SQLDataAccess DBObj;


        public UserStatusFactory()
        {
            UserStatusListObj = new List<UserStatus>();
        }

        public bool GetAllUserStatus(out UserStatus[] UserStatusObjArray, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            List<UserStatus> UserStatusListObj = null;
            UserStatusObjArray = null;

            if (GetAllUserStatusFromDB(out UserStatusListObj, out errorMessage) == true)
            {
                UserStatusObjArray = UserStatusListObj.ToArray<UserStatus>();
                retval = true;
            }

            return retval;
        }

        public bool GetUserStatus(int UserStatusId, out UserStatus UserStatus, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;
            UserStatus = null;

            List<UserStatus> UserStatusListObj = new List<UserStatus>();

            try
            {
                if (GetAllUserStatusFromDB(out UserStatusListObj, out errorMessage) == true)
                {
                    if (UserStatusListObj != null)
                        if (UserStatusListObj.Count > 0)
                        {
                            foreach (UserStatus TmpUserStatus in UserStatusListObj)
                            {
                                if (UserStatusId == TmpUserStatus.UserStatusId)
                                {
                                    UserStatus = TmpUserStatus;
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

        private bool GetAllUserStatusFromDB(out List<UserStatus> AllUserStatusListObj, out string errorMessage)
        {
            errorMessage = null;
            bool retval = false;
            AllUserStatusListObj = null;

            DataSet Ds = new DataSet();

            try
            {
                DBObj = SQLDataAccess.GetDBObj(out errorMessage);
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;
            }

            if (DBObj.ExecuteSPNoParamsWithRespDS(out Ds, out errorMessage, "NewAppDb..GetAllUserStatus") == false)
            {
                if (errorMessage != null)
                    return retval;
            }
            else
            {
                UserStatus UserStatus;

                AllUserStatusListObj = new List<UserStatus>();

                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    UserStatus = new UserStatus();

                    UserStatus.UserStatusId = Convert.ToInt32(dr[0].ToString());
                    UserStatus.UserStatusValue = dr[1].ToString();
                    UserStatus.UserStatusDescription = dr[2].ToString();

                    AllUserStatusListObj.Add(UserStatus);
                }
                retval = true;
            }

            return retval;
        }
    }
}
