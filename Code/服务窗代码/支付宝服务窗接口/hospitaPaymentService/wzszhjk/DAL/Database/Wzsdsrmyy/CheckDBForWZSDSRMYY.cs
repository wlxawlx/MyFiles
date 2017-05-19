using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data.SqlClient;
using HospitaPaymentService.Wzszhjk.Dbhelp;
using HospitaPaymentService.Wzszhjk.Model;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;
using HospitaPaymentService.Wzszhjk.Utils.SqlString;


namespace HospitaPaymentService.Wzszhjk.DAL.Database.WZSDSRMYY
{
    public class ReportDBForWZSDSRMYY : BaseDB
    {
        public ReportDBForWZSDSRMYY()
        {
        }

        #region 公有方法 查询检验单
        /// <summary>
        /// 根据病人ID获取检验单列表
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryReportListByBRID(string brid, string brlx, out ArrayList values, out string msg)
        {
            values = new ArrayList();
            msg = "";
            int ret = -1;

            try
            {
                string brxm = "", sfzh = "";
                ret = QueryPatientInfoByBrid(brid, brlx, out brxm, out sfzh, out msg);
                if (0 != ret)
                {
                    return ret;
                }

                string mzhmlist = "";
                ret = DB_inspectionBbxx(sfzh, brxm, out mzhmlist, out msg);
                if (0 != ret)
                {
                    return ret;
                }

                ArrayList tempValue = new ArrayList();
                ret = DB_inspectionList(mzhmlist, out tempValue, out msg);
                if (0 != ret)
                {
                    return ret;
                }

                if (tempValue.Count < 1 || null == tempValue)
                {
                    msg = "化验单列表中未找到病人任何报告单信息";
                    ret = 4;
                    return ret;
                }

                foreach (Inspection pd in tempValue)
                {
                    ReportInfo info = new ReportInfo();
                    info.bgdh = pd.doctadviseno;
                    info.sjmd = pd.examinaim;
                    info.jysj = pd.requesttime;

                    values.Add(info);
                }

                ret = 0;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                ret = -1;
            }
            return ret;
        }

