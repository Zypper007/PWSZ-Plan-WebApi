using Microsoft.EntityFrameworkCore;
using PWSZ_Plan_WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Helpers
{
    static public class Tools
    {
        public static void CopyValues<T1, T2>(T1 target, T2 source)
        {
            var pt = typeof(T1).GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
            var ps = typeof(T2).GetProperties().Where(prop => pt.Any(p => p.Name == prop.Name && p.MemberType == prop.MemberType));


            foreach (var prop in ps)
            {
                var val = prop.GetValue(source, null);
                if (val != null)
                    pt.Single(p => p.Name == prop.Name).
                        SetValue(target, val, null);
            }
        }

        public static bool ValidIsNumber(string val)
        {
            foreach (var v in val)
            {
                if (Char.IsNumber(v))
                    return true;
            }
            return false;
        }

        public static bool ValidIsUpper(string val)
        {
            foreach (var v in val)
            {
                if (Char.IsUpper(v))
                    return true;
            }
            return false;
        }

        public static bool ValidIsLower(string val)
        {
            foreach (var v in val)
            {
                if (Char.IsLower(v))
                    return true;
            }
            return false;
        }

        public static bool ValidIsSpecial(string val)
        {
            foreach (var v in val)
            {
                if (!Char.IsLetterOrDigit(v) && !Char.IsControl(v) && v != ' ')
                    return true;
            }
            return false;
        }
    }

}
