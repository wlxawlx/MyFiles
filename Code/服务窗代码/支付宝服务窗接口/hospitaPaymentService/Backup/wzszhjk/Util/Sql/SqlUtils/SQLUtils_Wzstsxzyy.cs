using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    public class SQLUtilsWZSTSXZYY : SQLUtils
    {
        public SQLUtilsWZSTSXZYY()
        {
            mzbrQueryStr = "select distinct mz.jzkh brid, (case when mz.ybkh is null then mz.jzkh else gy.ybkh end) bkhm ," +
                           "(case when length(mz.ybkh)=41 then 'smk' when length(mz.ybkh)=20 then 'ybk' else 'byk' end) bklx," +
                           "mz.brxm brxm, NLS_UPPER(gy.sfzh) sfzh, gy.lxdh lxdh , gy.jtzz jtdz from gy_brjbxxk gy, mz_mzsfk1 mz " +
                           "where mz.jzkh = gy.jzkh and mz.brxm = '{0}' and NLS_UPPER(gy.sfzh) = '{1}'";
            zybrQueryStr = "select distinct zy.bingrenbh brid, (case when gy.ybkh is null then gy.jzkh else gy.ybkh end) bkhm," +
                           "(case when length(zy.jiuzhenkh)=41 then 'smk' when length(zy.jiuzhenkh)=20 then 'ybk' else 'byk' end) bklx," +
                           "zy.xingming brxm, NLS_UPPER(zy.shenfenzh) sfzh,  zy.lianxidh lxdh, zy.jiatingdz jtdz, zy.ruyuanrq ryrq," +
                           "zy.dangqianbq szbq, zy.dangqiancw szcw , zy.bingrenbh as zyh  from  gy_brjbxxk gy, zy_bingrenxx zy where " +
                           "zy.shenfenzh = gy.sfzh and zy.xingming = '{0}' and NLS_UPPER(zy.shenfenzh) = '{1}'";
            mzbrBindCardstr = "select gy.lxdh lxdh from gy_brjbxxk gy, mz_mzsfk1 mz where gy.jzkh = mz.jzkh and " +
                              "mz.jzkh = '{0}' and NLS_UPPER(gy.sfzh) = '{1}' and mz.brxm = '{2}'";
            zybrBindCardstr = "select zy.lianxidh lxdh from gy_brjbxxk gy, zy_bingrenxx zy where zy.shenfenzh = gy.sfzh and zy.bingrenbh = '{0}'" +
                               " and NLS_UPPER(zy.shenfenzh) = '{1}' and zy.xingming = '{2}'";

            mzbrValidBkhm = "select distinct mz.jzkh brid from gy_brjbxxk gy, mz_mzsfk1 mz where  mz.jzkh = gy.jzkh  and " +
                            " gy.jzkh = '{0}' and mz.jzkh = '{1}' ";
            zybrValidBkhm = "select distinct zy.bingrenbh brid, (case when gy.ybkh is null then gy.jzkh else gy.ybkh end) bkhm from gy_brjbxxk gy," +
                            " zy_bingrenxx zy where  zy.shenfenzh = gy.sfzh and gy.ybkh = '{0}' and gy.jzkh = '{1}' ";
            mzbrForCreateOrder = "";
            zybrForCreateOrder = "";

            mzbrQueryBalance = "";
            zybrQueryBalance = "";

            mzbrValidId = "select count(mz.jzkh)  from mz_mzsfk1 mz where mz.jzkh = '{0}'";
            zybrValidId = "select count(zy.bingrenbh)  from gy_brjbxxk gy, zy_bingrenxx zy where zy.shenfenzh = gy.sfzh and zy.bingrenbh = '{0}'";


            mzbrReportList = "select a.SAMPLENO bgdh,a.examinaim sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj,b.xm sjr,to_char(a.receivetime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                              "a.receiver jyr,a.checkoperator shr,a.depart_bed jzch,a.diagnostic zdjg, a.examinaim bbmc," +
                              "decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                              "'' dyjb,'' bz,a.patientid hzbh,a.doctadviseno sbm,'' brxm,a.executetime executetime from l_patientinfo a,zjhis.gy_zgxx b, zjhis.gy_ksdm  c," +
                               "l_sampletype d where a.requester = b.zgid(+) and a.section = c.ksdm(+) and a.sampletype = d.sampletype(+) and " +
                               "a.patientid = '{0}' order by a.executetime";
            zybrReportList = "select a.SAMPLENO bgdh,a.examinaim sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj,b.xm sjr,to_char(a.receivetime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                              "a.receiver jyr,a.checkoperator shr,a.depart_bed jzch,a.diagnostic zdjg, a.examinaim bbmc," +
                              "decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                              "'' dyjb,'' bz,a.patientid hzbh,a.doctadviseno sbm,'' brxm,a.executetime executetime from l_patientinfo a,zjhis.gy_zgxx b, zjhis.gy_ksdm  c," +
                               "l_sampletype d where a.requester = b.zgid(+) and a.section = c.ksdm(+) and a.sampletype = d.sampletype(+) and " +
                               "a.patientid = '{0}' order by a.executetime";
            reportDetailByBgdh = "select a.SAMPLENO bgdh,a.examinaim sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj,b.xm sjr,to_char(a.receivetime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                                  "a.receiver jyr,a.checkoperator shr,a.depart_bed jzch,a.diagnostic zdjg, a.examinaim bbmc," +
                                  "decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                                  "'' dyjb,'' bz,a.patientid hzbh,a.doctadviseno sbm,'' brxm,a.executetime executetime from l_patientinfo a,zjhis.gy_zgxx b, zjhis.gy_ksdm  c," +
                                   "l_sampletype d where a.requester = b.zgid(+) and a.section = c.ksdm(+) and a.sampletype = d.sampletype(+) and " +
                                   "a.SAMPLENO = '{0}' order by a.executetime";
            reportDetailBySbm = "select a.SAMPLENO bgdh,a.examinaim sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj,b.xm sjr,to_char(a.receivetime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                                  "a.receiver jyr,a.checkoperator shr,a.depart_bed jzch,a.diagnostic zdjg, a.examinaim bbmc," +
                                  "decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                                  "'' dyjb,'' bz,a.patientid hzbh,a.doctadviseno sbm,'' brxm,a.executetime executetime from l_patientinfo a,zjhis.gy_zgxx b, zjhis.gy_ksdm  c," +
                                   "l_sampletype d where a.requester = b.zgid(+) and a.section = c.ksdm(+) and a.sampletype = d.sampletype(+) and " +
                                   "a.doctadviseno = '{0}' order by a.executetime";
            remaindBeds = "select b.ksmc,b.ksdm,  (b.zcw - a.sycw)  zycw, b.zcw, a.sycw from " +
                           "(select count(c.bed_no) as zcw,g.ksmc,g.ksdm from zy_bed c, gy_ksdm g where c.dept_code =g.ksdm " +
                           "group by g.ksdm,g.ksmc) b, (select count(c.bed_status) as sycw,g.ksdm from zy_bed c, gy_ksdm g " +
                           "where c.dept_code=g.ksdm and c.bed_status=0 group by g.ksdm,g.ksmc) a where a.ksdm = b.ksdm";
            medicineByPydm = "Select decode(a.yplxpb,'1','西药','2','成药','3','中药') As yplx,a.ypmc As ypmc," +
                            " a.ypbzdw As jldw,a.ypgg As ypgg,a.cdmc As ypcd,a.lsj As ypjg,a.srm1 As pydm, rownum d  From gy_ypcdjg a " +
                            "Where ( a.ypmc like'%{0}%' or a.srm1 like  '%{0}%' ) and rownum <=10";
            medicinePage = "select yplx,ypmc,ypdw,ypgg,cdmc,lsjg,d from " +
                            "(select decode(a.yplxpb,'1','西药','2','成药','3','中药') yplx,a.ypmc,a.ypbzdw ypdw, a.ypgg, a.cdmc, a.lsj lsjg, rownum d " +
                            "from gy_ypcdjg a where rownum <={0}) nr where nr.d >={1}";

            chargeByPydm = "select b.ylmc,a.ylmc,a.dw,a.dj from gy_ylsf a,(select * from gy_ylsf a where a.fldm = 0) b where a.fldm = b.ylxh " +
                            "and ( a.ylmc like'%{0}%' or a.srm1 like  '%{0}%' ) and rownum <=10";
            chargePage = "select sfmc,fymc,fydw,dj,d from (select b.ylmc sfmc,a.ylmc fymc,a.dw fydw,a.dj,rownum d from  gy_ylsf a," +
                         "(select * from gy_ylsf a where a.fldm = 0) b where a.fldm = b.ylxh and rownum <= {0}) nr where nr.d >= {1}";

            reportDetailMX = "Select b.Chinesename As mc, a.Unit As dw,replace(Nvl(a.Hint,Nvl(a.beizhu,RefLo || Decode(Nvl(RefHi, ' '), ' ', ' ', '-' || RefHi))),'  ','#') As ckjg," +
                             "Decode(Substr(a.Resultflag, 1, 1),'A','','B', '↑', 'C','↓','D','阴性','E','阳性',Substr(a.Resultflag, 1, 1)) As ts, Nvl(a.Hint, a.Testresult) As jg " +
                             "From l_Testresult a, l_Testdescribe b, l_patientinfo c Where a.Testid = b.Testid And (a.Testresult is not null) And a.Sampleno = '{0}' " +
                             "And a.Sampleno = c.Sampleno Order By b.Printord";

            accountListMzByPage = "";
            accountListMz = "";

            accountListZyByPage = "";
            accountListZy = "";

            mzbrReportJC = "select ACCESSNO as bgdh, modality||'+'||checkpos as sjmd, to_char(REPORTDATE,  'yyyy-mm-dd hh24:mi:ss') as  cjsj," +
                  "DOCTOR as sjr ,to_char(ADVANCEDATE,  'yyyy-mm-dd hh24:mi:ss')  as jysj, REPORTDOC as jyr, ADVANCEDOC as shr,'' as jzch, REPORTEND as zdjg, " +
                  " '' as bbmc, SQDEP as mzbz, '' as dyjb, '' as bz, PATIENT_ID as hzbh,  ACCESSNO as sbm , NAME as brxm , ADVANCEDATE  from" +
                   " v_reportinfo  where PATIENT_ID = '{0}' order by ADVANCEDATE ";
            zybrReportJC = "select ACCESSNO as bgdh, modality||'+'||checkpos as sjmd, to_char(REPORTDATE,  'yyyy-mm-dd hh24:mi:ss') as  cjsj," +
                  "DOCTOR as sjr ,to_char(ADVANCEDATE,  'yyyy-mm-dd hh24:mi:ss')  as jysj, REPORTDOC as jyr, ADVANCEDOC as shr,'' as jzch, REPORTEND as zdjg, " +
                  "'' as bbmc, SQDEP as mzbz, '' as dyjb, '' as bz, PATIENT_ID as hzbh,  ACCESSNO as sbm , NAME as brxm , ADVANCEDATE  from" +
                   " v_reportinfo  where PATIENT_ID = '{0}' order by ADVANCEDATE ";
            reportJCDetailByBgdh = "select ACCESSNO as bgdh, modality||'+'||checkpos as sjmd, to_char(REPORTDATE,  'yyyy-mm-dd hh24:mi:ss') as  cjsj," +
                  "DOCTOR as sjr ,to_char(ADVANCEDATE,  'yyyy-mm-dd hh24:mi:ss')  as jysj, REPORTDOC as jyr, ADVANCEDOC as shr,'' as jzch, REPORTEND as zdjg, " +
                  "'' as bbmc, SQDEP as mzbz, '' as dyjb, '' as bz, PATIENT_ID as hzbh,  ACCESSNO as sbm , NAME as brxm , ADVANCEDATE  from" +
                   " v_reportinfo  where ACCESSNO = '{0}' ";
            reportJCDetailBySbm = "select ACCESSNO as bgdh, modality||'+'||checkpos as sjmd, to_char(REPORTDATE,  'yyyy-mm-dd hh24:mi:ss') as  cjsj," +
                  "DOCTOR as sjr ,to_char(ADVANCEDATE,  'yyyy-mm-dd hh24:mi:ss')  as jysj, REPORTDOC as jyr, ADVANCEDOC as shr,'' as jzch, REPORTEND as zdjg, " +
                  "'' as bbmc, SQDEP as mzbz, '' as dyjb, '' as bz, PATIENT_ID as hzbh,  ACCESSNO as sbm , NAME as brxm , ADVANCEDATE  from" +
                   " v_reportinfo  where ACCESSNO = '{0}' ";
            reportJCDetailMX = "select '' as mc, '' as dw, '' as ckjg,reportinfo as ts,REPORTEND as jg " +
                  "from v_reportinfo where ACCESSNO = '{0}' ";
        }
    }
}