        /// <summary>
        /// 根据报告单号或者条码查询单张报告单信息
        /// </summary>
        /// <param name="code1"></param>
        /// <param name="lx"></param>
        /// <param name="brxm"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryReportListByCode(string code, string lx, string brxm, out ArrayList values, out string msg)
        {
            int ret = -1;

            msg = "";
            values = new ArrayList();

            try
            {
                ArrayList tempValues = new ArrayList();
                ret = DB_inspectionHead(code, out tempValues, out msg);
                if (0 != ret)
                {
                    return ret;
                }

                if (tempValues.Count < 1 || null == tempValues)
                {
                    msg = "化验抬头中未找到报告单， 请检查报告号码";
                    ret = 4;
                }

                ReportInfo info = new ReportInfo();
                foreach (InspectionHead head in tempValues)
                {
                    info.bgdh = head.doctadviseno; //条码号
                    info.sbm = head.doctadviseno;
                    info.cjsj = head.requesttime; //申请时间
                    info.sjr = head.executer;//采集人
                    info.jysj = head.receivetime;//接收时间
                    info.jyr = head.receiver;//接收人
                    info.bbmc = head.stayhospitalmode;//标本来源
                    info.bz = head.section;//申请科室
                    info.jzch = head.bedno;//床号
                    info.brxm = head.patientname;//病人姓名
                    info.sex = head.sex;//性别
                    info.age = head.age;//年龄
                    info.zdjg = head.diagnostic;//诊断
                    info.bbmc = head.sampletype;//标本类型
                    info.sjmd = head.examinaim;//检查项目

                    ArrayList detailValus = null;
                    ret = DB_inspectiondetail(info.bgdh, out detailValus, out msg);

                    if (ret != 0)
                    {
                        return ret;
                    }

                    if (null != detailValus && detailValus.Count > 0)
                    {
                        info.details = new List<ReportDetail>();
                        foreach (InspectionDetial detailInfo in detailValus)
                        {
                            ReportDetail detail = new ReportDetail();
                            detail.mc = detailInfo.xmmc;
                            detail.dw = detailInfo.xmdw;
                            detail.ckjg = detailInfo.ckfw;
                            detail.jg = detailInfo.result;
                            detail.ts = detailInfo.hint;
                            detail.jylx = detailInfo.jylx;

                            info.details.Add(detail);
                        }
                        values.Add(info);
                        ret = 0;
                    }
                    else
                    {
                        msg = "没有找到该条记录的详细信息";
                        ret = 3;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                ret = -1;
            }

            return ret;

        }
        #endregion

        #region 公有方法 获得检查报告
        /// <summary>
        /// 获得检查报告列表
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryReportJCListByBRID(string brid, string brlx, out ArrayList values, out string msg)
        {
            values = new ArrayList();
            msg = "";
            int ret = -1;

            try
            {
                string brxm = "", sfzh = "";
                ret = QueryPatientInfoByBrid(brid, brlx, out brxm, out sfzh, out msg);
                if (0 != ret)
                {
                    return ret;
                }

                ArrayList tempValues = new ArrayList();
                ret = DB_checkList(sfzh, brxm, out tempValues, out msg);

                if (0 != ret)
                {
                    return ret;
                }

                if (null == tempValues || tempValues.Count < 1)
                {
                    msg = "检查报告列表中没有找到该条信息";
                    ret = 1;
                    return ret;
                }

                foreach (CheckReportInfo info in tempValues)
                {
                    ReportInfo reportInfo = new ReportInfo();
                    reportInfo.bgdh = info.doctadviseno;
                    reportInfo.sjmd = info.examinaim;
                    reportInfo.jysj = info.requesttime;

                    if (!string.IsNullOrEmpty(reportInfo.bgdh))
                    {
                        values.Add(reportInfo);
                    }
                }

                ret = 0;
            }
            catch (Exception e)
            {
                msg = e.StackTrace; 
                ret = -1;
            }

            return ret;
        }

        /// <summary>
        /// 根据报告单号或者条码查询单张报告单信息
        /// </summary>
        /// <param name="code1"></param>
        /// <param name="lx"></param>
        /// <param name="brxm"></param>
        /// <param name="values"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryReportJCByCode(string code, string lx, out ArrayList values, out string msg)
        {
            int ret = -1;

            msg = "";
            values = new ArrayList();

            try
            {
                long _xmdm = 0;
                string _requesttime = "";
                string _requester = "";
                string _section = "";

                ret = DB_checkJclb(code, out _requesttime, out _requester, out _section, out _xmdm, out msg);
                if (ret != 0)
                {
                    return ret;
                }

                ArrayList tempList = new ArrayList();
                ret = getReportJCHeadInfo(code, _xmdm, _requesttime, _requester, _section, out tempList, msg);
                if (ret != 0)
                {
                    return ret;
                }

                if (null == tempList || tempList.Count < 1)
                {
                    msg = "没有找到该号码的检查单信息";
                    ret = 2;
                    return ret;
                }

                ReportInfo info = new ReportInfo();
                foreach (CheckHead head in tempList)
                {
                    info.bgdh = head.doctadviseno;
                    info.sbm = head.doctadviseno; //条码号
                    info.cjsj = head.requesttime; //申请时间
                    info.sjr = head.executer;//采集人
                    info.jyr = head.receivetime;//接收时间
                    info.jysj = head.receiver;//接收人
                    info.bbmc = head.stayhospitalmode;//标本来源
                    info.bz = head.section;//申请科室
                    info.jzch = head.bedno;//床号
                    info.brxm = head.patientname;//病人姓名
                    info.sex = head.sex;//性别
                    info.age = head.age;//年龄
                    info.zdjg = head.diagnostic;//诊断
                    info.bbmc = head.sampletype;//标本类型
                    info.sjmd = head.examinaim;//检查项目

                    ArrayList detailValus = new ArrayList();
                    string error_msg;
                    _xmdm = 0;
                    ret = DB_checkJclb(code, out _requesttime, out _requester, out _section, out _xmdm, out error_msg);
                    if (ret != 0)
                    {
                        return ret;
                    }

                    ArrayList _detailList = new ArrayList();
                    ret = checkDetail(code, _xmdm, out _detailList, out error_msg);

                    if (ret != 0)
                    {
                        return ret;
                    }

                    if (null != _detailList && _detailList.Count > 0)
                    {
                        info.details = new List<ReportDetail>();
                        foreach (CheckDetial detailInfo in _detailList)
                        {
                            ReportDetail detail = new ReportDetail();
                            detail.ts = detailInfo.studyresult;
                            detail.jg = detailInfo.diagresult;

                            info.details.Add(detail);
                        }

                        values.Add(info);
                        ret = 0;
                    }
                    else
                    {
                        msg = "没有找到该条记录的详细信息";
                        ret = 3;
                    }
                }
            }
            catch (Exception e)
            {
                msg = e.StackTrace; 
                ret = -1;
            }

            return ret;
        }

        #endregion



        #region  私有方法 检查单信息
        //化验列表-病人信息
        private int DB_inspectionBbxx(string sfzh, string brxm, out string mzhmlist, out string msg)
        {
            msg = "";
            mzhmlist = "";
            sfzh = sfzh.ToUpper();
            brxm = brxm.Trim();
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            string _temp = "";
            ArrayList _mzhm = new ArrayList();

            int ret = -1;

            try
            {
                string selectSql = "SELECT MZHM FROM MS_BRDA WHERE SFZH = '" + sfzh + "' and trim(brxm) = '" + brxm + "'";
                dr = DbHelperOra.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        _temp = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        _mzhm.Add(_temp);
                    }
                }
                dr.Close();

                if (_mzhm.Count <= 0)
                {
                    msg = "根据该用户身份证和姓名，没有找到记录";
                    ret = -1;
                    return ret;
                }

                for (int i = 0; i < _mzhm.Count; i++)
                {
                    if (i == 0)
                    {
                        mzhmlist = "'" + _mzhm[i] + "'";
                    }
                    else
                    {
                        mzhmlist = mzhmlist + ",'" + _mzhm[i] + "'";
                    }
                }
                ret = 0;
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                if (null != connection)
                {
                    connection.Close();
                }
            }

            return ret;
        }
        //化验列表
        private int DB_inspectionList(string mzhmlist, out ArrayList values, out string msg)
        {
            msg = "";
            values = new ArrayList();
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionLisString);
            OracleDataReader dr = null;

