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
    public class PatientMedicationDB : BaseDB
    {
        private BuilderSql _builder;
        public PatientMedicationDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 病人药品处方信息列表
        /// </summary>
        /// <param name="openid">用户标识</param>
        /// <param name="values">返回信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_DrugPrescriptionInforList(string patientid, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            values = new ArrayList();

            try
            {
                bool _flag = false;

                string findText = _builder.GetDrugPrescriptionInforListSql(patientid, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(findText, connection);

                while (dr.Read())
                {
                    Patientmedication ri = new Patientmedication();

                    ri.cflsh = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    ri.ksbm = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    ri.ksmc = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                    ri.ysbm = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    ri.ysxm = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                    ri.brid = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                    ri.kfrq = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                    ri.sfrq = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                    ri.zjje = !dr.IsDBNull(8) ? Convert.ToString(dr.GetDouble(8)) : " ";
                    ri.lxmc = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                    values.Add(ri);
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
        /// 病人药品服用信息
        /// </summary>
        /// <param name="cflsh">处方流水号</param>
        /// <returns>0-成功  大于0-失败   小于0-异常</returns>
        public int DB_PatientsTakingDrugsInfor(string cflsh, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            values = new ArrayList();
            try
            {

                bool _flag = false;

                string findText = _builder.GetPatientsTakingDrugsInforSql(cflsh, out _flag, out msg);
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, findText);

                if (!_flag)
                {
                    ret = 10;
                    return ret;
                }

                dr = DbHelperOra.ExecuteReader(findText, connection);

                while (dr.Read())
                {
                    Patientmedication ri = new Patientmedication();
                    ri.ypmc = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    ri.ypgg = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                    ri.ypsl = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                    ri.ycyl = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    ri.ysjy = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                    ri.sync = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                    ri.gytj = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                    values.Add(ri);
                }
                if (values.Count > 0)
                {
                    msg = "找到药品信息";
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