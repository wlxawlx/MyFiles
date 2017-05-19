using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using HospitaPaymentService.Wzszhjk.Common.Webservice;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;

namespace HospitaPaymentService.Wzszhjk.DAL.Webservice
{
    public class ReportInfoHandleForWZSZYY : BaseDB
    {
        public int ReportInfoList(string brxm, string lxdh, string startTime, string endTime, out ArrayList headlist, out string msg)
        {
            int result = -99;
            msg = "";
            headlist = new ArrayList();
            WebserviceOpe _webservice;
            _webservice = new WebserviceOpe();

            try
            {
                Object[] obj = new Object[4];
                obj[0] = brxm;
                obj[1] = lxdh;
                obj[2] = startTime;
                obj[3] = endTime;

                string _webResult = _webservice.WebserviceRetInfo(WebConfigParameter.WebserviceUrl, 
                    WebConfigParameter.WebserviceClassName, "getrequestno", obj);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(_webResult);

                XmlNode data = doc.SelectSingleNode("data");
                if (null == data)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                XmlNode stateNode = data.SelectSingleNode("state");
                if (null == stateNode)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                int stateResult = Convert.ToInt32(stateNode.InnerText);
                if (stateResult != 1)
                {
                    if (stateResult == 0)
                    {
                        XmlNode resultError = data.SelectSingleNode("result");
                        if (null == resultError)
                        {
                            msg = "调用后台WEBSERVICE出错";
                            return -1;
                        }
                        else
                        {
                            msg = resultError.InnerText;
                        }
                        return 2;
                    }
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                XmlNode resultNode = data.SelectSingleNode("result");
                if (null == resultNode)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                XmlNodeList itemNodes = resultNode.SelectNodes("item");

                if (null == itemNodes)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                foreach (XmlNode itemNode in itemNodes)
                {
                    ReportInfo reportInfo = new ReportInfo();
                    string _bgdh = "";
                    string _sjmd = "";
                    foreach (XmlNode childNode in itemNode.ChildNodes)
                    {
                        string name = childNode.Name;

                        switch (name)
                        {
                            case "ybbh":
                                _bgdh = childNode.InnerText;
                                break;
                            case "csxm":
                                _sjmd = childNode.InnerText;
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(_bgdh))
                    {
                        reportInfo.bgdh = _bgdh;
                        reportInfo.sjmd = _sjmd;
                        reportInfo.jysj = _bgdh.Substring(0, 8);
                    }

                    headlist.Add(reportInfo);
                }

                result = 0;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
            }
            return result;
        }

        public int ReportDetailInfoList(string bgdh, string brxm, string lxdh, string startTime, string endTime, out ArrayList headlist, out string msg)
        {
            int result = -99;
            msg = "";
            headlist = new ArrayList();
            WebserviceOpe _webservice;
            _webservice = new WebserviceOpe();

            try
            {
                Object[] obj = new Object[4];
                obj[0] = brxm;
                obj[1] = lxdh;
                obj[2] = startTime;
                obj[3] = endTime;

                string _webResult = _webservice.WebserviceRetInfo(WebConfigParameter.WebserviceUrl,
                    WebConfigParameter.WebserviceClassName, "getrequestno", obj);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(_webResult);

                XmlNode data = doc.SelectSingleNode("data");
                if (null == data)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                XmlNode stateNode = data.SelectSingleNode("state");
                if (null == stateNode)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                int stateResult = Convert.ToInt32(stateNode.InnerText);
                if (stateResult != 1)
                {
                    if (stateResult == 0)
                    {
                        XmlNode resultError = data.SelectSingleNode("result");
                        if (null == resultError)
                        {
                            msg = "调用后台WEBSERVICE出错";
                            return -1;
                        }
                        else
                        {
                            msg = resultError.InnerText;
                        }
                        return 2;
                    }
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                XmlNode resultNode = data.SelectSingleNode("result");
                if (null == resultNode)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                XmlNodeList itemNodes = resultNode.SelectNodes("item");

                if (null == itemNodes)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                foreach (XmlNode itemNode in itemNodes)
                {
                    ReportInfo reportInfo = new ReportInfo();
                    string _bgdh = "";
                    string _sjmd = "";
                    foreach (XmlNode childNode in itemNode.ChildNodes)
                    {
                        string name = childNode.Name;

                        switch (name)
                        {
                            case "ybbh":
                                _bgdh = childNode.InnerText;
                                break;
                            case "csxm":
                                _sjmd = childNode.InnerText;
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(_bgdh) && _bgdh.Equals(bgdh))
                    {
                        ReportInfo _info;
                        if (Patientinfo(_bgdh, out _info, out msg) == 0)
                        {
                            reportInfo = _info;
                            reportInfo.bgdh = _bgdh;
                            reportInfo.sjmd = _sjmd;
                        }
                        else
                        {
                            return 2;
                        }

                        ArrayList _list = new ArrayList();
                        if (DetailInfo(_bgdh, out  _list, out  msg) == 0)
                        {
                            if (_list.Count > 0)
                            {
                                ICollection<ReportDetail> rds = new List<ReportDetail>();
                                foreach (ReportDetail _detailInfo in _list)
                                {
                                    rds.Add(_detailInfo);
                                }
                                reportInfo.details = rds;
                            }
                        }
                        else
                        {
                            return 2;
                        }
                        headlist.Add(reportInfo);
                    }
                }

                result = 0;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
            }
            return result;
        }

        private int Patientinfo(string bgdh, out ReportInfo reportInfo, out string msg)
        {
            int result = -99;
            msg = "";
            reportInfo = new ReportInfo();
            WebserviceOpe _webservice;
            _webservice = new WebserviceOpe();
            try
            {
                Object[] obj = new Object[1];
                obj[0] = bgdh;

                string _webResult = _webservice.WebserviceRetInfo(WebConfigParameter.WebserviceUrl,
                    WebConfigParameter.WebserviceClassName, "getpatientinfo", obj);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(_webResult);

                XmlNode data = doc.SelectSingleNode("data");
                if (null == data)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                XmlNode stateNode = data.SelectSingleNode("state");
                if (null == stateNode)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                int stateResult = Convert.ToInt32(stateNode.InnerText);
                if (stateResult != 1)
                {
                    if (stateResult == 0)
                    {
                        XmlNode resultError = data.SelectSingleNode("result");
                        if (null == resultError)
                        {
                            msg = "调用后台WEBSERVICE出错";
                            return -1;
                        }
                        else
                        {
                            msg = resultError.InnerText;
                        }
                        return 2;
                    }
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                XmlNode resultNode = data.SelectSingleNode("result");
                if (null == resultNode)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                XmlNode itemNode = resultNode.SelectSingleNode("item");
                if (null == itemNode)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return -1;
                }

                foreach (XmlNode childNode in itemNode.ChildNodes)
                {
                    string name = childNode.Name;
                    switch (name)
                    {
                        case "requesttime":
                            reportInfo.cjsj = childNode.InnerText;
                            break;
                        case "requester":
                            reportInfo.sjr = childNode.InnerText;
                            break;
                        case "executetime":
                            reportInfo.jysj = childNode.InnerText;
                            break;
                        case "executer":
                            reportInfo.jyr = childNode.InnerText;
                            break;
                        case "receiver":
                            reportInfo.shr = childNode.InnerText;
                            break;
                        case "section":
                            reportInfo.jzch = childNode.InnerText;
                            break;
                        case "diagnostic":
                            reportInfo.zdjg = childNode.InnerText;
                            break;
                        case "sampletype":
                            reportInfo.bbmc = childNode.InnerText;
                            break;
                        case "stayhospitalmode":
                            reportInfo.mzbz = childNode.InnerText;
                            break;
                        case "patientid":
                            reportInfo.hzbh = childNode.InnerText;
                            break;
                        case "patientname":
                            reportInfo.brxm = childNode.InnerText;
                            break;
                    }
                }

                reportInfo.jgmc = WebConfigParameter.HospitalChinaName();
                result = 0;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
            }


            return result;
        }

        private int DetailInfo(string bgdh, out ArrayList list, out string msg)
        {
            int result = -1;
            list = new ArrayList();
            msg = "";
            WebserviceOpe _webservice;
            _webservice = new WebserviceOpe();
            try
            {
                Object[] obj = new Object[1];
                obj[0] = bgdh;

                string _webResult = _webservice.WebserviceRetInfo(WebConfigParameter.WebserviceUrl,
                    WebConfigParameter.WebserviceClassName, "getreportinfo", obj);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(_webResult);

                XmlNode data = doc.SelectSingleNode("data");
                if (null == data)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return 1;
                }

                XmlNode stateNode = data.SelectSingleNode("state");
                if (null == stateNode)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return 1;
                }

                int stateResult = Convert.ToInt32(stateNode.InnerText);
                if (stateResult != 1)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return 1;
                }

                XmlNode resultNode = data.SelectSingleNode("result");
                if (null == resultNode)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return 1;
                }

                XmlNodeList itemNodes = resultNode.SelectNodes("item");
                if (null == itemNodes)
                {
                    msg = "调用后台WEBSERVICE出错";
                    return 1;
                }

                foreach (XmlNode itemNode in itemNodes)
                {
                    ReportDetail reportInfo = new ReportDetail();
                    foreach (XmlNode childNode in itemNode)
                    {
                        string name = childNode.Name;

                        switch (name)
                        {
                            case "xmmc":
                                reportInfo.mc = childNode.InnerText;
                                break;
                            case "ckfw":
                                reportInfo.ckjg = childNode.InnerText;
                                break;
                            case "result":
                                reportInfo.jg = childNode.InnerText;
                                break;
                            case "xmdw":
                                reportInfo.dw = childNode.InnerText;
                                break;
                            case "hint":
                                reportInfo.ts = childNode.InnerText;
                                break;
                        }
                    }
                    list.Add(reportInfo);
                }

                result = 0;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
            }


            return result;
        }

    }
}