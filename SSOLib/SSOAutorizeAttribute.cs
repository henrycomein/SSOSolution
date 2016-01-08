using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSOLib
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SSOAutorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="appkey">当前系统的AppKey</param>
        /// <param name="ssoLoginUrl">认证服务器的登录地址</param>
        /// <param name="ssoGetTokenUrl">认证服务器获取token的地址</param>
        /// <param name="ssoGetUserUrl">认证服务器获取user的地址</param>
        public SSOAutorizeAttribute(string appkey, string ssoLoginUrl, string ssoGetTokenUrl, string ssoGetUserUrl)
        {
            if (string.IsNullOrEmpty(appkey) || string.IsNullOrEmpty(ssoLoginUrl) || string.IsNullOrEmpty(ssoGetTokenUrl) || string.IsNullOrEmpty(ssoGetUserUrl))
            {
                throw new ArgumentNullException("参数不能为空");
            }
            AppKey = appkey;
            LoginUrl = ssoLoginUrl;
            GetTokenUrl = ssoGetTokenUrl;
            GetUserUrl = ssoGetUserUrl;
        }
        public string LoginUrl { get; set; }
        public string GetTokenUrl { get; set; }
        public string GetUserUrl { get; set; }
        public string AppKey { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var username = string.Empty;
            if (filterContext.HttpContext.Session["username"] == null)
            {
                var token = filterContext.HttpContext.Request.QueryString["token"];
                if (string.IsNullOrEmpty(token))
                {
                    //先去服务器上获取;
                    filterContext.Result = new RedirectResult(GetTokenUrl + "?appkey=" + HttpUtility.UrlEncode(AppKey));

                }
                else
                {
                    username = new System.Net.Http.HttpClient().GetStringAsync(GetUserUrl + "?token=" + token).Result;
                    if (string.IsNullOrWhiteSpace(username))
                    {
                        //跳转到Api登录页
                        var loginUrl = LoginUrl + "?appkey=" + HttpUtility.UrlEncode(AppKey);
                        filterContext.Result = new RedirectResult(loginUrl);
                    }
                    else
                    {
                        filterContext.HttpContext.Session["username"] = username;
                    }
                }

            }
            //base.OnAuthorization(filterContext);
        }
    }
}