            int ret = -1;

            try
            {
                string selectSql = "select requisition_id,test_order_name,to_char(requisition_time,'yyyy-mm-dd hh24:mi:ss'),requisition_person from view_inspection_sample where outpatient_id  in (" + mzhmlist + ") and patient_type = 2 and requisition_time > sysdate - 90 and inspection_state in ('audited','reported') order by requisition_id";
                dr = DbHelperOra.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Inspection pd = new Inspection();

                        pd.doctadviseno = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.examinaim = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.requesttime = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.requester = !dr.IsDBNull(3) ? dr.GetString(3) : "";

                        if (!string.IsNullOrEmpty(pd.doctadviseno))
                        {
                            values.Add(pd);
                        }
                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，没有记录";
                    values = null;
                    ret = -2;
                }
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                values = null;
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }
        //化验台头
        private int DB_inspectionHead(string doctadviseno, out ArrayList values, out string msg)
        {
            msg = "";
            values = null;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionLisString);
            OracleDataReader dr = null;
            string selectSql = "";
            int ret = -1;
            try
            {
                selectSql = "select requisition_id,to_char(requisition_time,'yyyy-mm-dd hh24:mi:ss'),requisition_person,to_char(sampling_time,'yyyy-mm-dd hh24:mi:ss'),sampling_person," +
                             "'','','',outpatient_id,patient_dept_name,patient_bed,patient_name,decode(patient_sex,1,'男',2,'女'),age_input,clinical_diagnoses_name,sample_class_name," +
                             "test_order_name,'',check_person,to_char(check_time,'yyyy-mm-dd hh24:mi:ss'),'','' from view_inspection_sample where requisition_id ='" + doctadviseno + "'";
                dr = DbHelperOra.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    values = new ArrayList();
                    if (dr.Read())
                    {
                        InspectionHead pd = new InspectionHead();

                        pd.doctadviseno = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.requesttime = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.requester = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.executetime = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.executer = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.receivetime = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        pd.receiver = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        pd.stayhospitalmode = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        pd.patientid = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        pd.section = !dr.IsDBNull(9) ? dr.GetString(9) : "";
                        pd.bedno = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        pd.patientname = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        pd.sex = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        pd.age = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        pd.diagnostic = !dr.IsDBNull(14) ? dr.GetString(14) : "";
                        pd.sampletype = !dr.IsDBNull(15) ? dr.GetString(15) : "";
                        pd.examinaim = !dr.IsDBNull(16) ? dr.GetString(16) : "";
                        pd.requestmode = !dr.IsDBNull(17) ? dr.GetString(17) : "";
                        pd.checker = !dr.IsDBNull(18) ? dr.GetString(18) : "";
                        pd.checktime = !dr.IsDBNull(19) ? dr.GetString(19) : "";
                        pd.csyq = !dr.IsDBNull(20) ? dr.GetString(20) : "";
                        pd.profiletest = !dr.IsDBNull(21) ? dr.GetString(21) : "";

                        values.Add(pd);
                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，该条吗号码不存在";
                    values = null;
                    ret = -1;
                }
                dr.Close();
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

            return ret;
        }

        //化验明细
        private int DB_inspectiondetail(string doctadviseno, out ArrayList values, out string msg)
        {
            msg = "";
            values = null;

            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionLisString);
            OracleDataReader dr = null;

