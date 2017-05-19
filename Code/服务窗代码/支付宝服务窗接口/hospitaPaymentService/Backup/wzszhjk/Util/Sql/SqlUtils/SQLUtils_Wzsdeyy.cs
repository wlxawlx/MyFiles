using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    public class SQLUtilsWZSDEYY : SQLUtils
    {
        public SQLUtilsWZSDEYY()
        {
            mzbrQueryStr = "select ms.brid brid, gy.ckhm bkhm, (case when length(gy.ckhm) = 41 then " +
             " 'smk' when length(gy.ckhm) = 28 then'ybk' else 'byk' end) bklx, ms.brxm brxm, NLS_UPPER(ms.sfzh) sfzh, ms.jtdh lxdh, " +
           " ms.hkdz jtdz from gy_ckda gy, ms_brda ms where ms.brid = gy.id and  gy.zfpb = 0 and ms.brxm = '{0}' and NLS_UPPER(ms.sfzh) = '{1}'";
            zybrQueryStr = "select zy.zyh brid,  gy.ckhm bkhm, " +
          "  (case when length(gy.ckhm) = 41 then 'smk' when length(gy.ckhm) = 28 then 'ybk' else  'byk' end) bklx, zy.brxm brxm," +
          " NLS_UPPER(zy.sfzh) sfzh, zy.lxdh lxdh, zy.hkdz jtdz,zy.ryrq ryrq, ks.ksmc szbq, zy.brch szcwh, zy.zyhm as zyh " +
          " from zy_brry zy, gy_ksdm ks , gy_ckda gy where zy.brbq = ks.ksdm and zy.brxm = '{0}' and NLS_UPPER(zy.sfzh) = '{1}' " +
          " and gy.ckhm = zy.knxx and gy.zfpb = 0 ";

            mzbrBindCardstr = "select  ms.jtdh lxdh from gy_ckda gy,ms_brda ms where ms.brid = gy.id and " +
                           " gy.zfpb = 0 and ms.brid = '{0}' and NLS_UPPER(ms.sfzh) = '{1}' and ms.brxm = '{2}' ";
            zybrBindCardstr = "select zy.lxdh lxdh from zy_brry zy, gy_ckda gy  where zy.knxx = gy.ckhm " +
               " and gy.zfpb = 0 and zy.zyh = '{0}' " +
               " and NLS_UPPER(zy.sfzh) = '{1}' and zy.brxm = '{2}' ";

            mzbrValidBkhm = "select ms.brid brid from gy_ckda gy,ms_brda ms where  ms.brid = gy.id  and " +
               " gy.ckhm = '{0}' and ms.brid = '{1}'";
            zybrValidBkhm = "select zy.zyh brid " +
                 " from zy_brry zy, gy_ckda gy  where gy.ckhm = zy.knxx and gy.ckhm = '{0}' and zy.zyh = '{1}'";

            mzbrForCreateOrder = "select  ms.brxm brxm, NLS_UPPER(ms.sfzh) sfzh, ms.jtdh lxdh from ms_brda ms where ms.brid = '{0}'";
            zybrForCreateOrder = "select zy.brxm, NLS_UPPER(zy.sfzh), zy.lxdh from zy_brry zy where zy.zyh = '{0}'";

            mzbrQueryBalance = "select zhye from gy_ckda gy where gy.id  = '{0}' and gy.zfpd=0";
            zybrQueryBalance = "select zhye from gy_ckda gy where gy.id  = '{0}' and gy.zfpd=0";
            //zybrQueryBalance = "select gy.zhye from gy_ckda gy , ms_brda ms,  zy_brry zy where " +
            //   " zy.zyh = '{0}'and gy.id = ms.brid and zy.mzhm = ms.mzhm and zy.brxm = ms.brxm ";

            mzbrValidId = "select count(ms.brid)  from ms_brda ms where ms.brid = '{0}'";
            zybrValidId = "select count(zy.zyh)  from zy_brry zy where zy.zyh = '{0}'";


            mzbrReportList = "select distinct t.sampleno bgdh," +
                    "t.examinaim sjmd," +
                    "to_char(t.requesttime, 'yyyy-mm-dd hh24:mi:ss')  cjsj," +
                    "'' sjr," +
                    "to_char(t.requesttime, 'yyyy-mm-dd hh24:mi:ss')  jysj," +
                   " '' jyr," +
                   " '' shr," +
                   " '' jzch," +
                   " '' zdjg," +
                   " t.specimen_type bbmc," +
                   " '' mzbz," +
                   " '' as dyjb," +
                   " '' as bz," +
                   " '' as hzbh," +
                   " '' as sbm," +
                   " '' as brxm," +
                   "  t.requesttime   " +
      " from zh_inspection_info t " +
    " where patientid = '{0}' " +
    " and rownum in (select min(rownum) from zh_inspection_info where t.sampleno = sampleno )" +
    " order by t.requesttime desc ";
            zybrReportList = "select distinct t.sampleno bgdh," +
                     "t.examinaim sjmd," +
                     "to_char(t.requesttime, 'yyyy-mm-dd hh24:mi:ss')  cjsj," +
                     "'' sjr," +
                     "to_char(t.requesttime, 'yyyy-mm-dd hh24:mi:ss')  jysj," +
                    " '' jyr," +
                    " '' shr," +
                    " '' jzch," +
                    " '' zdjg," +
                    " t.specimen_type bbmc," +
                    " '' mzbz," +
                    " '' as dyjb," +
                    " '' as bz," +
                    " '' as hzbh," +
                    " '' as jgmc," +
                    " '' as sbm," +
                    " '' as brxm," +
                    "  t.requesttime   " +
       " from zh_inspection_info t " +
     " where patientid = '{0}' " +
     " and rownum in (select min(rownum) from zh_inspection_info where t.sampleno = sampleno )" +
     " order by t.requesttime desc  ";

            reportDetailByBgdh = "select t.sampleno bgdh," +
                    "t.examinaim sjmd," +
                    "to_char(t.requesttime, 'yyyy-mm-dd hh24:mi:ss')  cjsj," +
                    "'' sjr," +
                    "to_char(t.requesttime, 'yyyy-mm-dd hh24:mi:ss')  jysj," +
                   " '' jyr," +
                   " '' shr," +
                   " '' jzch," +
                   " '' zdjg," +
                   " t.specimen_type bbmc," +
                   " '' mzbz," +
                   " '' as dyjb," +
                   " '' as bz," +
                   " '' as hzbh," +
                   " '' as sbm," +
                   " '' as brxm," +
                   "  t.requesttime   " +
      " from zh_inspection_info t " +
    " where t.sampleno = '{0}' " +
       " and rownum in (select min(rownum) from zh_inspection_info where t.sampleno = sampleno )";
            reportDetailBySbm = "select l.sampleno bgdh," +
                    "l.examinaim sjmd," +
                    "to_char(l.requesttime, 'yyyy-mm-dd hh24:mi:ss')  cjsj," +
                    "'' sjr," +
                    "to_char(l.requesttime, 'yyyy-mm-dd hh24:mi:ss')  jysj," +
                   " '' jyr," +
                   " '' shr," +
                   " '' jzch," +
                   " '' zdjg," +
                   " '' bbmc," +
                   " '' mzbz," +
                   " '' as dyjb," +
                   " '' as bz," +
                   " '' as hzbh," +
                   " '' as sbm," +
                   " '' as brxm," +
                   "  l.requesttime   " +
      " from l_patientinfo l where substr(l.doctadviseno,3,11)='{0}' and l.requesttime>add_months(sysdate,-3) ";

            remaindBeds = "select b.ksmc,b.ksdm,  (b.zcw - a.sycw)  zycw, b.zcw, a.sycw from " +
   " (select sum(case when zyh>0 then 1 else 0 end ) as zcw,g.ksmc,g.ksdm from zy_cwsz c, gy_ksdm g where c.ksdm=g.ksdm and c.jcpb=0 " +
    " group by g.ksdm,g.ksmc, g.plsx) b, (select count(c.brch) as sycw,g.ksdm from zy_cwsz c, gy_ksdm g where c.ksdm=g.ksdm and c.jcpb=0  " +
           "  group by g.ksdm,g.ksmc, g.plsx) a where a.ksdm = b.ksdm order by b.plsx";

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

            reportDetailMX = "select item_name mc,  '' as dw, danger_toplimit || '~' || danger_lowerlimit as ckjg, " +
                      "'' as ts, item_value  as jg from zh_inspection_info where sampleno = '{0}'";

//            "select jkje, rq, zy   from (select (jfje - dfje) jkje, rq, zy, rownum rn from " +
//                " (select * from ms_szmx order by rq desc) " +
//                " where brid = '{0}' and rownum <= {1}  ) where rn >= {2}";
            accountListMzByPage = "select jkje, rq, zy   from (select (jfje - dfje) jkje, rq, zy, rownum rn from  " +
                 " (select * from ms_szmx where brid = '{0}' order by rq desc) " +
                " where rownum <= {1}) where rn >= {2}";
            accountListMz = "select (jfje - dfje), rq, zy from ms_szmx where brid = '{0}' order by rq desc";

            accountListZyByPage = " select jkje, rq, zy  from (select (a.jfje - a.dfje) jkje, a.rq, a.zy,rownum rn from " +
                                 " ( select * from  ms_szmx mx, zy_brry zy, ms_brda ms, gy_ckda gy " +
                 " where mx.brid = ms.brid  and zy.zyh = '{0}' and gy.zfpb = 0 and zy.mzhm = ms.mzhm and " +
                 " zy.brxm = ms.brxm  and gy.ckhm = zy.knxx  ) a" +
                 " where rownum <= {1} ) where rn >= {2} ";
            accountListZy = " select (mx.jfje - mx.dfje) jkje, mx.rq, mx.zy,rownum rn " +
                                 " from ms_szmx mx, zy_brry zy, ms_brda ms, gy_ckda gy " +
                 " where mx.brid = ms.brid  and zy.zyh = '{0}' and gy.zfpb = 0 and zy.mzhm = ms.mzhm and " +
                 " zy.brxm = ms.brxm  and gy.ckhm = zy.knxx order by rq desc";

            mzbrReportJC = "select  vp.yxhao bgdh, vp.modality sjmd, " +
                " to_char(vp.transcribe_dttm, 'yyyy-mm-dd hh24:mi:ss')  cjsj,  '' sjr, to_char(vp.transcribe_dttm, 'yyyy-mm-dd hh24:mi:ss')  jysj, '' jyr, '' shr, '' jzch, vp.report_text zdjg," +
                   " vp.exam_item  bbmc, '' mzbz, '' as dyjb, '' as bz, '' as hzbh,  '' as sbm,vp.name as brxm," +
                   " vp.transcribe_dttm from v_pacsreport vp,ms_brda ms where vp.patient_id =  ms.mzhm and  ms.brid = '{0}'  " +
                   "and rownum in (select min(rownum) from v_pacsreport where yxhao = vp.yxhao )" +
                   " order by  vp.transcribe_dttm  desc ";
            zybrReportJC = "select  vp.yxhao bgdh,  vp.modality  sjmd, " +
               " to_char(vp.transcribe_dttm, 'yyyy-mm-dd hh24:mi:ss')  cjsj,  '' sjr, to_char(vp.transcribe_dttm, 'yyyy-mm-dd hh24:mi:ss')  jysj, '' jyr, '' shr, '' jzch, vp.report_text zdjg," +
                  " vp.exam_item bbmc, '' mzbz, '' as dyjb, '' as bz, '' as hzbh, '' as sbm,vp.name as brxm," +
                  " vp.transcribe_dttm from v_pacsreport vp,ms_brda ms where vp.patient_id =  ms.mzhm and  ms.brid = '{0}'  " +
                  "and rownum in (select min(rownum) from v_pacsreport where yxhao = vp.yxhao )" +
                  " order by  vp.transcribe_dttm  desc ";
            reportJCDetailByBgdh = "select  vp.yxhao bgdh, vp.modality  sjmd, " +
               " to_char(vp.transcribe_dttm, 'yyyy-mm-dd hh24:mi:ss')  cjsj,  '' sjr, to_char(vp.transcribe_dttm, 'yyyy-mm-dd hh24:mi:ss')  jysj, '' jyr, '' shr, '' jzch, vp.report_text zdjg," +
                  " vp.exam_item  bbmc, '' mzbz, '' as dyjb, '' as bz, '' as hzbh,  '' as sbm,vp.name as brxm," +
                  " vp.transcribe_dttm from v_pacsreport vp where vp.yxhao = '{0}'  " +
                  "and rownum in (select min(rownum) from v_pacsreport where yxhao = vp.yxhao )" +
                  " order by  vp.transcribe_dttm  desc ";
            reportJCDetailBySbm = "";
            reportJCDetailMX = "select vp.exam_item mc,  '' as dw, '' ckjg, " +
               " vp.conclusion as ts, vp.report_text as jg from  v_pacsreport vp where vp.yxhao = '{0}'";
        }
    }
}