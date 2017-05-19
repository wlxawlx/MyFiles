using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using log4net;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OracleClient;
using System.Net;
using System.IO;

namespace NipicTask
{
    public class MyJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger("log");
        public void Execute(IJobExecutionContext context)
        {
            MyFun.Start();
        }


        public class MyFun
        {

            #region 调用webservice接口
            /// <summary>
            /// 
            /// </summary>
            /// <param name="url"></param>
            /// <param name="param"></param>
            public static void Methold(string url, string postDatas)
            {
                #region 例子1
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); //--需要封装的参数 
                request.CookieContainer = new CookieContainer();
                //以下是发送的http头 
                request.Referer = "";
                request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
                request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
                request.KeepAlive = true;
                //上面的http头看情况而定，但是下面俩必须加  
                request.ContentType = "application/x-www-form-urlencoded";
                Encoding encoding = Encoding.UTF8;//根据网站的编码自定义
                request.Method = "POST";  //--需要将get改为post才可行
                string postDataStr;
                Stream requestStream = request.GetRequestStream();
                if (postDatas != "")
                {
                    postDataStr = postDatas;//--需要封装,形式如“arg=arg1&arg2=arg2” 
                    byte[] postData = encoding.GetBytes(postDataStr);//postDataStr即为发送的数据，
                    //request.ContentLength = postData.Length;
                    requestStream.Write(postData, 0, postData.Length);
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                string retString = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                #endregion
            }
            #endregion

