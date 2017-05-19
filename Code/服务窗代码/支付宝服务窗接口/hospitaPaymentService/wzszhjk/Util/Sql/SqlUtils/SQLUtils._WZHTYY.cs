using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    //温州海坦医院
    public class SQLUtilsWZHTYY : SQLUtils
    {
        public SQLUtilsWZHTYY()
        {
            mzbrQueryStr = "select ms.brid brid, gy.ckxx bkhm, (case when length(gy.ckxx)=41 then 'smk' when length(gy.ckxx)=28 then 'ybk'" +
                     " else 'byk' end ) bklx, ms.brxm brxm, NLS_UPPER(ms.sfzh) sfzh,  ms.jtdh lxdh, ms.hkdz jtdz" +
                     " from gy_ckda gy,ms_brda ms where ms.brid = gy.brid and ms.brxm = '{0}' and ms.sfzh = '{1}' and gy.zfpb = 0";
            zybrQueryStr = "select zy.zyh brid, zy.knxx bkhm, (case when length(zy.knxx)=41 then 'smk' when length(zy.knxx)=28 then 'ybk'" +
                         " else 'byk' end ) bklx, zy.brxm brxm, NLS_UPPER(zy.sfzh) sfzh,  zy.jtdh lxdh, zy.hkdz jtdz, zy.ryrq ryrq, ks.ksmc szbq, " +
                         " zy.brch szcwh , '' as zyh " +
                         " from zy_brry zy, gy_ksdm ks " +
                         " where zy.brbq=ks.ksdm and " + " zy.brxm = '{0}' and zy.cypb = 0 and NLS_UPPER(zy.sfzh) = '{1}'";


            mzbrBindCardstr = "select  ms.jtdh lxdh from gy_ckda gy,ms_brda ms where ms.brid = gy.brid and " +
                             " ms.brid = '{0}' and NLS_UPPER(ms.sfzh) = '{1}' and ms.brxm = '{2}'  and gy.zfpb = 0";
            zybrBindCardstr = "select zy.jtdh lxdh from zy_brry zy, gy_ksdm ks where zy.brbq = ks.ksdm and zy.zyh = '{0}'" +
                 " and NLS_UPPER(zy.sfzh) = '{1}' and zy.brxm = '{2}' and zy.cypb = 0 ";

            mzbrValidBkhm = "select ms.brid brid from gy_ckda gy,ms_brda ms where  ms.brid = gy.brid  and " +
                 " gy.ckhm = '{0}' and ms.brid = '{1}' and  gy.zfpb = 0";
            zybrValidBkhm = "select zy.zyh brid, zy.knxx bkhm from zy_brry zy where  zy.knxx = '{0}'" +
                  " and zy.zyh = '{1}' and zy.cypb = 0 ";

            mzbrForCreateOrder = "";
            zybrForCreateOrder = "";

            mzbrQueryBalance = "";
            zybrQueryBalance = "";

            mzbrValidId = "select count(ms.brid)  from ms_brda ms where ms.brid = '{0}'";
            zybrValidId = "select count(zy.zyh)  from zy_brry zy where zy.zyh = '{0}'";


            mzbrReportList = "select a.SAMPLENO bgdh,to_nchar(a.examinaim) sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj,to_nchar(b.xm) sjr,to_char(a.receivetime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                              "a.receiver jyr,a.checkoperator shr,a.depart_bed jzch,to_nchar(a.diagnostic) zdjg, to_nchar(a.examinaim) bbmc," +
                              "to_nchar(decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核')) mzbz," +
                              "'' dyjb,'' bz,a.patientid hzbh,a.doctadviseno sbm,'' brxm,a.executetime executetime from l_patientinfo a,gy_zgxx b, gy_ksdm  c," +
                               "l_sampletype d where a.requester = b.zgid(+) and a.section = c.ksdm(+) and a.sampletype = d.sampletype(+) and " +
                               "a.patientid = '{0}' order by a.executetime";
            zybrReportList = "select a.SAMPLENO bgdh,to_nchar(a.examinaim) sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj,to_nchar(b.xm) sjr,to_char(a.receivetime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                              "a.receiver jyr,a.checkoperator shr,a.depart_bed jzch,to_nchar(a.diagnostic) zdjg, to_nchar(a.examinaim) bbmc," +
                              "to_nchar(decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核')) mzbz," +
                              "'' dyjb,'' bz,a.patientid hzbh,a.doctadviseno sbm,'' brxm,a.executetime executetime from l_patientinfo a, gy_zgxx b, gy_ksdm  c," +
                               "l_sampletype d where a.requester = b.zgid(+) and a.section = c.ksdm(+) and a.sampletype = d.sampletype(+) and " +
                               "a.patientid = '{0}' order by a.executetime";

            reportDetailByBgdh = "select a.SAMPLENO bgdh,to_nchar(a.examinaim) sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj,to_nchar(b.xm) sjr,to_char(a.receivetime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                                  "a.receiver jyr,a.checkoperator shr,a.depart_bed jzch,to_nchar(a.diagnostic) zdjg, to_nchar(a.examinaim) bbmc," +
                                  "to_nchar(decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核')) mzbz," +
                                  "'' dyjb,'' bz,a.patientid hzbh,a.doctadviseno sbm,'' brxm,a.executetime executetime from l_patientinfo a, gy_zgxx b, gy_ksdm  c," +
                                   "l_sampletype d where a.requester = b.zgid(+) and a.section = c.ksdm(+) and a.sampletype = d.sampletype(+) and " +
                                   "a.SAMPLENO = '{0}' order by a.executetime";
            reportDetailBySbm = "select a.SAMPLENO bgdh,to_nchar(a.examinaim) sjmd,to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj,to_nchar(b.xm) sjr,to_char(a.receivetime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                                  "a.receiver jyr,a.checkoperator shr,a.depart_bed jzch,to_nchar(a.diagnostic) zdjg, to_nchar(a.examinaim) bbmc," +
                                  "to_nchar(decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核')) mzbz," +
                                  "'' dyjb,'' bz,a.patientid hzbh,a.doctadviseno sbm,'' brxm,a.executetime executetime from l_patientinfo a, gy_zgxx b, gy_ksdm  c," +
                                   "l_sampletype d where a.requester = b.zgid(+) and a.section = c.ksdm(+) and a.sampletype = d.sampletype(+) and " +
                                   "a.doctadviseno = '{0}' order by a.executetime";

            remaindBeds = "select b.ksmc,b.ksdm,  (b.zcw - a.sycw)  zycw, b.zcw, a.sycw from " +
                         " (select count(c.brch) as zcw,g.ksmc,g.ksdm from zy_cwsz c, gy_ksdm g where c.ksdm=g.ksdm and c.jcpb=0 " +
                          " group by g.ksdm,g.ksmc) b, (select count(c.brch) as sycw,g.ksdm from zy_cwsz c, gy_ksdm g where c.ksdm=g.ksdm and c.jcpb=0  " +
                           " and zyh is null  group by g.ksdm,g.ksmc) a where a.ksdm = b.ksdm";

            medicineByPydm = "select decode(a.type,1,'西药',2,'中药',3,'草药'),a.ypmc,a.ypdw,a.ypgg,c.cdmc ,b.lsjg, " +
                            " rownum d from yk_typk a,yk_ypcd b,yk_cddz c where a.ypxh = b.ypxh and a.ykzf = 0 and b.ypcd = c.ypcd and ( a.ypmc " +
                            " like'%{0}%' or a.pydm like  '%{0}%' ) and rownum <=10";
            medicinePage = "select yplx,ypmc,ypdw,ypgg,cdmc,lsjg,d from "
                         + "(select decode(a.type,1,'西药',2,'中药',3,'草药') yplx,a.ypmc,a.ypdw, a.ypgg, c.cdmc, b.lsjg, rownum d from "
                         + "yk_typk a,yk_ypcd b,yk_cddz c where a.ypxh = b.ypxh and a.ykzf = 0 and b.ypcd = c.ypcd and rownum <={0})"
                         + " nr where nr.d >={1}";

            chargeByPydm = "select b.sfmc,a.fymc,a.fydw,a.fydj from gy_ylsf a,gy_sfxm b where a.fygb = b.sfxm " +
                           " and a.zfpb =0 and ( a.fymc like'%{0}%' or a.pydm like  '%{0}%' ) and rownum <=10";

            chargePage = "select sfmc,fymc,fydw,fydj,d from (select b.sfmc,a.fymc,a.fydw,a.fydj,rownum d "
                         + "from gy_ylsf a,gy_sfxm b where a.fygb = b.sfxm  and a.zfpb = 0 and rownum <= {0}) nr where nr.d >= {1}";

            reportDetailMX = "Select to_nchar(b.Chinesename) As mc, to_nchar(a.Unit) As dw,to_nchar(a.Hint) As ckjg," +
                             "to_nchar(Decode(Substr(a.Resultflag, 1, 1),'A','','B', '↑', 'C','↓','D','阴性','E','阳性',Substr(a.Resultflag, 1, 1))) As ts, to_nchar(Nvl(a.Hint, a.Testresult)) As jg " +
                             "From l_Testresult a, l_Testdescribe b, l_patientinfo c Where a.Testid = b.Testid And (a.Testresult is not null) And a.Sampleno = '{0}' " +
                             "And a.Sampleno = c.Sampleno Order By b.Printord";

            accountListMzByPage = "";
            accountListMz = "";

            accountListZyByPage = "";
            accountListZy = "";

            mzbrReportJC = "";
            zybrReportJC = "";
            reportJCDetailByBgdh = "";
            reportJCDetailBySbm = "";
            reportJCDetailMX = "";
        }
    }
}