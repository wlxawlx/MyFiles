package com.jandar.cloud.hospital.dao.impl;

import com.jandar.cloud.hospital.bean.Doctor;
import com.jandar.cloud.hospital.dao.DoctorDao;
import com.jandar.cloud.hospital.bean.Schedule;
import com.jandar.cloud.hospital.dao.ScheduleDao;
import com.jandar.util.DateUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;

import javax.annotation.Resource;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

/**
 * 医生排班的Dao应用
 * Created by admin on 2016/10/17.
 */
public class ScheduleDaoImpl {

    @Autowired
    private MongoTemplate mongoTemplate;
    @Resource
    private ScheduleDao scheduleDao;

    /**
     * 根据医生Code来查找排班信息
     */
    public List<Schedule> findByDoctorCode(String doctorCode) {
        Query query = new Query(Criteria.where("DoctorCode").in(doctorCode));
        query.fields().include("DoctorCode").include("DoctorName").include("DeptName").include("Remain")
                .include("Fee").include("Date").include("Sources");
        List<Schedule> scheduleList = mongoTemplate.find(query, Schedule.class);
        for (Schedule schedule : scheduleList) {
            List<Schedule.Sources> sourcesList = new ArrayList<>();
            List<Schedule.Sources> sourcesInfo = schedule.getSources();
            for (Schedule.Sources sources : sourcesInfo) {
                if (sources.getStatus().equals("2")) {
                    sourcesList.add(sources);
                }
            }
            schedule.setSources(sourcesList);
        }
        return scheduleList;
    }

    /**
     * 返回改号源的状态
     *
     * @param doctorCode
     * @param dateDay
     * @param number
     * @return
     */
    public String findSourceStatus(String doctorCode, String dateDay, Integer number) {
        Query query = new Query(Criteria.where("DoctorCode").in(doctorCode).and("Date").in(dateDay));
        Schedule schedule = mongoTemplate.findOne(query, Schedule.class);
        String status = null;
        if (schedule == null)
            return null;
        if (number > schedule.getSize())
            return null;
        status = schedule.getSources().get(number - 1).getStatus();
        return status;
    }

    /**
     * @param doctorCode
     * @param dateDay
     * @return
     */
    public Schedule findOne(String doctorCode,String dateDay){
        Query query = new Query(Criteria.where("DoctorCode").in(doctorCode).and("Date").in(dateDay));
        Schedule schedule = mongoTemplate.findOne(query, Schedule.class);
        return schedule;
    }

    /**
     * 更新已经选择号源的状态为锁定状态，即=“3”,同时更新schedule中Remain剩余号源数，减1
     *
     * @param doctorCode
     * @param dateDay
     * @param number
     */
    public void updateSourceStatus(String doctorCode, String dateDay, Integer number) {
        Integer remain;
        Query query = new Query(Criteria.where("DoctorCode").in(doctorCode).and("Date").in(dateDay));
        Schedule schedule = mongoTemplate.findOne(query, Schedule.class);
        remain = schedule.getRemain() - 1;
        mongoTemplate.updateMulti(new Query(Criteria.where("DoctorCode").is(doctorCode).and("Date").is(dateDay).and("Sources.Number").is(number)),
                new Update().set("Sources.$.Status", "3").set("Remain", remain), Schedule.class);
    }

    /**
     * 根据医生代码，返回医生已排班情况下，剩下的所有号源总数
     *
     * @param doctorCode
     * @return
     */
    public Integer backRemainNumber(String doctorCode) {
        Integer count = 0;
        Query query = new Query(Criteria.where("DoctorCode").in(doctorCode));
        List<Schedule> schedules = mongoTemplate.find(query, Schedule.class);
        Date date = new Date();
        String dateToday = DateUtil.formatTime1(date);
        for (Schedule schedule : schedules)
            if (schedule.getDate().compareTo(dateToday) >= 0)
                count = count + schedule.getRemain();
        return count;
    }
    SimpleDateFormat format = new SimpleDateFormat("yyyy/MM/dd HH:mm");

    private Date toDate(String dateSrc)
    {
        try {
            return format.parse(dateSrc);
        } catch (ParseException e) {
            e.printStackTrace();
            return new Date();
        }
    }

