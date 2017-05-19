using System;
using System.Collections.Generic;
using System.Web;

namespace HospitaPaymentService.Wzszhjk.Utils
{
    //温州市第七人民医院SQL
    public class SQLUtilsWZSDQRMYY : SQLUtils
    {
        public SQLUtilsWZSDQRMYY()
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

            mzbrQueryBalance = "";
            zybrQueryBalance = "select a.yjkye as zhye from AV_ZHUYUANYJKYE a, av_zhuyuanbrxx b where a.brid = '{0}' and a.brid = b.brid";

            mzbrValidId = "select count(brid)  from AV_MENZHENBRXX where brid = '{0}'";
            zybrValidId = "select count(brid)  from AV_ZHUYUANBRXX where brid = '{0}'";

            //"a.diagnostic zdjg," +
            mzbrReportList = "select a.SAMPLENO bgdh," +	//怀疑为报告单号
                        "a.examinaim sjmd," +
                        "to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj," +
                        "b.zhigongxm sjr," +
                        "to_char(a.checktime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                        "a.receiver jyr," +
                        "a.checkoperator shr," +
                        "c.keshimc jzch," +
                        "'' as  zdjg," +
                        "d.sampledescribe bbmc," +
                        "decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                        "'' as dyjb," +
                        "'' as bz, " +
                        "'' as hzbh," +
                        "'' as sbm, " +
                        "a.patientname brxm " +
                   " from lis.l_patientinfo a,his3.gy_zhigongxx b,his3.gy_keshi c,lis.l_sampletype d " +
                  "where a.requester = b.zhigongid(+) and a.section = c.keshiid(+) and a.sampletype = d.sampletype(+) " +
                  " and a.resultstatus in (4, 5, 6)  " +
                  " and a.stayhospitalmode in (1, 3, 4, 5, 6, 7, 8)" +
                  " and a.bingrenid = '{0}'  order by a.executetime desc";
            zybrReportList = "select a.SAMPLENO bgdh," +	//怀疑为报告单号
                        "a.examinaim sjmd," +
                        "to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj," +
                        "b.zhigongxm sjr," +
                        "to_char(a.checktime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                        "a.receiver jyr," +
                        "a.checkoperator shr," +
                        "c.keshimc jzch," +
                        "'' as  zdjg," +
                        "d.sampledescribe bbmc," +
                        "decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                        "'' as dyjb," +
                        "'' as bz, " +
                        "'' as hzbh," +
                        "'' as sbm, " +
                        "a.patientname brxm " +
                           "from lis.l_patientinfo a,his3.gy_zhigongxx b,his3.gy_keshi c,lis.l_sampletype d " +
                          "where a.requester = b.zhigongid(+) and a.section = c.keshiid(+) and a.sampletype = d.sampletype(+) " +
                          " and a.resultstatus in (4, 5, 6) " +
                          " and a.stayhospitalmode in (2)" +
                          "and a.bingrenzyid = '{0}' order by a.executetime desc";

            reportDetailByBgdh = "select a.SAMPLENO bgdh," +	//怀疑为报告单号
                        "a.examinaim sjmd," +
                        "to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj," +
                        "b.zhigongxm sjr," +
                        "to_char(a.checktime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                        "a.receiver jyr," +
                        "a.checkoperator shr," +
                        "c.keshimc jzch," +
                        "'' as  zdjg," +
                        "d.sampledescribe bbmc," +
                        "decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                        "'' as dyjb," +
                        "'' as bz, " +
                        "'' as hzbh," +
                        "'' as sbm, " +
                        "a.patientname brxm " +
                   "from lis.l_patientinfo a,his3.gy_zhigongxx b,his3.gy_keshi c,lis.l_sampletype d " +
                  "where a.requester = b.zhigongid(+) and a.section = c.keshiid(+) and a.sampletype = d.sampletype(+) " +
                  " and a.resultstatus in (4, 5, 6)  " +
                  " and '2' = '2' and a.SAMPLENO = '{0}' ";
            reportDetailBySbm = "select a.SAMPLENO bgdh," +	//怀疑为报告单号
                        "a.examinaim sjmd," +
                        "to_char(a.executetime, 'yyyy-mm-dd hh24:mi:ss') cjsj," +
                        "b.zhigongxm sjr," +
                        "to_char(a.checktime, 'yyyy-mm-dd hh24:mi:ss') jysj," +
                        "a.receiver jyr," +
                        "a.checkoperator shr," +
                        "c.keshimc jzch," +
                        "'' as  zdjg," +
                        "d.sampledescribe bbmc," +
                        "decode(a.stayhospitalmode,1,'门诊',2,'住院',3,'体检',4,'血库',5,'外单位',6,'药物验证',7,'科研',8,'结核') mzbz," +
                        "'' as dyjb," +
                        "'' as bz, " +
                        "'' as hzbh," +
                        "'' as sbm, " +
                        "a.patientname brxm " +
                             "from lis.l_patientinfo a,his3.gy_zhigongxx b,his3.gy_keshi c,lis.l_sampletype d , " +
                             " his3.gy_bingrenxx gy, his3.zy_bingrenxx zy " +
                            "where a.requester = b.zhigongid(+) and a.section = c.keshiid(+) and a.sampletype = d.sampletype(+) " +
                            " and a.resultstatus in (4, 5, 6)  " +
                            " and a.bingrenid = gy.bingrenid(+) and a.bingrenid = zy.bingrenzyid(+) " +
                            " and (gy.xingming = '{1}' or zy.xingming = '{1}' )" +
                            " and a.doctadviseno = '{0}'  ";

