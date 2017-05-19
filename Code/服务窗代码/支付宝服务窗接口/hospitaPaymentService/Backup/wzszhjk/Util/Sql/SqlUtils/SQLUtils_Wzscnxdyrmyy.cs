using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    public class SQLUtilsWZSCNXDYRMYY : SQLUtils
    {
        public SQLUtilsWZSCNXDYRMYY()
        {
            mzbrQueryStr = "select brid  , bkhm , bklx, brxm, NLS_UPPER(sfzh), lxdh , jtdz from AV_MENZHENBRXX" +
                       " where brxm = '{0}' and NLS_UPPER(sfzh) = '{1}'";
            zybrQueryStr = " select brid  , bkhm , bklx, brxm, NLS_UPPER(sfzh), lxdh , jtdz, ryrq as jlsj, " +
                       "szbq , szcw,  zyh  from AV_ZHUYUANBRXX" +
                       " where brxm = '{0}' and NLS_UPPER(sfzh) = '{1}'";

            mzbrBindCardstr = "select lxdh from av_menzhenbrxx where brid = '{0}' and NLS_UPPER(sfzh) = '{1}' and brxm = '{2}' " +
               " and bklx = '{3}'";
            zybrBindCardstr = "select lxdh from av_zhuyuanbrxx where brid = '{0}' and NLS_UPPER(sfzh) = '{1}' and brxm = '{2}' " +
               " and bklx = '{3}'";

            mzbrValidBkhm = "select brid from av_menzhenbrxx where bkhm = '{0}' and brid = '{1}'";
            zybrValidBkhm = "select brid from av_zhuyuanbrxx where bkhm = '{0}' and brid = '{1}'";

            mzbrForCreateOrder = "select brxm, sfzh, lxdh from av_menzhenbrxx where brid = '{0}'";
            zybrForCreateOrder = "select brxm, sfzh, lxdh from av_zhuyuanbrxx where brid = '{0}'";

            mzbrQueryBalance = "select yuer as ye from av_menzhenyjkye where brid = '{0}'";
            zybrQueryBalance = "select a.yjkye as zhye from AV_ZHUYUANYJKYE a where a.brid = '{0}'";

            mzbrValidId = "select count(brid)  from AV_MENZHENBRXX where brid = '{0}'";
            zybrValidId = "select count(brid)  from AV_ZHUYUANBRXX where brid = '{0}'";

            mzbrReportList = "select bgdh, sjmd, cjsj, sjr, jysj, jyr, shr, jzch, zdjg, bbmc, " +
              " mzbz, dyjb, bz, hzbh, tiaoma, brxm from av_jianyanjg where brid = '{0}' order by jysj desc";
            zybrReportList = "";

            reportDetailByBgdh = "select bgdh, sjmd, cjsj, sjr, jysj, jyr, shr, jzch, zdjg, bbmc, " +
              " mzbz, dyjb, bz, hzbh, tiaoma, brxm from av_jianyanjg where bgdh = '{0}' order by jysj desc";
            reportDetailBySbm = "select bgdh, sjmd, cjsj, sjr, jysj, jyr, shr, jzch, zdjg, bbmc, " +
              " mzbz, dyjb, bz, hzbh, tiaoma, brxm from av_jianyanjg where tiaoma = '{0}' order by jysj desc";

            reportDetailMX = "select mc, dw, ckjg, ts, jg from av_jianyanmx k where  k.sampleno  = '{0}'";


            remaindBeds = "select bqmc, bqid, zzrenshu, zdcws, sycw  from av_shengyucws t";

            medicineByPydm = "select  yplx, ypmc, jldw, ypgg, ypcd, ypjg from AV_YAOPINJG where (pydm like" +
       "'%{0}%' or ypmc like '%{0}%') and rownum < 21";

            medicinePage = "select yplx,ypmc,jldw,ypgg,ypcd,ypjg from AV_YAOPINJG " +
                       " where  xh <= {0} and xh >= {1}";

            chargeByPydm = "select fylx,fymc,fydw,fyjg from av_shoufeixm where (fymc like '%{0}%' or pydm like '%{0}%')" +
                       "and rownum < 21";

            chargePage = "select fylx,fymc,fydw,fyjg from av_shoufeixm where xh <= {0} and xh >= {1}";

            accountListMzByPage = "select  fangsje as jkje, caozuorq as jkrq, zhifufs as zffs from " +
               " (select a.*, ROWNUM rn from av_menzhenyjk a  where  brid = " +
                    "  '{0}' and rownum <= {1} order by caozuorq desc) where rn >= {2}";
            accountListMz = "select fangsje as jkje, caozuorq as jkrq, zhifufs as zffs from av_menzhenyjk a  where  brid = '{0}' order by caozuorq desc";

            accountListZyByPage = "select jkje, jkrq, zffs from (select a.*, ROWNUM rn from AV_ZHUYUANYJK a  where  brid = " +
                      "'{0}' and rownum <= {1} order by jkrq desc) where rn >= {2}";
            accountListZy = "select jkje, jkrq, zffs from AV_ZHUYUANYJK a  where  brid = '{0}' order by jkrq desc";

            mzbrReportJC = "select bgdh,sjmd,cjsj,sjr,jysj,jyr,shr,jzch,zdjg,bbmc,mzbz,dyjb,bz,hzbh,sbm,brxm "+
                            "from AV_JIANCHAJG where brid='{0}' order by jysj desc";
            zybrReportJC = "select bgdh,sjmd,cjsj,sjr,jysj,jyr,shr,jzch,zdjg,bbmc,mzbz,dyjb,bz,hzbh,sbm,brxm " +
                            "from AV_JIANCHAJG where brid='{0}' order by jysj desc";
            reportJCDetailByBgdh = "select bgdh,sjmd,cjsj,sjr,jysj,jyr,shr,jzch,zdjg,bbmc,mzbz,dyjb,bz,hzbh,sbm,brxm " +
                            "from AV_JIANCHAJG where bgdh='{0}' order by jysj desc";
            reportJCDetailBySbm = "select bgdh,sjmd,cjsj,sjr,jysj,jyr,shr,jzch,zdjg,bbmc,mzbz,dyjb,bz,hzbh,sbm,brxm " +
                            "from AV_JIANCHAJG where sbh='{0}' order by jysj desc";
            reportJCDetailMX = "select sjmd,'','',ts,zdjg from AV_JIANCHAJG where bgdh='{0}'";
        }
    }
}