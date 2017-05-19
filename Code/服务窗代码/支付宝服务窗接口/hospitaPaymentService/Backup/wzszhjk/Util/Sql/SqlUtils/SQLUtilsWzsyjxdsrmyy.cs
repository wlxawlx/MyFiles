using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    //温州市永嘉县第三人民医院
    public class SQLUtilsWZSYJXDSRMYY : SQLUtils
    {
        public SQLUtilsWZSYJXDSRMYY()
        {
            mzbrQueryStr = "select a.brbh brid,b.jzkh bkhm ,(case when a.fblx='01' then 'byk' when length(b.jzkh)='15' then 'smk' else 'ybk' end ) bklx," +
                " a.brxm brxm, NLS_UPPER(a.sfzhm) sfzh, a.tele lxdh, a.brdz jtdz from MZBRXX a,hmisw2003.mzbrjzkxx b" +
                " where a.brbh=b.brbh and a.brxm='{0}' ";
            zybrQueryStr = "select a.zyh brid,c.jzkh bkhm,(case when a.lb='01' then 'byk' when length(c.jzkh)='15' then 'smk' else 'ybk' end ) bklx," +
                " a.brxm brxm, NLS_UPPER(a.sfzh) sfzh, a.tele lxdh, a.addr jtdz, a.rysj ryrq, b.mc szbq, a.rycw szcw, '' as zyh " +
                " from hmisw2003.zybrxx a, hmisw2003.ksmc b, hmisw2003.mzbrjzkxx c where a.ksbh=b.no and a.brbh=c.brbh and a.brxm='{0}' and a.cyrq is null";


            mzbrBindCardstr = "select a.tele from mzbrxx a where a.brbh='{0}' and a.brxm='{2}'";
            zybrBindCardstr = "select a.tele from hmisw2003.zybrxx a where a.zyh='{0}' and a.brxm='{2}'";

            mzbrValidBkhm = "";
            zybrValidBkhm = "";

            mzbrForCreateOrder = "";
            zybrForCreateOrder = "";

            mzbrQueryBalance = "";
            zybrQueryBalance = "";

            mzbrValidId = "";
            zybrValidId = "";


            mzbrReportList = "select a.bgdbh as bgdh, a.yzmc as sjmd, to_char(c.hysjsj,  'yyyy-mm-dd hh24:mi:ss') as  cjsj, " +
                     " c.hysjryxm as sjr ,  to_char(c.hyjlsj,  'yyyy-mm-dd hh24:mi:ss')  as jysj, c.hyjlryxm as jyr, c.hyhdryxm as shr, '' as jzch, '' as zdjg, " +
                     " '' as bbmc, '' as mzbz, '' as dyjb, '' as  bz, '' as hzbh, '' as sbm , b.brxm as brxm  from  " +
                     " hmisw2003.hysqd a,mzbrxx b,hmisw2003.hybgd c where a.bgdbh=c.bgdbh and a.brbh=b.brbh and a.brbh='{0}' and a.zfry is null ";
            zybrReportList = "select a.bgdbh as bgdh, a.yzmc as sjmd, to_char(c.hysjsj,  'yyyy-mm-dd hh24:mi:ss') as  cjsj, " +
                     " c.hysjryxm as sjr ,  to_char(c.hyjlsj,  'yyyy-mm-dd hh24:mi:ss')  as jysj, c.hyjlryxm as jyr, c.hyhdryxm as shr, '' as jzch, '' as zdjg, " +
                     " '' as bbmc, '' as mzbz, '' as dyjb, '' as  bz, '' as hzbh, '' as sbm , b.brxm as brxm  from  " +
                     " hmisw2003.hysqd a,mzbrxx b,hmisw2003.hybgd c where a.bgdbh=c.bgdbh and a.brbh=b.brbh and a.zyh='{0}' and a.zfry is null ";

            reportDetailByBgdh = "select a.bgdbh as bgdh, a.yzmc as sjmd, to_char(c.hysjsj,  'yyyy-mm-dd hh24:mi:ss') as  cjsj, " +
                     " c.hysjryxm as sjr ,  to_char(c.hyjlsj,  'yyyy-mm-dd hh24:mi:ss')  as jysj, c.hyjlryxm as jyr, c.hyhdryxm as shr, '' as jzch, '' as zdjg, " +
                     " '' as bbmc, '' as mzbz, '' as dyjb, '' as  bz, '' as hzbh, '' as sbm , b.brxm as brxm  from  " +
                     " hmisw2003.hysqd a,mzbrxx b,hmisw2003.hybgd c where a.bgdbh=c.bgdbh and a.brbh=b.brbh and a.bgdbh='{0}' and a.zfry is null ";
            reportDetailBySbm = "select a.bgdbh as bgdh, a.yzmc as sjmd, to_char(c.hysjsj,  'yyyy-mm-dd hh24:mi:ss') as  cjsj, " +
                     " c.hysjryxm as sjr ,  to_char(c.hyjlsj,  'yyyy-mm-dd hh24:mi:ss')  as jysj, c.hyjlryxm as jyr, c.hyhdryxm as shr, '' as jzch, '' as zdjg, " +
                     " '' as bbmc, '' as mzbz, '' as dyjb, '' as  bz, '' as hzbh, '' as sbm , b.brxm as brxm  from  " +
                     " hmisw2003.hysqd a,mzbrxx b,hmisw2003.hybgd c where a.bgdbh=c.bgdbh and a.brbh=b.brbh and a.bgdbh='{0}' and a.zfry is null ";

            remaindBeds = "select g.mc,count(*) from (select f.mc mc,t.zyh zyh from hmisw2003.zycw t,hmisw2003.zybq f where t.bqh=f.dm) g group by g.mc,g.zyh having g.zyh is null";

            medicineByPydm = "select decode(a.lx,1,'西药',2,'中药',3,'草药') yplx,a.hzmc ypmc,a.dw ypdw,a.gg ypgg,a.cdmc ypcd,a.lsj ypjg, " +
                 " rownum d from hmisw2003.ypmc a,hmisw2003.ypmcsrm b where a.ypbh=b.ypbh and ( a.hzmc like'%{0}%' or b.srm1 like  '%{0}%' ) and rownum <=10";
            medicinePage = "select yplx,ypmc,ypdw,ypgg,ypcd,ypjg,d from" +
                 " (select decode(a.lx,1,'西药',2,'中药',3,'草药') yplx,a.hzmc ypmc,a.dw ypdw,a.gg ypgg,a.cdmc ypcd,a.lsj ypjg, " +
                 " rownum d from hmisw2003.ypmc a,hmisw2003.ypmcsrm b where a.ypbh=b.ypbh  and rownum <={0}) nr where nr.d >={1}";

            chargeByPydm = "select '' as fylx,a.mc fymc,'' as fydw,a.dj as fyjg,rownum d from hmisw2003.ssczmc a where (a.mc like'%{0}%' or a.srm1 like  '%{0}%') and rownum <=10";

            chargePage = "select fylx,fymc,fydw,fyjg,rownum d from (select '' as fylx,a.mc fymc,'' as fydw,a.dj as fyjg,rownum d from hmisw2003.ssczmc a where  rownum <={0}) nr where nr.d >={1}";

            reportDetailMX = "select a.xmmc as mc ,a.jgdw as dw, a.ckz as ckjg, (case when a.sfzc='Y' then '正常' when a.sfzc='N' then '不正常' when a.sfzc is null then '' end) as ts, " +
                 " a.jgsj as jg from hmisw2003.hyjgb a where a.bgdbh = '{0}'";

            accountListMzByPage = "";
            accountListMz = "";

            accountListZyByPage = "";
            accountListZy = "";

            mzbrReportJC = "select xno as bgdh, checkpos as cjmd,'' as cjsj,doctor as sjr,to_char(reportdate,  'yyyy-mm-dd hh24:mi:ss') as jysj,radio_doctor as jyr,advancedoc as shr,'' as jzch," +
                    " reportend as zdjg,'' as bbmc,'' as mzbs,'' as dyjb,'' as bz,patient_id  as hzbh,'' as sbm,name as brxm from pacsresult where resultflag='1' and patient_id='{0}'";
            zybrReportJC = "select xno as bgdh, checkpos as cjmd,'' as cjsj,doctor as sjr,to_char(reportdate,  'yyyy-mm-dd hh24:mi:ss') as jysj,radio_doctor as jyr,advancedoc as shr,'' as jzch," +
                    " reportend as zdjg,'' as bbmc,'' as mzbs,'' as dyjb,'' as bz,patient_id  as hzbh,'' as sbm,name as brxm from pacsresult where resultflag='1' and patient_id='{0}'";
            reportJCDetailByBgdh = "select xno as bgdh, checkpos as cjmd,'' as cjsj,doctor as sjr,to_char(reportdate,  'yyyy-mm-dd hh24:mi:ss') as jysj,radio_doctor as jyr,advancedoc as shr,'' as jzch," +
                    " reportend as zdjg,'' as bbmc,'' as mzbs,'' as dyjb,'' as bz,patient_id  as hzbh,'' as sbm,name as brxm from pacsresult where resultflag='1' and xno='{0}'";
            reportJCDetailBySbm = "select xno as bgdh, checkpos as cjmd,'' as cjsj,doctor as sjr,to_char(reportdate,  'yyyy-mm-dd hh24:mi:ss') as jysj,radio_doctor as jyr,advancedoc as shr,'' as jzch," +
                    " reportend as zdjg,'' as bbmc,'' as mzbs,'' as dyjb,'' as bz,patient_id  as hzbh,'' as sbm,name as brxm from pacsresult where resultflag='1' and xno='{0}'";
            reportJCDetailMX = "select '' as mc,'' as dw,'' as ckjg , reportinfo as ts,reportend as jg from pacsresult where xno='{0}'";
        }
    }
}