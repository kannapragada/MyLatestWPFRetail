using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewApp.BusinessTier.Models;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;



namespace NewApp.BusinessTier.Common
{
    public static class BusinessTierState 
    { 
        private static Dictionary<string, object> _values = new Dictionary<string, object>(); 
        
        public static void SetValue(string key, object value) 
        { 
            _values.Add(key, value);
        } 

        public static bool GetValue<T>(string key, out T value, out string errorMessage) 
        {
            bool retval = false;

            errorMessage = null;

            value = default(T);

            try
            {
                if (_values.Count > 0)
                {
                    value = (T)_values[key];
                    retval = true;
                }

                return retval;
            }
            catch (Exception ex1)
            {
                errorMessage = ex1.Message;

                value = default(T);

                return false;
            }
        }

        public static void DeleteValue(string key)
        {
            _values.Remove(key);
        }

        public static void Clear()
        {
            _values.Clear();
        }

        
    } 
}
