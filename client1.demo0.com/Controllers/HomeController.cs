using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using CommonLib;
namespace client1.demo0.com.Controllers
{

    public class HomeController : SSOAutorzieController
    {
        public string Index()
        {
            FileLogger.WriteLine("Index Page");
            if (Session["username"] != null)
                return Session["username"].ToString() + ",this is the index page for client1";
            return "not logon";
        }

    }
}
