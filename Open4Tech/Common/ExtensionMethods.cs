using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KidsVaccineReminder.Common
{
    public static class ExtensionMethods
    {
        public static bool IsAnyNullOrEmpty(this object myObject)
        {
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(myObject);
                    if (string.IsNullOrEmpty(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsObjectNullOrEmpty(this object myObject)
        {
            return myObject == null;
        }
    }
}
