using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using HospitaPaymentService.Wzszhjk.BLL;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.DAL.Database;
using HospitaPaymentService.Wzszhjk.DAL.Database.Alipay;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.Xml;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Common.Xml;

namespace HospitaPaymentService.Wzszhjk.BLL.Alipay
{
    public class UserRegisterLogic : BaseLogic
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <returns></returns>
        public XmlDocument UserRegister(UserInfo info)
        {
            XmlDocument doc;
            try
            {
                UserRegisterDB pdb = new UserRegisterDB();
                string msg;
                int ret = pdb.DB_UserRegister(info, out msg);
                if (ret == 0)
                {
                    msg = "注册成功！";
                    doc = ReplyXmlDoc.GetSuccessXML(ret, msg);
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return doc;
        }

        /// <summary>
        /// 用户注册_带绑卡
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <returns></returns>
        public XmlDocument UserRegisterBindCard(UserInfo info)
        {
            XmlDocument doc;
            try
            {
                //以下实现数据操作逻辑
                UserRegisterDB pdb = new UserRegisterDB();
                string msg;
                int ret = pdb.DB_UserRegisterBindCard(info, out msg);
                if (ret == 0)
                {
                    msg = "注册成功！";
                    doc = ReplyXmlDoc.GetSuccessXML(ret, msg);
                }
                else
                {
                    doc = ErrorReturnXml(ret, msg);
                }
            }
            catch (Exception ex)
            {
                doc = ReplyXmlDoc.GetExceptionXML(AppUtils.Default_Exception_Code, ex);
            }
            return doc;
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="openid">身份证号</param>
        /// <param name="usertype">用户类型 1-支付宝用户 2-微信用户</param>
        /// <returns></returns>
        public XmlDocument UserInfo(string openid, string usertype)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                UserRegisterDB pdb = new UserRegisterDB();
                string error_msg;
                UserInfo info = new UserInfo();
                int ret = pdb.DB_UserInfo(openid, usertype, out info, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml= XMLHelper.SerializeClassFileds(info.GetType(), info);
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
        /// 用户信息修改
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <returns></returns>
        public XmlDocument ModifyInfo(UserInfo info)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                UserRegisterDB pdb = new UserRegisterDB();
                string error_msg;
                int ret = pdb.DB_ModifyInfo(info, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);

                    XmlElement eleOpenID = doc.CreateElement(AppUtils.Tag_User_OpenID);
                    eleValue.AppendChild(eleOpenID);
                    eleOpenID.InnerText = info.openid;

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
        /// 添加常用联系人
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <returns></returns>
        public XmlDocument AddContacts(UserInfo info)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);
                //以下实现数据操作逻辑
                UserRegisterDB pdb = new UserRegisterDB();
                string error_msg;
                UserInfo linkInfo = new UserInfo();
                int ret = pdb.DB_AddContacts(info, out linkInfo, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml = XMLHelper.SerializeClassFileds(linkInfo.GetType(), linkInfo);
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
        /// 获得常用联系人信息列表
        /// </summary>
        /// <param name="openid">身份证号</param>
        /// <param name="linkmanid">联系人ID </param>
        /// <returns></returns>
        public XmlDocument FavoriteContactsListStr(string openid, string linkmanid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);

                //以下实现数据操作逻辑
                UserRegisterDB pdb = new UserRegisterDB();
                string error_msg;
                ArrayList _list;
                int ret = pdb.DB_FavoriteContactsListStr(openid, linkmanid, out _list, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    foreach (UserInfo info in _list)
                    {
                        XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                        eleMsg.AppendChild(eleValue);
                        eleValue.InnerXml= XMLHelper.SerializeClassFileds(info.GetType(), info);
                    }                  
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
        /// 删除常用联系人
        /// </summary>
        /// <param name="openid">身份证号</param>
        /// <param name="linkmanid">联系人ID </param>
        /// <returns></returns>
        public XmlDocument DeleteFavoriteContactsStrr(string openid, string linkmanid)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement(AppUtils.Tag_REXML_Root);
            doc.AppendChild(root);
            try
            {
                XmlElement eleMsg = doc.CreateElement(AppUtils.Tag_REXML_Message);
                root.AppendChild(eleMsg);
                //以下实现数据操作逻辑
                UserRegisterDB pdb = new UserRegisterDB();
                string error_msg;
                UserInfo info;
                int ret = pdb.DB_DeleteFavoriteContactsStrr(openid, linkmanid, out info, out error_msg);
                if (ret == 0)
                {
                    XmlElement eleResult = doc.CreateElement(AppUtils.Tag_REXML_Result);
                    eleResult.InnerText = AppUtils.Value_Return_Success;
                    root.AppendChild(eleResult);

                    XmlElement eleValue = doc.CreateElement(AppUtils.Tag_REXML_Value);
                    eleMsg.AppendChild(eleValue);
                    eleValue.InnerXml= XMLHelper.SerializeClassFileds(info.GetType(), info);
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

    }
}