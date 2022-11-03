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
    ///  杨杰打印 controller.
    /// </summary>
    [RoutePrefix("api/yj_auto")]
    public class YJPrintController : ApiController
    {
        [Route("print_inner")]
        [HttpPost]
        [ActionFilter]
        public IHttpActionResult PrintLabel([FromBody] YJItemInfo item)
        {
            List<string> names = new List<string>();
            List<string> values = new List<string>();

            if (!string.IsNullOrEmpty(item.pn))
            {
                names.Add("PN");
                values.Add(item.pn);
            }
            if (!string.IsNullOrEmpty(item.seq))
            {
                names.Add("SEQ");
                values.Add(item.seq);
            }
             if (!string.IsNullOrEmpty(item.eqp))
            {
                names.Add("EQP");
                values.Add(item.eqp);
            }
             if (!string.IsNullOrEmpty(item.qty))
            {
                names.Add("REEL_QTY");
                values.Add(item.qty);
            }
            if (!string.IsNullOrEmpty(item.laser_mark))
            {
                names.Add("LASER_MARK");
                values.Add(item.laser_mark);
            }
            if (!string.IsNullOrEmpty(item.dc))
            {
                names.Add("DC");
                values.Add(item.dc);
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
        public IHttpActionResult PrintTest([FromBody] YJItemInfo item)
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


    public class YJItemInfo
    {
        public string printer_name { get; set; }
        public string template_file { get; set; }
        public string print_count { get; set; }

        // 模板类型，指定模板名称的格式
        // 1: template_file 文件名 test.btw（默认）
        // 2: template_file 带全路径的文件名 c:\\print\\test.btw
        public string temp_type { get; set; } 

        public string eqp { get; set; }
        public string laser_mark { get; set; }
        public string lot_no { get; set; }
        public string pn { get; set; }
        public string qty { get; set; }
        public string seq { get; set; }
        public string dc { get; set; }
    }

}
