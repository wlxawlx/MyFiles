using System;
using System.Collections.Generic;
using System.Web;

using System.Data.OracleClient;
using System.Collections;
using HospitaPaymentService.Wzszhjk.Log;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Utils;
using HospitaPaymentService.Wzszhjk.Common;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;

namespace HospitaPaymentService.Wzszhjk.DAL.Database
{
    public class QueryDB : BaseDB
    {
        private BuilderSql _builder;

        public QueryDB()
        {
            _builder = new BuilderSql();
        }

        /// <summary>
        /// 药品分页查询
        /// </summary>
        /// <param name="pNumber">所在页数</param>
        /// <param name="pRows">每页显示条数</param>
        /// <param name="values">药品信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_PageMedicine(int pNumber, int pRows, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            values = new ArrayList();

            try
            {
                int ret = -99;
                long maxrow;
                long minrow;
                General.CalculatePage(pNumber, pRows, out maxrow, out minrow);

                bool _flag = false;

                string findId1 = _builder.GetMedicinePage(maxrow, minrow, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        MedicineDetail pd = new MedicineDetail();

                        pd.lx = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.mc = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.dw = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.gg = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.cd = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.jg = !dr.IsDBNull(5) ? dr.GetDouble(5) : 0;


                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
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
                return -1;
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
        /// 药品查询按拼音
        /// </summary>
        /// <param name="pydm">拼音代码</param>
        /// <param name="values">药品信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryMedicine(string pydm, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                int ret = -99;
                values = new ArrayList();

                bool _flag = false;

                string findId1 = _builder.GetMedicineByPy(pydm, out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        MedicineDetail pd = new MedicineDetail();

                        pd.lx = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.mc = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.dw = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.gg = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.cd = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.jg = !dr.IsDBNull(5) ? dr.GetDouble(5) : 0;

                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
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
                return -99;
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
        /// 收费查询(分页）
        /// </summary>
        /// <param name="pNumber">所在页数</param>
        /// <param name="pRows">每页显示条数</param>
        /// <param name="values">收费项目</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_PageCharge(int pNumber, int pRows, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();

            try
            {
                int ret = -99;

                long maxrow;
                long minrow;
                General.CalculatePage(pNumber, pRows, out maxrow, out minrow);

                bool _flag = false;

                string findId1 = _builder.GetChargePage(maxrow, minrow, out _flag, out msg);
//                UtilLog.GetInstance().WriteOrderLog("1", findId1);


                if (!_flag)
                {
                    return 10;
                }

                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ChargeDetail pd = new ChargeDetail();

                        pd.lx = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.mc = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.dw = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.jg = !dr.IsDBNull(3) ? dr.GetDouble(3) : 0;

                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 2;
                }

                dr.Close();
                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                msg = GetExceptionInfo(ex); ;
                values = null;
                return -99;
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
        /// 收费项目查询(按拼音码）
        /// </summary>
        /// <param name="pydm">拼音代码</param>
        /// <param name="values">收费信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryCharge(string pydm, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            values = new ArrayList();

            try
            {
                int ret = -1;

                bool _flag = false;

                string findId1 = _builder.GetChargeByPy(pydm, out _flag, out msg);
                if (!_flag)
                {
                    return 10;
                }

                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ChargeDetail pd = new ChargeDetail();

                        pd.lx = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.mc = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.dw = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.jg = !dr.IsDBNull(3) ? dr.GetDouble(3) : 0;

                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
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
                return -99;
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
        /// 查询医生信息
        /// </summary>
        /// <param name="pNumber">所在页数</param>
        /// <param name="pRows">每页显示条数</param>
        /// <param name="values">医生信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_PageDoctor(int pNumber, int pRows, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            int ret = -1;
            try
            {


                string findId1 = _builder.QueryPageDoctorsSql(pNumber, pRows);

                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        DoctorInfo pd = new DoctorInfo();

                        pd.dm = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.xm = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.jb = !dr.IsDBNull(2) ? dr.GetString(2) : "";

                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
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
                return -99;
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
        /// 查询医生信息(按拼音）
        /// </summary>
        /// <param name="queryName">拼音代码或医生名称</param>
        /// <param name="values">医生信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_ListDoctor(string queryName, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                int ret = -99;

                string findId1 = _builder.QueryDoctorInfoSql(queryName);

                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        DoctorInfo pd = new DoctorInfo();

                        pd.dm = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.xm = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.jb = !dr.IsDBNull(2) ? dr.GetString(2) : "";

                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
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
                return -99;
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
        /// 查询医生详细信息
        /// </summary>
        /// <param name="ysdm">医生代码</param>
        /// <param name="values">医生信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryDoctor(string ysdm, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                int ret = -99;

                string findId1 = _builder.QueryDoctorInfoByDmSql(ysdm);

                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        DoctorInfo pd = new DoctorInfo();

                        pd.jj = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.sc = !dr.IsDBNull(1) ? dr.GetString(1) : "";

                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = 2;
                }

                dr.Close();
                return ret;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                values = null;
                return -99;
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
        /// 查询剩余床位数
        /// </summary>
        /// <param name="list">床位信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryRemainBeds(out ArrayList list, out string msg)
        {
            msg = "";
            list = null;


            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                bool _flag = false;

                string sql = _builder.GetSqlRemaindBeds(out _flag, out msg);

                if (!_flag)
                {
                    return 10;
                }

                int ret = -1;

                dr = DbHelperOra.ExecuteReader(sql, connection);
                if (dr.HasRows)
                {
                    list = new ArrayList();
                    while (dr.Read())
                    {
                        RemainBeds ri = new RemainBeds();
                        ri.bqmc = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSYJXDSRMYY)
                        {
                            ri.sycw = ri.zdcws = !dr.IsDBNull(1) ? dr.GetInt32(1) : 0;

                        }
                        else
                        {
                            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDQRMYY ||
                                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXDYRMYY ||
                                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSCNXFYBJYY ||
                                WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSTSXZYY)
                            {
                                ri.bqid = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                            }
                            else
                            {
                                ri.bqid = !dr.IsDBNull(1) ? Convert.ToString(dr.GetInt64(1)) : "";
                            }

                            ri.zzrenshu = !dr.IsDBNull(2) ? dr.GetInt32(2) : 0;

                            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDQRMYY)
                            {
                                ri.zdcws = !dr.IsDBNull(3) ? Convert.ToInt32(dr.GetString(3)) : 0;
                            }
                            else
                            {
                                ri.zdcws = !dr.IsDBNull(3) ? dr.GetInt32(3) : 0;
                            }
                            ri.sycw = (ri.zdcws - ri.zzrenshu) > 0 ? (ri.zdcws - ri.zzrenshu) : 0;
                            if (WebConfigParameter.HospitalName() == AppUtils.HOSPITALNAME.WZSDEYY)
                            {
                                ri.sycw = -ri.zzrenshu;
                            }
                        }
                        list.Add(ri);
                    }

                    ret = 0;
                }
                else
                {
                    list = null;
                    msg = "没有找到床位信息。";
                    ret = 1;
                }

                return ret;
            }
            catch (Exception ex)
            {
                UtilLog.GetInstance().WriteProgramLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex);

                list = null;
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


        #region 温州市中医院特色
        /// <summary>
        /// 取药查询
        /// </summary>
        /// <param name="brxm">病人姓名</param>
        /// <param name="lxdh">联系电话</param>
        /// <param name="values">取药信息</param>
        /// <param name="msg">出错信息</param>
        /// <returns>0：成功  大于0：出错  小于0：异常</returns>
        public int DB_QueryPatientDrugInfo(string brxm, string lxdh, out ArrayList values, out string msg)
        {
            msg = "";

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                int ret = -1;

                string findId1 = _builder.QueryPatientDrugInfoSql(brxm, lxdh);
                dr = DbHelperOra.ExecuteReader(findId1, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        PatientDrugInfo pd = new PatientDrugInfo();

                        pd.rq = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.xh = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.zt = !dr.IsDBNull(2) ? dr.GetString(2) : "";

                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = -2;
                }

                dr.Close();
                return ret;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                values = null;
                return -1;
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
        #endregion
    }
}