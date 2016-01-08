using System;
using System.Linq;
using System.Xml.Linq;
using System.Web;

namespace CommonLib
{

    public partial class CacheHelper
    {

        /// <summary>
        /// 基本缓存（.net自带）
        /// </summary>
        public class Normal
        {
            private static string optionPath ;
            static Normal()
            { 
                //异步调用或非主线程调用时HttpContext.Current为null
                //optionPath= HttpContext.Current.Server.MapPath("~/App_Data/CacheExpirationOption.xml")
                optionPath = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\\CacheExpirationOption.xml");
            }
            public static string GetOptionPath()
            {
                return optionPath;
            }
            public static int ExpiredTime(string key)
            {
                int time = 5;//默认5分钟
                XElement element = XElement.Load(optionPath);
                if (element != null)
                {
                    var item = element.Elements("Item").FirstOrDefault(i => i.Attribute("name").Value == key);
                    if (item != null)
                    {
                        int.TryParse(item.Element("ExpirationTime").Value, out time);
                    }
                }
                return time;

            }
            public static object Get(string key)
            {
                return HttpRuntime.Cache.Get(key);
            }
            public static void Add(string key, object value)
            {
                HttpRuntime.Cache.Insert(key, value);
            }
            public static void AddWithDependency(string key, object value)
            {
                var dependency = new System.Web.Caching.CacheDependency(GetOptionPath());
                HttpRuntime.Cache.Insert(key, value, dependency);
            }
            public static void AddWithDependency(string key, object value, string name)
            {
                var dependency = new System.Web.Caching.CacheDependency(GetOptionPath());
                var slidingExpiration = ExpiredTime(name);
                HttpRuntime.Cache.Insert(key, value, dependency, 
                    System.Web.Caching.Cache.NoAbsoluteExpiration,
                    TimeSpan.FromMinutes(slidingExpiration),
                    System.Web.Caching.CacheItemPriority.Low, null);
            }
            public static void AddWithDependency(string key, object value, string name, DateTime absoluteExpiration)
            {
                var dependency = new System.Web.Caching.CacheDependency(GetOptionPath());
                var slidingExpiration = ExpiredTime(name);
                HttpRuntime.Cache.Insert(key, value, dependency, absoluteExpiration,
                    TimeSpan.FromMinutes(slidingExpiration), System.Web.Caching.CacheItemPriority.Low, null);
            }
        }
        
    }
}