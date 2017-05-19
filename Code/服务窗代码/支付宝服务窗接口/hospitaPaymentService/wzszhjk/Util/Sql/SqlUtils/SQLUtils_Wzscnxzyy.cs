using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    //温州市苍南县中西医院
    public class SQLUtilsWZSCNXZYY : SQLUtils
    {
        public SQLUtilsWZSCNXZYY()
        {
            mzbrQueryStr = "select ms.brid brid, gy.ckxx bkhm, (case when length(gy.ckxx)=41 then 'smk' when length(gy.ckxx)=28 then 'ybk'" +
                         " else 'byk' end ) bklx, ms.brxm brxm, NLS_UPPER(ms.sfzh) sfzh,  ms.jtdh lxdh, ms.hkdz jtdz" +
                         " from portal_his.gy_ckda gy,portal_his.ms_brda ms where ms.brid = gy.brid and ms.brxm = '{0}' and ms.sfzh = '{1}' and gy.zfpb = 0";
            zybrQueryStr = "select zy.zyh brid, zy.knxx bkhm, (case when length(zy.knxx)=41 then 'smk' when length(zy.knxx)=28 then 'ybk'" +
                         " else 'byk' end ) bklx, zy.brxm brxm, NLS_UPPER(zy.sfzh) sfzh,  zy.lxdh lxdh, zy.hkdz jtdz, zy.ryrq ryrq, ks.ksmc szbq, " +
                         " zy.brch szcwh , '' as zyh " +
                         " from portal_his.zy_brry zy, portal_his.gy_ksdm ks " +
                         " where zy.brbq=ks.ksdm and " + " zy.brxm = '{0}' and zy.cypb = 0 and NLS_UPPER(zy.sfzh) = '{1}'";


            mzbrBindCardstr = "select  ms.jtdh lxdh from portal_his.gy_ckda gy,portal_his.ms_brda ms where ms.brid = gy.brid and " +
                             " ms.brid = '{0}' and NLS_UPPER(ms.sfzh) = '{1}' and ms.brxm = '{2}'  and gy.zfpb = 0";
            zybrBindCardstr = "select zy.jtdh lxdh from portal_his.zy_brry zy, portal_his.gy_ksdm ks where zy.brbq = ks.ksdm and zy.zyh = '{0}'" +
                 " and NLS_UPPER(zy.sfzh) = '{1}' and zy.brxm = '{2}' and zy.cypb = 0 ";

            mzbrValidBkhm = "select ms.brid brid from portal_his.gy_ckda gy,portal_his.ms_brda ms where  ms.brid = gy.brid  and " +
                 " gy.ckhm = '{0}' and ms.brid = '{1}' and  gy.zfpb = 0";
            zybrValidBkhm = "select zy.zyh brid, zy.knxx bkhm from portal_his.zy_brry zy where  zy.knxx = '{0}'" +
                  " and zy.zyh = '{1}' and zy.cypb = 0 ";

            mzbrForCreateOrder = "";
            zybrForCreateOrder = "";

            mzbrQueryBalance = "";
            zybrQueryBalance = "";

            mzbrValidId = "select count(ms.brid)  from portal_his.ms_brda ms where ms.brid = '{0}'";
            zybrValidId = "select count(zy.zyh)  from portal_his.zy_brry zy where zy.zyh = '{0}'";


            mzbrReportList = "";
            zybrReportList = "";

            reportDetailByBgdh = "";
            reportDetailBySbm = "";

            remaindBeds = "select b.ksmc,b.ksdm,  (b.zcw - a.sycw)  zycw, b.zcw, a.sycw from " +
     " (select count(c.brch) as zcw,g.ksmc,g.ksdm from portal_his.zy_cwsz c, portal_his.gy_ksdm g where c.ksdm=g.ksdm and c.jcpb=0 and g.ksdm not in(215,209,901,207)" +
      " group by g.ksdm,g.ksmc) b, (select count(c.brch) as sycw,g.ksdm from portal_his.zy_cwsz c, portal_his.gy_ksdm g where c.ksdm=g.ksdm and c.jcpb=0 and zyh is " +
             " null and g.ksdm not in(215,209,901,207) group by g.ksdm,g.ksmc) a where a.ksdm = b.ksdm";

            medicineByPydm = "select decode(a.type,1,'西药',2,'中药',3,'草药'),a.ypmc,a.ypdw,a.ypgg,c.cdmc ,b.lsjg, " +
                 " rownum d from portal_his.yk_typk a,portal_his.yk_ypcd b,portal_his.yk_cddz c where a.ypxh = b.ypxh and a.ykzf = 0 and b.ypcd = c.ypcd and ( a.ypmc " +
             " like'%{0}%' or a.pydm like  '%{0}%' ) and rownum <=10";
            medicinePage = "select yplx,ypmc,ypdw,ypgg,cdmc,lsjg,d from "
                         + "(select decode(a.type,1,'西药',2,'中药',3,'草药') yplx,a.ypmc,a.ypdw, a.ypgg, c.cdmc, b.lsjg, rownum d from "
                         + "portal_his.yk_typk a,portal_his.yk_ypcd b,portal_his.yk_cddz c where a.ypxh = b.ypxh and a.ykzf = 0 and b.ypcd = c.ypcd and rownum <={0})"
                         + " nr where nr.d >={1}";

            chargeByPydm = "select b.sfmc,a.fymc,a.fydw,a.fydj from portal_his.gy_ylsf a,portal_his.gy_sfxm b where a.fygb = b.sfxm " +
                 " and a.zfpb =0 and ( a.fymc like'%{0}%' or a.pydm like  '%{0}%' ) and rownum <=10";

            chargePage = "select sfmc,fymc,fydw,fydj,d from (select b.sfmc,a.fymc,a.fydw,a.fydj,rownum d "
                         + "from portal_his.gy_ylsf a,portal_his.gy_sfxm b where a.fygb = b.sfxm  and a.zfpb = 0 and rownum <= {0}) nr where nr.d >= {1}";

            reportDetailMX = "";

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