package com.jandar.alipay.dao;

import com.jandar.alipay.core.HospitalException;
import net.sf.json.JSONObject;
import org.apache.log4j.Logger;

import java.io.*;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/*
 * 功能:
 * 		读取三个json文件，返回三个list集合
 * 
 * 实现步骤:
 * 		1.静态方法返回获得的List
 * 		2.读取文件
 * 		3.解析文件，返回list
 * 
 */
public class IntelligentGuideData {

    static Logger logger = Logger.getLogger(IntelligentGuideData.class);

    private static IntelligentGuideData data = new IntelligentGuideData();
    private static List<JSONObject> bodyparts;
    private static List<JSONObject> symptom_classific;
    private static List<JSONObject> symptoms;
    private static List<JSONObject> departments;
    private static List<JSONObject> sickness;

    /**
     * 身份部位索引,key 大部位代码, value 列表下标
     */
    private static Map<String, Integer> bodyparts_partcode_index;
    private static Map<String, Integer> symptomClassific_Index;
    private static Map<String, Integer> departments_index;
    private static Map<String, Integer> sickness_index;

    /**
     * 症状代码索引,key 为 症状代码, value 列表一点下标
     */
    private static Map<String, Integer> symptoms_code_index;

    /**
     * 症状 所属部位与分类的索引 key 为 部位代码_分类代码  value 是列表下标的列表
     */
    private static Map<String, List<Integer>> symptoms_part_classific_index;

    private IntelligentGuideData() {
        String bodypartsPath = IntelligentGuideData.class.getClassLoader().getResource("bodyparts.json").getPath();
        String symptom_classificPath = IntelligentGuideData.class.getClassLoader().getResource("symptom_classific.json")
                .getPath();
        String symptomsPath = IntelligentGuideData.class.getClassLoader().getResource("symptoms.json").getPath();
        String departmentsPath = IntelligentGuideData.class.getClassLoader().getResource("departments.json").getPath();
        String sicknessPath = IntelligentGuideData.class.getClassLoader().getResource("sickness.json").getPath();

        bodyparts = readFile(new File(bodypartsPath));
        symptom_classific = readFile(new File(symptom_classificPath));
        symptoms = readFile(new File(symptomsPath));
        departments = readFile(new File(departmentsPath));
        sickness = readFile(new File(sicknessPath));

		/*
         * 对部位的部位代码设索引
		 */
        bodyparts_partcode_index = new HashMap<String, Integer>();
        for (int index = 0; index < bodyparts.size(); index++) {
            bodyparts_partcode_index.put((String) bodyparts.get(index).get("partcode"), index);
        }

		/*
         * 对症状分类的分类部位代码设索引
		 */
        symptomClassific_Index = new HashMap<String, Integer>();
        for (int index = 0; index < symptom_classific.size(); index++) {
            symptomClassific_Index.put((String) symptom_classific.get(index).get("partcode"), index);
        }

		/*
         * 对症状的相关字段设索引
		 * 
		 */
        symptoms_part_classific_index = new HashMap<String, List<Integer>>();
        symptoms_code_index = new HashMap<String, Integer>();
        for (int index = 0; index < symptoms.size(); index++) {

            JSONObject jsonObject = symptoms.get(index);

            // 组织症状代码的索引
            String symptomcode = jsonObject.getString("symptomcode");
            symptoms_code_index.put(symptomcode, index);

            // 组织症状部位及分类的索引
            String partcode = jsonObject.getString("partcode");
            String classify = jsonObject.getString("classificcode");
            String key = partcode + "_" + classify;

            List<Integer> indexs = symptoms_part_classific_index.get(key);
            if (indexs == null) {
                indexs = new ArrayList<Integer>();
            }
            indexs.add(index);
            symptoms_part_classific_index.put(key, indexs);

            // 组织症状部位及默认分类的索引
            if (!"01".equals(classify)) {
                key = partcode + "_01";
                indexs = symptoms_part_classific_index.get(key);
                if (indexs == null) {
                    indexs = new ArrayList<Integer>();
                }
                indexs.add(index);
                symptoms_part_classific_index.put(key, indexs);
            }
        }

		/*
         * 对部门建立索引
		 */
        departments_index = new HashMap<String, Integer>();
        for (int index = 0; index < departments.size(); index++) {
            departments_index.put((String) departments.get(index).get("visitdepart"), index);
        }

		/*
         * 对疾病代码建立索引
		 */
        sickness_index = new HashMap<String, Integer>();
        for (int index = 0; index < sickness.size(); index++) {
            sickness_index.put((String) sickness.get(index).get("code"), index);
        }

    }

    public List<JSONObject> getBodyparts() {
        return bodyparts;
    }

    public static List<JSONObject> getSymptom_classific() {
        return symptom_classific;
    }

    public static List<JSONObject> getSymptoms() {
        return symptoms;
    }

