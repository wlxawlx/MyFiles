package com.jandar.cloud.hospital.dao;

import com.jandar.cloud.hospital.bean.CheckList;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

/**
 * 云医院预约的检查单信息,包含基本信息,包含状态
 * Created by zzw on 16/8/30.
 */
@Repository("CheckListDao")
public interface CheckListDao extends MongoRepository<CheckList, ObjectId> {

    /** 以下方式写的函数可以直接使用
     * 格式为“findBy+字段名+方法后缀”
     * */
    /**
     * 有@Query声明查询, 方法名不需要严格遵守规定
     * @param name
     * @param ageFrom
     * @param ageTo
     * @param pageable
     * @return
     */
//    @Query("{'name':{'$regex':?0}, 'age': {'$gte':?1,'$lte':?2}}")
//    public Page<User> findByNameAndAgeRange(String name, int ageFrom, int ageTo, Pageable pageable);

    /**
     GreaterThan(大于)
     findByAgeGreaterThan(int age)
     {"age" : {"$gt" : age}}

     LessThan（小于）
     findByAgeLessThan(int age)
     {"age" : {"$lt" : age}}

     Between（在...之间）
     findByAgeBetween(int from, int to)
     {"age" : {"$gt" : from, "$lt" : to}}

     IsNotNull, NotNull（是否非空）
     findByFirstnameNotNull()
     {"age" : {"$ne" : null}}

     IsNull, Null（是否为空）
     findByFirstnameNull()
     {"age" : null}

     Like（模糊查询）
     findByFirstnameLike(String name)
     {"age" : age} ( age as regex)

     (No keyword) findByFirstname(String name)
     {"age" : name}

     Not（不包含）
     findByFirstnameNot(String name)
     {"age" : {"$ne" : name}}

     Near（查询地理位置相近的）
     findByLocationNear(Point point)
     {"location" : {"$near" : [x,y]}}

     Within（在地理位置范围内的）
     findByLocationWithin(Circle circle)
     {"location" : {"$within" : {"$center" : [ [x, y], distance]}}}

     Within（在地理位置范围内的）
     findByLocationWithin(Box box)
     {"location" : {"$within" : {"$box" : [ [x1, y1], x2, y2]}}}
     */
}
