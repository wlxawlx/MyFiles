
$(function() {

    $(".ui-searchbar").on('click', function() {
        $('.ui-searchbar-wrap').addClass('focus');
        $('.ui-searchbar-input input').focus();
    });
    $(".ui-searchbar-cancel").on('click',function() {
        $('.ui-searchbar-wrap').removeClass('focus');
    });


    if ($("div.self-main").is(".hide")) {
      $(".data-null").append("<img class='center-pic' src='./assets/images/ts_14.png' alt='pic' >");
      $(".data-null").append("<span>当前还没有信息！</span>");
    }

});