            int ret = -1;

            try
            {
                string _inspection_id = "";
                string _group_id = "";
                string _requisition_id = "";
                dr = DbHelperOra.ExecuteReader("select inspection_id,group_id,requisition_id from lis_inspection_sample where requisition_id = '" + doctadviseno + "'", connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        _inspection_id = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        _group_id = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        _requisition_id = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        if (_requisition_id != doctadviseno)
                        {
                            msg = "该条码号码有误";
                            values = null;
                            return 1;
                        }
                    }
                    else
                    {
                        msg = "记录已找到，但是没有检验记录";
                        values = null;
                        return 2;
                    }
                }
                else
                {
                    msg = "该条码号码没有检验记录，确认条码号码";
                    values = null;
                    return 3;
                }
                if (null != dr)
                {
                    dr.Close();
                }


                if (_group_id != "G999")
                {
                    string findId1 = "select trim( chinese_name )||(case when english_name is null Or trim(english_name) =''  then '' else '(' ||trim(english_name)||')' end),quantitative_result,"
                                    + " case when qualitative_result = 'h' then '↑'when qualitative_result = 'l' then '↓' when qualitative_result = 'ah' then '↑' when qualitative_result = 'al' then '↓' when qualitative_result = 'ch' then '↑' when qualitative_result = 'cl' then '↓' else '' end, "
                                    + " test_item_reference,test_item_unit from view_inspection_result where inspection_id ='" + _inspection_id + "' order by test_item_sort";
                    Console.WriteLine(findId1);
                    dr = DbHelperOra.ExecuteReader(findId1, connection);

                    if (dr.HasRows)
                    {
                        values = new ArrayList();
                        while (dr.Read())
                        {
                            InspectionDetial pd = new InspectionDetial();

                            pd.jylx = "common";
                            pd.xmmc = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                            pd.result = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                            pd.hint = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                            pd.ckfw = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                            pd.xmdw = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                            pd.micmc = "";
                            pd.mictpjg = "";
                            pd.micjg = "";
                            pd.mickss = "";
                            pd.micdl = "";
                            pd.micdx = "";
                            values.Add(pd);

                        }
                        ret = 0;
                    }
                    else
                    {
                        msg = "亲，没有记录";
                        values = null;
                        ret = 4;
                    }
                }
                else
                {
                    string findId2 = "select micro_data_id_name,decode(result_type,'py','阴性','ya','阳性'),micro_data_id_result1,micro_data_id2_name,micro_data_id2_result1,micro_data_id2_result2 from view_inspection_result_micro"
                                    + " where inspection_id ='" + _inspection_id + "'";
                    Console.WriteLine(findId2);
                    dr = DbHelperOra.ExecuteReader(findId2, connection);

                    if (dr.HasRows)
                    {
                        values = new ArrayList();
                        while (dr.Read())
                        {
                            InspectionDetial pd = new InspectionDetial();

                            pd.jylx = "micro";
                            pd.xmmc = "";
                            pd.result = "";
                            pd.hint = "";
                            pd.ckfw = "";
                            pd.xmdw = "";
                            pd.micmc = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                            pd.mictpjg = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                            pd.micjg = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                            pd.mickss = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                            pd.micdl = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                            pd.micdx = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                            values.Add(pd);

                        }
                        ret = 0;
                    }
                    else
                    {
                        msg = "亲，没有记录";
                        values = null;
                        ret = 4;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                values = null;
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }
        #endregion


        #region 私有方法 检查单信息
        /// <summary>
        /// 检查报告明细
        /// </summary>
        /// <param name="code"></param>
        /// <param name="_xmdm"></param>
        /// <param name="list"></param>
        /// <param name="error_msg"></param>
        /// <returns></returns>
        private int checkDetail(string code, long _xmdm, out ArrayList list, out string error_msg)
        {
            int ret = -1;
            list = null;
            try
            {
                switch (_xmdm)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 14:
                    case 17:
                    case 18:
                    case 30:
                    case 31:
                    case 34:
                    case 37:
                    case 38:
                        ret = DB_checkdetailPacs(code, out list, out error_msg);
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                        ret = DB_checkdetailXdt(code, out list, out error_msg);
                        break;
                    case 15:
                    case 16:
                        ret = DB_checkdetailWj(code, out list, out error_msg);
                        break;
                    default:
                        error_msg = "检查结果不支持";
                        break;
                }
            }
            catch (Exception e)
            {
                error_msg = e.Message;
                ret = -1;
            }

            return ret;
        }

