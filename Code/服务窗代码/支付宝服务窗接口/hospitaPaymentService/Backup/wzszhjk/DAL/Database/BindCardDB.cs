using System;
using System.Collections;
using System.Data.OracleClient;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Log;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;

namespace HospitaPaymentService.Wzszhjk.DAL.Database
{
    public class BindCardDB : BaseDB
    {
        private BuilderSql _builder;
        public BindCardDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 卡查询
        /// </summary>
        /// <param name="brxm">病人姓名</param>
        /// <param name="sfzh">身份证号</param>
        /// <param name="brlx">病人类别 1-门诊  2-住院</param>
        /// <param name="list">返回的病人信息</param>
        /// <param name="msg">成功、错误或异常信息</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_QueryCard(string brxm, string sfzh, string brlx, out ArrayList values, out string msg)
        {
            msg = "";


            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            string findId1 = "";
            values = new ArrayList();

            try
            {
                bool _flag = false;

                findId1 = _builder.GetQueryCardSql(brxm, sfzh, brlx, out _flag, out msg);

                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findId1);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(findId1, connection);


                while (dr.Read())
                {
                    PatientInfo paintentInfo = new PatientInfo();

                    if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDQRMYY ||
                        WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDYRMYY ||
                        WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXFYBJYY ||
                        WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTXRMYY ||
                        WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSRAZYY ||
                        (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTSXZYY && brlx == "1")) 
                    {
                        paintentInfo.brid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    }
                    else
                    {
                        if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSYJXDSRMYY && brlx == "2")
                        {
                            paintentInfo.brid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        }
                        else
                        {
                            paintentInfo.brid = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                        }
                    }

                    paintentInfo.bkhm = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    paintentInfo.bklx = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                    paintentInfo.brxm = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    paintentInfo.sfzh = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                    paintentInfo.lxdh = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                    paintentInfo.jtdz = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                    paintentInfo.jlsj = DateTime.MinValue;

                    //判断是否住院病人   否则没有下列信息
                    if (brlx.Equals("2"))
                    {
                        paintentInfo.jlsj = !dr.IsDBNull(7) ? dr.GetDateTime(7) : DateTime.MinValue;
                        paintentInfo.szbq = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        paintentInfo.szcw = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                        if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTSXZYY)
                        {
                            paintentInfo.zyh = !dr.IsDBNull(10) ? Convert.ToString(dr.GetInt64(10)) : "";
                        }
                        else
                        {
                            paintentInfo.zyh = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        }
                    }

                    values.Add(paintentInfo);
                }

                if (values.Count > 0)
                {
                    msg = "找到病人信息";
                    ret = 0;
                }
                else
                {
                    msg = "医院端未找到，请核对信息后重试";
                    values = null;
                    ret = 2;
                }

                dr.Close();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                values = null;

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
        /// 绑卡
        /// </summary>
        /// <param name="brid">病人ID</param>
        /// <param name="brxm">病人姓名</param>
        /// <param name="sfzh">身份证号</param>
        /// <param name="bklx">绑卡类型</param>
        /// <param name="lxdh">联系电话</param>
        /// <param name="brlx">病人类别 1-门诊  2-住院</param>
        /// <param name="msg">成功、错误或异常信息</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_BindCard(string brid, string brxm, string sfzh, string bklx, string lxdh, string brlx, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            try
            {
                DateTime _bksj;
                if (!DB_Sysdate(out _bksj))
                {
                    msg = "读取系统时间出错";
                    ret = 5;
                    return ret;
                }

                bool _flag = false;

                string findText = _builder.GetBingCardSql(brid, sfzh, brxm, bklx, brlx, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(findText, connection);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {

                        string _lxdh = !dr.IsDBNull(0) ? dr.GetString(0).ToUpper() : ""; 

                        if (_lxdh.Equals(lxdh) && !_lxdh.Equals(""))
                        {
                            msg = "成功绑定";
                            ret = 0;
                        }
                        else if (!_lxdh.Equals(lxdh) && !_lxdh.Equals(""))
                        {
                            msg = "绑定失败，电话号码不匹配";
                            ret = 2;
                        }
                        else if (_lxdh.Equals(""))
                        {
                            msg = "绑定失败，联系号码未登记";
                            ret = 4;
                        }
                        else
                        {
                            msg = "绑定失败，该卡已被绑定";
                            ret = 3;
                        }
                    }
                    else
                    {
                        msg = "绑卡失败";
                        ret = 4;
                    }
                }
                else
                {
                    msg = "绑卡失败，失败原因没有找到记录";
                }

                if (null != dr)
                {
                    dr.Close();
                }

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
                            WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDYRMYY||
                            WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTXRMYY)
                        {
                            _brid = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        }
                        else
                        {

                            _brid = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";

                        }

                        if (_brid != brid || _brid.Equals(""))
                        {
                            msg = "该病人绑卡数据有误";
                            ret = 1;
                        }
                        else
                        {
                            msg = "成功，该病人绑卡数据有效";
                            ret = 0;
                        }
                    }
                    else
                    {
                        msg = "记录已找到，但是没有该病人ID";
                        ret = 2;
                    }
                }
                else
                {
                    msg = "绑卡记录没找到，确认办卡信息";
                    ret = 3;
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