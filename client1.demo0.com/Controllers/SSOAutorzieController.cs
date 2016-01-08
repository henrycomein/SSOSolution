
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSOLib;
namespace client1.demo0.com.Controllers
{
    [SSOAutorize("zpp7dhhca4iqpfutwjorjovddh+cb2f3", "http://sso.demo0.com/login", "http://sso.demo0.com/gettoken", "http://sso.demo0.com/getuser")]
    public class SSOAutorzieController : Controller
    {

    }
}
