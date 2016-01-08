using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
namespace CommonLib
{
    public class FileLogger
    {
        public static object _obj = new object();
        public const string fileExtention = ".log";
        public static readonly string fileSavePath;
        static FileLogger()
        {
            fileSavePath = ConfigurationManager.AppSettings.Get("ErrorLog");
        }
        public static void WriteLine(Exception ex)
        {
            string message = string.Format("Error: {1}{0}{2}", Environment.NewLine,
                ex.Message, ex.StackTrace);
            WriteLine(message);
           
        }
        public static void WriteLine(string message)
        {
            lock(_obj)
            {
                InitDirectory();
                string name =fileSavePath+DateTime.Now.ToString("yyyyMMdd")+fileExtention;
                File.AppendAllText(name, string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), " ", message,Environment.NewLine));
            }
        }
        public static void InitDirectory()
        {
            if (!string.IsNullOrEmpty(fileSavePath))
            {
                if (!Directory.Exists(fileSavePath))
                {
                    Directory.CreateDirectory(fileSavePath);
                }
            }
        }

    }
}
