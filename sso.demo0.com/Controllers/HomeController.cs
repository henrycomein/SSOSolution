using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using sso.demo0.com.Models;
using Newtonsoft.Json;
namespace sso.demo0.com.Controllers
{
public class HomeController : Controller
{
    public static List<SSOClients> clients = null;
    static HomeController()
    {
        LoadClients();
    }
    private static void LoadClients()
    {
        clients = new List<SSOClients>();
        var path = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\\Client.xml");
        XElement element = XElement.Load(path);
        var _clients = element.Elements("Client");
        foreach (var c in _clients)
        {
            var tempClient = new SSOClients();
            tempClient.ClientID = c.Attribute("ID").Value;
            tempClient.ClientName = c.Element("Name").Value;
            tempClient.ClientIcon = c.Element("Icon").Value;
            tempClient.ClientUrl = c.Element("Url").Value;
            tempClient.ClientKey = c.Element("Key").Value;
            tempClient.ClientStatus = int.Parse(c.Element("Status").Value);
            clients.Add(tempClient);
        }
    }
    public ActionResult Index()
    {
        return View(clients);
    }
    public ActionResult Login(string appkey)
    {
        var client = GetClient(appkey);
        return View(client);
    }
    [HttpPost]
    public JsonResult Login(string username, string appkey)
    {
        var objResult = new HandleDataResult();
        var returnUrl = string.Empty;
        var token = string.Empty;
        if (!string.IsNullOrWhiteSpace(username))
        {
            username = username.ToLower();
            var client = GetClient(appkey);
            token = Guid.NewGuid().ToString().ToLower().Replace("-", "");
            returnUrl = client == null ? Url.Action("Index") : client.ClientUrl + "?token=" + token;

            var _cookie = new HttpCookie("logon-token", CommonLib.DEncrypt.Encrypt(token));
            _cookie.Domain = "demo0.com";
            //_cookie.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Add(_cookie);
            //如果有必要 还可以检查改User目前状态是否为可用状态。不可用则移除Cookie以及Cache
            //增加有效期时间,并且认为此次请求不需要登录,
            var user = new { UserName= username ,Age=18};

            //CacheHelper.Normal.Add("logon-user-" + token, JsonConvert.SerializeObject(user));
            CacheHelper.Normal.Add("logon-user-" + token, user.UserName);
        }
        objResult.url = returnUrl;
        objResult.message = "登录成功";
        objResult.success = 1;

        return new NewtonJsonResult() { Data = objResult };
    }
    /// <summary>
    /// 如果获取到了UserName，则证明不需要登录，否则则需要登录
    /// </summary>
    /// <returns></returns>
    public string GetUser(string token)
    {
        //检查记录UserName Cookie是否存在，存在则证明User目前是登录状态;
        var userName = string.Empty;
        if (!string.IsNullOrEmpty(token))
        {
            token=token.ToLower();
            if (CacheHelper.Normal.Get("logon-user-" + token) != null) {
                userName = CacheHelper.Normal.Get("logon-user-" + token).ToString();
            }
        }
        return userName;
    }
    /// <summary>
    /// 获取Token
    /// </summary>
    /// <param name="appkey"></param>
    /// <returns></returns>
    public ActionResult GetToken(string appkey) {
        var client = GetClient(appkey);
        ActionResult resultPage = null;
        if (client == null)
        {
            resultPage = RedirectToAction("Login");
        }
        else { 
            var token=GetTokenByCookie();

            if (token.Length > 0)
            {
                //用户已登录过，直接返回
                resultPage= Redirect(client.ClientUrl + "?token=" + token);
            }
            else { 
            //跳转到登录页
                resultPage = RedirectToAction("Login", new { appkey = client.ClientKey });
            }
        }
        return resultPage;
    }
    /// <summary>
    /// 根据Appkey检测是否已被授权,已授权返回客户端地址
    /// </summary>
    /// <param name="appkey"></param>
    /// <returns></returns>
    private SSOClients GetClient(string appkey)
    {
        SSOClients __client = null;
        var url = string.Empty;
        if (!string.IsNullOrEmpty(appkey))
        {
            appkey = appkey.ToLower();
            __client = clients.FirstOrDefault(e => e.ClientKey == appkey);
        }
        return __client;
    }
    private string GetTokenByCookie() { 
        if(Request.Cookies["logon-token"] !=null){
            try
            {
                return CommonLib.DEncrypt.Decrypt(Request.Cookies["logon-token"].Value);
            }
            catch {
                return string.Empty;
            }
        }
        return string.Empty;
    }
}
}