    /*
     * 读取JSON文件，返回一个JSON的列表
     */
    private static List<JSONObject> readFile(File bodyparts) {
        BufferedReader br = null;
        List<JSONObject> list = new ArrayList<JSONObject>();
        try {
            br = new BufferedReader(new InputStreamReader(new FileInputStream(bodyparts), "UTF-8"));
            String line = null;
            while ((line = br.readLine()) != null) {
                list.add(JSONObject.fromObject(line));
                line = null;
            }
            br.close();
            br = null;
        } catch (IOException e) {
            e.printStackTrace();
        }
        return list;
    }

    /*
     * 实现单利，返回该类的对象
     */
    public static IntelligentGuideData getIntelligentGuideData() {
        return data;
    }

    /**
     * 根据大部位的代码获得部位信息
     *
     * @param partcode 大部位的代码
     * @return
     * @throws HospitalException
     */
    public Map<String, Object> getBodyPartByCodeFromPartcode(String partcode) throws HospitalException {
        if (bodyparts_partcode_index.containsKey(partcode)) {
            Integer index = bodyparts_partcode_index.get(partcode);
            return bodyparts.get(index);
        } else {
            throw new HospitalException("找不到这个部位");
        }
    }

    /**
     * 根据分类部位代码获得部位信息
     *
     * @param partcode 部位代码
     * @return
     * @throws HospitalException
     */
    public Map<String, Object> getSymptomClassificFromPartcode(String partcode) throws HospitalException {
        if (symptomClassific_Index.containsKey(partcode)) {
            Integer index = symptomClassific_Index.get(partcode);
            return symptom_classific.get(index);
        } else {
            throw new HospitalException("找不到该部位的分类信息");
        }
    }

    /**
     * 通过部位代码及分类代码获得相应症状的数据集合
     *
     * @param partcode
     * @param classificcode
     * @return
     * @throws HospitalException
     */
    public List<Map<String, Object>> getSymptomFromPartcodeClassific(String partcode, String classificcode) throws HospitalException {
        String key = partcode + "_" + classificcode;
        if (symptoms_part_classific_index.containsKey(key)) {
            List<Integer> indexs = symptoms_part_classific_index.get(key);
            return getList(symptoms, indexs);
        } else {
            throw new HospitalException("找不到您输入的部位代码");
        }
    }

    /**
     * 通过症状代码获得相应的症状map
     *
     * @param symptomcode
     * @return
     */
    public Map<String, Object> getSymptomFromSymptomcode(String symptomcode) {
        if (symptoms_code_index.containsKey(symptomcode)) {
            int index = (int) symptoms_code_index.get(symptomcode);
            return symptoms.get(index);
        }
        return null;
    }

    /**
     * 通过症状代码获得相应的症状map,并用sex去筛选sickness,并且返回的sickness中的departments也已获取到
     *
     * @param symptomcode
     * @return
     * @throws HospitalException 注返回的疾病已筛选性别，且 departments 是一个 List<Map<String,String>> ## 疾病
     *                      (sickness) | **参数** | **参数名** | **类型** | **必选** | **描述** | |
     *                      ----------- | ----------- | ---------- | ------ |
     *                      ---------------------------------------- | | code | 疾病代码 |
     *                      string | 是 | 疾病的代码 | | name | 疾病名称 | string | 是 | 疾病的名称 | |
     *                      applyobject | 疾病适用对象 | string | 是 | 0：女；1：男；9或空：所有 | |
     *                      symptoms | 疾病相关症状 | list | 是 | 这疾病可能引起哪些症状 | | departments |
     *                      疾病就诊科室 | list | 是 |
     */

