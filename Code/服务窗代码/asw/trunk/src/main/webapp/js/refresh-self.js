var _start = 0,
    _end = 0;

var tragt = false;

//第一步：下拉过程执行(touchmove)
function slideDownStep1(dist) {  // dist 下滑的距离，用以拉长背景模拟拉伸效果
    var slideDown1 = document.getElementById("slideDown1"),
        slideDown2 = document.getElementById("slideDown2");
    slideDown2.style.display = "none";
    slideDown1.style.display = "block";
    console.log(dist);
    dist = dist * 0.5;
    if (parseInt(dist) > -70) {
        slideDown1.style.height = (parseInt("20px") - dist) + "px";
    } else if (dist < -50) {
        slideDown1.style.height = 90 + "px";
        $('#slideDown1 i').css('transform', 'rotate(180deg)')
        $('#slideDown1 span').text('松开刷新')
    } else if (dist < 0 && dist > -15) {
        return;
    }

}
//第二步：下拉松手执行(touchend)
function slideDownStep2() {
    var slideDown1 = document.getElementById("slideDown1"),
        slideDown2 = document.getElementById("slideDown2");
    slideDown1.style.display = "none";
    slideDown1.style.height = "20px";
    slideDown2.style.display = "block";
    //刷新数据
    //location.reload();
}
//第三步：刷新完成，回归之前状态
function slideDownStep3() {
    var slideDown1 = document.getElementById("slideDown1"),
        slideDown2 = document.getElementById("slideDown2");
    slideDown1.style.display = "none";
    slideDown2.style.display = "none";
    $('#slideDown1 i').css('transform', 'rotate(0deg)')
    $('#slideDown1 span').text('下拉加载')
    _end = 0;
}

// 上拉刷新过程
// 上拉过程执行(touchmove)
function slideUpStep1() {
    $('#slideUp').removeClass('hide');
    tragt = true;
    setTimeout(function () {
        if (tragt) {
            doLoading();
            tragt = false;
        } else
            console.log('不用执行');
    }, 5000);
}
// 上拉松手执行(touchend)
function slideUpStep2() {
    $('#slideUp p').html('<i class=\"fa fa-spinner fa-spin\"></i><span>正在刷新</span>');
    console.log('执行了');
    doLoading();
}
// 刷新完成
function slideUpStep3() {
    $('#slideUp').addClass('hide');
    tragt = false;
}

//下滑刷新调用
k_touch("content", "y");
//contentId表示对其进行事件绑定，way==>x表示水平方向的操作，y表示竖直方向的操作
function k_touch(contentId, way) {
    console.log(contentId);
    var _content = document.getElementById(contentId);
    console.log(_content);
    _content.addEventListener("touchstart", touchStart, false);
    _content.addEventListener("touchmove", touchMove, false);
    _content.addEventListener("touchend", touchEnd, false);
    function touchStart(event) {

        var touch = event.targetTouches[0];
        if (way == "x") {
            _start = touch.pageX;
        } else {
            _start = touch.pageY;
        }
    }

    function touchMove(event) {
        var touch = event.targetTouches[0];
        if (way == "x") {
            _end = (_start - touch.pageX);
        } else {
            _end = (_start - touch.pageY);
            if (_end < 0) {  //下拉过程执行
                if ($('#slideDown').length) {
                    slideDownStep1(_end);
                }

            } else if (_end > 0) {  //上拉过程执行
                if ($('#slideUp').length) {
                    slideUpStep1();
                }
            }

        }

    }

    function touchEnd(event) {
        if (_end > 0) {
            if ($('#slideDown').length) {
                var slideDown1 = document.getElementById("slideDown1"),
                    slideDown2 = document.getElementById("slideDown2");
                slideDown1.style.display = "none";
                slideDown2.style.display = "none";
            }
            if ($('#slideUp').length) {  //上拉松手开始刷新
                slideUpStep2();
                setTimeout(function () {  //上拉刷新完执行(延时模拟加载数据完成)
                    slideUpStep3();
                }, 2500);
            }

        } else if (_end < 0) {  //下拉松手开始刷新
            if ($('#slideDown').length) {
                slideDownStep2();
                setTimeout(function () {  //下拉刷新完执行
                    slideDownStep3();
                }, 2500);
            }
        }
    }
}
