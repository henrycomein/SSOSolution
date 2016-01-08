using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;

namespace sso.demo0.com
{
    public class NewtonJsonResult:JsonResult
    {
        public NewtonJsonResult(bool allowGet = false)
         {
             if (allowGet) this.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
             else this.JsonRequestBehavior = JsonRequestBehavior.DenyGet;
             //Settings = new JsonSerializerSettings
             //{
             //    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
             //};
         }

         //public JsonSerializerSettings Settings { get; private set; }
 
         public override void ExecuteResult(ControllerContext context)
         {
             if (context == null)
                 throw new ArgumentNullException("context canot be null!");
             if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals    (context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                 throw new InvalidOperationException("JSON GET is not allowed");
 
             HttpResponseBase response = context.HttpContext.Response;
             response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;
             //if (this.ContentEncoding != null) 
             response.ContentEncoding = this.ContentEncoding ?? System.Text.Encoding.UTF8;
             if (this.Data == null) return;
 
             //var scriptSerializer = JsonSerializer.Create(this.Settings);
             //using (var sw = new StringWriter())
             //{
             //    scriptSerializer.Serialize(sw, this.Data);
             //    response.Write(sw.ToString());
             //}

             var content = JsonConvert.SerializeObject(this.Data);
             response.Write(content);
         }
    }
}