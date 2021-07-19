using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Net.Mail;
using System.Net.NetworkInformation;

using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

using System.Net.Http;
using System.Web.Script.Serialization;
using System.Text;
using System.Data.SqlClient;
using System.Web.Mvc;
using Teach_Ease_Lite.DataHandle;

public static class Common
{
    //public static string DateFormat = "dd/mm/yyyy";
    public static string DateFormat = "dd-M-yyyy";
    public static string DateFormatShort = "dd-M-yyyy";
    //public static string DateFormatShort = "dd/mm/yy";
    public static string Encode(string encodeMe)
    {
        byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
        return Convert.ToBase64String(encoded);
    }

    public static string Decode(string decodeMe)
    {
        byte[] encoded = Convert.FromBase64String(decodeMe);
        return System.Text.Encoding.UTF8.GetString(encoded);
    }
    public static void Createphysicalpath(byte[] byteOriginal, string P)
    {

        string newImagePath = P;


        using (MemoryStream ms = new MemoryStream())
        {
            ms.Write(byteOriginal, 0, byteOriginal.Length);

            Image imageToBeResized = System.Drawing.Image.FromStream(ms);

            Bitmap bitmap = new Bitmap(imageToBeResized);
            using (System.Drawing.Image img = (System.Drawing.Image)bitmap)
            {

                //SaveJPGWithCompressionSetting(img, newImagePath, 7L);
                // SaveJPGWithCompressionSetting(img, newImagePath, 7L);

                EncoderParameters eps = new EncoderParameters(1);

                eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 7L);

                ImageCodecInfo ici = GetEncoderInfo("image/png");
                //if (File.Exists(HttpContext.Current.Server.MapPath("/" + newImagePath)))
                //{
                //    File.Delete(HttpContext.Current.Server.MapPath("/" + newImagePath));
                //}
                //image.Save(szFileName, ici, eps);
                img.Save(newImagePath, ImageFormat.Png);

            }
            bitmap.Dispose();
            imageToBeResized.Dispose();
        }




        //int imageHeight = imageToBeResized.Height;
        //int imageWidth = imageToBeResized.Width;

        //int maxHeight =Convert.ToInt32( Width);
        //int maxWidth = Convert.ToInt32(Height);
        //imageHeight = (imageHeight * maxWidth) / imageWidth;
        //imageWidth = maxWidth;

        //if (imageHeight > maxHeight)
        //{
        //    imageWidth = (imageWidth * maxHeight) / imageHeight;
        //    imageHeight = maxHeight;
        //}

        // Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);

    }
    private static void SaveJPGWithCompressionSetting(System.Drawing.Image image, string szFileName, long lCompression)
    {



    }
    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {

        int j;

        ImageCodecInfo[] encoders;

        encoders = ImageCodecInfo.GetImageEncoders();

        for (j = 0; j < encoders.Length; ++j)
        {

            if (encoders[j].MimeType == mimeType)

                return encoders[j];

        }

        return null;

    }
    //private static string baseURI = ConfigurationManager.AppSettings["ApiBaseURI"].ToString();
    public static int ConvertDBNullToInt32(object obj)
    {
        try
        {
            return Convert.ToInt32(obj);
        }
        catch
        {
            return 0;
        }

    }

    public static short ConvertDBNullToInt16(object obj)
    {
        try
        {
            return Convert.ToInt16(obj);
        }
        catch
        {
            return 0;
        }

    }

    public static long ConvertDBNullToLong(object obj)
    {
        try
        {
            return Convert.ToInt64(obj);
        }
        catch
        {
            return 0;
        }

    }

    public static decimal ConvertDBNullToDecimal(object obj)
    {
        try
        {
            return Convert.ToDecimal(obj);
        }
        catch (Exception ex)
        {
            return decimal.MinValue;
        }

    }

    public static TimeSpan ConvertDBNullToTimeSpan(object obj)
    {
        try
        {
            string TimeString = obj.ToString().ToLower();


            if (TimeString.Contains("pm"))
            {
                TimeString = TimeString.Replace("pm", "");
                TimeSpan tsp = new TimeSpan(12, 0, 0);
                TimeSpan ReturnedTsp = TimeSpan.Parse(TimeString);

                if (ReturnedTsp < tsp)
                    ReturnedTsp = ReturnedTsp.Add(tsp);

                return ReturnedTsp;
            }
            else
            {
                TimeString = TimeString.Replace("am", "");
                return TimeSpan.Parse(TimeString);
            }

        }
        catch (Exception ex)
        {
            return TimeSpan.Parse("0");
        }

    }

    public static bool ConvertDBNullToBoolean(object obj)
    {
        try
        {
            return Convert.ToBoolean(obj);
        }
        catch
        {
            return false;
        }

    }

    public static string ConvertDBNullToString(object obj)
    {
        try
        {
            return Convert.ToString(obj);
        }
        catch
        {
            return "";
        }

    }
    public static DateTime ConvertDBNullToDateTime(object obj)
    {
        try
        {
            return Convert.ToDateTime(obj);
        }
        catch (Exception ex)
        {

            return Convert.ToDateTime("01/01/1900");
            //return DateTime.MinValue;
        }

    }
    public static string Get12HourTime(object obj)
    {
        try
        {
            TimeSpan ts = TimeSpan.Parse(obj.ToString());
            TimeSpan ts1 = TimeSpan.Parse("12:00");

            int t = TimeSpan.Compare(ts1, ts);

            if (t > 1)
                return ts.Hours.ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0') + " AM";
            else
            {
                TimeSpan ts2 = ts - ts1;
                return ts2.Hours.ToString().PadLeft(2, '0') + ":" + ts2.Minutes.ToString().PadLeft(2, '0') + " PM";

            }

        }
        catch (Exception)
        {
            return "00:00";
        }
    }

    public static string EncryptSymbolString(string strSymbol)
    {
        //  If you want to change this string,  Change in both common class (WCF and Application) and Encrypt/Decrypt
        return strSymbol.Replace('`', '⫁').Replace('~', 'ș').Replace('!', 'ȭ').Replace('@', 'ȶ').Replace('#', 'ȇ')
                        .Replace('$', 'Ȏ').Replace('%', '୰').Replace('^', 'ȅ').Replace('&', 'ɏ').Replace('*', 'Ɏ').Replace('(', 'ɍ')
                        .Replace(')', 'Ɍ').Replace('+', 'ɋ').Replace('[', 'ġ').Replace(']', 'ɉ').Replace('{', 'Ɉ').Replace('}', 'ɇ')
                        .Replace('<', 'Ɇ').Replace('>', 'Ʉ').Replace('/', 'Ƀ').Replace('?', 'ɂ').Replace("\'", "ȿ").Replace('"', 'Ⱦ')
                        .Replace('-', 'ȼ').Replace('_', 'Ȼ').Replace('.', 'ų').Replace(',', 'ŵ').Replace(';', 'ǂ').Replace(':', 'ǯ')
                        .Replace('=', 'ǩ').Replace("\\", "Ģ").Replace('|', 'ķ');
    }
    public static string DecryptSymbolString(string strSymbol)
    {
        //  If you want to change this string,  Change in both common class (WCF and Application) and Encrypt/Decrypt
        return strSymbol.Replace('⫁', '`').Replace('ș', '~').Replace('ȭ', '!').Replace('ȶ', '@').Replace('ȇ', '#')
                        .Replace('Ȏ', '$').Replace('୰', '%').Replace('ȅ', '^').Replace('ɏ', '&').Replace('Ɏ', '*').Replace('ɍ', '(')
                        .Replace('Ɍ', ')').Replace('ɋ', '+').Replace('ġ', '[').Replace('ɉ', ']').Replace('Ɉ', '{').Replace('ɇ', '}')
                        .Replace('Ɇ', '<').Replace('Ʉ', '>').Replace('Ƀ', '/').Replace('ɂ', '?').Replace("ȿ", "\'").Replace('Ⱦ', '"')
                        .Replace('ȼ', '-').Replace('Ȼ', '_').Replace('ų', '.').Replace('ŵ', ',').Replace('ǂ', ';').Replace('ǯ', ':')
                        .Replace('ǩ', '=').Replace("Ģ", "\\").Replace('ķ', '|');
    }

    public static DataTable FillComboByEnum(Type objEnum, bool AddBalnk = false, string BlankVal = "")
    {
        try
        {
            DataTable DtFillCombo = new DataTable();

            DtFillCombo.Columns.Add("Name");
            DtFillCombo.Columns.Add("Value", typeof(Int16));

            if (AddBalnk == true)
            {
                DataRow Item = DtFillCombo.NewRow();
                Item["Name"] = BlankVal;
                Item["Value"] = -1;
                DtFillCombo.Rows.Add(Item);
            }

            var _with1 = Enum.GetValues(objEnum);
            for (Int16 i = 0; i <= _with1.GetUpperBound(0); i++)
            {
                DataRow Item = DtFillCombo.NewRow();
                Item["Name"] = Enum.GetName(objEnum, _with1.GetValue(i)).Replace("_", " ");
                Item["Value"] = _with1.GetValue(i);
                DtFillCombo.Rows.Add(Item);
            }

            return DtFillCombo;
        }
        catch (Exception)
        {
            return new DataTable();
        }

    }

    public static byte[] String_To_Bytes(string strInput)
    {
        try
        {
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];

            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }

            return bytes;
        }
        catch
        {
            throw;
        }
    }

    public static string GetIP()
    {
        try
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }

            return string.Empty;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }


    public static List<T> ConvertDataTable<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }
    public static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                {
                    if (dr[column.ColumnName] == System.DBNull.Value)
                        continue;
                    //if (pro.GetType().)

                    pro.SetValue(obj, dr[column.ColumnName], null);
                }
                else
                    continue;
            }
        }
        return obj;
    }


    public static List<T> ConvertDatatableToList<T>(this DataTable dataTable) where T : new()
    {
        var dataList = new List<T>();

        //Define what attributes to be read from the class
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

        //Read Attribute Names and Types
        var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
            Select(item => new
            {
                Name = item.Name,
                Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
            }).ToList();

        //Read Datatable column names and types
        var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
            Select(item => new
            {
                Name = item.ColumnName,
                Type = item.DataType
            }).ToList();

        foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
        {
            var classObj = new T();

            foreach (var dtField in dtlFieldNames)
            {
                PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                var field = objFieldNames.Find(x => x.Name == dtField.Name);

                if (field != null)
                {

                    if (propertyInfos.PropertyType == typeof(DateTime))
                    {
                        propertyInfos.SetValue
                        (classObj, Convert.ToDateTime(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(int))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertDBNullToInt32(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(long))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertDBNullToLong(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(decimal))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertDBNullToDecimal(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(bool))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertDBNullToBoolean(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(String))
                    {
                        if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, Convert.ToDateTime(dataRow[dtField.Name]), null);
                        }
                        else
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertDBNullToString(dataRow[dtField.Name]), null);
                        }
                    }
                }

            }
            dataList.Add(classObj);
        }
        return dataList;
    }






    public static bool SendMail(string strMailTo, string strMailFrom, string strSubject, string strBody, string ServerName, string Username, string Password, int SMTPPort = 25, bool EnableSSL = false, bool IsBodyHtml = true)
    {
        try
        {
            if (!string.IsNullOrEmpty(strMailTo) && !string.IsNullOrEmpty(strMailFrom))
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(strMailTo);
                mail.From = new MailAddress(strMailFrom);
                mail.Subject = strSubject;
                mail.Body = strBody;
                mail.IsBodyHtml = IsBodyHtml;

                int port = 0;
                bool enableSSL = false;

                port = SMTPPort;
                enableSSL = EnableSSL;

                if (port == 0)
                    port = 25;

                //SmtpClient smtp = new SmtpClient(ServerName, 25); // 192.168.0.149
                SmtpClient smtp = new SmtpClient(ServerName, port); // 192.168.0.149
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.EnableSsl = false;
                smtp.EnableSsl = enableSSL;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(Username, Password);

                smtp.Send(mail);

                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
    public static bool SendMailSMTP(string strMailTo, string strMailFrom, string strSubject, string strBody, string ServerName, string Username, string Password, int SMTPPort = 25, bool EnableSSL = false, bool IsBodyHtml = true)
    {
        try
        {
            if (!string.IsNullOrEmpty(strMailTo) && !string.IsNullOrEmpty(strMailFrom))
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(strMailTo);
                mail.From = new MailAddress(strMailFrom);
                mail.Subject = strSubject;
                mail.Body = strBody;
                mail.IsBodyHtml = IsBodyHtml;

                int port = 0;
                bool enableSSL = false;

                port = SMTPPort;
                enableSSL = EnableSSL;

                if (port == 0)
                    port = 25;

                //SmtpClient smtp = new SmtpClient(ServerName, 25); // 192.168.0.149
                SmtpClient smtp = new SmtpClient(ServerName, port); // 192.168.0.149
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.EnableSsl = false;
                smtp.EnableSsl = enableSSL;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(Username, Password);

                smtp.Send(mail);

                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }



    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType().GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DisplayAttribute>()
                       .Name;
    }
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute),
            false);

        if (attributes != null &&
            attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }


    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }





    public static string Get_DateString_From_Date(DateTime myDate)
    {
        string returnDateString = "";
        try
        {

            returnDateString = myDate.Month.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Day.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Year.ToString().PadLeft(2, '0').ToString();
        }
        catch (Exception)
        {
            return "";
        }
        return returnDateString;
    }



    //public class PushNotification
    //{
    //    public string title { get; set; }
    //    public string body { get; set; }

    //}

    public class Data
    {
        public string title { get; set; }
        public string body { get; set; }

    }

    public class NotificationData
    {
        public Data data { get; set; }
        public IList<string> registration_ids { get; set; }
    }



    public static string Base64Encode(string plainText)
    {

        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }



    public static int GetEnumValue(Enum enm)
    {
        return Convert.ToInt32(enm);
    }



    public static byte[] CreateThumbnailOfImage(byte[] byteOriginal, int width = 135, int height = 125)
    {

        MemoryStream ms = new MemoryStream();

        Image.GetThumbnailImageAbort callback =
               new Image.GetThumbnailImageAbort(ThumbnailCallback);
        try
        {

            // Stream / Write Image to Memory Stream from the Byte Array.
            ms.Write(byteOriginal, 0, byteOriginal.Length);

            Image OriginalImage = System.Drawing.Image.FromStream(ms);

            // New
            int sourceWidth = OriginalImage.Width;
            int sourceHeight = OriginalImage.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(OriginalImage, 0, 0, destWidth, destHeight);
            g.Dispose();

            OriginalImage = (Image)b;
            // New

            //Image ThumbnailImage = OriginalImage.GetThumbnailImage(width, height, callback, IntPtr.Zero);
            Image ThumbnailImage = OriginalImage;

            MemoryStream myMS = new MemoryStream();
            //ThumbnailImage.Save(myMS, System.Drawing.Imaging.ImageFormat.Jpeg);
            ThumbnailImage.Save(myMS, System.Drawing.Imaging.ImageFormat.Png);

            byte[] test_imge = myMS.ToArray();

            return test_imge;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public static bool ThumbnailCallback()
    {
        return true;
    }
    public static string Get_DateString_From_Date(DateTime myDate, String dateFormat)
    {
        string returnDateString = "";
        try
        {
            if (dateFormat.ToLower() == "dd/mm/yy")
            {

                returnDateString = myDate.Day.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Month.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Year.ToString().PadLeft(2, '0').ToString();
            }
            else if (dateFormat.ToLower() == "mm/dd/yy")
            {
                returnDateString = myDate.Month.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Day.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Year.ToString().PadLeft(2, '0').ToString();
            }

            if (returnDateString == "01/01/1900")
            {
                returnDateString = string.Empty;
            }
        }
        catch (Exception)
        {
            return String.Empty;
        }
        return returnDateString;
    }
    public static string Get_YYYYMMDD(DateTime myDate)
    {
        string returnDateString = "";
        try
        {

            returnDateString = myDate.Year.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Month.ToString().PadLeft(2, '0').ToString() + "/" + myDate.Day.ToString().PadLeft(2, '0').ToString();

        }
        catch (Exception)
        {
            return "1900-01-01";
        }
        return returnDateString;
    }
    public static string GetDescription(this Enum GenericEnum)
    {

        Type genericEnumType = GenericEnum.GetType();
        System.Reflection.MemberInfo[] memberInfo =
                    genericEnumType.GetMember(GenericEnum.ToString());

        if ((memberInfo != null && memberInfo.Length > 0))
        {

            dynamic _Attribs = memberInfo[0].GetCustomAttributes
                  (typeof(System.ComponentModel.DescriptionAttribute), false);
            if ((_Attribs != null && _Attribs.Length > 0))
            {
                return ((System.ComponentModel.DescriptionAttribute)_Attribs[0]).Description;
            }
        }

        return GenericEnum.ToString();
    }
    public static string GetYYMMDDDateString(string myDateString, string DateForamt)
    {
        try
        {
            string line = myDateString.ToString();
            string[] dates = new string[8];
            if (line.Contains("-"))
            {
                dates = line.Split('-');
            }
            if (line.Contains("/"))
            {
                dates = line.Split('/');
            }
            string returnDate = string.Empty;

            if (DateForamt.ToLower() == "dd/mm/yy")
            {
                returnDate = ConvertDBNullToString(dates[2]) + "-" + ConvertDBNullToString(dates[1]) + "-" + ConvertDBNullToString(dates[0]);
            }
            else if (DateForamt.ToLower() == "mm/dd/yy")
            {
                returnDate = ConvertDBNullToString(dates[2]) + "-" + ConvertDBNullToString(dates[1]) + "-" + ConvertDBNullToString(dates[0]);
            }

            return returnDate;
        }
        catch (Exception ex)
        {
            return myDateString;
        }
    }

    public static int ErrorLog(string ErrorMessage, string FunctionName, string ErrorFrom)
    {
        try
        {
            LoginDB lg = new LoginDB();
            int i = lg.InsertErrorLog(ErrorMessage, FunctionName, ErrorFrom);
            return i;
        }
        catch (Exception)
        {

            return 0;
        }
    }
    public static string GetEnumString(string RoleType)
    {
        return Enum.GetName(typeof(ProjectEnum.RoleType), Common.ConvertDBNullToInt32(RoleType));
    }
    public static byte[] ConvertObjectToByteArray(object obj)
    {
        try
        {
            return (byte[])obj;
        }
        catch
        {
            return null;
        }
    }
    public static string ConvertByteArrayToImageURL(byte[] a)
    {
        if (a == null)
            return "";

        return Convert.ToBase64String(a, 0, a.Length);
    }
}


public static class Extensions
{
    public static DataTable ToDataTable<T>(this List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        //Get all the properties  
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Defining type of data column gives proper data table   
            var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
            //Setting column names as Property names  
            dataTable.Columns.Add(prop.Name, type);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows  
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        return dataTable;
    }

    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute),
            false);

        if (attributes != null &&
            attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }
}
