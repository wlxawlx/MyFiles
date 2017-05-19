using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.Log;
using HospitaPaymentService.Wzszhjk.Utils;


namespace HospitaPaymentService.Wzszhjk.DAL.Database.Alipay
{
    public class AilpayQueryReportDB :BaseDB
    {
        private BuilderSql _builder;
        public AilpayQueryReportDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 化验报告单列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="idcardno">身份证号</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_AilpayQueryReport(string name, string idcardno, out ArrayList values, out string msg)
        {
            string _oracleConStr = WebConfigParameter.ConnectionHisString;
            values = new ArrayList();

            OracleConnection connection = new OracleConnection(_oracleConStr);
            OracleDataReader dr = null;

            try
            {
                bool _flag = false;

                string sql = _builder.GetSqlAilpayReportList(name, idcardno, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, sql);

                if (!_flag)
                {
                    return 10;
                }

                int ret = -1;
                msg = "";
                dr = DbHelperOra.ExecuteReader(sql, connection);
                if (null != dr && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AlipayReportList ri = new AlipayReportList();
                        ri.doctadviseno = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                        ri.examinaim = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        ri.requesttime = !dr.IsDBNull(2) ? dr.GetDateTime(2).ToString(AppUtils.DateTime_Format_All) : "";
                        ri.requester = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        values.Add(ri);
                    }

                    ret = 0;
                }
                else
                {
                    values = null;
                    msg = "未能找到报告单";
                    ret = 17;

                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                values = null;
                msg = GetExceptionInfo(ex);
                return AppUtils.Default_Exception_Code;
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
        /// 一个化验报告单抬头信息
        /// </summary>
        /// <param name="doctadviseno">条码号</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_LaboratoryTestsReportNameInformation(string doctadviseno, out AlipayReportInfo alipayReportInfo, out string msg)
        {

            msg = "";
            alipayReportInfo = new AlipayReportInfo();

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            try
            {
                bool _flag = false;
                double number = Convert.ToDouble(doctadviseno);
                string findText = _builder.GetLaboratoryTestsReportNameInformation(number, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    return 10;
                }

                msg = "";
                dr = DbHelperOra.ExecuteReader(findText, connection);
                if (null != dr && dr.HasRows)
                {
                    if (dr.Read())
                    {
                        alipayReportInfo.doctadviseno = !dr.IsDBNull(0) ? Convert.ToString(dr.GetInt64(0)) : "";
                        alipayReportInfo.requesttime = !dr.IsDBNull(1) ? dr.GetDateTime(1).ToString(AppUtils.DateTime_Format_All) : "";
                        alipayReportInfo.requester = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        alipayReportInfo.executetime = !dr.IsDBNull(3) ? dr.GetDateTime(3).ToString(AppUtils.DateTime_Format_All) : "";
                        alipayReportInfo.executer = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        alipayReportInfo.receivetime = !dr.IsDBNull(5) ? dr.GetDateTime(5).ToString(AppUtils.DateTime_Format_All) : "";
                        alipayReportInfo.receiver = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        alipayReportInfo.stayhospitalmode = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        alipayReportInfo.patientid = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        alipayReportInfo.section = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                        alipayReportInfo.bedno = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        alipayReportInfo.patientname = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        alipayReportInfo.sex = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        alipayReportInfo.age = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        alipayReportInfo.diagnostic = !dr.IsDBNull(14) ? dr.GetString(14) : "";
                        alipayReportInfo.sampletype = !dr.IsDBNull(15) ? dr.GetString(15) : "";
                        alipayReportInfo.examinaim = !dr.IsDBNull(16) ? dr.GetString(16) : "";
                        alipayReportInfo.requestmode = !dr.IsDBNull(17) ? dr.GetString(17) : "";
                        alipayReportInfo.checker = !dr.IsDBNull(18) ? dr.GetString(18) : "";
                        alipayReportInfo.checktime = !dr.IsDBNull(19) ? dr.GetDateTime(19).ToString(AppUtils.DateTime_Format_All) : "";
                        alipayReportInfo.csyq = !dr.IsDBNull(20) ? dr.GetString(20) : "";
                        alipayReportInfo.profiletest = !dr.IsDBNull(21) ? dr.GetString(21) : "";
                        ret = 0;
                    }
                }
                else
                {
                    msg = "未能找到报告单";
                    ret = 17;

                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                return AppUtils.Default_Exception_Code;
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
        /// 一个化验报告单详细列表信息
        /// </summary>
        /// <param name="doctadviseno">条码号</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_LaboratoryTestsReportDetailedListInformation(string doctadviseno, out ArrayList values, out string msg)
        {

            msg = "";
            values = new ArrayList();
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool _flag = false;
                double number = Convert.ToDouble(doctadviseno);
                string findText = _builder.GetLaboratoryTestsReportDetailedListInformation(number, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                int ret = -1;
                msg = "";
                dr = DbHelperOra.ExecuteReader(findText, connection);
                if (null != dr && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AlipayReportdetailInfo ri = new AlipayReportdetailInfo();
                        ri.jylx = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        ri.xmmc = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        ri.result = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        ri.hint = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        ri.jkfw = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        ri.xmdw = !dr.IsDBNull(5) ? dr.GetString(5) : "";

                        values.Add(ri);
                    }

                    ret = 0;
                }
                else
                {
                    values = null;
                    msg = "未能找到报告单";
                    ret = 17;

                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                values = null;
                msg = GetExceptionInfo(ex);
                return AppUtils.Default_Exception_Code;
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
        /// 检验报告单列表
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="idcardno">身份证号</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_InspectionReportList(string name, string idcardno, out ArrayList values, out string msg)
        {

            string _oracleConStr = WebConfigParameter.ConnectionHisString;
            string _sqlConStr = WebConfigParameter.ConnectionLisString;
            values = new ArrayList();

            OracleConnection oracleConnection = new OracleConnection(_oracleConStr);
            SqlConnection sqlConnection = new SqlConnection(_sqlConStr);
            OracleDataReader dr = null;
            SqlDataReader myReader = null;

            try
            {
                bool _flag = false;
                string sqlstr = "";
                string oraclestr = "";
                _builder.GetInspectionReportList(name, idcardno, out sqlstr, out oraclestr, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                int ret = -1;
                msg = "";

                if (!string.IsNullOrEmpty(_sqlConStr))
                {
                    myReader = DbHelperSQL.ExecuteReader(sqlstr, sqlConnection);
                    if (null != myReader && myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            AlipayReportList ri = new AlipayReportList();
                            ri.doctadviseno = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                            ri.examinaim = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                            ri.requesttime = !myReader.IsDBNull(2) ? myReader.GetDateTime(2).ToString(AppUtils.DateTime_Format_All) : "";
                            ri.requester = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";
                            values.Add(ri);
                        }
                    }
                }

                dr = DbHelperOra.ExecuteReader(oraclestr, oracleConnection);
                if (null != dr && dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AlipayReportList ri = new AlipayReportList();
                        ri.doctadviseno = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        ri.examinaim = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        ri.requesttime = !dr.IsDBNull(2) ? dr.GetDateTime(2).ToString(AppUtils.DateTime_Format_All) : "";
                        ri.requester = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        values.Add(ri);
                    }
                }               
                if (values.Count > 0)
                {
                    ret = 0;
                }
                else
                {
                    values = null;
                    msg = "未能找到报告单";
                    ret = 17;
                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                values = null;
                msg = GetExceptionInfo(ex);
                return AppUtils.Default_Exception_Code;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                if (null != myReader)
                {
                    myReader.Close();
                }
                oracleConnection.Close();
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// 一个检验报告单抬头信息
        /// </summary>
        /// <param name="doctadviseno">条码号</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_InspectionReportNameInformation(string doctadviseno, out AlipayReportInfo alipayReportInfo, out string msg)
        {

            msg = "";
            alipayReportInfo = new AlipayReportInfo();

            OracleConnection oracleConnection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            SqlConnection sqlConnection = new SqlConnection(WebConfigParameter.ConnectionLisString);
            OracleDataReader dr = null;
            SqlDataReader myReader = null;

            int ret = -1;
            try
            {
                bool _flag = false;
                string sqlstr = "";
                string oraclestr = "";
                _builder.GetInspectionReportNameInformation(doctadviseno, out sqlstr, out oraclestr, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlstr);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, oraclestr);

                if (!_flag)
                {
                    return 10;
                }

                msg = "";
                dr = DbHelperOra.ExecuteReader(oraclestr, oracleConnection);
                if (null != dr && dr.HasRows)
                {
                    if (dr.Read())
                    {
                        alipayReportInfo.doctadviseno = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        alipayReportInfo.requesttime = !dr.IsDBNull(1) ? dr.GetDateTime(1).ToString(AppUtils.DateTime_Format_All) : "";
                        alipayReportInfo.requester = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        alipayReportInfo.executetime = !dr.IsDBNull(3) ? dr.GetDateTime(3).ToString(AppUtils.DateTime_Format_All) : "";
                        alipayReportInfo.executer = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        alipayReportInfo.receivetime = !dr.IsDBNull(5) ? dr.GetDateTime(5).ToString(AppUtils.DateTime_Format_All) : "";
                        alipayReportInfo.receiver = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        alipayReportInfo.stayhospitalmode = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        alipayReportInfo.patientid = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        alipayReportInfo.section = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                        alipayReportInfo.bedno = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        alipayReportInfo.patientname = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        alipayReportInfo.sex = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        alipayReportInfo.age = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        alipayReportInfo.diagnostic = !dr.IsDBNull(14) ? dr.GetString(14) : "";
                        alipayReportInfo.sampletype = !dr.IsDBNull(15) ? dr.GetString(15) : "";
                        alipayReportInfo.examinaim = !dr.IsDBNull(16) ? dr.GetString(16) : "";
                        alipayReportInfo.requestmode = !dr.IsDBNull(17) ? dr.GetString(17) : "";
                        alipayReportInfo.checker = !dr.IsDBNull(18) ? dr.GetString(18) : "";
                        alipayReportInfo.checktime = !dr.IsDBNull(19) ? dr.GetDateTime(19).ToString(AppUtils.DateTime_Format_All) : "";
                        alipayReportInfo.csyq = !dr.IsDBNull(20) ? dr.GetString(20) : "";
                        alipayReportInfo.profiletest = !dr.IsDBNull(21) ? dr.GetString(21) : "";
                        ret = 0;
                    }
                    return ret;
                }

                if (!string.IsNullOrEmpty(WebConfigParameter.ConnectionLisString))
                {
                    myReader = DbHelperSQL.ExecuteReader(sqlstr, sqlConnection);
                    if (null != myReader && myReader.HasRows)
                    {
                        if (myReader.Read())
                        {
                            alipayReportInfo.doctadviseno = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                            alipayReportInfo.requesttime = !myReader.IsDBNull(1) ? myReader.GetDateTime(1).ToString(AppUtils.DateTime_Format_All) : "";
                            alipayReportInfo.requester = !myReader.IsDBNull(2) ? myReader.GetString(2) : "";
                            alipayReportInfo.executetime = !myReader.IsDBNull(3) ? myReader.GetDateTime(3).ToString(AppUtils.DateTime_Format_All) : "";
                            alipayReportInfo.executer = !myReader.IsDBNull(4) ? myReader.GetString(4) : "";
                            alipayReportInfo.receivetime = !myReader.IsDBNull(5) ? myReader.GetDateTime(5).ToString(AppUtils.DateTime_Format_All) : "";
                            alipayReportInfo.receiver = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";
                            alipayReportInfo.stayhospitalmode = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                            alipayReportInfo.patientid = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                            alipayReportInfo.section = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                            alipayReportInfo.bedno = !myReader.IsDBNull(10) ? myReader.GetString(10) : "";
                            alipayReportInfo.patientname = !myReader.IsDBNull(11) ? myReader.GetString(11) : "";
                            alipayReportInfo.sex = !myReader.IsDBNull(12) ? Convert.ToString(myReader.GetInt32(12)) : "";
                            if (alipayReportInfo.sex == "1")
                            {
                                alipayReportInfo.sex = "男";
                            }
                            else if (alipayReportInfo.sex == "2")
                            {
                                alipayReportInfo.sex = "女";
                            }
                            alipayReportInfo.age = !myReader.IsDBNull(13) ? Convert.ToString(myReader.GetDouble(13)) : "";
                            alipayReportInfo.diagnostic = !myReader.IsDBNull(14) ? myReader.GetString(14) : "";
                            alipayReportInfo.sampletype = !myReader.IsDBNull(15) ? myReader.GetString(15) : "";
                            alipayReportInfo.examinaim = !myReader.IsDBNull(16) ? myReader.GetString(16) : "";
                            alipayReportInfo.requestmode = !myReader.IsDBNull(17) ? myReader.GetString(17) : "";
                            alipayReportInfo.checker = !myReader.IsDBNull(18) ? myReader.GetString(18) : "";
                            alipayReportInfo.checktime = !myReader.IsDBNull(19) ? myReader.GetDateTime(19).ToString(AppUtils.DateTime_Format_All) : "";
                            alipayReportInfo.csyq = !myReader.IsDBNull(20) ? myReader.GetString(20) : "";
                            alipayReportInfo.profiletest = !myReader.IsDBNull(21) ? myReader.GetString(21) : "";
                            ret = 0;
                        }
                    }
                }
                else
                {
                    msg = "未能找到报告单";
                    ret = 17;
                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                return AppUtils.Default_Exception_Code;
            }
            finally
            {
                if (null != dr )
                {
                    dr.Close();
                }
                if (null != myReader)
                {
                    myReader.Close();
                }
                oracleConnection.Close();
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// 一个检验报告单详细列表信息
        /// </summary>
        /// <param name="doctadviseno">条码号</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_InspectionReportResultsInformation(string doctadviseno, out AlipayInspectionReport alipayInspectionReport, out string msg)
        {

            msg = "";
            alipayInspectionReport = new AlipayInspectionReport();

            OracleConnection oracleConnection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            SqlConnection sqlConnection = new SqlConnection(WebConfigParameter.ConnectionLisString);
            OracleDataReader dr = null;
            SqlDataReader myReader = null;

            int ret = -1;
            try
            {
                bool _flag = false;
                string sqlstr = "";
                string oraclestr = "";
                _builder.GetDInspectionReportResultsInformation(doctadviseno, out sqlstr, out oraclestr, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, sqlstr);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, oraclestr);

                if (!_flag)
                {
                    return 10;
                }

                msg = "";
                dr = DbHelperOra.ExecuteReader(oraclestr, oracleConnection);
                if (null != dr && dr.HasRows)
                {
                    if (dr.Read())
                    {
                        alipayInspectionReport.studyresult = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        alipayInspectionReport.diagresult = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        ret = 0;
                    }
                 return ret;
                }

                if (!string.IsNullOrEmpty(WebConfigParameter.ConnectionLisString))
                {
                    myReader = DbHelperSQL.ExecuteReader(sqlstr, sqlConnection);
                    if (null != myReader && myReader.HasRows)
                    {
                        if (myReader.Read())
                        {
                            alipayInspectionReport.studyresult = !myReader.IsDBNull(0) ? myReader.GetString(0) : "";
                            alipayInspectionReport.diagresult = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";
                            ret = 0;
                        }
                    }
                }
                else
                {
                    msg = "未能找到报告单";
                    ret = 17;
                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                msg = GetExceptionInfo(ex);
                return AppUtils.Default_Exception_Code;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                if (null != myReader)
                {
                    myReader.Close();
                }
                oracleConnection.Close();
                sqlConnection.Close();
                oracleConnection.Close();
                sqlConnection.Close();
            }
        }

    }
}