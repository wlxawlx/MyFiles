using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    public class SQLUtilsWZSDSRMYY : SQLUtils
    {
        public SQLUtilsWZSDSRMYY()
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


            mzbrReportList = "";
            zybrReportList = "";

            reportDetailByBgdh = "";
            reportDetailBySbm = "";

            remaindBeds = "select b.ksmc, b.ksdm,  (b.zcw - a.sycw)  zycw, b.zcw, a.sycw " +
                                               " from (select count(c.brch) as zcw,g.ksmc,g.ksdm,g.plsx from zy_cwsz c, gy_ksdm g " +
                                               " where c.ksdm=g.ksdm and c.jcpb=0 and g.edcw >0 group by g.ksdm,g.ksmc,g.plsx) b, (select count(c.brch) as sycw,g.ksdm,g.plsx " +
                                               " from zy_cwsz c, gy_ksdm g where c.ksdm=g.ksdm and c.jcpb=0 and zyh is null and g.edcw >0 group by g.ksdm,g.ksmc,g.plsx) a " +
                                               " where a.ksdm = b.ksdm order by a.plsx";

            medicineByPydm = "select decode(a.type,1,'西药',2,'中药',3,'草药'),a.ypmc,a.ypdw,a.ypgg,c.cdmc ,b.lsjg, " +
                 " rownum d from yk_typk a,yk_ypcd b,yk_cddz c where a.ypxh = b.ypxh and a.ykzf = 0 and b.ypcd = c.ypcd and ( a.ypmc " +
             " like'%{0}%' or a.pydm like  '%{0}%' ) and rownum <=10";
            medicinePage = "select yplx,ypmc,ypdw,ypgg,cdmc,lsjg,d from "
                         + "(select decode(a.type,1,'西药',2,'中药',3,'草药') yplx,a.ypmc,a.ypdw, a.ypgg, c.cdmc, b.lsjg, rownum d from "
                         + "yk_typk a,yk_ypcd b,yk_cddz c where a.ypxh = b.ypxh and a.ykzf = 0 and b.ypcd = c.ypcd and rownum <={0})"
                         + " nr where nr.d >={1}";

            chargeByPydm = "select b.sfmc,a.fymc,a.fydw,a.fydj from gy_ylsf a,gy_sfxm b where a.fygb = b.sfxm " +
                 " and a.zfpb =0 and ( a.fymc like '%{0}%' or a.pydm like  '%{0}%' ) and rownum <=10";

            chargePage = "select sfmc,fymc,fydw,fydj,d from (select b.sfmc,a.fymc,a.fydw,a.fydj,rownum d "
                         + "from gy_ylsf a,gy_sfxm b where a.fygb = b.sfxm  and a.zfpb = 0 and rownum <= {0}) nr where nr.d >= {1}";

            reportDetailMX = "";

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