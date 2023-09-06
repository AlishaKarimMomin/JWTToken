using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Utility.Configuration;

namespace Utility.Common
{
    public static class Common
    {
        public const int MaxFileSize = 25;
        public static string TrimEncryptedString(string value)
        {
            while (value.EndsWith("="))
                value = value.Substring(0, value.Length - 1);

            return value;
        }

        public static string FixLengthOfEncryptedString(string value)
        {
            if (value.Length % 4 > 0)
                value = value.PadRight(value.Length + 4 - value.Length % 4, '=');

            return value;
        }

        public static DateTime GetLocalDateTime(string TimeZone)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TimeZone))
                    return DateTime.Now;


                DateTime utc = DateTime.UtcNow;
                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
                DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utc, zone);
                return localDateTime;
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static bool IsNumericType(Type t)
        {
            Type underLyingType = Nullable.GetUnderlyingType(t);
            TypeCode typeCode = TypeCode.String;
            if (underLyingType == null)
                typeCode = Type.GetTypeCode(t);
            else
                typeCode = Type.GetTypeCode(underLyingType);
            switch (typeCode)
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsDateType(Type t)
        {
            Type underLyingType = Nullable.GetUnderlyingType(t);
            TypeCode typeCode = TypeCode.String;
            if (underLyingType == null)
                typeCode = Type.GetTypeCode(t);
            else
                typeCode = Type.GetTypeCode(underLyingType);
            switch (typeCode)
            {
                case TypeCode.DateTime:
                    return true;
                default:
                    return false;
            }
        }
        private static DateTime getLastSunday(DateTime aDate)
        {
            return aDate.AddDays(7 - (int)aDate.DayOfWeek).AddDays(-7);
        }

        public static string DecodeFromBase64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);



            string returnValue = System.Text.Encoding.Unicode.GetString(encodedDataAsBytes);



            return returnValue;
        }
        public static string EncodeToBase64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.Encoding.Unicode.GetBytes(toEncode);



            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);



            return returnValue;
        }

        public static string EncodeForUrl(string toEncode)
        {
            return HttpUtility.UrlEncode(toEncode);
        }
        public static string DecodeFromUrl(string toEncode)
        {
            return HttpUtility.UrlDecode(toEncode);
        }

        public static string GetQueryString(Dictionary<string, string> dic)
        {
            string query = "_";
            foreach (var item in dic)
            {
                query += item.Key + "_" + item.Value + "_";
            }
            return query;
        }

        public static bool IsNumeric(string sNumber)
        {
            Decimal a;
            bool isNumeric = true;
            try
            {
                a = Decimal.Parse(sNumber);
            }
            catch
            {
                isNumeric = false;
            }
            return isNumeric;
        }
        public static bool IsBoolean(string sBool)
        {
            bool value;
            return bool.TryParse(sBool, out value);
        }

        public static bool ValidExtension(string ext)
        {
            ext = ext.Replace(".", "");
            if (ext == "txt" || ext == "csv" || ext == "pdf" || ext == "xlsx" || ext == "zip" || ext == "doc" || ext == "docx" || ext == "xml" || ext == "xls" || ext == "jpg" || ext == "jpeg" || ext == "png" || ext == "ppt" || ext == "rpt" || ext == "aci" || ext == "zap" || ext == "zoo")
            {
                return true;
            }
            return false;
        }
        public static string GetErrorMessage(Exception ex)
        {
            string message = "";
            string innerMessage = "";

            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    innerMessage = GetErrorMessage(ex.InnerException);
                }

                message = ex.Message + "\n" + innerMessage;
            }
            return message;
        }

        public static DateTime GetAppDate(string timezone = "")
        {
            DateTime AppDate = System.DateTime.Today;

            //  DateTime AppDate = System.Data.SqlTypes.SqlDateTime.Today;

            //string Timezone = HttpContext.Current.Application["Timezone"].ToString();



            if (!String.IsNullOrEmpty(timezone))
            {
                foreach (TimeZoneInfo tzi in TimeZoneInfo.GetSystemTimeZones())
                {
                    if (tzi.Id == timezone)
                    {
                        AppDate = System.DateTime.UtcNow.AddHours(tzi.BaseUtcOffset.TotalHours).Date;
                    }
                }
            }



            return AppDate;
        }

        public static DateTime GetAppDateTime(string timezone = "")
        {
            DateTime AppDate = System.DateTime.UtcNow;
            //string Timezone = HttpContext.Current.Application["Timezone"].ToString();
            double hoursToAdd = 0;
            if (!String.IsNullOrEmpty(timezone))
            {
                foreach (TimeZoneInfo tzi in TimeZoneInfo.GetSystemTimeZones())
                {
                    if (tzi.Id == timezone)
                    {
                        hoursToAdd = tzi.BaseUtcOffset.TotalHours;
                        if (tzi.IsDaylightSavingTime(AppDate))
                        {
                            hoursToAdd += 1;
                        }
                        AppDate = System.DateTime.UtcNow.AddHours(hoursToAdd);
                        break;
                    }
                }
            }



            return AppDate;
        }

        public static DateTime GetYearEndDate(DateTime date)
        {
            //DateTime date = DateTime.Parse(startDate).Date;
            DateTime yearEndDate = new DateTime(date.Year, 12, 31);
            return yearEndDate;
        }
        public static int GetNumberOfYears(DateTime dateFrom)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            DateTime DateTo = GetAppDate().Date;
            //int numberOfDays = (DateTo - DateFrom).Days + 1;
            TimeSpan span = DateTo - dateFrom.Date;
            int years = (zeroTime + span).Year - 1;



            return years;
        }

        public static DateTime GetQuarterEnd(DateTime StartDate)
        {
            if (((StartDate.Month + 2) / 3) == 1)
            {
                DateTime EndDate = new DateTime(StartDate.Year, 3, 31);
                return EndDate;
            }
            else if (((StartDate.Month + 2) / 3) == 2)
            {
                DateTime EndDate = new DateTime(StartDate.Year, 6, 30);
                return EndDate;
            }
            else if (((StartDate.Month + 2) / 3) == 3)
            {
                DateTime EndDate = new DateTime(StartDate.Year, 9, 30);
                return EndDate;
            }
            else
            {
                DateTime EndDate = new DateTime(StartDate.Year, 12, 31);
                return EndDate;
            }

        }

        public static DateTime GetMonthEnd(DateTime StartDate)
        {

            DateTime EndDate = new DateTime(StartDate.Year, StartDate.Month, DateTime.DaysInMonth(StartDate.Year, StartDate.Month));
            return EndDate;

        }





    }
}
