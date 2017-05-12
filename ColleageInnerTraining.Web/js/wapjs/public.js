$(function () {

    $(".lesson_tab .lesson_a").click(function () {
        $(".lesson-bg,.lesson-cc").height($(document).height() - 90);
        if ($(this).next("em").attr("date-em") == "date-em") {
            $(this).parent().removeClass("current");
            $(this).next("em").attr("date-em", "");
            $(".lesson-bg").hide();

        } else {
            $(this).parent().addClass("current");
            $(this).next("em").attr("date-em", "date-em");
            $(this).parent().siblings(".lesson_tab").removeClass("current");
            $(this).parent().siblings(".lesson_tab").find("em").attr("date-em", "");
            $(".lesson-bg").show();
        }
    });

    $(".lesson-bg,.lesson-cc").click(function () {
        $(".lesson_tab").removeClass("current");
        $(".lesson_tab").find("em").attr("date-em", "");
        $(".lesson-bg").hide();
    });
    var objtype = null;

    $(".lesson_screen_top a").click(function () {
        $(objtype).removeClass("cur");
        $(this).addClass("cur");
        objtype = this;
      
    });
    var objsort = $(".lesson_classily a.cur");
    $(".lesson_classily a").click(function () {
        if (objsort!=null)
        $(objsort).removeClass("cur");
        $(this).addClass("cur");
        objsort = this;
    });

    var categoryli = $(".lesson_category_ul li.cur");
    $(".lesson_category_ul li").click(function () {
        if (categoryli != null)
            $(categoryli).removeClass("cur");
        $(this).addClass("cur");
        categoryli = this;

    });


    var categorya = $(".lesson_classily_con li a.cur");
    $(".lesson_classily_con li a").click(function () {
        if (categorya != null)
            $(categorya).removeClass("cur");
        $(this).addClass("cur");
        categorya = this;

    });



    

    $(".lesson_screen_bottom a.fl").click(function () {
        $(".lesson_screen_top a").removeClass("cur")
    });

    $(".lesson_category_ul li").click(function () {
        var $li = $('.lesson_category_ul li');
        var $ul = $('.lesson_classily_con');
        var $this = $(this);
        var $t = $this.index();
        $li.removeClass("cur");
        $this.addClass('cur');
        $ul.css('display', 'none');
        $ul.eq($t).css('display', 'block');
    });

    $(".lalala .teacher_btn").click(function () {
        var $li = $('.lalala .teacher_btn');
        var $ul = $('.teacher_intro .comment_con');
        var $this = $(this);
        var $t = $this.index();
        $li.removeClass("cur");
        $this.addClass('cur');
        $ul.css('display', 'none');
        $ul.eq($t).css('display', 'block');
    });

    $(".tab_nav span").click(function () {
        var $li = $('.tab_nav span');
        var $ul = $('.interactive');
        var $this = $(this);
        var $t = $this.index();
        $li.removeClass("cur");
        $this.addClass('cur');
        $ul.css('display', 'none');
        $ul.eq($t).css('display', 'block');
    });

    $(".exam_nav span").click(function () {
        var $li = $('.exam_nav span');
        var $ul = $('.exam_con');
        var $this = $(this);
        var $t = $this.index();
        $li.removeClass("cur");
        $this.addClass('cur');
        $ul.css('display', 'none');
        $ul.eq($t).css('display', 'block');
    });

    $(".lesson_select").click(function () {
        $(this).find("ul").slideToggle(500);
    });

    if ($('#banner_slider').length > 0) {
        $('#banner_slider').flexslider({
            animation: 'slide',
            slideDirection: "vertical",
            slideshowSpeed: 3000,
            slideshow: true,
            animationLoop: true
        });
    }

    //exam-footer
    $(".exam_tab").click(function () {
        $(".exam-con,.exam-opbg").removeClass("hide");
        $(".exam_tab span").click(function () {
            var $li = $('.exam_tab span');
            var $ul = $('.exam-con .exam_tnum');
            var $this = $(this);
            var $t = $this.index();
            $li.removeClass("cur");
            $this.addClass('cur');
            $ul.addClass('hide');
            $ul.eq($t).removeClass("hide");
        });

    });

    $(".exam-opbg,.exam_tnum a").click(function () {
        $(".exam-con,.exam-opbg").addClass("hide");
    });



});