    public Map<String, Object> getSymptomFromSymptomcodeWithSex(String symptomcode, String sex) throws HospitalException {
        Map<String, Object> dataSymptomMap = getSymptomFromSymptomcode(symptomcode);
        if (dataSymptomMap == null) {
            return null;
        }

        /** 获得这个症状可能的疾病 */
        List<Map<String, Object>> sickness = (List<Map<String, Object>>) dataSymptomMap.get("sickness");
        // List<Map<String, Object>> dataSicknessList =
        // FilterSicknessWithSex(sickness, sex);

        Map<String, Object> result = new HashMap<String, Object>(dataSymptomMap);
        List<Map<String, Object>> resultSicknessList = new ArrayList<Map<String, Object>>();

        // 读取json文件，将 Department 和 sickness 进行转换
        for (Map<String, Object> dataSicknessMap1 : sickness) {
            String code = (String) dataSicknessMap1.get("code");
            Map<String, Object> sicknessMap = getSicknessFormCode(code);
            if (sicknessMap == null) {
                logger.error("未知的疾病代码:" + code);
                continue;
            }
            String dataSex = String.valueOf(sicknessMap.get("applyobject"));
            if (!(dataSex == null || dataSex.equals(sex) || dataSex.equals("9"))) {
                continue;
            }

            Map<String, Object> resultSicknessMap = new HashMap<String, Object>();

            resultSicknessMap.put("code", code);
            resultSicknessMap.put("name", sicknessMap.get("name"));
            resultSicknessMap.put("applyobject", dataSex);

            // 用性别过滤症状
            List<Map<String, Object>> resultSymptomsList = new ArrayList<Map<String, Object>>();
            List<Map<String, Object>> dataSymptomsList = (List<Map<String, Object>>) sicknessMap.get("symptoms");

            for (Map<String, Object> dataSymptomsMap : dataSymptomsList) {
                Map<String, Object> resultSymptomsMap = new HashMap<String, Object>();
                String dataSymptomsSex = String.valueOf(dataSymptomsMap.get("applyobject"));
                if (!(dataSymptomsSex == null || dataSymptomsSex.equals(sex) || dataSymptomsSex.equals("9"))) {
                    continue;
                }
                resultSymptomsMap.put("name", dataSymptomsMap.get("name"));
                resultSymptomsMap.put("code", dataSymptomsMap.get("code"));
                resultSymptomsList.add(resultSymptomsMap);
            }

            resultSicknessMap.put("symptoms", resultSymptomsList);

            // 将疾病列表中的疾病就诊科室进行转换
            List<Object> dataDepartmentsList = (List<Object>) sicknessMap.get("departments");
            List<Map<String, Object>> resultDepartmentsList = new ArrayList<Map<String, Object>>();

            for (Object dataDepartments : dataDepartmentsList) {
                Map<String, Object> resultDepartments = getDepartment((String) dataDepartments);
                resultDepartmentsList.add(resultDepartments);
            }

            resultSicknessMap.put("departments", resultDepartmentsList);
            resultSicknessList.add(resultSicknessMap);
        }

        result.put("sickness", resultSicknessList);
        return result;
    }

	/*
	 * 用性别去过滤疾病
	 */
    // private List<Map<String, Object>> FilterSicknessWithSex(List<Map<String,
    // Object>> dataSicknessList, String sex)
    // throws HospitalException {
    // List<Map<String, Object>> resultSicknessList = new ArrayList<>();
    //
    // for (Map<String, Object> dataMap : dataSicknessList) {
    // String code = (String) dataMap.get("code");
    //
    // Map<String, Object> sicknessMap = getSicknessFormCode(code);
    // if (sicknessMap == null) {
    // logger.error("未知的疾病代码:" + code);
    // continue;
    // }
    // String dataSex = String.valueOf(sicknessMap.get("applyobject"));
    // if (dataSex == null || dataSex.equals(sex) || dataSex.equals("9")) {
    // resultSicknessList.add(dataMap);
    // }
    // }
    // return resultSicknessList;
    // }

    /**
     * 通过 getDepartment 通过建议就诊科室获得医院真正对应的科室信息
     *
     * @param visitdepart 建议就诊科室
     */
    public Map<String, Object> getDepartment(String visitdepart) {
        if (departments_index.containsKey(visitdepart)) {
            int index = (int) departments_index.get(visitdepart);
            return departments.get(index);
        }
        return null;
    }

    /**
     * 通过疾病代码获取一条疾病的 map
     *
     * @param code
     */
    public Map<String, Object> getSicknessFormCode(String code) {
        if (sickness_index.containsKey(code)) {
            int index = sickness_index.get(code);
            return sickness.get(index);
        }
        return null;
    }

    /**
     * 根据一个集合的索引集合重构一个集合
     *
     * @param list
     * @param indexs
     * @return
     */
    public List<Map<String, Object>> getList(List<JSONObject> list, List<Integer> indexs) {
        if (list == null || indexs == null) {
            return null;
        }
        List<Map<String, Object>> result = new ArrayList<Map<String, Object>>();
        for (int index : indexs) {
            result.add(list.get(index));
        }
        return result;
    }

    public static void main(String[] args) throws HospitalException {
        IntelligentGuideData data = IntelligentGuideData.getIntelligentGuideData();
        Map<String, Object> map = data.getBodyPartByCodeFromPartcode("06");
        Map<String, Object> map2 = data.getSymptomClassificFromPartcode("06");

        System.out.println(map.get("partname"));
        System.out.println(map2.get("partname"));

        System.out.println("---------------");

        Map<String, Object> map3 = data.getSymptomFromSymptomcode("00001");
        System.out.println(map3);

        System.out.println("--------------------------");
        Map<String, Object> map4 = data.getDepartment("耳鼻喉科");
        System.out.println(map4);
        System.out.println("--------------------------");

        // 测试 getSymptomFromPartcodeClassific 该方法，当传入是一个一级的 partcode 时能否查找其子集
        // 当partcode为"01"时 实现后3803 实现前2290
        // 当partcode为"02"时 实现后103 实现前103
        // List<Map<String, Object>> list=getSymptomFromPartcodeClassific("01");
        // System.out.println(list.size());
    }
}
