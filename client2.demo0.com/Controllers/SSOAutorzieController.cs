
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSOLib;
namespace client2.demo0.com.Controllers
{
    [SSOAutorize("rl1rzsq/h3iqpfutwjorjovddh+cb2f3", "http://sso.demo0.com/login", "http://sso.demo0.com/gettoken", "http://sso.demo0.com/getuser")]
    public class SSOAutorzieController : Controller
    {

    }
}
