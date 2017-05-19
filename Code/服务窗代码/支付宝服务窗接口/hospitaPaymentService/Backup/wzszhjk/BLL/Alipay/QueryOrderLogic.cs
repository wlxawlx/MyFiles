using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.Common.Xml;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.DAL.Database;
using HospitaPaymentService.Wzszhjk.DAL.Database.Alipay;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Common.Webservice;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils.String;

namespace HospitaPaymentService.Wzszhjk.BLL.Alipay
{
    public class QueryOrderLogic : BaseLogic
    {
        /// <summary>
        /// 预约科室信息
        /// </summary>
        /// <param name="departcode">一级科室代码</param>
        /// <returns></returns>
        public XmlDocument AppointmentInfor(string departcode)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                QueryOrderDB pdb = new QueryOrderDB();
                string msg;
                ArrayList values;
                int ret = pdb.DB_AppointmentInfor(departcode, out values, out msg);

                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayAppointmentInfo ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        //XmlElement eledepartcode = doc.CreateElement(AppUtils.Tag_User_DepartCode);
                        //eleValue.AppendChild(eledepartcode);
                        //eledepartcode.InnerText = ri.departcode;

                        //XmlElement eledepartname = doc.CreateElement(AppUtils.Tag_User_DepartName);
                        //eleValue.AppendChild(eledepartname);
                        //eledepartname.InnerText = ri.departname;

                        //XmlElement elesecondcode = doc.CreateElement(AppUtils.Tag_User_SecondCode);
                        //eleValue.AppendChild(elesecondcode);
                        //elesecondcode.InnerText = ri.secondcode;

                        //XmlElement elesecondname = doc.CreateElement(AppUtils.Tag_User_SecondName);
                        //eleValue.AppendChild(elesecondname);
                        //elesecondname.InnerText = ri.secondname;

                        //XmlElement eledescribe = doc.CreateElement(AppUtils.Tag_User_Describe);
                        //eleValue.AppendChild(eledescribe);
                        //eledescribe.InnerText = ri.describe;

                        eleMsg.AppendChild(eleValue);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }

        /// <summary>
        /// 科室排班信息
        /// </summary>
        /// <param name="departcode">二级科室代码</param>
        /// <returns></returns>
        public XmlDocument DepartmentSchedul(string departcode)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            QueryOrderDB pdb = new QueryOrderDB();
            string msg;
            ArrayList values;
            int ret = pdb.DB_DepartmentSchedul(departcode, out values, out msg);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayDepartmentSchedul ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        //XmlElement elescheduledate = doc.CreateElement(AppUtils.Tag_User_ScheduleDate);
                        //eleValue.AppendChild(elescheduledate);
                        //elescheduledate.InnerText = ri.scheduledate;

                        //XmlElement eleremain = doc.CreateElement(AppUtils.Tag_User_Remain);
                        //eleValue.AppendChild(eleremain);
                        //eleremain.InnerText = ri.remain;

