$(function () {
    var itemIndex = 0;
    var tab1LoadEnd = false;
    var tab2LoadEnd = false;

    var requestParams = {
        skipCount: 0,
        pageNumber: 1,
        maxResultCount: MaxPageSize,
        sorting: null,
        status: 1
    };



    // tab
    $('.tab .item').on('click', function () {
        var $this = $(this);
        itemIndex = $this.index();
        $this.addClass('cur').siblings('.item').removeClass('cur');
        $('.lists').eq(itemIndex).show().siblings('.lists').hide();

        // 如果选中菜单一
        if (itemIndex == '0') {
            // 如果数据没有加载完
            if (!tab1LoadEnd) {
                // 解锁
                dropload.unlock();
                dropload.noData(false);
            } else {
                // 锁定
                dropload.lock('down');
                dropload.noData();
            }
            // 如果选中菜单二
        } else if (itemIndex == '1') {
            if (!tab2LoadEnd) {
                // 解锁
                dropload.unlock();
                dropload.noData(false);
            } else {
                // 锁定
                dropload.lock('down');
                dropload.noData();
            }
        } else if (itemIndex == '2') {
            if (!tab2LoadEnd) {
                // 解锁
                dropload.unlock();
                dropload.noData(false);
            } else {
                // 锁定
                dropload.lock('down');
                dropload.noData();
            }
        }
        // 重置
        dropload.resetload();
    });

    var counter = 0;
    // 每页展示4个
    var num = 4;
    var pageStart = 0, pageEnd = 0;

    var dropload = null;

    var pageStart0 = 0;
    var pageEnd0 = 0;
    var counter0 = 0;

    var pageStart1 = 0;
    var pageEnd1 = 0;
    var counter1 = 0;

    var pageStart2 = 0;
    var pageEnd2 = 0;
    var counter2 = 0;
    var ajaxUrl = '/Wap/Member/TrainingList';


    function init() {
        // dropload
        dropload = $('.content').dropload({
            scrollArea: window,
            loadDownFn: function (me) {
                switch (itemIndex) {
                    case 0:
                        requestParams.status = 1;
                        pageStart = pageStart0;
                        pageEnd = pageEnd0;
                        counter = counter0;
                        break;
                    case 1:
                        requestParams.status = 2;
                        pageStart = pageStart1;
                        pageEnd = pageEnd1;
                        counter = counter1;
                        break;
                    case 2:
                        requestParams.status = 3;
                        pageStart = pageStart2;
                        pageEnd = pageEnd2;
                        counter = counter2;
                        break;

                }


                $.ajax({
                    type: 'GET',
                    url: ajaxUrl,
                    data: {
                        "maxResultCount": 1000,
                        "skipCount": 0,
                        "status": requestParams.status
                    },
                    success: function (data) {
                        var resultData = { data: data };
                        console.log(resultData);
                        var result = '';
                        counter++;
                        pageEnd = num * counter;
                        pageStart = pageEnd - num;
                        if (pageStart <= resultData.data.length) {
                            switch (itemIndex) {
                                case 0:
                                    pageStart0 = pageStart;
                                    pageEnd0 = pageEnd;
                                    counter0 = counter;
                                    break;
                                case 1:
                                    pageStart1 = pageStart;
                                    pageEnd1 = pageEnd;
                                    counter1 = counter;
                                    break;
                                case 2:
                                    pageStart2 = pageStart;
                                    pageEnd2 = pageEnd;
                                    counter2 = counter;
                                    break;

                            }
                            for (var i = pageStart; i < pageEnd; i++) {
                                if (resultData.data.length != 0) {
                                    result += '<div class="course_section overhide" id="collection_' + resultData.data[i].Id + '">'
                                                + '<a href="/Wap/Course/CourseDetail?courseId=' + resultData.data[i].Id + '"><img class="fl" src="' + resultData.data[i].ImageUrl + '" /></a>'
                                                + '<div class="fr">'
						                        + '<h2 class="c33">' + resultData.data[i].CourseName + '</h2>'
						                        + '<p class="c99">学习人数：' + resultData.data[i].Enrollment + '</p>'
						                        + '<p class="c99">类型：' + resultData.data[i].TypeName + '</p>'
						                        + '<p class="c33">时长：' + resultData.data[i].TimeLength + '分钟</p>'
					                            + '</div>';
                                    result += '</div>';
                                }
                                if ((i + 1) >= resultData.data.length) {
                                    // 数据加载完
                                    tab1LoadEnd = true;
                                    // 锁定
                                    me.lock();
                                    // 无数据
                                    me.noData();
                                    break;
                                }
                            }
                            // 为了测试，延迟1秒加载
                            setTimeout(function () {
                                $('.lists').eq(itemIndex).append(result);
                                $(".btn_cancel").click(function () {
                                    var $input = $(this);
                                    var courseId = $input.attr('id');
                                    var type = $input.attr('name');

                                    $.ajax({
                                        type: 'GET',
                                        url: abp.appPath + 'Wap/Collection/DelCollection',
                                        data: { bizId: courseId, type: type },
                                        success: function (result) {
                                            alert('取消收藏成功！');
                                            $("#collection_" + courseId).css("display", "none");
                                        }
                                    });
                                });


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
                        console.log(xhr);
                        //alert('Ajax error!');
                        // 即使加载出错，也得重置
                        //me.resetload();
                    }
                });

            }
        });
    }
    loadSetting(init);
});

