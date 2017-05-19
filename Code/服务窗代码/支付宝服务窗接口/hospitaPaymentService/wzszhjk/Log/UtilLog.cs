using System;
using System.IO;
using System.Text;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;

namespace HospitaPaymentService.Wzszhjk.Log
{
    /// <summary>
    /// 单例的日志类  不支持多线程  记录订单日志信息和错误信息
    /// </summary>
    public class UtilLog
    {
        private static UtilLog _utilLog;

        private UtilLog()
        {
        }

        public static UtilLog GetInstance()
        {
            if (null == _utilLog)
            {
                _utilLog = new UtilLog();
            }

            return _utilLog;
        }

        /// <summary>
        /// writeProgramLogFlag 是否记录程序日志
        /// true: 记录  false: 不记录
        /// </summary>
        private bool _writeProgramLogFlag;
        public bool WriteProgramLogFlag
        {
            get { return _writeProgramLogFlag; }
            set { _writeProgramLogFlag = value; }
        }

        /// <summary>
        /// 订单日志文件名
        /// </summary>
        private string _orderLogFile;
        public string OrderLogFile
        {
            get { return _orderLogFile; }
            set { _orderLogFile = value; }
        }

        /// <summary>
        /// 订单日志路径
        /// </summary>
        private string _orderLogPath;
        public string OrderLogPath
        {
            get { return _orderLogPath; }
            set { _orderLogPath = value; }
        }

        /// <summary>
        /// 记录订单日志
        /// </summary>
        /// <param name="type">订单类型</param>
        /// <param name="msg">消息体</param>
        public void WriteOrderLog(string type, string msg)
        {
            System.Object lockThis = new System.Object();

            lock (lockThis)
            {
                try
                {
                    InitFileinfo();
                    FileInfo file = OpenFile(_orderLogPath, _orderLogFile);
                    StreamWriter sw = file.AppendText();

                    sw.WriteLine(Convert.ToString(DateTime.Now) + ": " + "--type--" + type + " ; --message--" + msg + " ;");
                    sw.Flush();
                    sw.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[日志写入失败]" + ex.Message + ":" + ex.Source);
                }
            }
        }

        /**
       *  输入参数 fullPath(文件所在的全路径） fileName(包含扩展名的文件全称)
       * 
       */
        public FileInfo OpenFile(string fullPath, string fileName)
        {
            DateTime dt = DateTime.Now;
            FileInfo info = new FileInfo(fullPath + "\\" + fileName);
            DirectoryInfo dir = new DirectoryInfo(fullPath);
            if (!dir.Exists)
            {
                //创建路径
                dir.Create();
            }
            dir.Refresh();
            if (!dir.Exists)
            {
                Console.WriteLine("Make Directory False!!");
                return null;
            }
            if (!info.Exists)
            {
                //创建文件
                FileStream stream = info.Create();

                stream.Close();
            }

            info.Refresh();
            if (!info.Exists)
            {
                Console.WriteLine("Make File False!!");
                return null;
            }

            return info;
        }

        private void InitFileinfo()
        {
            _orderLogPath = WebConfigParameter.OrderLogPath;
            _orderLogFile = WebConfigParameter.OrderLogPrefName + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="name">类名 +　函数名</param>
        /// <param name="error">错误信息</param>
        public void WriteProgramLog(string name, string error)
        {
            if (!_writeProgramLogFlag)
            {
                return;
            }

            FileInfo info = CreatProgramLogFile();
            if (null != info)
            {
                StreamWriter sw = info.AppendText();

                sw.WriteLine("Error: " + Convert.ToString(DateTime.Now) + " className: " + name +  " : " + error);
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="name">类名 +　函数名</param>
        /// <param name="ex">异常</param>
        public void WriteProgramLog(string name, Exception ex)
        {
            if (!_writeProgramLogFlag)
            {
                return;
            }

            FileInfo info = CreatProgramLogFile();
            if (null != info)
            {
                StreamWriter sw = info.AppendText();

                sw.WriteLine("Error: " + Convert.ToString(DateTime.Now) + ": " + " className: " + name + " exception: " +
                    ex.Message);
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// 创建错误信息文件
        /// </summary>
        /// <returns></returns>
        private FileInfo CreatProgramLogFile()
        {
            try
            {
                DateTime dt = DateTime.Now;

                string path = HPService.GetFilePath();
                string fileName = path + "/error" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                FileInfo info = new FileInfo(fileName);
                if (info.Exists && info.Length > 5 * 1024 * 1024)
                {
                    info.Delete();
                }
                if (!info.Exists)
                {
                    FileStream stream = info.Create();
                    stream.Close();
                }

                info.Refresh();

                return info;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 读取程序日志
        /// </summary>
        /// <returns></returns>
        public string ReadProgramLog()
        {
            string path = HPService.GetFilePath();
            string log = System.IO.File.ReadAllText(path + "/error" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

            return log;
        }

        /// <summary>
        /// 写日志 (不知道以前用在什么地方的）
        /// </summary>
        /// <param name="action"></param>
        /// <param name="strMessage"></param>
        /// <param name="time"></param>
        public static void WriteTextLog(string action, string strMessage, DateTime time)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Log\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string fileFullPath = path + time.ToString("yyyy-MM-dd") + ".txt";
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + time.ToString() + "\r\n");
            str.Append("Action:  " + action + "\r\n");
            str.Append("Message: " + strMessage + "\r\n");
            str.Append("---------------------------------------------------------------------------\r\n");
            StreamWriter sw;
            if (!File.Exists(fileFullPath))
            {
                sw = File.CreateText(fileFullPath);
            }
            else
            {
                sw = File.AppendText(fileFullPath);
            }
            sw.WriteLine(str.ToString());
            sw.Close();
        }
    }
}