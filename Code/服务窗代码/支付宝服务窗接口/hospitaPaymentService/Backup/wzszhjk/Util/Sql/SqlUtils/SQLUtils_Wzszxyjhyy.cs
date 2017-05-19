using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    //温州市中西医结合医院
    public class SQLUtilsWZSZXYJHYY : SQLUtils
    {
        public SQLUtilsWZSZXYJHYY()
        {
            mzbrQueryStr = "select ms.brid brid, gy.ckhm bkhm, (case when length(gy.ckhm)=41 then 'smk' when length(gy.ckhm)=28 then 'ybk'" +
                     " else 'byk' end ) bklx, ms.brxm brxm, NLS_UPPER(ms.sfzh) sfzh,  ms.jtdh lxdh, gy.hkdz jtdz" +
                     " from gy_ckda gy,ms_brda ms where ms.brid = gy.id and ms.brxm = '{0}' and ms.sfzh = '{1}' and gy.zfpb = 0";
            zybrQueryStr = "select zy.zyh brid, zy.knxx bkhm, (case when length(zy.knxx)=41 then 'smk' when length(zy.knxx)=28 then 'ybk'" +
                         " else 'byk' end ) bklx, zy.brxm brxm, NLS_UPPER(zy.sfzh) sfzh,  zy.lxdh lxdh, zy.hkdz jtdz, zy.ryrq ryrq, ks.ksmc szbq, " +
                         " zy.brch szcwh , '' as zyh " +
                         " from zy_brry zy, gy_ksdm ks " +
                         " where zy.brbq=ks.ksdm and " + " zy.brxm = '{0}' and zy.cypb = 0 and NLS_UPPER(zy.sfzh) = '{1}'";


            mzbrBindCardstr = "select  ms.jtdh lxdh from gy_ckda gy,ms_brda ms where ms.brid = gy.id and " +
                             " ms.brid = '{0}' and NLS_UPPER(ms.sfzh) = '{1}' and ms.brxm = '{2}'  and gy.zfpb = 0";
            zybrBindCardstr = "select zy.lxdh lxdh from zy_brry zy, gy_ksdm ks where zy.brbq = ks.ksdm and zy.zyh = '{0}'" +
                 " and NLS_UPPER(zy.sfzh) = '{1}' and zy.brxm = '{2}' and zy.cypb = 0 ";

            mzbrValidBkhm = "select ms.brid brid from gy_ckda gy,ms_brda ms where  ms.brid = gy.id  and " +
                 " gy.ckhm = '{0}' and ms.brid = '{1}' and  gy.zfpb = 0";
            zybrValidBkhm = "select zy.zyh brid, zy.knxx bkhm, from zy_brry zy, gy_ksdm where  zy.knxx = '{0}'" +
                  " and zy.zyh = '{1}' and zy.cypb = 0 ";

            mzbrForCreateOrder = "select  ms.brxm brxm, NLS_UPPER(ms.sfzh) sfzh, ms.jtdh lxdh from ms_brda ms where ms.brid = '{0}'";
            zybrForCreateOrder = "select zy.brxm, NLS_UPPER(zy.sfzh) sfzh, zy.lxdh from zy_brry zy where zy.zyh = '{0}'";

            mzbrQueryBalance = "select zhye from gy_ckda where id= '{0}' and zfpb=0";
            zybrQueryBalance = "";

            mzbrValidId = "select count(ms.brid)  from ms_brda ms where ms.brid = '{0}'";
            zybrValidId = "select count(zy.zyh)  from zy_brry zy where zy.zyh = '{0}'";


            mzbrReportList = "select sampleno as bgdh, zmc as sjmd, to_char(executetime,  'yyyy-mm-dd hh24:mi:ss') as  cjsj, " +
                 " '' as sjr ,  to_char(jysj,  'yyyy-mm-dd hh24:mi:ss')  as jysj, '' as jyr, '' as shr, '' as jzch, '' as zdjg, " +
                 " '' as bbmc, '' as mzbz, '' as dyjb, '' as  bz, '' as hzbh,  doctadviseno as sbm , '' as brxm , executetime  from  " +
                 " zhlis.vi_patient_dzbl vi  where vi.zyh = '{0}'  and ( " +
                 " (select count(*) from zhlis.vi_result_xj_dzbl xj where xj.sampleno = vi.sampleno) " +
                 " + (select count(*) from " +
                 " zhlis.vi_result_dzbl b where b.sampleno = vi.sampleno)  > 0) order by executetime ";
            zybrReportList = "select sampleno as bgdh, zmc as sjmd, to_char(executetime,  'yyyy-mm-dd hh24:mi:ss') as  cjsj, " +
                 " '' as sjr ,  to_char(jysj,  'yyyy-mm-dd hh24:mi:ss')  as jysj, '' as jyr, '' as shr, '' as jzch, '' as zdjg, " +
                 " '' as bbmc, '' as mzbz, '' as dyjb,  bz, '' as hzbh,  doctadviseno as sbm , '' as brxm , executetime  from  " +
                 " zhlis.vi_patient_dzbl vi  where vi.zyh = '{0}'   and ( " +
                 " (select count(*) from zhlis.vi_result_xj_dzbl xj where xj.sampleno = vi.sampleno) " +
                 " + (select count(*) from " +
                 " zhlis.vi_result_dzbl b where b.sampleno = vi.sampleno)  > 0) order by executetime ";

            reportDetailByBgdh = "select sampleno as bgdh, zmc as sjmd, to_char(executetime,  'yyyy-mm-dd hh24:mi:ss') as  cjsj, " +
                 " '' as sjr ,  to_char(jysj,  'yyyy-mm-dd hh24:mi:ss')  as jysj, '' as jyr, '' as shr, '' as jzch, '' as zdjg, " +
                 " '' as bbmc, '' as mzbz, '' as dyjb,  bz, '' as hzbh,  doctadviseno as sbm , '' as brxm , executetime  from  " +
                 " zhlis.vi_patient_dzbl vi  where vi.sampleno = '{0}' order by executetime ";
            reportDetailBySbm = "select sampleno as bgdh, zmc as sjmd, to_char(executetime,  'yyyy-mm-dd hh24:mi:ss') as  cjsj, " +
                 " '' as sjr ,  to_char(jysj,  'yyyy-mm-dd hh24:mi:ss')  as jysj, '' as jyr, '' as shr, '' as jzch, '' as zdjg, " +
                 " '' as bbmc, '' as mzbz, '' as dyjb,  bz, '' as hzbh,  doctadviseno as sbm , '' as brxm , executetime  from  " +
                 " zhlis.vi_patient_dzbl vi  where vi.doctadviseno = '{0}' and ( " +
                 " (select count(*) from zhlis.vi_result_xj_dzbl xj where xj.sampleno = vi.sampleno) " +
                 " + (select count(*) from " +
                 " zhlis.vi_result_dzbl b where b.sampleno = vi.sampleno)  > 0) order by executetime ";

            remaindBeds = "select b.ksmc,b.ksdm,  (b.zcw - a.sycw)  zycw, b.zcw, a.sycw from " +
     " (select count(c.brch) as zcw,g.ksmc,g.ksdm from zy_cwsz c, gy_ksdm g where c.ksdm=g.ksdm and c.jcpb=0 and g.ksdm not in(196,201,325,524)" +
      " group by g.ksdm,g.ksmc) b, (select count(c.brch) as sycw,g.ksdm from zy_cwsz c, gy_ksdm g where c.ksdm=g.ksdm and c.jcpb=0 and zyh is " +
             " null and g.ksdm not in(196,201,325,524) group by g.ksdm,g.ksmc) a where a.ksdm = b.ksdm";

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

            reportDetailMX = "select xj.xmmc as mc ,'' as dw, '' as ckjg, '' as ts, xj.xmjg as jg from " +
                 " zhlis.vi_result_xj_dzbl xj where xj.sampleno = '{0}' union " +
                 " select b.xmmc as mc , b.xmdw as dw, b.ckz as ckjg, b.gdbj as ts, b.xmjg as jg " +
                 " from zhlis.vi_result_dzbl b where b.sampleno = '{0}'";

            accountListMzByPage = "select jkje, rq, zy   from (select (jfje - dfje) jkje, rq, zy, rownum rn from " +
                 " (select * from ms_szmx order by rq desc) " +
                 " where brid = '{0}' and rownum <= {1}  ) where rn >= {2}";
            accountListMz = "select (jfje - dfje), rq, zy from ms_szmx where brid = '{0}' order by rq desc";

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