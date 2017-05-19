/**
 * Created by liangyining on 3/9/16. 协议号BD
 */
/**
 * 变量定义
 */
var id;
var mInspectListNode = $('.line-cure');//预约列表node
var mInspectWaitNode;//检查候诊候诊信息node
var pdlsh;


//检查预约列表BD010101
/*
 检查预约列表显示的内容如下:
 | brxm  | 病人姓名  |
 | yysj  | 预约时间  |
 | pdlsh | 排队序号  |
 | jcxm  | 检查名称  |
 | zsmc  | 诊室名称  |
 | zswz  | 诊室位置  |
 | fjhm  | 房间号码  |
 |  zysx | 注意事项  |
 */
function inspectList(returnData) {
    if (returnData.status == '99') {
        mInspectListNode.html(getNothingHtml(returnData.msg));
    } else {
        var inspectList = returnData.data.list;
        var inspectHtml = '';
        $.each(inspectList, function (index, info) {
            //TODO 添加html节点到inspectHtml
            inspectHtml += '<div class=\"item item' + info.pdlsh + '\" data-id=\"' + info.yylsh + '\" id=\"' + info.pdlsh + '\">' +
                '<div class=\"detail01\"><span class=\"zsmc\">' + info.zsmc + '</span></div>' +
                '<div class=\"detail02\"><span>病人姓名:</span><span class=\"brxm\">' + info.brxm + '</span></div>' +
                '<div class=\"detail02\"><span>预约时间:</span><span class=\"yysj\">' + info.yysj + '</span></div>' +
                '<div class=\"detail02\"><span>排队号码:</span><span class=\"pdhm\">' + info.pdhm + '</span></div>' +
                '<div class=\"detail02\"><span>检查名称:</span><span class=\"jcxm\">' + info.jcxm + '</span></div>' +
                '<div class=\"detail02\"><span>诊室名称:</span><span class=\"zsmc\">' + ($.isEmptyObject(info.zsmc) ? '暂无诊室名称' : info.zsmc) + '</span></div>' +
                '<div class=\"detail02\"><span>诊室位置:</span><span><span class=\"zswz\">' + ($.isEmptyObject(info.zswz) ? '暂无诊室位置' : info.zswz) + '</span><span class=\"fjhm\">' + info.fjhm + '</span></span></div>' +
                '</div>';
            mInspectListNode.append(inspectHtml);

            if (info.pdzt == 0) {
                pdlsh = info.pdlsh;

                $.JDoAjax().isLoading(false).add('BD010301', inspectWait,{
                    'pdlsh': info.pdlsh
                }).run();
            } else if (info.pdzt == -1) {
                //TODO 未报到的UI界面显示

                var checkin = $('<div class="checkin link-red00">报到</div>');
                $('.item' + info.pdlsh).append(checkin);

                $('.checkin').on('click', function () {
                    id = $(this).parent().attr('data-id');
                    pdlsh = $(this).parent().attr('id');
                    $.JDoAjax().isLoading(false).add('BD010201', inspectRegist,{
                        'yylsh': id
                    }).run();
                })
            } else if (info.pdzt == 1) {//已就诊
                //TODO 已经检查的界面显示
                var oval = $('<div class="oval">已检查</div>');
                $('.item' + info.pdlsh).append(oval);
            }
        });

    }
}

//检查预约报到（BD010201）
function inspectRegist(returnData) {
    if (returnData.status == '99') {
        alert(returnData.msg);
    } else {
        var state = returnData.data.state;
        if (state == '1') {
            alert('报到成功');
            //TODO 检查预约成功后相关的界面变化操作

            $('.item' + pdlsh + ' .checkin').remove();

            $.JDoAjax().isLoading(false).add('BD010301', inspectWait,{
                'pdlsh': pdlsh
            }).run();
        } else {
            alert('后台程序异常')
        }
    }
}

//候诊信息（BD010301）
function inspectWait(returnData) {
    var detail03 = $('<div class="detail03"></div>');
    $('.item' + pdlsh).append(detail03);

    if (returnData.status == '99') {
        detail03.html(returnData.msg);
    } else {
        detail03.html('前面还有 <span class="num"></span> 人等待就诊');

        mInspectWaitNode = $('.item' + pdlsh + ' .num');
        var inspectMessage = returnData.data.message;
        //TODO 获取检查候诊信息后处理message
        var chars = [];
        inspectMessage = inspectMessage.split('【');
        chars[1] = inspectMessage[2].split('】')[0];
        mInspectWaitNode.html(chars[1]);
    }
}
