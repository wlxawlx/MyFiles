var myScroll;
var pullDownFlag,pullUpFlag;
var pullDown,pullUp;
var spinner;
function positionJudge(){
    if(this.y>40){    //判断下拉
        //pullDown.innerHTML = "放开刷新页面";
        //pullDownFlag = 1;
        return;
    }else if(this.y<(this.maxScrollY-40)){   //判断上拉
        pullUp.innerHTML = "放开刷新页面";
        pullUpFlag = 1;
    }
}
function action(){
    if(pullDownFlag==1){
        pullDownAction();
        pullDown.innerHTML = "下拉刷新…";
        pullDownFlag = 0;
    }else if(pullUpFlag==1){
        pullUpAction();
        pullUp.innerHTML = "上拉刷新…";
        pullUpFlag = 0;
    }
}
function loaded(){
    pullDownFlag = 0;
    pullUpFlag = 0;
    pullDown = document.getElementById("pullDown");
    pullUp = document.getElementById("pullUp");
    spinner = document.getElementById("spinner");
    myScroll = new IScroll("#wrapper",{
        probeType: 3,
//        momentum: false,//关闭惯性滑动
        mouseWheel: true,//鼠标滑轮开启
        scrollbars: true,//滚动条可见
        fadeScrollbars: true,//滚动条渐隐
        interactiveScrollbars: true,//滚动条可拖动
        shrinkScrollbars: 'scale', // 当滚动边界之外的滚动条是由少量的收缩
        useTransform: true,//CSS转化
        useTransition: true,//CSS过渡
        bounce: true,//反弹
        freeScroll: false,//只能在一个方向上滑动
        startX: 0,
        startY: 0,
//        snap: "li",//以 li 为单位
    });
    myScroll.on('scroll',positionJudge);
    myScroll.on("scrollEnd",action);
}
function pullDownAction(){
    return;
//    spinner.style.display = "block";
//    setTimeout(function(){
//        var ul = document.getElementById("list");
//        var lis = ul.getElementsByTagName("li");
//        for(var i=0;i<lis.length;i++){
//            lis[i].innerHTML = "下拉刷新";
//        }
//        spinner.style.display = "none";
//        myScroll.refresh();
//    },1000);
}
function pullUpAction(){
    //spinner.style.display = "block";
    //showLoading();
    
    setTimeout(function(){

        //dismissLoading();
        myScroll.refresh();
    },1000);
}
function updatePosition(){
    pullDown.innerHTML = this.y>>0;
}
document.addEventListener('touchmove', function (e) {
    e.preventDefault();
}, false);
document.addEventListener('touchmove', function (e) {
    e.preventDefault();
}, false);