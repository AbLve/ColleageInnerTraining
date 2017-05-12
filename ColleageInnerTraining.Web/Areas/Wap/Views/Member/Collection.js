$(function () {
    var itemIndex = 0;
    var tab1LoadEnd = false;
    var tab2LoadEnd = false;

    var requestParams = {
        skipCount: 0,
        pageNumber: 1,
        maxResultCount: MaxPageSize,
        sorting: null,
        type: "Course"
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
    var ajaxUrl = '/Wap/Member/CollectionList';

   
    function init() {
        // dropload
        dropload = $('.content').dropload({
            scrollArea: window,
            loadDownFn: function (me) {
                switch (itemIndex) {
                    case 0:
                        requestParams.type = "Course";
                        pageStart = pageStart0;
                        pageEnd = pageEnd0;
                        counter = counter0;
                        ajaxUrl = '/Wap/Member/CollectionList';
                        break;
                    case 1:
                        requestParams.type = "Train";
                        pageStart = pageStart1;
                        pageEnd = pageEnd1;
                        counter = counter1;
                        ajaxUrl = '/Wap/Member/CollectionList';
                        break;
                    case 2:
                        requestParams.type = "Course";
                        pageStart = pageStart2;
                        pageEnd = pageEnd2;
                        counter = counter2;
                        ajaxUrl = '/Wap/Member/CollectionList';
                        break;
                    
                }


                $.ajax({
                    type: 'GET',
                    url: ajaxUrl,
                    data: {
                        "maxResultCount":1000,
                        "skipCount": 0,
                        "BizType": requestParams.type
                    },
                    success: function (data) {
                        console.log(data);
                        var result = '';
                        counter++;
                        pageEnd = num * counter;
                        pageStart = pageEnd - num;
                        console.log(data);
                        var url = new Array("Exam/Examing", "参加考试", "预考中");
                        if (pageStart <= data.Items.length) {
                            switch (itemIndex) {
                                case 0:
                                    requestParams.type = "Course";
                                    pageStart0 = pageStart;
                                    pageEnd0 = pageEnd;
                                    counter0 = counter;
                                    break;
                                case 1:
                                    requestParams.type = "Train";
                                    pageStart1 = pageStart;
                                    pageEnd1 = pageEnd;
                                    counter1 = counter;
                                    break;
                                case 2:
                                    requestParams.type = "Course";
                                    pageStart2 = pageStart;
                                    pageEnd2 = pageEnd;
                                    counter2 = counter;
                                    break;
                                 
                            }
                            for (var i = pageStart; i < pageEnd; i++) {
                                if (data.Items.length != 0) {
                                    result += '<div class="course_section overhide" id="collection_' + data.Items[i].BizId + '">'
                                                + '<a href="/Wap/Course/CourseDetail?courseId=' + data.Items[i].Id + '"><img class="fl" src="' + data.Items[i].ImageUrl + '" /></a>'
                                                + '<div class="fr">'
						                        + '<h2 class="c33">' + data.Items[i].BizName + '</h2>'
						                        + '<p class="c99">学习人数：' + data.Items[i].Enrollment + '</p>'
						                        + '<p class="c99">类型：' + data.Items[i].TypeName + '</p>'
						                        + '<p class="c33">时长：' + data.Items[i].TimeLength + '分钟</p>'
					                            + '</div>'
					                            + '<a href="#" name="' + data.Items[i].BizType + '" id="' + data.Items[i].BizId + '" class="btn_cancel">取消收藏</a>';
                                    result += '</div>';
                                }
                                if ((i + 1) >= data.Items.length) {
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

