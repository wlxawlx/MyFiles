function inspectList(s){if("99"==s.status)mInspectListNode.html(getNothingHtml(s.msg));else{var a=s.data.list,i="";$.each(a,function(s,a){if(i+='<div class="item item'+a.pdlsh+'" data-id="'+a.yylsh+'" id="'+a.pdlsh+'"><div class="detail01"><span class="zsmc">'+a.zsmc+'</span></div><div class="detail02"><span>病人姓名:</span><span class="brxm">'+a.brxm+'</span></div><div class="detail02"><span>预约时间:</span><span class="yysj">'+a.yysj+'</span></div><div class="detail02"><span>排队号码:</span><span class="pdhm">'+a.pdhm+'</span></div><div class="detail02"><span>检查名称:</span><span class="jcxm">'+a.jcxm+'</span></div><div class="detail02"><span>诊室名称:</span><span class="zsmc">'+($.isEmptyObject(a.zsmc)?"暂无诊室名称":a.zsmc)+'</span></div><div class="detail02"><span>诊室位置:</span><span><span class="zswz">'+($.isEmptyObject(a.zswz)?"暂无诊室位置":a.zswz)+'</span><span class="fjhm">'+a.fjhm+"</span></span></div></div>",mInspectListNode.append(i),0==a.pdzt)pdlsh=a.pdlsh,$.JDoAjax().isLoading(!1).add("BD010301",inspectWait,{pdlsh:a.pdlsh}).run();else if(-1==a.pdzt){var t=$('<div class="checkin link-red00">报到</div>');$(".item"+a.pdlsh).append(t),$(".checkin").on("click",function(){id=$(this).parent().attr("data-id"),pdlsh=$(this).parent().attr("id"),$.JDoAjax().isLoading(!1).add("BD010201",inspectRegist,{yylsh:id}).run()})}else if(1==a.pdzt){var d=$('<div class="oval">已检查</div>');$(".item"+a.pdlsh).append(d)}})}}function inspectRegist(s){if("99"==s.status)alert(s.msg);else{var a=s.data.state;"1"==a?(alert("报到成功"),$(".item"+pdlsh+" .checkin").remove(),$.JDoAjax().isLoading(!1).add("BD010301",inspectWait,{pdlsh:pdlsh}).run()):alert("后台程序异常")}}function inspectWait(s){var a=$('<div class="detail03"></div>');if($(".item"+pdlsh).append(a),"99"==s.status)a.html(s.msg);else{a.html('前面还有 <span class="num"></span> 人等待就诊'),mInspectWaitNode=$(".item"+pdlsh+" .num");var i=s.data.message,t=[];i=i.split("【"),t[1]=i[2].split("】")[0],mInspectWaitNode.html(t[1])}}var id,mInspectListNode=$(".line-cure"),mInspectWaitNode,pdlsh;