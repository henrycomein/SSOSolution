using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonLib;
namespace client2.demo0.com.Controllers
{
    public class HomeController : SSOAutorzieController
    {
        public string Index()
        {
            if (Session["username"] != null)
                return Session["username"].ToString() + ",this is the index page for client2";
            return "not logon";
        }
    }
}