        /// <summary>
        /// 获得检查报告头信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="_xmdm"></param>
        /// <param name="_requesttime"></param>
        /// <param name="_requester"></param>
        /// <param name="_section"></param>
        /// <param name="list"></param>
        /// <param name="error_msg"></param>
        /// <returns></returns>
        private int getReportJCHeadInfo(string code, long _xmdm, string _requesttime, string _requester, string _section,
            out ArrayList list, string error_msg)
        {
            list = null;
            int ret = -1;

            try
            {
                switch (_xmdm)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 14:
                    case 17:
                    case 18:
                    case 30:
                    case 31:
                    case 34:
                    case 37:
                    case 38:
                        ret = DB_checkHeadPacs(code, _requesttime, _requester, _section, out list, out error_msg);
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                        ret = DB_checkHeadXdt(code, _requesttime, _requester, _section, out list, out error_msg);
                        break;
                    case 15:
                    case 16:
                        ret = DB_checkHeadWj(code, _requesttime, _requester, _section, out list, out error_msg);
                        break;
                    default:
                        error_msg = "检查结果不支持";
                        ret = 1;
                        break;
                }
            }
            catch (Exception e)
            {
                error_msg = e.Message;
                ret = -1;
            }

            return ret;
        }

        //检查列表
        private int DB_checkList(string sfzh, string brxm, out ArrayList values, out string msg)
        {
            msg = "";
            values = null;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            string selectSql = "";
            int ret = -1;
            long _temp = 0;
            ArrayList _brid = new ArrayList();
            try
            {
                selectSql = "SELECT BRID FROM MS_BRDA WHERE SFZH = '" + sfzh + "' and trim(brxm) = '" + brxm + "'";
                dr = DbHelperOra.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        _temp = !dr.IsDBNull(0) ? dr.GetInt64(0) : 0;
                        _brid.Add(_temp);
                    }
                }
                dr.Close();

                if (_brid.Count <= 0)
                {
                    msg = "根据该用户身份证和姓名，没有找到记录";
                    return -1;
                }

                selectSql = "select to_char(yjxh),jcxm,to_char(kdrq,'yyyy-mm-dd hh24:mi:ss'),kdys from v_web_jcxx where kdrq > sysdate - 90 " +
                            " and brid in (select brid from ms_brda where SFZH = '" + sfzh + "' and trim(brxm) = '" + brxm + "') order by kdrq";

                dr = DbHelperOra.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        CheckReportInfo pd = new CheckReportInfo();

                        pd.doctadviseno = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.examinaim = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        pd.requesttime = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        pd.requester = !dr.IsDBNull(3) ? dr.GetString(3) : "";

                        values.Add(pd);
                    }
                    ret = 0;
                }
                else
                {
                    msg = "检查列表没有记录";
                    values = null;
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                values = null;
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }
        //检查台头_检查类别
        private int DB_checkJclb(string doctadviseno, out string requesttime, out string requester, out string section, out long xmdm, out string msg)
        {
            msg = "";
            xmdm = 0;
            requesttime = "";
            requester = "";
            section = "";
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;
            string selectSql = "";
            int ret = -1;
            long _yjxh = 0;
            _yjxh = Convert.ToInt64(doctadviseno);
            try
            {
                selectSql = "select xmdm,to_char(kdrq,'yyyy-mm-dd hh24:mi:ss'),kdys,kdks from v_web_jcxx where yjxh = " + _yjxh;
                dr = DbHelperOra.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        xmdm = !dr.IsDBNull(0) ? dr.GetInt64(0) : 0;
                        requesttime = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        requester = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        section = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                    }
                    ret = 0;
                }
                else
                {
                    msg = "亲，该检查号码不存在";
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }
        //检查台头_PACS
        private int DB_checkHeadPacs(string doctadviseno, string requesttime, string requester, string section, out ArrayList values, out string msg)
        {
            msg = "";
            values = null;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionPacsString);
            OracleDataReader dr = null;
            string selectSql = "";
            int ret = -1;
            string _orderseq = "";
            try
            {
                selectSql = "select orderseq from t_order where his_id = '" + doctadviseno + "'";
                dr = DbHelperOra.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        _orderseq = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    }
                    ret = 1;
                }
                else
                {
                    msg = "亲，该检查号码不存在,或者不是电子申请单";
                    ret = -1;
                }
                dr.Close();

                if (ret == -1)
                {
                    return -1;
                }

                selectSql = "select '','','',orderdt,orderdr,'','','',patseq,'','',patname,decode(patsex,'F','女','M','男')," +
                            "to_char(to_number(to_char(sysdate,'yyyy')) - to_number(substr(patbirthday,1,4))),disease,modality,orderexamitem,''," +
                            "reportdr,reportdt,'','' From viewqueryreport where orderseq = '" + _orderseq + "'";
                dr = DbHelperOra.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    values = new ArrayList();
                    if (dr.Read())
                    {
                        CheckHead pd = new CheckHead();

                        pd.doctadviseno = doctadviseno;
                        pd.requesttime = requesttime;
                        pd.requester = requester;
                        pd.executetime = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.executer = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.receivetime = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        pd.receiver = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        pd.stayhospitalmode = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        pd.patientid = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        pd.section = section;
                        pd.bedno = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        pd.patientname = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        pd.sex = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        pd.age = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        pd.diagnostic = !dr.IsDBNull(14) ? dr.GetString(14) : "";
                        pd.sampletype = !dr.IsDBNull(15) ? dr.GetString(15) : "";
                        pd.examinaim = !dr.IsDBNull(16) ? dr.GetString(16) : "";
                        pd.requestmode = !dr.IsDBNull(17) ? dr.GetString(17) : "";
                        pd.checker = !dr.IsDBNull(18) ? dr.GetString(18) : "";
                        pd.checktime = !dr.IsDBNull(19) ? dr.GetString(19) : "";
                        pd.csyq = !dr.IsDBNull(20) ? dr.GetString(20) : "";
                        pd.profiletest = !dr.IsDBNull(21) ? dr.GetString(21) : "";
                        values.Add(pd);
                    }
                    ret = 0;
                }
                else
                {
                    msg = "检查抬头PACS该检查号码不存在";
                    values = null;
                    ret = 2;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                values = null;
                ret = 1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }

        //检查明细_PACS
        private int DB_checkdetailPacs(string doctadviseno, out ArrayList values, out string msg)
        {
            msg = "";
            values = null;
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionPacsString);
            OracleDataReader dr = null;
            string selectSql = "";
            int ret = -1;
            string _orderseq = "";
            try
            {

                selectSql = "select orderseq from t_order where his_id = '" + doctadviseno + "'";
                dr = DbHelperOra.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        _orderseq = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                    }
                    ret = 1;
                }
                else
                {
                    msg = "亲，该检查号码不存在,或者不是电子申请单";
                    ret = -1;
                }
                dr.Close();
                if (ret == -1)
                {
                    return -1;
                }

                selectSql = "select studyresult,diagresult from viewqueryreport where orderseq = '" + _orderseq + "'";
                dr = DbHelperOra.ExecuteReader(selectSql, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        CheckDetial pd = new CheckDetial();

                        pd.studyresult = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.diagresult = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "PACS表中没有记录";
                    values = null;
                    ret = -2;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                values = null;
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }

        //检查台头_心电
        private int DB_checkHeadXdt(string doctadviseno, string requesttime, string requester, string section, out ArrayList values, out string msg)
        {
            msg = "";
            values = null;
            SqlConnection connection = new SqlConnection(WebConfigParameter.ConnectionXdtString);
            SqlDataReader dr = null;
            string selectSql = "";
            int ret = -1;
            long _temp = 0;
            try
            {
                selectSql = "select count(applycode) from dbo.viewreport1001 where applycode = '" + doctadviseno + "'";
                dr = DbHelperSQL.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        _temp = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                    }
                    ret = 1;
                }
                else
                {
                    msg = "亲，该检查号码不存在,或者不是电子申请单";
                    ret = -1;
                }
                dr.Close();

                if (ret == -1)
                {
                    return -1;
                }

                selectSql = "select '','','',repcreatedate,repcreator,repcreatedate,repcreator,'',cliniccode,applydepartment,''," +
                            "patname,pgender,cast(patientage as varchar),'','XDT',checkproject,'',repcreator,repcreatedate,'',''" +
                            " from dbo.viewreport1001 where applycode = '" + doctadviseno + "'";
                dr = DbHelperSQL.ExecuteReader(selectSql, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    if (dr.Read())
                    {
                        CheckHead pd = new CheckHead();

                        pd.doctadviseno = doctadviseno;
                        pd.requesttime = requesttime;
                        pd.requester = requester;
                        pd.executetime = !dr.IsDBNull(3) ? dr.GetDateTime(3).ToString() : "";
                        pd.executer = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.receivetime = !dr.IsDBNull(5) ? dr.GetDateTime(5).ToString() : "";
                        pd.receiver = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        pd.stayhospitalmode = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        pd.patientid = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        pd.section = section;
                        pd.bedno = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        pd.patientname = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        pd.sex = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        pd.age = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        pd.diagnostic = !dr.IsDBNull(14) ? dr.GetString(14) : "";
                        pd.sampletype = !dr.IsDBNull(15) ? dr.GetString(15) : "";
                        pd.examinaim = !dr.IsDBNull(16) ? dr.GetString(16) : "";
                        pd.requestmode = !dr.IsDBNull(17) ? dr.GetString(17) : "";
                        pd.checker = !dr.IsDBNull(18) ? dr.GetString(18) : "";
                        pd.checktime = !dr.IsDBNull(19) ? dr.GetDateTime(19).ToString() : "";
                        pd.csyq = !dr.IsDBNull(20) ? dr.GetString(20) : "";
                        pd.profiletest = !dr.IsDBNull(21) ? dr.GetString(21) : "";
                        values.Add(pd);
                    }
                    ret = 0;
                }
                else
                {
                    msg = "心电抬头该检查号码不存在";
                    values = null;
                    ret = 2;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                values = null;
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }
        //检查明细_心电
        private int DB_checkdetailXdt(string doctadviseno, out ArrayList values, out string msg)
        {
            msg = "";
            values = null;
            SqlConnection connection = new SqlConnection(WebConfigParameter.ConnectionXdtString);
            SqlDataReader dr = null;
            string selectSql = "";
            int ret = -1;
            long _temp = 0;
            try
            {

                selectSql = "select count(applycode) from dbo.viewreport1001 where applycode = '" + doctadviseno + "'";
                dr = DbHelperSQL.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        _temp = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                    }
                    ret = 1;
                }
                else
                {
                    msg = "亲，该检查号码不存在,或者不是电子申请单";
                    ret = -1;
                }
                dr.Close();
                if (ret == -1)
                {
                    return -1;
                }

                selectSql = "select clincdiagnose,repdiagnose from dbo.viewreport1001 where applycode = '" + doctadviseno + "'";
                dr = DbHelperSQL.ExecuteReader(selectSql, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        CheckDetial pd = new CheckDetial();

                        pd.studyresult = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.diagresult = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "心电明细表没有记录";
                    values = null;
                    ret = 2;
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                values = null;
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }
        //检查台头_胃镜
        private int DB_checkHeadWj(string doctadviseno, string requesttime, string requester, string section, out ArrayList values, out string msg)
        {
            msg = "";
            values = null;
            SqlConnection connection = new SqlConnection(WebConfigParameter.ConnectionWjString);
            SqlDataReader dr = null;
            string selectSql = "";
            int ret = -1;
            long _temp = 0;
            try
            {
                selectSql = "select count(trepid) from dbo.heartrep where thangye = '" + doctadviseno + "'";
                dr = DbHelperSQL.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        _temp = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                    }
                    ret = 1;
                }
                else
                {
                    msg = "亲，该检查号码不存在,或者不是电子申请单";
                    ret = -1;
                }
                dr.Close();
                if (ret == -1)
                {
                    return -1;
                }
                selectSql = "select '','','',convert(varchar(100),a.dcheckdate,120),a.Tcheckdoc,convert(varchar(100),a.dcheckdate,120),a.Tcheckdoc,'',a.brbm,'',''," +
                            " a.tname,a.tsex,rtrim(a.tage) + ltrim(a.tmonth),b.lczd,'WJ',a.tsjcheck,'',a.Tcheckdoc,convert(varchar(100),a.dcheckdate,120),'',''" +
                            " from dbo.heartrep a,dbo.tshenqingdan b where a.thangye = b.yjxh and a.thangye ='" + doctadviseno + "'";
                dr = DbHelperSQL.ExecuteReader(selectSql, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    if (dr.Read())
                    {
                        CheckHead pd = new CheckHead();

                        pd.doctadviseno = doctadviseno;
                        pd.requesttime = requesttime;
                        pd.requester = requester;
                        pd.executetime = !dr.IsDBNull(3) ? dr.GetString(3) : "";
                        pd.executer = !dr.IsDBNull(4) ? dr.GetString(4) : "";
                        pd.receivetime = !dr.IsDBNull(5) ? dr.GetString(5) : "";
                        pd.receiver = !dr.IsDBNull(6) ? dr.GetString(6) : "";
                        pd.stayhospitalmode = !dr.IsDBNull(7) ? dr.GetString(7) : "";
                        pd.patientid = !dr.IsDBNull(8) ? dr.GetString(8) : "";
                        pd.section = section;
                        pd.bedno = !dr.IsDBNull(10) ? dr.GetString(10) : "";
                        pd.patientname = !dr.IsDBNull(11) ? dr.GetString(11) : "";
                        pd.sex = !dr.IsDBNull(12) ? dr.GetString(12) : "";
                        pd.age = !dr.IsDBNull(13) ? dr.GetString(13) : "";
                        pd.diagnostic = !dr.IsDBNull(14) ? dr.GetString(14) : "";
                        pd.sampletype = !dr.IsDBNull(15) ? dr.GetString(15) : "";
                        pd.examinaim = !dr.IsDBNull(16) ? dr.GetString(16) : "";
                        pd.requestmode = !dr.IsDBNull(17) ? dr.GetString(17) : "";
                        pd.checker = !dr.IsDBNull(18) ? dr.GetString(18) : "";
                        pd.checktime = !dr.IsDBNull(19) ? dr.GetString(19) : "";
                        pd.csyq = !dr.IsDBNull(20) ? dr.GetString(20) : "";
                        pd.profiletest = !dr.IsDBNull(21) ? dr.GetString(21) : "";
                        values.Add(pd);
                    }
                    ret = 0;
                }
                else
                {
                    msg = "胃镜表中该检查号码不存在";
                    values = null;
                    ret = 2;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                values = null;
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }
        //检查明细_胃镜
        private int DB_checkdetailWj(string doctadviseno, out ArrayList values, out string msg)
        {
            msg = "";
            values = null;
            SqlConnection connection = new SqlConnection(WebConfigParameter.ConnectionWjString);
            SqlDataReader dr = null;
            string selectSql = "";
            int ret = -1;
            long _temp = 0;
            string _tcheckpart = "";
            try
            {

                selectSql = "select count(trepid) from dbo.heartrep where thangye = '" + doctadviseno + "'";
                dr = DbHelperSQL.ExecuteReader(selectSql, connection);
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        _temp = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                    }
                    ret = 1;
                }
                else
                {
                    msg = "亲，该检查号码不存在,或者不是电子申请单";
                    ret = -1;
                }
                dr.Close();
                if (ret == -1)
                {
                    return -1;
                }

                selectSql = "select mfind,mdiag,tcheckpart from dbo.heartrep where thangye = '" + doctadviseno + "'";
                dr = DbHelperSQL.ExecuteReader(selectSql, connection);

                if (dr.HasRows)
                {
                    values = new ArrayList();
                    while (dr.Read())
                    {
                        CheckDetial pd = new CheckDetial();

                        pd.studyresult = !dr.IsDBNull(0) ? dr.GetString(0) : "";
                        pd.diagresult = !dr.IsDBNull(1) ? dr.GetString(1) : "";
                        _tcheckpart = !dr.IsDBNull(2) ? dr.GetString(2) : "";
                        if (_tcheckpart.Trim().Length > 0)
                        {
                            pd.diagresult = pd.diagresult + "　【病理诊断】" + _tcheckpart;
                        }
                        values.Add(pd);

                    }
                    ret = 0;
                }
                else
                {
                    msg = "胃镜明细表没有记录";
                    values = null;
                    ret = 2;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                values = null;
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                connection.Close();
            }

            return ret;
        }

        #endregion
        /// <summary>
        /// 通过病人ID 查询病人信息
        /// </summary>
        /// <param name="brid"></param>
        /// <param name="brlx"></param>
        /// <param name="brxm"></param>
        /// <param name="sfzh"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int QueryPatientInfoByBrid(string brid, string brlx, out string brxm, out string sfzh, out string msg)
        {
            int ret = -1;
            msg = "";
            brxm = "";
            sfzh = "";
            OracleConnection connection = new OracleConnection(WebConfigParameter.ConnectionHisString);
            OracleDataReader dr = null;

            try
            {
                BuilderSql builder = new BuilderSql();
                bool flag = false;

                string sql = builder.QueryPainterInfoForWZSDSRMYY(brid, brlx, out flag, out msg);
                if (!flag)
                {
                    return 10;
                }

                dr = DbHelperOra.ExecuteReader(sql, connection);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        brxm = !dr.IsDBNull(0) ? dr.GetString(0).Trim() : "";
                        sfzh = !dr.IsDBNull(1) ? dr.GetString(1).Trim().ToUpper() : "";
                    }

                    if (string.IsNullOrEmpty(brxm) || string.IsNullOrEmpty(sfzh))
                    {
                        msg = "根据病人ID查询出来的病人姓名或身份证号为空";
                        ret = 3;
                    }
                    else
                    {
                        ret = 0;
                    }
                }
                else
                {
                    msg = "病人信息表中没有找到记录";
                    ret = 2;
                }
            }
            catch (Exception ex)
            {
                msg = GetExceptionInfo(ex);
                ret = -1;
            }
            finally
            {
                if (null != dr)
                {
                    dr.Close();
                }
                if (null != connection)
                {
                    connection.Close();
                }
            }

            return ret;
        }
    }
}