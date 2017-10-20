using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using WPFRetail.BusinessTier;
using AVDBLib;
using System.Data.SqlClient;
  

namespace WPFRetail.Common
{
    class Settings
    {
        private string ConStr;

        static Settings MySettings = new Settings();

        private SQLDataAccess MyDBObj;


        private Settings()
        {
            try
            {
                string Option;

                Option = "";

                AppSettingsReader MyAppRdr = new AppSettingsReader();

                Option = MyAppRdr.GetValue("HomeorOffice", typeof(string)).ToString();

                if (Option == "O")
                    ConStr = MyAppRdr.GetValue("WPFRetailOfficeConnectionString", typeof(string)).ToString();
                else if (Option == "H")
                    ConStr = MyAppRdr.GetValue("WPFRetailHomeConnectionString", typeof(string)).ToString();


                MyDBObj = new SQLDataAccess(ConStr);

            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
            }
        }

        public static Settings GetProperty()
        {
            if (MySettings != null)
                return MySettings;
            else
            {
                MySettings = new Settings();
                return MySettings;
            }
        }

        public SQLDataAccess GetDBObj()
        {
            try
            {
                if (MyDBObj != null)
                    return MyDBObj;
                else
                {
                    MyDBObj = new SQLDataAccess(ConStr);
                    return MyDBObj;
                }
            }
            catch (Exception ex1)
            {
                throw ex1.InnerException;
            }
        }

        private string ConnectionString()
        {
            return ConStr;
        }
    }
}
