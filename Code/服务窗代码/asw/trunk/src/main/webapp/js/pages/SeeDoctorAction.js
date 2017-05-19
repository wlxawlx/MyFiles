/**
 * Created by liangyining on 3/9/16. 就诊信息 JZ
 */

/**
 * 参量定义
 */
var mSeeDoctorListNode = $('.see-doctor-list');//就诊列表节点
var mSeeDoctorItemNode = $();//就诊单个节点
var mDiagnoseDetailNode = $('.see-diagnose-detail');//就诊信息表单节点
var mTakeDrugNode = $('.see-guide');//就诊信息表单节点


/**
 * 就诊信息列表（JZ010101）
 */
/*
 展示内容如下:
 | jzrq | 就诊日期 
 | ksmc | 科室名称 
 | ysxm | 医生姓名 
 | zdxx | 诊断信息 
 */
function SeeDoctorList(returnData) {
    if (returnData.status == '99') {
        mSeeDoctorListNode.html(getNothingHtml(returnData.msg));
    } else {
        var seeDoctorData = returnData.data.list;
        mSeeDoctorListNode.html('');
        $.each(seeDoctorData, function (index, item) {
            //TODO 单个就诊信息的HTML结构
            var diagnoseItemHtml = '<div class="card-white-grey00 infoItem" data-id="' + item.jzxh + '">' +
                '<div class="detail00"><span>' + item.ksmc + '</span></div>' +
                '<div class="detail01"><span>诊断信息:</span><span>' + item.zdxx + '</span></div>' +
                '<div class="detail01"><span>医生姓名:</span><span>' + item.ysxm + '</span></div>' +
                '<div class="detail01"><span>就诊日期:</span><span>' + item.jzrq + '</span></div>' +
                '<div class="detail02"><span class="link-red00 guide">指引单</span></div>' +
                '</div>';
            mSeeDoctorListNode.append(diagnoseItemHtml);
        });
        mSeeDoctorItemNode = $('.infoItem');
        mSeeDoctorItemNode.on('click', function () {
            sessionStorage.jzxh = $(this).attr('data-id');
            //TODO 跳转到就诊详细页面
            location.href = 'seeDiagnoseDetail.html'
        });

        $('.guide').on('click', function (e) {
            e.stopPropagation();
            sessionStorage.jzxh = $(this).parent().parent().attr('data-id');
            location.href = 'seeGuide.html'
        })
    }
}

/**
 * 病历信息（JZ010201）
 */
/*
 主要显示内容如下:
 | xbs  | 现病史  
 | jws  | 既往史  
 | grs  | 个人史  
 | gms  | 过敏史  
 | hys  | 婚育史  
 | jzs  | 家族史  
 | tgjc | 体格检查 
 | fzjc | 辅助检查 
 | clyj | 处理意见 
 */
function DiagnoseDetail(returnData) {
    if (returnData.status == '99') {
        swal({
            title: returnData.msg,
            type: 'error',
            confirmButtonText: '返回'
        }, function () {
            history.back();
        });
    } else {
        var diagnoseDetailData = returnData.data;
        //TODO 组织病历数据然后填充数据
        loadingData('diagnose', diagnoseDetailData);
    }
}

/**
 * 指引单（取药）（JZ010301）
 */
/*
 指引单需要显示的信息项:
 | kfrq | 开方日期   
 | ksmc | 开方科室名称 
 | zynr | 指引内容   
 | zywz | 指引位置   
 | ysxm | 医生姓名   
 | zjje | 总计金额   
 */
function TakeDrug(returnData) {
    if (returnData.status == '99') {
        swal({
            title: returnData.msg,
            type: 'error',
            confirmButtonText: '返回'
        }, function () {
            history.back();
        });
    } else {
        var takeDrugData = returnData.data;
        takeDrugData.zjje = '¥' + takeDrugData.zjje;
        //TODO 组织取药数据填充HTML文件
        loadingData('takedurg', takeDrugData);
    }
}
