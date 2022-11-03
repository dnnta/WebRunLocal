using NetFwTypeLib;
using Seagull.BarTender.Print;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WebRunLocal.Services;
using WebRunLocal.Utils;

namespace WebRunLocal.Managers
{
    public class WrlServiceManager
    {

        private static HttpService _http;
        public static string autoUpdateExePath = Path.Combine(Application.StartupPath, "WRL自动更新.exe");
        public static Engine printEngine;
        private static LabelFormatDocument btFormatDoc { get; set; }


        /// <summary>
        /// 启动WRL服务并进行初始化操作
        /// </summary>
        public static async void StartWrlServiceAsync()
        {
            string lisenerPort = ConfigurationManager.AppSettings["ListenerPort"].ToString();
            var port = Convert.ToInt32(lisenerPort);
            _http = new HttpService(port);
            await _http.StartHttpServer();

            //创建系统所需要的目录
            DirectoryInfo fi = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Plugins"));
            if (!fi.Exists)
            {
                fi.Create();
            }

            fi = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Log"));
            if (!fi.Exists)
            {
                fi.Create();
            }

            //设置软件自启动
            AutoStartByRegistry.SetMeStart(bool.Parse(ConfigurationManager.AppSettings["AutoStart"]));

            //创建桌面快捷方式
            if (bool.Parse(ConfigurationManager.AppSettings["DesktopLnk"].ToString()))
            {
                AppLnkUtil.CreateDesktopQuick();
            }

            //将监听端口的端口添加到防火墙例外
            FireWallUtil.NetFwAddPorts("TYX-Print", int.Parse(lisenerPort), NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);

            //清理日志文件
            int retainLogDays = Convert.ToInt32(ConfigurationManager.AppSettings["RetainLogDays"]);
            FileUtil.DeleteFiles(Path.Combine(Directory.GetCurrentDirectory(), @"Log"), retainLogDays);

            //是否启动自动更新
            //if (bool.Parse(ConfigurationManager.AppSettings["AutoUpdate"]) && File.Exists(autoUpdateExePath))
            //{
            //    ProcessUtil.StartExe(autoUpdateExePath);
            //}

            printEngine = new Engine(true);

        }

        private static bool PrintLabelStart(string sLabel, string sPrinter)
        {
            try
            {

                printEngine.Start();

                //加载标签模板，指定打印机
                btFormatDoc = printEngine.Documents.Open(sLabel, sPrinter);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private static bool PrintLabelEnd()
        {
            try
            {
                //关闭标签模板
                printEngine.Stop();
               
                return true;
            }
            catch (Exception ex)
            {
                
                printEngine.Stop();
                return false;
            }
        }

        public static bool PrintLabel(string sLabel, string sPrinter, List<string> lstSubStringName, List<string> lstValue, string print_count = "1")
        {
            try
            {
                //开始打印
                if (!PrintLabelStart(sLabel, sPrinter)) return false;


                for (int iName = 0; iName < lstSubStringName.Count; iName++)
                {
                    btFormatDoc.SubStrings[lstSubStringName[iName]].Value = lstValue[iName];
                }

                if (string.IsNullOrEmpty(print_count))
                {
                    print_count = "1";
                }

                int pCount = Int32.Parse(print_count);
                btFormatDoc.PrintSetup.IdenticalCopiesOfLabel = pCount;

                Result result = btFormatDoc.Print();

                btFormatDoc.Close(SaveOptions.DoNotSaveChanges);

                return result == Result.Success;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }
}
