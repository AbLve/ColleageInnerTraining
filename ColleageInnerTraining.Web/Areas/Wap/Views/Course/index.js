$(function () {
    var params = {
        Path: 0,//考试id
        Sort: "ReadTimes",//当前用户id
        Type: 0
    };
    var counter = 0;
    // 每页展示4个
    var num = 4;
    var pageStart = 0, pageEnd = 0;

    //课程分类导航
    $(document).on('click', '.categoryNav', function () {
        var Path = $(this).attr('name');
        params.Path = Path;
        params.Type = 0;
        $('.lists').html('');
        $('.content').html('<div class="lists"></div>');
        //$('.content').css("height","500px")
        counter = 0;
        pageStart = 0;
        pageEnd = 0;
        init();
    });
    //课程分类导航
    $(document).on('click', '.typeNav', function () {
        var type = $(this).attr('name');
        params.Type = type;
        $('.lists').html('');
        $('.content').html('<div class="lists"></div>');

        params.Path = 0;
        counter = 0;
        pageStart = 0;
        pageEnd = 0;
        init();
    });
    //课程排序导航
    $(document).on('click', '.sortNav', function () {
        var Sort = $(this).attr('name');
        params.Sort = Sort;
        $('.lists').html('');
        $('.content').html('<div class="lists"></div>');

        counter = 0;
        pageStart = 0;
        pageEnd = 0;
        init();
    });
    init(); 
    function init() {

        // dropload
        dropload = $('.content').dropload({
            scrollArea: window,
            loadDownFn: function (me) {
                $.ajax({
                    type: 'GET',
                    url: abp.appPath + 'Wap/Course/GetCourseList',
                    data: { Path: params.Path, Sorting: params.Sort, Type: params.Type },
                    success: function (data) {
                        console.log(data.result);
                        var result = '';
                        counter++;
                        pageEnd = num * counter;
                        pageStart = pageEnd - num;
                        var typename = null;
                        if (pageStart <= data.result.items.length) {
                            for (var i = pageStart; i < pageEnd; i++) {
                                if (data.result.items.length != 0) {
                                    $('.content').css("height", "auto")

                                    result += '<div class="lesson_li col-xs-12"><a class="fl lesson_img" href="/Wap/Course/CourseDetail?courseId=' + data.result.items[i].id + '">'
                                        + '<img class="w" src="' + data.result.items[i].imageUrl + '" />'
                                        + '<div class="cff">' + data.result.items[i].courseName + '</div>' + ' </a>'
                                        + '<div class="fr">'
                                        + '<a href="/Wap/Course/CourseDetail?courseId=' + data.result.items[i].id + '">'
                                            + '<h2 class="c33">' + data.result.items[i].description + '</h2>'
                                        + '</a>'
                                        + '<p class="c99">学习人数：' + data.result.items[i].enrollment + '</p>';
                                    switch (data.result.items[i].type) {
                                        case 1:
                                            typename = "系列课程";
                                            break;
                                        case 2:
                                            typename = "直播课程";
                                            break;
                                        case 3:
                                            typename = "点播课程";
                                            break;
                                        case 4:
                                            typename = "线下培训";
                                            break;
                                    }
                                    result += '<p class="c99">类型：' + typename + '</p>'
                                    if (data.result.items[i].type != 4)
                                        result += '<p class="yanhse">时长：45分</p>';

                                    result += '</div>'
                                        + '</div>';
                                }
                                if ((i + 1) >= data.result.items.length) {
                                    // 锁定
                                    me.lock();
                                    // 无数据
                                    me.noData();
                                    break;
                                }
                            }
                            // 为了测试，延迟1秒加载
                            setTimeout(function () {
                                $('.lists').append(result);
                                // 每次数据加载完，必须重置
                                me.resetload();
                            }, 100);
                        }
                        else {
                            // 数据加载完
                            tab1LoadEnd = true;
                            // 锁定
                            me.lock();
                            // 无数据
                            me.noData();
                            // 为了测试，延迟1秒加载
                            setTimeout(function () {
                                $('.lists').eq(itemIndex).append(result);
                                // 每次数据加载完，必须重置
                                me.resetload();
                            }, 100);
                        }
                    },
                    error: function (xhr, type) {
                        //alert('Ajax error!');
                        // 即使加载出错，也得重置
                        me.resetload();
                    }
                });
            }
        });
    }
});