            #region 获取所有已发送的商户通知
            public static List<Ghtz> GetDataList()
            {
                List<Ghtz> values = new List<Ghtz>();
                string sqlQuery = string.Format("select * from ZFB_GHTZ ");
                string connStr2 = ConfigurationManager.ConnectionStrings["oracleStr"].ToString();
                using (OracleConnection conn = new OracleConnection(connStr2))
                {
                    conn.Open();
                    OracleCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sqlQuery;
                    OracleDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Ghtz result = new Ghtz();
                            result.OpenId = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                            result.Ghid = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                            result.Xh = !dr.IsDBNull(2) ? dr.GetDecimal(2).ToString() : " ";
                            values.Add(result);
                        }
                    }
                    conn.Close();
                }
                return values;
            }
            #endregion

            #region 记录发送的商户通知
            public static int InsertGhtz(string openid, string ghid, string xh)
            {
                string sqlQuery = string.Format(" insert into ZFB_GHTZ(openid,ghid,xh) values('{0}','{1}','{2}') ", openid, ghid, xh);
                string connStr2 = ConfigurationManager.ConnectionStrings["oracleStr"].ToString();
                int result = 0;
                using (OracleConnection conn = new OracleConnection(connStr2))
                {
                    conn.Open();
                    OracleCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sqlQuery;
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return result;
            }
            #endregion

            public static void Start()

            {
                try
                {
                    logger.Info("MyJob       推送消息程序被调用");

                    #region 根据医生名称和科室名称进行分组
                    string connStr = ConfigurationManager.ConnectionStrings["sqlStr"].ToString();
                    List<Info> array = new List<Info>();
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = "select doctorname,departname from zfb_paiduihujiao group by doctorname,departname ";
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Info entity = new Info();
                                entity.DoctorName = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                                entity.DepartName = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                                array.Add(entity);
                            }
                        }
                        conn.Close();
                    }
                    #endregion

                    #region 获取每个分组的前三条数据
                    List<PaiDuiInfo> dataList = new List<PaiDuiInfo>();
                    int paiXu = 0;
                    foreach (var item in array)
                    {
                        paiXu = 0;
                        using (SqlConnection conn = new SqlConnection(connStr))
                        {
                            conn.Open();
                            SqlCommand cmd = conn.CreateCommand();
                            cmd.CommandText = string.Format("select top 3 preengageno,queueseq,doctorname,departname,shiftname from zfb_paiduihujiao where doctorname='{0}' and departname='{1}' and queuestate=0 order by preengageno", item.DoctorName, item.DepartName);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    paiXu++;
                                    PaiDuiInfo entity = new PaiDuiInfo();
                                    entity.PaiXu = paiXu;
                                    entity.Preengageno = !dr.IsDBNull(0) ? dr.GetDouble(0).ToString() : " ";
                                    entity.Queueseq = !dr.IsDBNull(1) ? dr.GetString(1) : " ";//1000547168
                                    entity.DoctorName = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                                    entity.DepartName = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                                    entity.ShiftName = !dr.IsDBNull(4) ? dr.GetString(4) : " ";
                                    dataList.Add(entity);
                                }
                            }
                            conn.Close();
                        }
                    }
                    #endregion

                    #region 根据sqlserver中的queueseq 和oracle中的yyy_yueyue中的registerid相等查出数据

                    List<ResultModel> dataList1 = new List<ResultModel>();
                    foreach (var item in dataList)
                    {
                        string sqlQuery = string.Format("select openid,patientname,place,sourcetype from yyy_yueyue where registerid='{0}' ", item.Queueseq);
                        string connStr2 = ConfigurationManager.ConnectionStrings["oracleStr"].ToString();
                        using (OracleConnection conn = new OracleConnection(connStr2))
                        {
                            conn.Open();
                            OracleCommand cmd = conn.CreateCommand();
                            cmd.CommandText = sqlQuery;
                            OracleDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ResultModel result = new ResultModel();
                                    result.OpenId = !dr.IsDBNull(0) ? dr.GetString(0) : " ";
                                    result.Pdhm = item.PaiXu.ToString();
                                    result.Brxm = !dr.IsDBNull(1) ? dr.GetString(1) : " ";
                                    result.Place = !dr.IsDBNull(2) ? dr.GetString(2) : " ";
                                    result.SourceType = !dr.IsDBNull(3) ? dr.GetString(3) : " ";
                                    result.Ysxm = item.DoctorName;
                                    result.Preengageno = item.Preengageno;
                                    result.ShiftName = item.ShiftName;
                                    result.DepartName = item.DepartName;
                                    result.Queueseq = item.Queueseq;
                                    dataList1.Add(result);
                                }
                            }
                            conn.Close();
                        }
                    }
                    #endregion

                    #region 发送推送消息

                    logger.Info(dataList1.Count);
                    foreach (ResultModel item in dataList1)
                    {
                        string ghip = ConfigurationManager.ConnectionStrings["ghip"].ToString();
                        string formatSql = ghip;
                        string param = "pcode=LM010102&content={lines:[{\"openid\":\"" + item.OpenId + "\",\"pdhm\":\"" + item.Pdhm + "\",\"brxm\":\"" + item.Brxm + "\",\"ysxm\":\"" + item.Ysxm + "\",\"preengageno\":\"" + item.Preengageno + "\",\"shiftname\":\"" + item.ShiftName + "\",departname:\"" + item.DepartName + "\",\"sourcetype\":\"" + item.SourceType + "\",\"place\":\"" + item.Place + "\"}]}";

                        List<Ghtz> ghtzList = GetDataList();
                        if (ghtzList.Where(r => r.Xh == item.Pdhm && r.OpenId == item.OpenId && r.Ghid == item.Queueseq).FirstOrDefault() == null)
                        {
                            logger.Info("openid:" + item.OpenId + "queueseq" + item.Queueseq + "pdhm" + item.Pdhm);
                            Methold(formatSql, param);
                            InsertGhtz(item.OpenId, item.Queueseq, item.Pdhm);
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {

                    logger.Info(ex.Message);
                }



            }
        }
    }

    #region Model

    public class Ghtz
    {
        public string OpenId { get; set; }

        public string Ghid { get; set; }

        public string Xh { get; set; }
    }

    public class ResultModel
    {
        public string OpenId { get; set; }

        public string Pdhm { get; set; }

        public string Brxm { get; set; }

        public string Ysxm { get; set; }

        public string Preengageno { get; set; }

        public string ShiftName { get; set; }

        public string DepartName { get; set; }

        public string Place { get; set; }

        public string SourceType { get; set; }

        public string Queueseq { get; set; }
    }

    class PaiDuiInfo
    {
        public string Preengageno { get; set; }
        public int PaiXu { get; set; }
        public string DoctorName { get; set; }
        public string DepartName { get; set; }
        public string Queueseq { get; set; }
        public string ShiftName { get; set; }
    }

    class Info
    {
        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 房间
        /// </summary>
        public string DepartName { get; set; }
    }
}
    #endregion