                        //XmlElement eletotal = doc.CreateElement(AppUtils.Tag_User_Total);
                        //eleValue.AppendChild(eletotal);
                        //eletotal.InnerText = ri.total;

                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);

                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }

        /// <summary>
        /// 预约医生信息
        /// </summary>
        /// <param name="departcode">二级科室代码</param>
        /// <param name="scheduledate">排班日期</param>
        /// <returns></returns>
        public XmlDocument ReservationDoctor1(string departcode, string scheduledate)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                QueryOrderDB pdb = new QueryOrderDB();
                string msg;
                ArrayList values;
                int ret = pdb.DB_ReservationDoctor1(departcode, scheduledate, out values, out msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayReservationDoctor1 ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        //XmlElement eledoctorcode = doc.CreateElement(AppUtils.Tag_User_Doctorcode);
                        //eleValue.AppendChild(eledoctorcode);
                        //eledoctorcode.InnerText = ri.doctorcode;

                        //XmlElement eledoctorname = doc.CreateElement(AppUtils.Tag_User_DoctorName);
                        //eleValue.AppendChild(eledoctorname);
                        //eledoctorname.InnerText = ri.doctorname;

                        //XmlElement elepictureURL = doc.CreateElement(AppUtils.Tag_User_PictureURL);
                        //eleValue.AppendChild(elepictureURL);
                        //elepictureURL.InnerText = ri.pictureurl;

                        //XmlElement elelevel = doc.CreateElement(AppUtils.Tag_User_Level);
                        //eleValue.AppendChild(elelevel);
                        //elelevel.InnerText = ri.level;

                        //XmlElement elerecommend = doc.CreateElement(AppUtils.Tag_User_Recommend);
                        //eleValue.AppendChild(elerecommend);
                        //elerecommend.InnerText = ri.recommend;

                        //XmlElement eleadept = doc.CreateElement(AppUtils.Tag_User_Adept);
                        //eleValue.AppendChild(eleadept);
                        //eleadept.InnerText = ri.adept;

                        //XmlElement elereserve = doc.CreateElement(AppUtils.Tag_User_Reserve);
                        //eleValue.AppendChild(elereserve);
                        //elereserve.InnerText = ri.reserve;

                        //XmlElement eleScheduleDates = doc.CreateElement(AppUtils.Tag_User_ScheduleDates);
                        //eleValue.AppendChild(eleScheduleDates);
                        //eleScheduleDates.InnerText = ri.scheduledates;
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }

        /// <summary>
        /// 医生排班信息
        /// </summary>
        /// <param name="doctorcode">医生代码</param>
        /// <param name="departcode">二级科室代码</param>
        /// <param name="scheduledate">排班日期</param>
        /// <returns></returns>
        public XmlDocument DoctorSchedul(string doctorcode, string departcode, string scheduledate)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                QueryOrderDB pdb = new QueryOrderDB();
                string msg;
                ArrayList values;
                int ret = pdb.DB_DoctorSchedul(doctorcode, departcode, scheduledate, out values, out msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayReservationDoctor1 ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);

                        //XmlElement eleScheduleseq = doc.CreateElement(AppUtils.Tag_User_Scheduleseq);
                        //eleValue.AppendChild(eleScheduleseq);
                        //eleScheduleseq.InnerText = ri.scheduleseq;

                        //XmlElement eleDepartCode = doc.CreateElement(AppUtils.Tag_User_DepartCode);
                        //eleValue.AppendChild(eleDepartCode);
                        //eleDepartCode.InnerText = ri.departcode;

                        //XmlElement eleDepartName = doc.CreateElement(AppUtils.Tag_User_DepartName);
                        //eleValue.AppendChild(eleDepartName);
                        //eleDepartName.InnerText = ri.departname;

                        //XmlElement eleDoctorcode = doc.CreateElement(AppUtils.Tag_User_Doctorcode);
                        //eleValue.AppendChild(eleDoctorcode);
                        //eleDoctorcode.InnerText = ri.doctorcode;

                        //XmlElement eleDoctorName = doc.CreateElement(AppUtils.Tag_User_DoctorName);
                        //eleValue.AppendChild(eleDoctorName);
                        //eleDoctorName.InnerText = ri.doctorname;

                        //XmlElement eleSpecial = doc.CreateElement(AppUtils.Tag_User_Special);
                        //eleValue.AppendChild(eleSpecial);
                        //eleSpecial.InnerText = ri.special;

                        //XmlElement eleRemain = doc.CreateElement(AppUtils.Tag_User_Remain);
                        //eleValue.AppendChild(eleRemain);
                        //eleRemain.InnerText = ri.remain;

                        //XmlElement eleTotal = doc.CreateElement(AppUtils.Tag_User_Total);
                        //eleValue.AppendChild(eleTotal);
                        //eleTotal.InnerText = ri.total;

                        //XmlElement eleAddress = doc.CreateElement(AppUtils.Tag_User_Address);
                        //eleValue.AppendChild(eleAddress);
                        //eleAddress.InnerText = ri.address;

                        //XmlElement eleScheduleDate = doc.CreateElement(AppUtils.Tag_User_ScheduleDate);
                        //eleValue.AppendChild(eleScheduleDate);
                        //eleScheduleDate.InnerText = ri.scheduledate;

                        //XmlElement eleShiftCode = doc.CreateElement(AppUtils.Tag_User_ShiftCode);
                        //eleValue.AppendChild(eleShiftCode);
                        //eleShiftCode.InnerText = ri.shiftcode;

                        //XmlElement eleShiftName = doc.CreateElement(AppUtils.Tag_User_ShiftName);
                        //eleValue.AppendChild(eleShiftName);
                        //eleShiftName.InnerText = ri.shiftname;

                        //XmlElement eleFee = doc.CreateElement(AppUtils.Tag_User_Fee);
                        //eleValue.AppendChild(eleFee);
                        //eleFee.InnerText = ri.fee;
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }

        /// <summary>
        /// 门诊号源信息
        /// </summary>
        /// <param name="doctorcode">医生代码</param>
        /// <param name="scheduleseq">排班流水号</param>
        /// <param name="shiftcode">排班日期</param>
        /// <returns></returns>
        public XmlDocument mzReservationInfor(string doctorcode, string scheduleseq, string shiftcode)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                QueryOrderDB pdb = new QueryOrderDB();
                string msg;
                ArrayList values;
                int ret = pdb.DB_mzReservationInfor(doctorcode, scheduleseq, shiftcode, out values, out msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipayQueryOrderSeq ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }

        /// <summary>
        /// 门诊预约
        /// </summary>
        /// <param name="openid">身份证号</param>
        /// <param name="doctorcode">医生代码</param>
        /// <param name="scheduleseq">排班流水号</param>
        /// <param name="orderseq">号源流水号</param>
        /// <param name="patientid">病人ID</param>
        /// <param name="patientname">病人姓名</param>
        /// <param name="patientidcardno">病人身份证号</param>
        /// <param name="patientphone">病人电话</param>
        /// <param name="patientaddress">病人地址</param>
        /// <returns></returns>
        public XmlDocument mzReservation(AlipaymzOrderNeedInfo info)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                //以下实现数据操作逻辑
                QueryOrderDB pdb = new QueryOrderDB();
                string error_msg;
                AlipaymzReservation pd;
                int ret = pdb.mzOrder(info, out pd, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);

                    XmlElement elePreengageseq = doc.CreateElement(AppUtils.Tag_User_Preengageseq);
                    eleValue.AppendChild(elePreengageseq);
                    elePreengageseq.InnerText = pd.preengageseq;

                    XmlElement elePreengageDate = doc.CreateElement(AppUtils.Tag_User_PreengageDate);
                    eleValue.AppendChild(elePreengageDate);
                    elePreengageDate.InnerText = pd.preengagedate;

                    XmlElement elePreengageTime = doc.CreateElement(AppUtils.Tag_User_PreengageTime);
                    eleValue.AppendChild(elePreengageTime);
                    elePreengageTime.InnerText = pd.preengagetime;

                    XmlElement eleDepartCode = doc.CreateElement(AppUtils.Tag_User_DepartCode);
                    eleValue.AppendChild(eleDepartCode);
                    eleDepartCode.InnerText = pd.departcode;

                    XmlElement eleDepartName = doc.CreateElement(AppUtils.Tag_User_DepartName);
                    eleValue.AppendChild(eleDepartName);
                    eleDepartName.InnerText = pd.departname;

                    XmlElement elePatientName = doc.CreateElement(AppUtils.Tag_User_PatientName);
                    eleValue.AppendChild(elePatientName);
                    elePatientName.InnerText = pd.patientname;

                    XmlElement elePreengageno = doc.CreateElement(AppUtils.Tag_User_Preengageno);
                    eleValue.AppendChild(elePreengageno);
                    elePreengageno.InnerText = pd.patientidcardno;

                    XmlElement elePlace = doc.CreateElement(AppUtils.Tag_User_Place);
                    eleValue.AppendChild(elePlace);
                    elePlace.InnerText = pd.place;

                }
                else
                {
                    doc = ErrorReturnXml(ret, error_msg);
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return doc;
        }

        /// <summary>
        /// 门诊预约历史
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <returns></returns>
        public XmlDocument mzReservationHistory(string openid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                QueryOrderDB pdb = new QueryOrderDB();
                string msg;
                ArrayList values;
                int ret = pdb.DB_mzReservationHistory(openid, out values, out msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (AlipaymzReservation ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }

        /// <summary>
        /// 取消门诊预约
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="preengageseq">预约流水号</param>
        /// <returns></returns>
        public XmlDocument CancelmzReservation(string openid, string preengageseq)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                QueryOrderDB pdb = new QueryOrderDB();
                string msg;
                AlipaymzReservation info;

                int ret = pdb.DB_CancelmzReservation(openid, preengageseq, out info, out msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);
                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(info.GetType(), info);
                    eleMsg.AppendChild(eleValue);

                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }

        /// <summary>
        /// 门诊预约报到
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="preengageseq">预约流水号</param>
        /// <returns></returns>
        public XmlDocument mzReservationReport(string openid, string preengageseq)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                //以下实现数据操作逻辑
                QueryOrderDB pdb = new QueryOrderDB();
                string error_msg;
                mzreporter info;
                int ret = pdb.DB_mzReservationReport(openid, preengageseq, out info, out error_msg);

                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(info.GetType(), info);
                    eleMsg.AppendChild(eleValue);

                }else
                {
                    doc = ErrorReturnXml(ret, error_msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
           
        }

        /// <summary>
        /// 挂号退号
        /// </summary>
        /// <param name="guahaoID">挂号标识</param>
        /// <returns></returns>
        public XmlDocument RegistrationNumber(string guahaoID)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                //以下实现数据操作逻辑
                QueryOrderDB pdb = new QueryOrderDB();
                string error_msg;
                mzreporter info;
                int ret = pdb.DB_RegistrationNumber(guahaoID, out info, out error_msg);

                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(info.GetType(), info);
                    eleMsg.AppendChild(eleValue);

                }
                else
                {
                    doc = ErrorReturnXml(ret, error_msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }

        }

        /// <summary>
        /// 门诊预约排队列表
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="queueseq">排队流水号</param>
        /// <returns></returns>
        public XmlDocument mzReservationQueue(string openid, string queueseq)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                //以下实现数据操作逻辑
                QueryOrderDB pdb = new QueryOrderDB();
                string msg;
                ArrayList values;
                int ret = pdb.DB_mzReservationQueue(openid, queueseq, out values,out msg);

                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (mzreporter ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                        eleMsg.AppendChild(eleValue);
                    }
                }else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }

        /// <summary>
        /// 门诊预约所有排队前十人员列表信息
        /// <returns></returns>
        public XmlDocument mzReservationQueueInfo()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string msg = "";
                ArrayList values = new ArrayList();

                QueryOrderDB pdb = new QueryOrderDB();
                int ret = pdb.DB_mzReservationQueueInfo(out values, out msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (mzreporter ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);


                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }

        /// <summary>
        /// 检查预约报到排队列表
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="queueseq">排队流水号</param>
        /// <returns></returns>
        public XmlDocument CheckReservationReportList(string queueseq)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                //以下实现数据操作逻辑
                QueryOrderDB pdb = new QueryOrderDB();
                string msg;
                ArrayList values;
                int ret = pdb.DB_CheckReservationReportList(queueseq, out values, out msg);

                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (CheckReservationReportList ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }

        /// <summary>
        /// 检查预约报到
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="preengageseq">预约流水号</param>
        /// <returns></returns>
        public XmlDocument CheckReservationReport(string patientid, string preengageseq)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                //以下实现数据操作逻辑
                QueryOrderDB pdb = new QueryOrderDB();
                string error_msg;
                mzreporter info;
                int ret = pdb.DB_CheckReservationReport(patientid, preengageseq, out info, out error_msg);

                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(info.GetType(), info);
                    eleMsg.AppendChild(eleValue);

                }
                else
                {
                    doc = ErrorReturnXml(ret, error_msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }

        }

        /// <summary>
        /// 检查预约所有排队前十人员列表信息
        /// <returns></returns>
        public XmlDocument CheckReservationReportQueueList()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);

            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                string msg = "";
                ArrayList values = new ArrayList();

                QueryOrderDB pdb = new QueryOrderDB();
                int ret = pdb.DB_CheckReservationReportQueueList(out values, out msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (CheckReservationReportList ri in values)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);


                        eleValue.InnerXml = XMLHelper.SerializeClassFileds(ri.GetType(), ri);
                        eleMsg.AppendChild(eleValue);
                    }
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }

                return doc;
            }
            catch (Exception ex)
            {
                return ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
        }
    }
}