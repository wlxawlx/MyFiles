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
    public class MzMedicalRecordsDB : BaseDB
    {
        private BuilderSql _builder;
        public MzMedicalRecordsDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 门诊就诊病历列表
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="values">返回信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_mzMedicalRecordsList(string patientid, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            values = new ArrayList();

            try
            {
                bool _flag = false;

                string findText = _builder.GetmzMedicalRecordsListSql(patientid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(findText, connection);

                while (dr.Read())
                {
                    mzMedicalRecords ri = new mzMedicalRecords();

                    ri.jzxh = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    ri.jzrq = !dr.IsDBNull(1) ? dr.GetDateTime(1).ToString(AppUtils.DateTime_Format_All) : " ";
                    ri.ksmc = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                    ri.ysxm = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    ri.zdxx = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                    values.Add(ri);
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
        ///  电子病历内容
        /// </summary>
        /// <param name="jzxh">就诊序号</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_ElectronicMedicalRecordt(string jzxh, out mzMedicalRecords ri, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            ri = new mzMedicalRecords();
            try
            {

                bool _flag = false;

                string findText = _builder.GetElectronicMedicalRecordtSql(jzxh, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(findText, connection);

                if (dr.Read())
                {
                    ri.mzzs = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    ri.xbs = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    ri.jws = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                    ri.grs = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    ri.gms = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                    ri.hys = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                    ri.jzs = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                    ri.tgjc = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                    ri.fzjc = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                    ri.clyj = !dr.IsDBNull(9) ? dr.GetString(9) : "";
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
        /// 门诊指引单
        /// </summary>
        /// <param name="jzxh">就诊序号</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_mzSingleGuideAndTakeMedicine(string jzxh, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            values = new ArrayList();
            try
            {

                bool _flag = false;

                string findText = _builder.GetmzSingleGuideAndTakeMedicineSql(jzxh, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(findText, connection);
                
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        mzMedicalRecords ri = new mzMedicalRecords();

                        ri.cfxh = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        ri.fphm = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        ri.kfrq = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        ri.ksmc = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        ri.zynr = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        ri.zywz = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        ri.ysxm = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        ri.zjje = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        values.Add(ri);
                    }
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
    }
}