//设置测试的Ajax URL地址
function setTestURL(url, pcode) {
    if (typeof jQuery == "undefined") {
        return url;
    } else {
        return pcode;
    }
}

var loadingDailog;
var localURL = '/';
var serviceURL = '/alipayservice';
var directory = '/wzsrmyy';
var logoURL = '/image' + directory + '/logo.jpg';
var error = function () {
    console.log("error")
};

var checkout = {
    phone: function (phone) {
        var r2 = /^(13[0-9]|14[0-9]|15[0-9]|18[0-9])\d{8}$/;
        return !!(phone == "" || r2.test(phone));
    },
    idcardno: function (code) {
        //新建普通实例
        var Validator = new IDValidator();
        //或使用带地址码实例,需要引入GB2260
        var Validator = new IDValidator(GB2260);
        return Validator.isValid(code);
    }
};


//集成DoLoadingAsync方法
(function ($) {
    function DoLoadingAsync() {
        this.loadingState = true;
        this.pcodes = [];
        this.sendDatas = [];
        this.successFunctions = [];
        this.errorFunctions = [];
        this.isLoading = function (loadingState) {
            this.loadingState = loadingState;
            return this;
        };
        this.add = function (pcode, success, sendData, error) {
            this.pcodes.push(pcode);
            if ($.isEmptyObject(sendData))
                this.sendDatas.push('');
            else
                this.sendDatas.push(JSON.stringify(sendData));
            this.successFunctions.push(success);
            if (!$.isEmptyObject(error)) {
                this.errorFunctions.push(error);
            } else {
                this.errorFunctions.push(null);
            }
            return this;
        };
        this.run = function () {
            var insidePcodes = this.pcodes;
            var insideSendDatas = this.sendDatas;
            var insideSuccessFunction = this.successFunctions;
            var insideErrorFunction = this.errorFunctions;
            var insideLoadingState = this.loadingState;
            if (this.loadingState && $.isEmptyObject(loadingDailog))
                showLoading();
            $.Deferred(
                function (dtd) {
                    var ajaxIndex = 0;
                    $.each(insidePcodes, function (index, value) {
                        var data = {
                            'pcode': value,
                            'content': insideSendDatas[index]
                        };
                        var ajaxDtd;
                        if (typeof jQuery == "undefined") {
                            ajaxDtd = $.post(serviceURL, data, function () {
                            }, 'json');
                        } else {
                            ajaxDtd = jQuery.post(value, data, function () {
                            }, 'json');
                        }
                        ajaxDtd.done(function (returnData) {
                            console.log('协议号:' + value);
                            console.log('发送报文内容:' + insideSendDatas[index]);
                            if (typeof returnData == 'string')
                                returnData = JSON.parse(returnData);
                            ajaxReturnSuccess(returnData, insideSuccessFunction[index])
                        });
                        ajaxDtd.fail(function () {
                            console.log('协议号:' + value);
                            if ($.isEmptyObject(insideErrorFunction[index]))
                                error();
                            else
                                insideErrorFunction[index]();
                        });
                        ajaxDtd.always(function () {
                            ajaxIndex++;
                            if (ajaxIndex === insidePcodes.length) {
                                dtd.resolve();
                            }
                        });
                    });
                    return dtd;
                }
            ).then(function () {
                if (insideLoadingState)
                    dismissLoading();
            });
        };
    }

    $.JDoAjax = function () {
        return new DoLoadingAsync();
    }
})(Zepto);

/**
 * 获取url中键值对传递的值
 * @param name 键
 */
function getURLData(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(decodeURIComponent(r[2]));
}

function locationURL(pageURL, childDir) {
    var dir = '';
    if (childDir) dir = childDir + '/';
    return directory + '/' + dir + pageURL;
}

/**
 * 获取内容为空的html代码
 * @param msg 展示的信息内容
 * @returns {string} html代码
 */
function getNothingHtml(msg) {
    return '<div class="system-hint nothing"><span><i class="fa fa-minus-circle fa-8x"></i></span><div class="msg"> ' + msg + '</div></div>';
}

/**
 * 获取医生头像url
 * @param doctorid 医生id
 * @returns {string} 医生头像url
 */
function getDoctorImageUrl(doctorid) {
    return '/PIC/' + doctorid + '.jpg';
}

/**
 * 获取中文的缴费状态
 * @param type 状态码
 * @returns {*} 中文
 */
function getPayState(type) {
    switch (type) {
        case '1':
            return '缴费成功';
        case '0':
            return '缴费失败';
        case '-1':
            return '取消缴费';
        default:
            return '';
    }
}