            reportDetailMX = "Select b.Chinesename As mc,a.Unit As dw, " +
                         " replace(Nvl(a.Hint,Nvl(a.beizhu,RefLo||Decode(Nvl(RefHi,' '),' ',' ','-'||RefHi))),'  ','#') As ckjg, " +
                         " Decode(Substr(a.Resultflag, 1, 1),'A','','B','↑','C','↓','D','阴性','E','阳性',Substr(a.Resultflag, 1, 1)) As ts," +
                         " Nvl(a.Hint,a.Testresult) As jg," +
                         "b.Printord As PRINTORDER From lis.l_Testresult a, lis.l_Testdescribe b, lis.l_patientinfo c " +
                         "Where a.Testid = b.Testid And (a.Testresult is not null ) And a.Sampleno = '{0}' And a.Sampleno = c.Sampleno " +
                         "Order By b.Printord";


            remaindBeds = "select bingqumc, bingquid, renshu, zuidacws, zuidacws - renshu  from av_shengyucws t";

            medicineByPydm = "select  yplx, ypmc, jldw, ypgg, ypcd, ypjg from AV_YAOPINJG where (pydm like" +
         "'%{0}%' or ypmc like '%{0}%') and rownum < 21";

            medicinePage = "select yplx,ypmc,jldw,ypgg,ypcd,ypjg from AV_YAOPINJG " +
                         " where  xh <= {0} and xh >= {1}";

            chargeByPydm = "select fylx,fymc,fydw,fyjg from av_shoufeixm where (fymc like '%{0}%' or pydm like '%{0}%')" +
                         "and rownum < 21";

            chargePage = "select fylx,fymc,fydw,fyjg from av_shoufeixm where xh <= {0} and xh >= {1}";

            accountListMzByPage = "";
            accountListMz = "";

            accountListZyByPage = "select jkje, jkrq, zffs from (select a.*, ROWNUM rn from AV_ZHUYUANYJK a  where  brid = " +
                        "'{0}' and rownum <= {1} order by jkrq desc) where rn >= {2}";
            accountListZy = "select jkje, jkrq, zffs from AV_ZHUYUANYJK a  where  brid = '{0}' order by jkrq desc";

            mzbrReportJC = "";
            zybrReportJC = "";
            reportJCDetailByBgdh = "";
            reportJCDetailBySbm = "";
            reportJCDetailMX = "";