    /**
     * 根据医生id返回相关排班信息
     * //要加上大于或等于今天
     *
     * @param doctorCode
     * @return
     */
    public List<Schedule> findByScheduleInfo(String doctorCode) {


        String today=DateUtil.getCurrentDate("yyyy/MM/dd");
        String nowMinute=DateUtil.getCurrentDate("HH:mm");
        Date nowDate=new Date();

        Criteria criatira = new Criteria();
        criatira.andOperator(Criteria.where("DoctorCode").is(doctorCode),Criteria.where("Date").gte(today));
        Query query = new Query(criatira);
        query.fields().include("Remain").include("Fee").include("Date").include("Sources");
        List<Schedule> scheduleList = mongoTemplate.find(query, Schedule.class);

        //剔除不符合规范号源
        for(Schedule sch:scheduleList)
        {
            if(today.equals(sch.getDate()))//今天的号源  还要比较时间
            {
                Iterator<Schedule.Sources> iteSource=sch.getSources().iterator();
                while (iteSource.hasNext())
                {
                    Schedule.Sources source=iteSource.next();
                    String thatTime=today+" "+source.getTime();
                    if(nowDate.after(toDate(thatTime)))
                    {
                        //System.out.println("============toDate=======cur_Time:"+source.getTime()+" sch.getDate():"+sch.getDate());
                        iteSource.remove();
                    }
                    else
                    {
                        if(!"2".equals(source.getStatus()))//2表示未预约
                        {
                            //System.out.println("===========already:"+source.getTime()+" sch.getDate():"+sch.getDate());
                            iteSource.remove();
                        }
                    }
                }
            }
            else//以后的
            {
                Iterator<Schedule.Sources> iteSource=sch.getSources().iterator();
                while (iteSource.hasNext())
                {
                    Schedule.Sources source=iteSource.next();
                    if(!"2".equals(source.getStatus()))//2表示未预约
                    {
                        //System.out.println("===========already2:"+source.getTime()+" sch.getDate():"+sch.getDate());
                        iteSource.remove();
                    }
                }
            }

            sch.setRemain(sch.getSources().size());

        }


        return scheduleList;
    }




    /**
     * 返回号源的具体时间，时分部分
     *
     * @param doctorCode
     * @param dateDay
     * @param number
     * @return
     */
    public String backDateTime(String doctorCode, String dateDay, Integer number) {
        Query query = new Query(Criteria.where("DoctorCode").in(doctorCode).and("Date").in(dateDay));
        Schedule schedule = mongoTemplate.findOne(query, Schedule.class);
        String dateTime = null;
        if (schedule == null)
            return null;
        if (number > schedule.getSize())
            return null;
        dateTime = schedule.getSources().get(number - 1).getTime();
        return dateTime;
    }

    /**
     * 返回号源的挂号费
     *
     * @param doctorCode
     * @param dateDay
     * @return
     */
    public String backFee(String doctorCode, String dateDay) {
        Query query = new Query(Criteria.where("DoctorCode").in(doctorCode).and("Date").in(dateDay));
        Schedule schedule = mongoTemplate.findOne(query, Schedule.class);
        String fee = null;
        if (schedule == null)
            return null;
        fee = schedule.getFee();
        return fee;
    }

    /**
     * 更新已经选择号源的状态为已经预约，即=“1”,
     * 如果号源状态为2，同时更新schedule中Remain剩余号源数，减1；如果号源状态为3，无需更新schedule中Remain剩余数量
     *
     * @param doctorCode
     * @param dateDay
     * @param number
     */
    public void updateSourceStatusUsed(String doctorCode, String dateDay, Integer number, String patientCode, String patientName) {
        Integer remain;
        String status = null;
        Query query = new Query(Criteria.where("DoctorCode").in(doctorCode).and("Date").in(dateDay));
        Schedule schedule = mongoTemplate.findOne(query, Schedule.class);
        if (schedule != null)
            if (number <= schedule.getSize()) {
                status = schedule.getSources().get(number - 1).getStatus();
                if ("2".equals(status)) {
                    remain = schedule.getRemain() - 1;
                    mongoTemplate.updateMulti(new Query(Criteria.where("DoctorCode").is(doctorCode).and("Date").is(dateDay).and("Sources.Number").is(number)),
                            new Update().set("Sources.$.Status", "1").set("Remain", remain).set("Sources.$.PatientCode", patientCode).set("Sources.$.PatientName", patientName), Schedule.class);
                }
                if ("3".equals(status)) {
                    mongoTemplate.updateMulti(new Query(Criteria.where("DoctorCode").is(doctorCode).and("Date").is(dateDay).and("Sources.Number").is(number)),
                            new Update().set("Sources.$.Status", "1").set("Sources.$.PatientCode", patientCode).set("Sources.$.PatientName", patientName), Schedule.class);
                }
            }
    }


    /**
     * 根据排班日期和医生代码返回数量
     *
     * @param doctorCode
     * @param date
     */
    public long countByDoctorAndDate(String doctorCode, String date) {
        Query query = new Query(Criteria.where("DoctorCode").is(doctorCode).and("Date").is(date));
        long count = mongoTemplate.count(query, Schedule.class);
        return count;
    }

    /**
     * 根据排班代码修改排班是否有效
     *
     * @param code
     */

    public void updateValid(String code) {
        mongoTemplate.updateMulti(new Query(Criteria.where("Code").is(code)),
                new Update().set("Valid", "No"), Doctor.class);
    }

    /**
     * 锁定且超过40分钟未付款解锁
     * 更改号源状态为未预约，且删除预约记录。
     */
    public void scheduleUnlock(){
        Criteria criteria = Criteria.where("CreatTime");
        Query query = new Query();


    }

}
