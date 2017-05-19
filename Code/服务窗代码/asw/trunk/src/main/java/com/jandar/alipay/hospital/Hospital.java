package com.jandar.alipay.hospital;


import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import com.jandar.alipay.core.struct.DepartmentInfo;

public class Hospital {
    public static final String HOSPITAL_ID = "1";

    private static Hospital hospital;
    private List<Section> sections = new ArrayList<Section>();
    public static boolean flag;

    private Hospital() {

    }

    public static Hospital getInstance(){
        if(hospital == null) {
            synchronized (Hospital.class) {
                if(hospital == null) {
                    hospital = new Hospital();
                }
            }
        }
        return hospital;
    }

//    public List<Section> getSections(List<Map<String, String>> data) {
//        if(!flag) {
//            synchronized (Hospital.class) {
//                if(!flag) {
//                    dataformat(data);
//                    flag = true;
//                }
//            }
//        }
//        return Collections.unmodifiableList(sections);
//    }
    
    public List<Section> getSections(List<DepartmentInfo> data) {
        if(!flag) {
            synchronized (Hospital.class) {
                if(!flag) {
                    dataformat(data);
                    flag = true;
                }
            }
        }
        return Collections.unmodifiableList(sections);
    }

//    private void dataformat(List<Map<String, String>> data) {
//        for(Map<String, String> per : data) {
//            String deptid = per.get("deptid");
//            if(!containerThisOneLevelSection(per)) {
//                Section newOneLevelSection = new Section(deptid, per.get("ksfl"), new ArrayList<Section>());
//                sections.add(newOneLevelSection);
//            }
//            for(Section s : sections) {
//                if(s.getDepartid().equals(deptid)){
//                    Section newTwoLevelSection = new Section(per.get("ksbm"), per.get("ksmc"), null);
//                    newTwoLevelSection.setDescribe(per.get("ksms"));
//                    s.getList().add(newTwoLevelSection);
//                }
//            }
//        }
//    }

    private void dataformat(List<DepartmentInfo> data) {
        for(DepartmentInfo per : data) {
            String deptid = per.getDepartcode();
            if(!containerThisOneLevelSection(per)) {
                Section newOneLevelSection = new Section(deptid, per.getDepartname(), new ArrayList<Section>());
                sections.add(newOneLevelSection);
            }
            for(Section s : sections) {
                if(s.getDepartid().equals(deptid)){
                     Section newTwoLevelSection = new Section(per.getSecondcode(), per.getSecondname(),null);
                     newTwoLevelSection.setDescribe(per.getDescribe());
                    s.getList().add(newTwoLevelSection);
                }
            }
        }
    }
    
//    private boolean containerThisOneLevelSection(Map<String, String> map) {
//        for(int i = 0; i<sections.size(); i++) {
//            if(sections.get(i).getDepartid().equals(map.get("deptid"))){
//                return true;
//            }
//        }
//        return false;
//    }
    
    private boolean containerThisOneLevelSection(DepartmentInfo map) {
        for(int i = 0; i<sections.size(); i++) {
            if(sections.get(i).getDepartid().equals(map.getDepartcode())){
                return true;
            }
        }
        return false;
    }
}
