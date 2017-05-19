(function() {
  var myScroll,
    pullDownEl, pullDownOffset,
    pullUpEl, pullUpOffset,
    generatedCount = 0;

  function pullDownAction() {
    setTimeout(function() {
      var el, li, i;
      el = document.getElementById('thelist');


      myScroll.refresh();
      console.log(myScroll);
    }, 1000);
  }

  function pullUpAction() {

    setTimeout(function(){
      pullUpEl.className = 'hide'
      // console.log(pullUpEl);
      // pullUpEl.addClass('hide')
      // $('#pullUp').addClass("hide")

    },900);
    setTimeout(function() {
      var el, li, i;
      el = document.getElementById('thelist');
      myScroll.refresh();
    }, 1000);
  }

  function loaded() {
    pullDownEl = $('#pullDown')[0];
    var  pullDownEl1 = $('#pullDown');
    console.log(pullDownEl1);
    pullDownOffset = pullDownEl.offsetHeight;
    pullUpEl = $('#pullUp')[0];
    pullUpOffset = pullUpEl.offsetHeight;

    myScroll = new iScroll('wrapper', {
      useTransition: true,
      topOffset: pullDownOffset,
      onRefresh: function() {
        if (pullDownEl.className.match('loading')) {
          pullDownEl.className = 'hide';
          pullDownEl.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
        } else if (pullUpEl.className.match('loading')) {
          pullUpEl.className = '';
          pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
        }
      },
      onScrollMove: function() {
        if (this.y > 5 && !pullDownEl.className.match('flip')) {
          // pullDownEl.className = 'flip';
          pullDownEl.className = 'hide';
          pullDownEl.querySelector('.pullDownLabel').innerHTML = '松开刷新...';
          this.minScrollY = 0;
        } else if (this.y < 5 && pullDownEl.className.match('flip')) {
          pullDownEl.className = 'hide';
          pullDownEl.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
          this.minScrollY = -pullDownOffset;
        } else if (this.y < (this.maxScrollY - 5) && !pullUpEl.className.match('flip')) {
          pullUpEl.className = 'flip';
          pullUpEl.querySelector('.pullUpLabel').innerHTML = '松开刷新...';
          this.maxScrollY = this.maxScrollY;
        } else if (this.y > (this.maxScrollY + 5) && pullUpEl.className.match('flip')) {
          pullUpEl.className = '';
          pullUpEl.querySelector('.pullUpLabel').innerHTML = '下拉加载更多...';
          this.maxScrollY = pullUpOffset;
        }
      },
      onScrollEnd: function() {
        if (pullDownEl.className.match('flip')) {
          // pullDownEl.className = 'loading';
          pullDownEl.className = 'hide';
          pullDownEl.querySelector('.pullDownLabel').innerHTML = 'Loading...';
          pullDownAction();
        } else if (pullUpEl.className.match('flip')) {
          pullUpEl.className = 'loading';
          pullUpEl.querySelector('.pullUpLabel').innerHTML = 'Loading...';

          pullUpAction();
        }
      }
    });

    setTimeout(function() {
      document.getElementById('wrapper').style.left = '0';
    }, 800);
  }
  document.addEventListener('touchmove', function(e) {
    e.preventDefault();
  }, false);

  document.addEventListener('DOMContentLoaded', function() {
    setTimeout(loaded, 200);
  }, false);

  function allowFormsInIscroll() {
    [].slice.call(document.querySelectorAll('input, select, button')).forEach(function(el) {
      el.addEventListener(('ontouchstart' in window) ? 'touchstart' : 'mousedown', function(e) {
        e.stopPropagation();

      })
    })
  }
  document.addEventListener('DOMContentLoaded', allowFormsInIscroll, false)
})();
