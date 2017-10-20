using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewApp.BusinessTier.Common
{
    public class UtilityClass
    {
        public bool IsValidDate(DateTime DateTime, out string errorMessage)
        {
            bool retval = false;

            errorMessage = null;

            
            DateTime date1 = new DateTime(1900, 1, 1, 12, 0, 0);
            DateTime date2 = new DateTime(2500, 1, 1, 12, 0, 0);
            
            
            int result1 = DateTime.Compare(date1, DateTime);
            int result2 = DateTime.Compare(DateTime, date2);
            

            
            if ((result1 < 0) && (result2 < 0))
                retval = true;  


            return retval;
        }
    }
}
