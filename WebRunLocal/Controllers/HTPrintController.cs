using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using WebRunLocal.Filters;
using WebRunLocal.Managers;

namespace WebRunLocal.Controllers
{
    /// <summary>
    ///  华天打印 controller.
    /// </summary>
    [RoutePrefix("api/ht_auto")]
    public class HTPrintController : ApiController
    {
        [Route("print_inner")]
        [HttpPost]
        [ActionFilter]
        public IHttpActionResult PrintLabel([FromBody] HTItemInfo item)
        {
            List<string> names = new List<string>();
            List<string> values = new List<string>();

            if (!string.IsNullOrEmpty(item.AllData))
            {
                names.Add("AllData");
                values.Add(item.AllData);
            }
            if (!string.IsNullOrEmpty(item.BINCode))
            {
                names.Add("BINCode");
                values.Add(item.BINCode);
            }
             if (!string.IsNullOrEmpty(item.CardId))
            {
                names.Add("CardId");
                values.Add(item.CardId);
            }
             if (!string.IsNullOrEmpty(item.Code))
            {
                names.Add("Code");
                values.Add(item.Code);
            }
            if (!string.IsNullOrEmpty(item.CustomerCode))
            {
                names.Add("CustomerCode");
                values.Add(item.CustomerCode);
            }
            if (!string.IsNullOrEmpty(item.LabelCode))
            {
                names.Add("LabelCode");
                values.Add(item.LabelCode);
            }
            if (!string.IsNullOrEmpty(item.PrintTime))
            {
                names.Add("PrintTime");
                values.Add(item.PrintTime);
            }
            if (!string.IsNullOrEmpty(item.Qty))
            {
                names.Add("Qty");
                values.Add(item.Qty);
            }
            if (!string.IsNullOrEmpty(item.TesterId))
            {
                names.Add("TesterId");
                values.Add(item.TesterId);
            }
            if (!string.IsNullOrEmpty(item.package))
            {
                names.Add("package");
                values.Add(item.package);
            }
            if (!string.IsNullOrEmpty(item.package))
            {
                names.Add("package");
                values.Add(item.package);
            }
            if (!string.IsNullOrEmpty(item.WorkerName))
            {
                names.Add("WorkerName");
                values.Add(item.WorkerName);
            }

            if (string.IsNullOrEmpty(item.temp_type))
            {
                item.temp_type = "1";
            }

            if (item.temp_type == "1")
            {
                string sPathFolder = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                item.template_file = (sPathFolder + "\\plugins\\" + item.template_file);
            }

            bool success = WrlServiceManager.PrintLabel(item.template_file, item.printer_name, names, values, item.print_count);

            return Json(new { status = success ? $"1" : "0" });
        }


        [Route("print_test")]
        [HttpPost]
        [ActionFilter]
        public IHttpActionResult PrintTest([FromBody] NJHTItemInfo item)
        {
            List<string> names = new List<string>();
            List<string> values = new List<string>();
            if (string.IsNullOrEmpty(item.temp_type))
            {
                item.temp_type = "1";
            }

            if (string.IsNullOrEmpty(item.template_file))
            {
                item.template_file = "test.btw";
            }

            if (item.temp_type == "1")
            {
                string sPathFolder = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                item.template_file = (sPathFolder + "\\plugins\\" + item.template_file);
            }

            WrlServiceManager.PrintLabel(item.template_file, item.printer_name, names, values, item.print_count);

            return Json(new { status = $"ok" });
        }
    }

    public class HTItemInfo
    {
        public string printer_name { get; set; }
        public string template_file { get; set; }
        public string print_count { get; set; }
        public string temp_type { get; set; }

        public string AllData { get; set; }
        public string BINCode { get; set; }
        public string CardId { get; set; }
        public string Code { get; set; }
        public string CustomerCode { get; set; }
        public string LabelCode { get; set; }
        public string PrintTime { get; set; }
        public string Qty { get; set; }
        public string TesterId { get; set; }
        public string package { get; set; }
        public string WorkerName { get; set; }

    }

}