           /*################################### 支付宝服务窗相关###########################################*/
            userRegisterStr = "insert into zfb_yonghuzc_bk (name, phone, idcardno, address, openid, headurl, usertype, patientid) " +
                "values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '  ')";
            userRegisterBindCardStr = "insert into zfb_yonghuzc_bk (name, phone, idcardno, address, openid, headurl, cardno, patientid, usertype) " +
                "values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')";
            landStr = "select openid, name, phone, idcardno, address, headurl, cardno, patientid from zfb_yonghuzc_bk t where openid = '{0}' and usertype = '{1}'";
            modifylandStr = "update zfb_yonghuzc_bk set name = (case when '{1}' is null then name else '{1}' end), phone = (case when '{2}' is null then phone else '{2}' end), " +
                "idcardno = (case when '{3}' is null then idcardno else '{3}' end), address = (case when '{4}' is null then address else '{4}' end), headurl = (case when '{5}' is null then headurl else '{5}' end), " +
                "usertype = (case when '{6}' is null then usertype else cast('{6}' as int) end) where openid = '{0}'";
            addContactsStr = "insert into ZFB_TIANJIACYLXR (openid, label, name, phone, idcardno, address, linkmanid)" +
                " values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')";
            favoriteContactsListStr = "select linkmanid, label, name, phone, idcardno, address, bindcardfalg, patientid, cardno from ZFB_TIANJIACYLXR "+
                "where openid = '{0}'";
            favoriteContactsListStrWithLinkmanid = "select linkmanid, label, name, phone, idcardno, address, bindcardfalg, patientid, cardno from ZFB_TIANJIACYLXR " +
                "where openid = '{0}' and linkmanid = '{1}'";
            findLinkManNameStr = "select linkmanid, name from ZFB_TIANJIACYLXR " +
                "where openid = '{0}' and linkmanid = '{1}'";
            deleteFavoriteContactsStr = "delete from ZFB_TIANJIACYLXR where openid = '{0}' and linkmanid = '{1}' ";
            getmzklistStr = "select cardtype, cardname, cardno, idcardno, patientid, patientname, birthday, phone, balance, cost from ZFB_MENZHENKLB t" +
                " where idcardno = '{0}' and patientname = '{1}'";
            getmzkInforStr = "select cardtype, cardname, cardno, idcardno, patientid, patientname, birthday, phone, balance, cost from ZFB_MENZHENKLB t" +
                " where patientid = '{0}'";
            usermzkBindStr = "select cardtype, cardname, cardno from ZFB_MENZHENKLB t" +
                " where cardno = '{0}' and patientid = '{1}'";
            updateUsermzkBindInfoStr = "update zfb_yonghuzc_bk set cardno = '{0}', patientid = '{1}' where openid = '{2}'";
            usermzkRelieveBindStr = "update zfb_yonghuzc_bk set cardno = ' ', patientid = ' ' where openid = '{0}'";
            getCardNoFromIDStr = "select cardno from zfb_yonghuzc_bk t where openid = '{0}'";
            getLinkmanCardNoFromIDStr = "select cardno from ZFB_TIANJIACYLXR t where openid = '{0}' and linkmanid = '{1}' ";
            updateLinkManBindInfoSqlStr = "update ZFB_TIANJIACYLXR t set cardno = '{2}', patientid = '{3}', bindcardfalg = {4} where openid = '{0}' and linkmanid = '{1}' ";
            favoriteContactsrmzkRelieveBindStr = "update ZFB_TIANJIACYLXR t set cardno = ' ', patientid = ' ', bindcardfalg = {2} where openid = '{0}' and linkmanid = '{1}' ";
            dctorInfoXingming = "select distinct doctorcode, doctorname, sex, pictureurl, level1, recommend, adept, departcode, departname from (select doctorcode, doctorname, sex, pictureurl, level1, recommend, " +
                 "adept, departcode, departname, rownum d from ZFB_YUYUE_YS t where (doctorname like '%{0}%') and rownum <={1}) nr where nr.d >={2}";
            dctorInfoPinYin = "select distinct doctorcode, doctorname, sex, pictureurl, level1, recommend, adept, departcode, departname from (select doctorcode, doctorname, sex, pictureurl, level1, recommend, " +
                " adept, departcode, departname, reserve,rownum d from ZFB_YUYUE_YS t where (namepy like upper('%{0}%') or doctorname like '%{0}%') and rownum <={1}) nr where nr.d >={2}";
            doctorInfoByCode = "select distinct doctorcode, doctorname, sex, pictureurl, level1, recommend, adept, departcode, departname from ZFB_YUYUE_YS where (doctorcode like '%{0}%')";
            doctorInfoTingZhen = "select code, name, pictureurl, departcode, departname, stopdate, stopshift from (select code, name, pictureurl, departcode, departname, stopdate, stopshift, rownum d from ZFB_YISHENGXX t where rownum <={0}) nr where nr.d >={1}";
			appointmentInforWithDepartcode = "select departcode, departname, secondcode, secondname, describe from  ZFB_YUYUE_KS t where departcode = '{0}'";
            appointmentInfor = "select departcode, departname, secondcode, secondname, describe from  ZFB_YUYUE_KS t";
            departmentSchedul = "select scheduledate, remain, total from zfb_yuyue_kspb t where departcode = '{0}'";
            reservationDoctorWithScheduledate = "select doctorcode, doctorname, pictureurl, level1, recommend, adept, reserve, scheduledates from ZFB_YUYUE_YS t" +
                " where departcode = '{0}' and scheduledate = to_date('{1}','yyyy-MM-dd')";
            reservationDoctor = "select doctorcode, doctorname, pictureurl, level1, recommend, adept, sum(reserve) as reserve , scheduledates, xuhao " +
                "from ZFB_YUYUE_YS t where departcode = '{0}' group by doctorcode, doctorname, pictureurl, level1, recommend, adept, scheduledates, xuhao order by xuhao, doctorname";
            doctorSchedul = "select scheduleseq, departcode, departname, doctorcode, doctorname, special, sum(reserve), total, address, scheduledate, " +
                "shiftcode, shiftname, fee from ZFB_YUYUE_YSPB t where doctorcode = '{0}' and departcode = '{1}' and scheduledate = to_date('{2}','yyyy-MM-dd') group by scheduleseq, departcode, departname, " +
                "doctorcode, doctorname, special,  total, address, scheduledate, shiftcode, shiftname, fee order by scheduledate";
            doctorSchedulWithDoctorcode = "select scheduleseq, departcode, departname, doctorcode, doctorname, special, sum(reserve), total, address, scheduledate, " +
                "shiftcode, shiftname, fee from ZFB_YUYUE_YSPB t where doctorcode = '{0}' group by scheduleseq, departcode, departname, " +
                "doctorcode, doctorname, special,  total, address, scheduledate, shiftcode, shiftname, fee order by scheduledate";
            doctorSchedulWithDoctorcodeAndDepartcode = "select scheduleseq, departcode, departname, doctorcode, doctorname, special, sum(reserve), total, address, scheduledate, " +
                "shiftcode, shiftname, fee from ZFB_YUYUE_YSPB t where doctorcode = '{0}' and departcode = '{1}' group by scheduleseq, departcode, departname, " +
                "doctorcode, doctorname, special,  total, address, scheduledate, shiftcode, shiftname, fee order by scheduledate";
            doctorSchedulWithDoctorcodeAndScheduledate = "select scheduleseq, departcode, departname, doctorcode, doctorname, special, sum(reserve), total, address, scheduledate, " +
                "shiftcode, shiftname, fee from ZFB_YUYUE_YSPB t where doctorcode = '{0}' and scheduledate = to_date('{2}','yyyy-MM-dd') group by scheduleseq, departcode, departname, " +
                "doctorcode, doctorname, special,  total, address, scheduledate, shiftcode, shiftname, fee order by scheduledate";
            getZhuYuanPatientInfoStrWithName = "select inpatientno, patientname, patientidcardno, sex, birthday, address, phone, admitdate, dischargedate, stayday, xzmc, " +
                "endemicarea, brch, departcode, departname, ylhj, lwhj, zfje, jkje, zyzt, doctorcode, doctorname from ZFB_ZHUYUANBRXX t where patientidcardno = '{0}' and patientname = '{1}'";
            getZhuYuanPatientInfoStr = "select inpatientno, patientname, patientidcardno, sex, birthday, address, phone, admitdate, dischargedate, stayday, xzmc, " +
                "endemicarea, brch, departcode, departname, ylhj, lwhj, zfje, jkje, zyzt, doctorcode, doctorname from ZFB_ZHUYUANBRXX t where patientidcardno = '{0}'";
            getZhuYuanCostInfoStr = "select costcode, costname, totalfee, payfee from ZFB_ZHUYUANFY t where inpatientno = '{0}'";
            getZhuYuanCostInfoStrWithCostdate = "select costcode, costname, totalfee, payfee from ZFB_ZHUYUANFY t where inpatientno = '{0}' and costdate = '{1}'";
            ailpaybrReportList = "select doctadviseno, examinaim, requesttime, requester from ZFB_HUAYANBG t where name = '{0}' and idcardno = '{1}'";
            laboratoryTestsReportNameInformation = "select doctadviseno, requesttime, requester, executetime, executer, receivetime, receiver, stayhospitalmode, " +
                "patientid, section, bedno, patientname, sex, age, diagnostic, sampletype, examinaim, requestmode, checker, checktime, csyq, profiletest from ZFB_HUAYANBG t " +
                "where doctadviseno = '{0}'";
            getLaboratoryTestsReportDetailedListInformation = "select jylx, xmmc, result, hint, ckfw, xmdw from ZFB_HUAYANBG_MX t where doctadviseno = '{0}'";
            getInspectionReportList = "select doctadviseno, examinaim, requesttime, requester from ZFB_JIANCHA t where name = '{0}' and idcardno = '{1}'";
            getInspectionReportListWithECG = "select doctadviseno, examinaim, requesttime, requester from view_1 where name = '{0}' and idcardno = '{1}'";
            getInspectionReportNameInformation = "select doctadviseno, requesttime, requester, executetime, executer, receivetime, receiver, stayhospitalmode, " +
                "patientid, section, bedno, patientname, sex, age, diagnostic, sampletype, examinaim, requestmode, checker, checktime, csyq, profiletest from ZFB_JIANCHA t " +
                "where doctadviseno = '{0}'";
            getInspectionReportNameInformationWithECG = "select doctadviseno, requesttime, requester, executetime, executer, receivetime, receiver, stayhospitalmode, " +
                "patientid, section, bedno, patientname, sex, age, diagnostic, sampletype, examinaim, requestmode, checker, checktime, csyq, profiletest from view_1 t " +
                "where doctadviseno = '{0}'";
            getDInspectionReportResultsInformation = "select studyresult, diagresult from ZFB_JIANCHA t where doctadviseno = '{0}'";
            getDInspectionReportResultsInformationWithECG = "select studyresult, diagresult from view_1 t where doctadviseno = '{0}'";
            mzReservationInforSql = "select orderno, yiyongyuyuehao, orderseq, Doctorcode, Shiftcode, Week, Departcode, scheduledate from zfb_yuyue_mzhy t where Scheduleseq = '{1}' and shiftcode = {2}";
            mzReservationTimeInforSql = "select ordertime, orderno, orderendtime, shiftcode from zfb_yuyue_xhsj t where Departcode = '{0}' and week = {1} and shiftcode = {2} and doctorcode = '{3}' and orderno in ({4}) order by ordertime";
            mzReservationHistory = "select preengageseq, preengagedate, preengagetime, departcode, departname, doctorcode, doctorname, patientname, patientidcardno, " +
                "patientphone, patientaddress, preengageno, place, fee, preengagestate from ZFB_YUYUE_YYLS t where openid = '{0}'";
            mzOrderRetInfoSql = "select preengageseq, preengagedate, preengagetime, departcode, departname, doctorcode, doctorname, patientname, preengageno, place " +
                "from ZFB_YUYUE_YYLS t where preengageseq = '{0}'";
            cancelmzOrderRetInfoSql = "select preengageseq, preengagedate, preengagetime, departname, doctorname, patientname, preengageno, place from ZFB_YUYUE_YYLS t " +
                "where preengageseq = '{0}'";
            mzbrValidCardno = "select cardno from ZFB_MENZHENKLB t where patientid = '{0}' and cardno = '{1}'";
            CheckInformation = "select yylsh, brlx, brid, brxm, czje, openid from A_YL_DD t where ddzt = {0}";
            InformPatient = "select Openid, Patientname, Doctorname, Place, Preengagetime, Preengagedate from ZFB_YUYUE_YYLS t where Preengagestate = {0}";
            DrugPrescriptionInforList = "select cflsh, ksbm, ksmc, ysbm, ysxm, brid, kfrq, sfrq, zjje, lxmc from ZFB_CHUFANG t where brid = '{0}'";
            PatientsTakingDrugsInfor = "select ypmc, ypgg, ypsl, ycyl, yishengjianyi, sync, gytj from ZFB_CHUFANG_MX t where cflsh = '{0}'";
            mzMedicalRecordsList = "select jzxh, jzrq, ksmc, ysxm, zdxx from ZFB_MENZHENBL t where patient_id = '{0}'";
            ElectronicMedicalRecordt = "select mzzs, xbs, jws, grs, gms, hys, jzs, tgjc, fzjc, clyj from ZFB_MENZHENBL t where jzxh = '{0}'";
            mzSingleGuideAndTakeMedicine = "select cfxh, fphm, kfrq, ksmc, zynr, zywz, ysxm, zjje from ZFB_MENZHENBL_ZYD t where jzxh = '{0}'";
            mzReservationQueue= "select bingrenid, preengageseq, preengageno, queueseq, queueid, queuetime, shiftname, departname, doctorname,  " +
                "roomname, roompos, roomno, currentid, queuestate,patient_name from zfb_paiduihujiao where doctorname = '{0}' and shiftname = '{1}' order by preengageno";
            Queueseq = "select guahaoid from ZFB_YUYUE_YYLS t where openid = '{0}'";
            QueueseqWithQueueseq = "select guahaoid from ZFB_YUYUE_YYLS t where openid = '{0}' and guahaoid = '{1}'";
            IsZYBrxxValid = "select inpatientno from ZFB_ZHUYUANBRXX t where patientidcardno = '{0}' and patientname = '{1}'";
            SelectDoctorname = "select doctorname, shiftname, queueseq from zfb_paiduihujiao where queueseq in ({0})";
            SelectPaibanYS = "select distinct doctorname, shiftname from zfb_paiduihujiao";
            GetSelectOpenidsql = "select openid, guahaoid, preengageseq from ZFB_YUYUE_YYLS t where guahaoid = '{0}'";
        }
    }
}