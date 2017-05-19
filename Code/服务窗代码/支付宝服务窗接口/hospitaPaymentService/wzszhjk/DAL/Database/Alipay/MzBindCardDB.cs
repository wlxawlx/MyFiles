using System;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Log;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;

namespace HospitaPaymentService.Wzszhjk.DAL.Database.Alipay
{
    public class MzBindCardDB : BaseDB
    {
        private BuilderSql _builder;
        public MzBindCardDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        ///  获得门诊卡列表
        /// </summary>
        /// <param name="idcardno">身份证号</param>
        /// <param name="name">姓名</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_GetmzkListStr(string idcardno, string name, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            values = new ArrayList();

            try
            {
                bool _flag = false;

                string findText = _builder.GetGetmzkListSql(idcardno, name, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(findText, connection);

                while (dr.Read())
                {
                    PatientInfo paintentInfo = new PatientInfo();

                    paintentInfo.bklx = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    paintentInfo.cardname = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    paintentInfo.bkhm = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                    paintentInfo.sfzh = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    paintentInfo.brid = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                    paintentInfo.brxm = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                    paintentInfo.birthday = !dr.IsDBNull(6) ? dr.GetDateTime(6).ToString("yyyy-MM-dd") : "";
                    paintentInfo.lxdh = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                    paintentInfo.balance = !dr.IsDBNull(8) ? Convert.ToString(dr.GetDouble(8)) : " ";
                    paintentInfo.cost = !dr.IsDBNull(9) ? Convert.ToString(dr.GetDouble(9)) : " ";
                    values.Add(paintentInfo);                   
                }
                if (values.Count > 0)
                {
                    msg = "找到病人信息";
                    ret = 00;
                }
                else
                {
                    msg = "医院端未找到，请核对信息后重试";
                    values = null;
                    ret = 10;                    
                }
                dr.Close();

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }

        /// <summary>
        ///  获得门诊卡信息_病人ID
        /// </summary>
        /// <param name="patientid">病人ID</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_GetmzkInforStr(string patientid, out PatientInfo paintentInfo, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            paintentInfo = new PatientInfo();
            try
            {

                bool _flag = false;

                string findText = _builder.GetGetmzkInforStrSql(patientid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(findText, connection);

                if(dr.Read())
                {
                    paintentInfo.bklx = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    paintentInfo.cardname = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    paintentInfo.bkhm = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                    paintentInfo.sfzh = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    paintentInfo.brid = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                    paintentInfo.brxm = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                    paintentInfo.birthday = !dr.IsDBNull(6) ? dr.GetDateTime(6).ToString("yyyy-MM-dd") : "";
                    paintentInfo.lxdh = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                    paintentInfo.balance = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                    paintentInfo.cost = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                    ret = 0;
                }
                else
                {
                    msg = "医院端未找到，请核对信息后重试";
                    ret = 10;
                }
                dr.Close();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }

        /// <summary>
        ///  本人门诊卡绑定
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="cardno">就诊卡号</param>
        /// <param name="patientid">病人ID</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_UsermzkBindStr(string openid, string cardno, string patientid, out PatientInfo paintentInfo, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            OracleDataReader dr = null;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            OracleTransaction tx = connection.BeginTransaction();
            cmd.Transaction = tx;

            int ret = -1;
            paintentInfo = new PatientInfo();
            try
            {
                bool _flag = false;

                string findText = _builder.GetUsermzkBindSql(cardno, patientid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                cmd.CommandText = findText;
                dr = cmd.ExecuteReader();

                if(dr.Read())
                {
                    paintentInfo.bklx = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    paintentInfo.cardname = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    paintentInfo.bkhm = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                }
                else
                {
                    msg = "医院端未找到，请核对信息后重试";
                    ret = 10;
                    dr.Close();
                    tx.Commit();
                    return ret;
                }

                string updateText = _builder.UpdateUsermzkBindInfoSql(openid, cardno, patientid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, updateText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                cmd.CommandText = updateText;
                ret = cmd.ExecuteNonQuery();
                if (ret == 1)
                {
                    ret = 0;
                }
                else
                {
                    ret = 11;
                    msg = "门诊卡绑定更新异常！";
                    tx.Rollback();
                }

                dr.Close();
                tx.Commit();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                ret = -1;
                tx.Rollback();
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }

        /// <summary>
        ///  本人门诊卡解绑
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_UsermzkRelieveBindStr(string openid, out PatientInfo paintentInfo, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            OracleDataReader dr = null;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            OracleTransaction tx = connection.BeginTransaction();
            cmd.Transaction = tx;

            int ret = -1;
            paintentInfo = new PatientInfo();
            try
            {
                bool _flag = false;
                string findText = _builder.GetCardNoFromIDSql(openid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }
                cmd.CommandText = findText;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    paintentInfo.bkhm = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                }
                else
                {
                    msg = "医院端未找到，请核对信息后重试";
                    ret = 10;
                    dr.Close();
                    tx.Commit();
                    return ret;
                }

                string updateText = _builder.GetUsermzkRelieveBindSql(openid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, updateText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }
                cmd.CommandText = updateText;
                ret = cmd.ExecuteNonQuery();
                if (ret == 1)
                {
                    ret = 0;
                }
                else
                {
                    ret = 12;
                    msg = "解绑更新异常！";
                    tx.Rollback();
                }

                dr.Close();
                tx.Commit();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                ret = -1;
                tx.Rollback();
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }

        /// <summary>
        ///  常用联系人门诊卡绑定
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="linkmanid">联系人ID</param>
        /// <param name="cardno">就诊卡号</param>
        /// <param name="patientid">病人ID</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_FavoriteContactsBindStr(string openid, string linkmanid, string cardno, string patientid, out PatientInfo paintentInfo, out string msg)
        {
            msg = "";
            int ret = -1;
            paintentInfo = new PatientInfo();
            bool isbindcard = isUserbindcard(openid, cardno, patientid, out msg);
            if (isbindcard)
            {
                msg = "亲，该卡已绑定！";
                return 99;
            }

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            OracleDataReader dr = null;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            OracleTransaction tx = connection.BeginTransaction();
            cmd.Transaction = tx;

            try
            {
                bool _flag = false;

                string findText = _builder.GetUsermzkBindSql(cardno, patientid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                cmd.CommandText = findText;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    paintentInfo.bklx = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    paintentInfo.cardname = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    paintentInfo.bkhm = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                }
                else
                {
                    msg = "医院端未找到，请核对信息后重试";
                    ret = 10;
                    dr.Close();
                    tx.Commit();
                    return ret;
                }

                string updateText = _builder.UpdateLinkManBindInfoSql(openid, linkmanid, cardno, patientid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, updateText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                cmd.CommandText = updateText;
                ret = cmd.ExecuteNonQuery();
                if (ret == 1)
                {
                    ret = 0;
                }
                else
                {
                    ret = 11;
                    msg = "门诊卡绑定更新异常！";
                    tx.Rollback();
                }

                dr.Close();
                tx.Commit();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                ret = -1;
                tx.Rollback();
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }

        private bool isUserbindcard(string openid, string cardno, string patientid, out string msg)
        {
            msg = "";
            bool isbindcard = false;
            string number = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                string result = "";
                string isUserbindcardSql = "select name, phone, idcardno, cardno, patientid from ZFB_TIANJIACYLXR  where openid = '{0}' and cardno = '{1}' and patientid = '{2}'";

                string[] _paramters = new string[3];
                _paramters[0] = openid;
                _paramters[1] = cardno;
                _paramters[2] = patientid;

                result = string.Format(isUserbindcardSql, _paramters);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, result);

                dr = DbHelperOra.ExecuteReader(result, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        number = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    }
                    isbindcard = true;
                }

                return isbindcard;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                return isbindcard;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

        }

        /// <summary>
        ///  常用联系人门诊卡解绑
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="linkmanid">联系人ID</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_FavoriteContactsrmzkRelieveBindStr(string openid, string linkmanid, out PatientInfo paintentInfo, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            OracleDataReader dr = null;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            OracleTransaction tx = connection.BeginTransaction();
            cmd.Transaction = tx;

            int ret = -1;
            paintentInfo = new PatientInfo();
            try
            {
                bool _flag = false;
                string findText = _builder.GetLinkmanCardNoFromIDSql(openid, linkmanid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }
                cmd.CommandText = findText;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    paintentInfo.bkhm = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                }
                else
                {
                    msg = "医院端未找到，请核对信息后重试";
                    ret = 10;
                    dr.Close();
                    tx.Commit();
                    return ret;
                }

                string updateText = _builder.GetFavoriteContactsrmzkRelieveBindSql(openid, linkmanid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, updateText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }
                cmd.CommandText = updateText;
                ret = cmd.ExecuteNonQuery();
                if (ret == 1)
                {
                    ret = 0;
                }
                else
                {
                    ret = 12;
                    msg = "解绑更新异常！";
                    tx.Rollback();
                }

                dr.Close();
                tx.Commit();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                ret = -1;
                tx.Rollback();
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }
        /// <summary>
        /// 卡号是否是否有效
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="bkhm">病人号码</param>
        /// <param name="brlx">病人类型 1:门诊病人 2:住院病人</param>
        /// <param name="msg">返回错误信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_ValidCard(string brid, string bkhm, string brlx, out string msg)
        {
            msg = "";
            int ret = -1;

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            try
            {
                //说明返回值 
                // 0:成功，
                // 1：该病人绑卡数据有误
                // 2: 记录已找到，但是没有该病人ID
                // 3: 绑卡记录没找到，确认办卡信息
                string _brid = "";


                bool _flag = false;

                string sqlStr = _builder.GetBridByBkhmSql(bkhm, brid, brlx, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlStr);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(sqlStr, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDQRMYY ||
                            WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDYRMYY)
                        {
                            _brid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        }
                        else
                        {
                            _brid = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                        }

                        if (_brid != brid || _brid.Equals(""))
                        {
                            msg = "绑卡信息有误";
                            ret = 13;
                        }
                        else
                        {
                            msg = "成功，该病人绑卡数据有效";
                            ret = 00;
                        }
                    }
                    else
                    {
                        msg = "记录已找到，但是没有该病人ID";
                        ret = 14;
                    }
                }
                else
                {
                    msg = "绑卡记录没找到，确认办卡信息";
                    ret = 15;
                }

                dr.Close();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex);
                ret = -1;
                return ret;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

        }

        /// <summary>
        /// 病人ID是否有效
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="brlx">病人类型</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public bool ValidBrid(string brid, string brlx, out string msg)
        {
            bool result = false;
            msg = "";
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int _count = -1;
            try
            {
                bool _flag = false;

                string findId1 = _builder.GetSqlValidBrid(brid, brlx, out _flag, out msg);
                if (!_flag)
                {
                    return false;
                }

                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        _count = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                        if (_count <= 0)
                        {
                            msg = "该病人id没找到或者已经过期";
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }
                else
                {
                    msg = "该病人没找到";
                    result = false;
                }

            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                return false;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return result;
        }


        //读取系统时间
        public Boolean DB_Sysdate(out DateTime sysdate)
        {
            sysdate = Convert.ToDateTime(AppUtils.DateTime_Format);
            Boolean _value = false;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                string findId = _builder.SysdateSql();
                dr = DbHelperOra.ExecuteReader(findId, connection);
                if (dr.Read())
                {
                    sysdate = !dr.IsDBNull(0) ? dr.GetDateTime(0) : Convert.ToDateTime(AppUtils.DateTime_Format);
                    _value = true;
                }
                else
                {
                    _value = false;
                }
                dr.Close();
                return _value;

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }
        }
    }
}