/**
 * ajax返回成功的处理方法
 * @param returnData
 * @param success
 */
function ajaxReturnSuccess(returnData, success) {
    console.log('返回报文如下:');
    console.log(returnData);
    if (returnData.status == '98') {
        sessionStorage.creatPatient = 1;
        location.href = locationURL('patientRegister.html');
    } else if (returnData.status == '03') {
        //重新加载一次，如果没刷新返回404页面
        var value = sessionStorage.firstReload;
        console.log(value == true);
        if ($.isEmptyObject(value) || value == true) {
            window.location.reload(true);
        } else {
            self.location.replace(localURL + '404&building/loadingFail.html');
        }
        sessionStorage.firstReload = false;
    } else if (returnData.status == '97') {
        location.replace(localURL + '404&building/loadingFail.html');
    } else {
        if (returnData.status == '02') {
            // 兼容处理
            returnData.status = '99';
        }
        success(returnData);
    }
}

function SwitchTab(flag) {
    $('#icon').on('click', function () {
        flag = !flag;
        switchStatu(flag)
    });

    $('#menu span').on('click', function () {
        $('#menu span').removeClass('cur');
        $(this).addClass('cur');

        flag = !flag;
        switchStatu(flag);
        $('#headhint').text($(this).text());
    });

}

function switchStatu(flag) {
    switch (flag) {
        case false:
            $('#menu').fadeOut();
            $('#more .icon i').removeClass('rotate00');
            $('#more .icon i').addClass('rotate01');
            break;
        case true:
            $('#menu').removeClass('hide');
            $("#menu").fadeIn();
            $('#more .icon i').removeClass('rotate01');
            $('#more .icon i').addClass('rotate00');
            break;
        default:
            break;
    }
}
/**
 * 判断输入是否满足金额格式
 **/
function judgMoney(value, max) {
    var exp = /^(-)?(([1-9]{1}\d*)|([0]{1}))(\.(\d){0,2})?$/;
    if (parseInt(value) > max) {
        swal('不支持' + max + '元以上金额的缴费');
        return false;
    } else {
        if (exp.exec(value)) {
            return true;
        } else {
            swal('请输入正确的金额');
            return false;
        }
    }
}

$('.ui-searchbar-cancel').on('click', function () {
    $(this).parent().find('input').val('');
});

/**
 * 关闭当前窗口,貌似没有什么作用
 */
function closeWP() {
    var Browser = navigator.appName;
    var indexB = Browser.indexOf('Explorer');

    if (indexB > 0) {
        var indexV = navigator.userAgent.indexOf('MSIE') + 5;
        var Version = navigator.userAgent.substring(indexV, indexV + 1);

        if (Version >= 7) {
            window.open('', '_self', '');
            window.close();
        }
        else if (Version == 6) {
            window.opener = null;
            window.close();
        }
        else {
            window.opener = '';
            window.close();
        }

    }
    else {
        window.close();
    }
}

function showLoading() {
    if (typeof ($.loading) == 'function') {
        loadingDailog = $.loading({content: '加载中...'});
    }
}

function dismissLoading() {
    if (!$.isEmptyObject(loadingDailog)) {
        loadingDailog.loading('hide');
        loadingDailog = null;
    }
}
//自动加载数据
function loadingData(namespace, dataSelf) {
    if (!$.isEmptyObject(dataSelf)) {
        $.each(dataSelf, function (key, value) {
            $('.' + namespace + '-' + key).append(value);
        })
    } else {
        return false;
    }
}


//初始化一些主界面的隐藏工作
$(function () {
    $('.test').addClass('hide');
    $('.test-html').html('');
    $('.prep-data').addClass('hide');

    /**
     * 清空input
     **/
    $('.ui-icon-close').on('click', function () {
        $(this).parent().find('input').val('');
    });
});

//切换选择联系人
function switchStatu00(flag) {
    switch (flag) {
        case false:
            $('#patient-choose').removeClass('fade-in');
            $('#patient-choose').addClass('fade-out');
            setTimeout(function () {  //下拉刷新完执行
                $('#patient-choose').addClass('hide');
            }, 900);

            break;
        case true:
            $('#patient-choose').removeClass('hide');
            $('#patient-choose').removeClass('fade-out');
            $('#patient-choose').addClass('fade-in');
            break;
        default:
            break;
    }
}
//日期格式
Date.prototype.Format = function (fmt) { //author: meizz
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours(), //小时
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};

