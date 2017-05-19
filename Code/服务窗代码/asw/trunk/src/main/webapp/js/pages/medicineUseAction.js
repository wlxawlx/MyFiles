/**
 * Created by liangyining on 3/9/16. 服药信息 FY
 */

/**
 * 变量
 */
var mDiagnoseListNode = $('.medicine-record');//诊断列表节点
var mDiagnoseItemNode = $();//诊断单个节点
var mMedicineListNode = $('.medicine-detail');//药品列表节点
var mMedicineItemNode = $();//药品单个节点

/**
 * 信息列表（FY010101）
 * @param returnData
 */
/*
 网页中需要展示的信息如下:
 | brxm  | 病人姓名   |
 | ksmc  | 科室名称   |
 | ysxm  | 医生姓名   |
 | kfrq  | 开方日期   |
 | zjje  | 总计金额   |
 | lxmc  | 处方类型名称 |
 */
function diagnoseInfoList(returnData) {
    if (returnData.status == '99') {
        mDiagnoseListNode.html(getNothingHtml(returnData.msg));
    } else {
        var diagnoseData = returnData.data.list;
        if(diagnoseData.length==0){
            mDiagnoseListNode.html(getNothingHtml('暂无数据'));
            return;
        }
        mDiagnoseListNode.html('');
        $.each(diagnoseData, function (index, diagnose) {
            //TODO 诊断信息HTML内容
            var diagnoseItemHtml = '<div class="diagnoseItem card-white-grey00" data-id="' + diagnose.cflsh + '">' +
                '<div class="detail00"><span>' + diagnose.ksmc + '</span><span class="float-r">' + diagnose.ysxm + '</span></div>' +
                '<div class="detail01"><span>处方类型:</span><span>' + diagnose.lxmc + '</span></div>' +
                '<div class="detail01 zjje"><span>总计金额:</span><span class="red">¥<span class="sum">' + diagnose.zjje + '</span></span></div>' +
                '<div class="detail02">' + diagnose.kfrq + '</div>' +
                '</div>';
            mDiagnoseListNode.append(diagnoseItemHtml);
        });

        if($.isEmptyObject(diagnoseData[0].zjje)){
            $('.diagnoseItem').find('.zjje').css('display','none');
        }
        mDiagnoseItemNode = $('.diagnoseItem');
        mDiagnoseItemNode.on('click', function () {
            sessionStorage.diagnoseId = $(this).attr('data-id');
            //TODO 跳转到药品html文件
            location.href = 'medicineDetail.html';
        })
    }
}

/**
 * 药品服用信息（FY010201）
 * @param returnData
 */
function medicineInfoList(returnData) {
    if (returnData.status == '99') {
        mMedicineListNode.html(getNothingHtml(returnData.msg));
    } else {
        var medicineData = returnData.data;
        mMedicineListNode.html('');
        $.each(medicineData, function (index, medicine) {
            //TODO 药品信息HTML内容
            var medicineItemHtml = '<div class="medicineItem card-white-grey00">' +
                '<div class="detail00 nowrap"><span>' + medicine.ypmc + '</span></div>' +
                '<div class="detail01"><span>药品信息:</span><span><span>' + medicine.ypgg + '</span>*<span>' + medicine.ypsl + '</span></span></div>' +
                '<div class="detail01"><span>使用频次:</span><span>' + medicine.sync + '</span></div>' +
                '<div class="detail01"><span>给药途径:</span><span>' + medicine.gytj + '</span></div>' +
                '<div class="detail01"><span class="letter-2" style="width: 96px;display: inline-block">用量</span>:<span>' + medicine.ycyl + '</span></div>' +
                '<div class="detail01"><span class="justify" style="width: 65px;display: inline-block"></span><span>' + medicine.ysjy + '</span></div>' +
                '</div>';
            mMedicineListNode.append(medicineItemHtml);
        })
    }
}
