using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    public class SQLUtilsWZSRAZYY : SQLUtils
    {
        public SQLUtilsWZSRAZYY()
        {
            mzbrQueryStr = "select ms.mzhm brid, gy.brkh bkhm, (case when length(knxx) = 41 then  " +
               " 'smk' when length(knxx) = 28 then 'ybk' else 'byk' end) bklx, ms.brxm brxm, NLS_UPPER(ms.sfzh) sfzh, ms.jtdh lxdh, " +
             " ms.hkdz jtdz from ms_brzh gy, ms_brda ms where ms.brid = gy.brid and ms.brxm = '{0}' and NLS_UPPER(ms.sfzh) = '{1}'";
            zybrQueryStr = "select zy.zyhm brid,  " +
            " zy.YBKH as bkhm, (case when length(zy.jzkh) = 41 then 'smk' when length(zy.jzkh) = 28 then 'ybk' else  'byk' end) bklx, zy.brxm brxm," +
            " NLS_UPPER(zy.sfzh) sfzh, zy.lxdh lxdh, zy.hkdz jtdz,zy.ryrq ryrq, ks.ksmc szbq, zy.brch szcwh, zy.zyhm as zyh " +
            " from zy_brry zy, gy_ksdm ks where zy.brbq = ks.ksdm and zy.brxm = '{0}' and NLS_UPPER(zy.sfzh) = '{1}'";

            mzbrBindCardstr = "select  ms.jtdh lxdh from ms_brzh gy, ms_brda ms where ms.brid = gy.brid and " +
                             " ms.mzhm = '{0}' and NLS_UPPER(ms.sfzh) = '{1}' and ms.brxm = '{2}' ";
            zybrBindCardstr = "select zy.lxdh lxdh from zy_brry zy, gy_ksdm ks where zy.brbq = ks.ksdm and zy.zyhm = '{0}'" +
                 " and NLS_UPPER(zy.sfzh) = '{1}' and zy.brxm = '{2}' ";

            mzbrValidBkhm = "select ms.mzhm brid from  ms_brzh gy,  ms_brda ms where  ms.brid = gy.brid  and " +
                 " gy.brkh = '{0}' and ms.mzhm = '{1}'";
            zybrValidBkhm = "select zy.zyhm brid, zy.ybkh  bkhm " +
                 " from  zy_brry zy  where  zy.ybkh = '{0}' and zy.zyhm = '{1}'";

            mzbrForCreateOrder = "select  ms.brxm brxm, NLS_UPPER(ms.sfzh) sfzh, ms.jtdh lxdh from ms_brda ms where ms.mzhm = '{0}'";
            zybrForCreateOrder = "select zy.brxm, NLS_UPPER(zy.sfzh), zy.lxdh from zy_brry zy where zy.zyhm = '{0}'";

            mzbrQueryBalance = "select zhye from  ms_brzh a ,  ms_brda b where a.brid=b.brid and mzhm  = '{0}' and zfpb=0";
            zybrQueryBalance = "select zhye from  ms_brzh a ,  zy_brry b where a.brkh=b.ybkh and b.ybkh is not null and zyhm  = '{0}' and zfpb=0";

            mzbrValidId = "select count(ms.mzhm)  from ms_brda ms where ms.mzhm = '{0}'";
            zybrValidId = "select count(ms.zyhm)  from zy_brry ms where ms.zyhm = '{0}'";


            mzbrReportList = "select a.SAMPLENO bgdh,a.examinaim sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj, '' sjr, to_char(a.checktime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                        "a.receiver jyr,a.checkoperator shr,'' jzch,a.notes zdjg,'' bbmc,decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                        "'' as dyjb,'' as bz, '' as hzbh,'' as sbm,a.patientname brxm from l_patientinfo a,l_sampletype d where a.resultstatus in (4, 5, 6)  and a.sampletype = d.sampletype(+)" +
                   "and a.stayhospitalmode in (1, 3, 4, 5, 6, 7, 8) and a.patientid = '{0}' order by a.executetime desc  ";
            zybrReportList = "select a.SAMPLENO bgdh,a.examinaim sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj, '' sjr, to_char(a.checktime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                        "a.receiver jyr,a.checkoperator shr,'' jzch,a.notes  zdjg,'' bbmc,decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                        "'' as dyjb,'' as bz, '' as hzbh,'' as sbm,a.patientname brxm from l_patientinfo a,l_sampletype d where a.resultstatus in (4, 5, 6)  and a.sampletype = d.sampletype(+)" +
                   " and a.patientid = '{0}' order by a.executetime desc  ";

            reportDetailByBgdh = "select a.SAMPLENO bgdh,a.examinaim sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj,'' as sjr,to_char(a.checktime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                        "a.receiver jyr,a.checkoperator shr,'' as jzch,a.notes zdjg,d.sampledescribe bbmc,decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                        "'' as dyjb,'' as bz, '' as hzbh,'' as sbm, a.patientname brxm from l_patientinfo a,l_sampletype d where a.sampletype = d.sampletype(+) and a.resultstatus in (4, 5, 6) and '2' = '2' and a.SAMPLENO = '{0}'";
            reportDetailBySbm = "select a.SAMPLENO bgdh,a.examinaim sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj,'' as sjr,to_char(a.checktime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                        "a.receiver jyr,a.checkoperator shr,'' as jzch,a.notes zdjg,d.sampledescribe bbmc,decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                        "'' as dyjb,'' as bz, '' as hzbh,'' as sbm, a.patientname brxm from l_patientinfo a,l_sampletype d where a.sampletype = d.sampletype(+) and a.resultstatus in (4, 5, 6) and '2' = '2' and a.doctadviseno like '%{0}%'";

            reportDetailMX = "Select b.Chinesename As mc,a.Unit As dw,case when  a.refhi is null then a.reflo   else (a.reflo ||'~'|| a.refhi) end As ckjg, Decode(Substr(a.Resultflag, 1, 1),'A','','B','↑','C','↓','D','阴性','E','阳性'," +
                        " Substr(a.Resultflag, 1, 1)) As ts,a.Testresult As jg,b.Printord As PRINTORDER From l_Testresult a, l_Testdescribe b Where a.Testid = b.Testid And (a.Testresult is not null ) " +
                         " And a.Sampleno = '{0}' Order By b.Printord";

            remaindBeds = "select b.ksmc,b.ksdm,  (b.zcw - a.sycw)  zycw, b.zcw, a.sycw from " +
     " (select count(c.brch) as zcw,g.ksmc,g.ksdm from zy_cwsz c, gy_ksdm g where c.ksdm=g.ksdm and c.jcpb=0 " +
      " group by g.ksdm,g.ksmc) b, (select count(c.brch) as sycw,g.ksdm from zy_cwsz c, gy_ksdm g where c.ksdm=g.ksdm and c.jcpb=0 and zyh is " +
             " null  group by g.ksdm,g.ksmc) a where a.ksdm = b.ksdm";

            medicineByPydm = "select decode(a.type,1,'西药',2,'中药',3,'草药'),a.ypmc,a.ypdw,a.ypgg,c.cdmc ,b.lsjg, " +
                 " rownum d from yk_typk a, yk_ypcd b, yk_cddz c where a.ypxh = b.ypxh and a.ykzf = 0 and b.ypcd = c.ypcd and ( a.ypmc " +
             " like'%{0}%' or a.pydm like  '%{0}%' ) and rownum <=10";
            medicinePage = "select yplx,ypmc,ypdw,ypgg,cdmc,lsjg,d from "
                         + "(select decode(a.type,1,'西药',2,'中药',3,'草药') yplx,a.ypmc,a.ypdw, a.ypgg, c.cdmc, b.lsjg, rownum d from "
                         + " yk_typk a, yk_ypcd b, yk_cddz c where a.ypxh = b.ypxh and a.ykzf = 0 and b.ypcd = c.ypcd and rownum <={0})"
                         + " nr where nr.d >={1}";

            chargeByPydm = "select b.sfmc,a.fymc,a.fydw,a.fydj from gy_ylsf a, gy_sfxm b where a.fygb = b.sfxm " +
                 " and a.zfpb =0 and ( a.fymc like'%{0}%' or a.pydm like  '%{0}%' ) and rownum <=10";

            chargePage = "select sfmc,fymc,fydw,fydj,d from (select b.sfmc,a.fymc,a.fydw,a.fydj,rownum d "
                         + "from gy_ylsf a, gy_sfxm b where a.fygb = b.sfxm  and a.zfpb = 0 and rownum <= {0}) nr where nr.d >= {1}";

            accountListMzByPage = "select jkje, rq, zy   from (select (jfje - dfje) jkje, rq, zy, rownum rn from " +
                 " (select * from ms_szmx order by rq desc) " +
                 " where brid = '{0}' and rownum <= {1}  ) where rn >= {2}";
            accountListMz = "select (jfje - dfje), rq, zy from ms_szmx where brid = '{0}' order by rq desc";

            accountListZyByPage = "";
            accountListZy = "";

            mzbrReportJC = "select a.checkid as bgdh, a.checkpos || '-' || a.dep as sjmd, to_char(a.checkdate, 'yyyy-mm-dd hh24:mi:ss') as cjsj," +
                           "a.doctor as sjr, to_char(a.reportdate, 'yyyy-mm-dd hh24:mi:ss') as sjsj," +
                           "a.reportdoc as jyr, '' as shr, '' as jzch, a.reportend as zdjg, '' as bbmc, a.pat_type as mzbz, '' as dyjb, '' as bz, '' as hzbh," +
                           "'' as sbm, b.name as brxm from patexam a, patregister b where a.patid = b.clinicno and a.check_status != '已删除' and a.patient_id = '{0}'";
            zybrReportJC = "select a.checkid as bgdh, a.checkpos || '-' || a.dep as sjmd, to_char(a.checkdate, 'yyyy-mm-dd hh24:mi:ss') as cjsj," +
                           "a.doctor as sjr, to_char(a.reportdate, 'yyyy-mm-dd hh24:mi:ss') as sjsj," +
                           "a.reportdoc as jyr, '' as shr, '' as jzch, a.reportend as zdjg, '' as bbmc, a.pat_type as mzbz, '' as dyjb, '' as bz, '' as hzbh," +
                           "'' as sbm, b.name as brxm, a.wardno || '-' || a.bedno as jzch from patexam a, patregister b where a.patid = b.clinicno and a.check_status != '已删除' and a.patient_id = '{0}'"; ;
            reportJCDetailByBgdh = "select a.checkid as bgdh, a.checkpos || '-' || a.dep as sjmd, to_char(a.checkdate, 'yyyy-mm-dd hh24:mi:ss') as cjsj," +
                                  "a.doctor as sjr, to_char(a.reportdate, 'yyyy-mm-dd hh24:mi:ss') as sjsj," +
                                  "a.reportdoc as jyr, '' as shr, '' as jzch, a.reportend as zdjg, '' as bbmc, a.pat_type as mzbz, '' as dyjb, '' as bz, a.patid as hzbh," +
                                  "'' as sbm, b.name as brxm from patexam a, patregister b where a.patid = b.clinicno and a.check_status != '已删除' and a.checkid = '{0}'";
            reportJCDetailBySbm = "select a.checkid as bgdh, a.checkpos || '-' || a.dep as sjmd, to_char(a.checkdate, 'yyyy-mm-dd hh24:mi:ss') as cjsj," +
                                  "a.doctor as sjr, to_char(a.reportdate, 'yyyy-mm-dd hh24:mi:ss') as sjsj," +
                                  "a.reportdoc as jyr, '' as shr, '' as jzch, a.reportend as zdjg, '' as bbmc, a.pat_type as mzbz, '' as dyjb, '' as bz, a.patid as hzbh," +
                                  "'' as sbm, b.name as brxm, a.wardno || '-' || a.bedno as jzch from patexam a, patregister b where a.patid = b.clinicno and a.check_status != '已删除' and a.xno = '{0}'";
            reportJCDetailMX = "select '' as mc, '' as dw, '' as ckjg ,reportinfo ts, diseasetype jg from patexam where patid = '{0}'";
        }
    }
}