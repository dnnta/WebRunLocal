using Newtonsoft.Json.Linq;
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
    ///  Hello controller.
    /// </summary>
    [RoutePrefix("api/print")]
    public class PrintController : ApiController
    {
        [Route("remote")]
        [HttpPost]
        [ActionFilter]
        public IHttpActionResult PrintItem([FromBody] JObject jsonParam)
        {
            string printer_name = jsonParam["printer_name"].ToString();
            string file_name = jsonParam["template_file"].ToString();
            string print_count = jsonParam["print_count"].ToString();
            string sPathFolder = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            file_name = sPathFolder + "\\plugins\\" + file_name;

            List<string> names = new List<string>();
            List<string> values = new List<string>();

            foreach (var item in jsonParam)
            {
                string key = item.Key.ToString();
                if (key.Equals("printer_name") || key.Equals("template_file") || key.Equals("print_count"))
                {
                    continue;
                }
                names.Add(item.Key.ToString());
                values.Add(item.Value.ToString());
            }
            WrlServiceManager.PrintLabel(file_name, printer_name, names, values, print_count);

            return Json(new { status = $"ok" });
        }

        [Route("print_inner")]
        [HttpPost]
        [ActionFilter]
        public IHttpActionResult PrintItem([FromBody] ItemInfo item)
        {
            List<string> names = new List<string>();
            List<string> values = new List<string>();

            if (!string.IsNullOrEmpty(item.cust_po))
            {
                names.Add("cust_po");
                values.Add(item.cust_po);
            }
            if (!string.IsNullOrEmpty(item.line_no))
            {
                names.Add("line_no");
                values.Add(item.line_no);
            }
             if (!string.IsNullOrEmpty(item.manuf))
            {
                names.Add("manuf");
                values.Add(item.manuf);
            }
             if (!string.IsNullOrEmpty(item.manuf_no))
            {
                names.Add("manuf_no");
                values.Add(item.manuf_no);
            }
            if (!string.IsNullOrEmpty(item.product_name))
            {
                names.Add("product_name");
                values.Add(item.product_name);
            }
            if (!string.IsNullOrEmpty(item.count))
            {
                names.Add("count");
                values.Add(item.count);
            }
            if (!string.IsNullOrEmpty(item.datecode))
            {
                names.Add("datecode");
                values.Add(item.datecode);
            }
            if (!string.IsNullOrEmpty(item.expdate))
            {
                names.Add("expdate");
                values.Add(item.expdate);
            }
            if (!string.IsNullOrEmpty(item.wd))
            {
                names.Add("wd");
                values.Add(item.wd);
            }
            if (!string.IsNullOrEmpty(item.sd))
            {
                names.Add("sd");
                values.Add(item.sd);
            }
            if (!string.IsNullOrEmpty(item.lot))
            {
                names.Add("lot");
                values.Add(item.lot);
            }
            if (!string.IsNullOrEmpty(item.lot_str))
            {
                names.Add("lot_str");
                values.Add(item.lot_str);
            }
            if (!string.IsNullOrEmpty(item.erp_lot))
            {
                names.Add("erp_lot");
                values.Add(item.erp_lot);
            }
            if (!string.IsNullOrEmpty(item.big_code))
            {
                names.Add("big_code");
                values.Add(item.big_code);
            }
            if (!string.IsNullOrEmpty(item.adh))
            {
                names.Add("adh");
                values.Add(item.adh);
            }
            if (!string.IsNullOrEmpty(item.adh_lot))
            {
                names.Add("adh_lot");
                values.Add(item.adh_lot);
            }
            if (!string.IsNullOrEmpty(item.adh_datecode))
            {
                names.Add("adh_datecode");
                values.Add(item.adh_datecode);
            }
            if (!string.IsNullOrEmpty(item.adh_expcode))
            {
                names.Add("adh_expcode");
                values.Add(item.adh_expcode);
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

            WrlServiceManager.PrintLabel(item.template_file, item.printer_name, names, values, item.print_count);

            return Json(new { status = $"ok" });
        }

        [Route("print_inner2")]
        [HttpPost]
        [ActionFilter]
        public IHttpActionResult PrintItem2([FromBody] ItemInfo item)
        {
            List<string> names = new List<string>();
            List<string> values = new List<string>();

           
            if (!string.IsNullOrEmpty(item.line_no))
            {
                //names.Add("line_no");
                //values.Add(item.line_no);
            }
            if (!string.IsNullOrEmpty(item.manuf))
            {
                names.Add("manuf");
                values.Add(item.manuf);
            }

            if (!string.IsNullOrEmpty(item.lot_str))
            {
                names.Add("lot_str");
                values.Add(item.lot_str);
            }

            if (!string.IsNullOrEmpty(item.product_name))
            {
                names.Add("product_name");
                values.Add(item.product_name);
            }
            if (!string.IsNullOrEmpty(item.count))
            {
                names.Add("count");
                values.Add(item.count);
            }
            
            if (!string.IsNullOrEmpty(item.expdate))
            {
                names.Add("expdate");
                values.Add(item.expdate);
            }

            if (!string.IsNullOrEmpty(item.wd))
            {
                names.Add("wd");
                values.Add(item.wd);
            }
            if (!string.IsNullOrEmpty(item.sd))
            {
                names.Add("sd");
                values.Add(item.sd);
            }
            if (!string.IsNullOrEmpty(item.lot))
            {
                names.Add("lot");
                values.Add(item.lot);
            }
            
            if (!string.IsNullOrEmpty(item.big_code))
            {
                names.Add("big_code");
                values.Add(item.big_code);
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

            WrlServiceManager.PrintLabel(item.template_file, item.printer_name, names, values, item.print_count);

            return Json(new { status = $"ok" });
        }

        [Route("print_out")]
        [HttpPost]
        [ActionFilter]
        public IHttpActionResult PrintBoxItem([FromBody] ItemInfo item)
        {
            List<string> names = new List<string>();
            List<string> values = new List<string>();

            if (!string.IsNullOrEmpty(item.cust_po))
            {
                names.Add("cust_po");
                values.Add(item.cust_po);
            }
            if (!string.IsNullOrEmpty(item.line_no))
            {
                names.Add("line_no");
                values.Add(item.line_no);
            }
             if (!string.IsNullOrEmpty(item.manuf))
            {
                names.Add("manuf");
                values.Add(item.manuf);
            }
             if (!string.IsNullOrEmpty(item.manuf_no))
            {
                names.Add("manuf_no");
                values.Add(item.manuf_no);
            }
            if (!string.IsNullOrEmpty(item.product_name))
            {
                names.Add("product_name");
                values.Add(item.product_name);
            }
            if (!string.IsNullOrEmpty(item.count))
            {
                names.Add("count");
                values.Add(item.count);
            }
            if (!string.IsNullOrEmpty(item.datecode))
            {
                names.Add("datecode");
                values.Add(item.datecode);
            }
            if (!string.IsNullOrEmpty(item.expdate))
            {
                names.Add("expdate");
                values.Add(item.expdate);
            }
            if (!string.IsNullOrEmpty(item.wd))
            {
                names.Add("wd");
                values.Add(item.wd);
            }
            if (!string.IsNullOrEmpty(item.sd))
            {
                names.Add("sd");
                values.Add(item.sd);
            }
            if (!string.IsNullOrEmpty(item.lot))
            {
                names.Add("lot");
                values.Add(item.lot);
            }
            if (!string.IsNullOrEmpty(item.erp_lot))
            {
                names.Add("erp_lot");
                values.Add(item.erp_lot);
            }
            if (!string.IsNullOrEmpty(item.big_code))
            {
                names.Add("big_code");
                values.Add(item.big_code);
            }
            if (!string.IsNullOrEmpty(item.adh))
            {
                names.Add("adh");
                values.Add(item.adh);
            }
            if (!string.IsNullOrEmpty(item.adh_lot))
            {
                names.Add("adh_lot");
                values.Add(item.adh_lot);
            }
            if (!string.IsNullOrEmpty(item.adh_datecode))
            {
                names.Add("adh_datecode");
                values.Add(item.adh_datecode);
            }
            if (!string.IsNullOrEmpty(item.adh_expcode))
            {
                names.Add("adh_expcode");
                values.Add(item.adh_expcode);
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

            WrlServiceManager.PrintLabel(item.template_file, item.printer_name, names, values, item.print_count);

            return Json(new { status = $"ok" });
        }

        [Route("print_batch")]
        [HttpPost]
        [ActionFilter]
        public IHttpActionResult PrintBatch([FromBody] ItemInfo item)
        {
            List<string> names = new List<string>();
            List<string> values = new List<string>();

           
            if (!string.IsNullOrEmpty(item.lot))
            {
                names.Add("lot");
                values.Add(item.lot);
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

            WrlServiceManager.PrintLabel(item.template_file, item.printer_name, names, values, item.print_count);

            return Json(new { status = $"ok" });
        }

        [Route("print_expdate")]
        [HttpPost]
        [ActionFilter]
        public IHttpActionResult PrintExpDate([FromBody] ItemInfo item)
        {
            List<string> names = new List<string>();
            List<string> values = new List<string>();


            if (!string.IsNullOrEmpty(item.expdate))
            {
                names.Add("expdate");
                values.Add(item.expdate);
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

            WrlServiceManager.PrintLabel(item.template_file, item.printer_name, names, values, item.print_count);

            return Json(new { status = $"ok" });
        }
    }


    public class ItemInfo
    {
        public string printer_name { get; set; }
        public string template_file { get; set; }
        public string print_count { get; set; }

        public string cust_po { get; set; }
        public string line_no { get; set; }
        public string product_name { get; set; }
        public string count { get; set; }
        public string manuf { get; set; }
        public string manuf_no { get; set; }

        public string datecode { get; set; }
        public string expdate { get; set; }
        public string wd { get; set; }
        public string sd { get; set; }
        public string lot { get; set; }
        public string lot_str { get; set; }

        public string erp_lot { get; set; }
        public string big_code { get; set; }
        public string adh { get; set; }
        public string adh_lot { get; set; }
        public string adh_datecode { get; set; }
        public string adh_expcode { get; set; }

        // 模板类型，指定模板名称的格式
        // 1: template_file 文件名 test.btw（默认）
        // 2: template_file 带全路径的文件名 c:\\print\\test.btw
        public string temp_type { get; set; }

    }

}
