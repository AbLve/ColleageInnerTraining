var statuses = new Array(1,2,3);

$(function () {
    var itemIndex = 0;
    var tab1LoadEnd = false;
    var tab2LoadEnd = false;

    var requestParams = {
        skipCount: 0,
        pageNumber: 1,
        maxResultCount: MaxPageSize,
        sorting: null,
        type: null        
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
        } else if (itemIndex == '3') {
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

    var pageStart3 = 0;
    var pageEnd3 = 0;
    var counter3 = 0;

    function init() {
        // dropload
        dropload = $('.content').dropload({
            scrollArea: window,
            loadDownFn: function (me) {
                switch (itemIndex) {
                    case 0:
                        requestParams.type = 0;
                        pageStart = pageStart0;
                        pageEnd = pageEnd0;
                        counter = counter0;
                        break;
                    case 1:
                        requestParams.type = 1;
                        pageStart = pageStart1;
                        pageEnd = pageEnd1;
                        counter = counter1;
                        break;
                    case 2:
                        requestParams.type = 2;
                        pageStart = pageStart2;
                        pageEnd = pageEnd2;
                        counter = counter2;
                        break;
                    case 3:
                        requestParams.type = 3;
                        pageStart = pageStart3;
                        pageEnd = pageEnd3;
                        counter = counter3;
                        break;
                }                
               

                $.ajax({
                    type: 'GET',
                    url: commSetting.apiUrl + '/api/exam/userexam/listUserExamApply',
                    dataType: 'JSONP',
                    jsonp: 'jsonp',
                    jsonpCallback: 'callback',
                    data: {
                        sTimestamp: commSetting.sTimestamp,
                        appkey: commSetting.appkey,
                        sign: commSetting.sign,
                        "pageSize": requestParams.maxResultCount,
                        "currentPage": requestParams.pageNumber,
                        "userId": commSetting.userId,
                        "type": requestParams.type
                    },
                    success: function (data) {
                        var result = '';
                        counter++;
                        pageEnd = num * counter;
                        pageStart = pageEnd - num;
                        console.log(data);
                        var url = new Array("Exam/Examing", "参加考试", "预考中");
                        if (pageStart <= data.data.length) {
                            switch (itemIndex) {
                                case 0:
                                    requestParams.type = 0;
                                    pageStart0 = pageStart;
                                    pageEnd0 = pageEnd;
                                    counter0 = counter;
                                    url[0] = "#";
                                    url[1] = "参加考试";
                                    url[2] = "预考中";
                                    break;
                                case 1:
                                    requestParams.type = 1;
                                    pageStart1 = pageStart;
                                    pageEnd1 = pageEnd;
                                    counter1 = counter;
                                    url[0] = "/Wap/Exam/Examing";
                                    url[1] = "参加考试";
                                    url[2] = "考试中";
                                    break;
                                case 2:
                                    requestParams.type = 2;
                                    pageStart2 = pageStart;
                                    pageEnd2 = pageEnd;
                                    counter2 = counter;
                                    url[0] = "/Wap/Exam/Examing";
                                    url[1] = "参加考试";
                                    url[2] = "未参考";
                                    break;
                                case 3:
                                    requestParams.type = 3;
                                    pageStart3 = pageStart;
                                    pageEnd3 = pageEnd;
                                    counter3 = counter;
                                    url[0] = "/Wap/Exam/ExamResult";
                                    url[1] = "我的成绩";
                                    url[2] = "已考完";
                                    break;
                            }
                            for (var i = pageStart; i < pageEnd; i++) {
                                if (data.data.length != 0 && i < data.data.length) {
                                    result += ' <div class="exam_section">'
                                                + '<p>考  试  项：' + data.data[i].examName + '</p>'
                                                + '<p>考试时间：' + data.data[i].examStartTime + "-" + data.data[i].examEndTime + '</p>';
                                    if (itemIndex != 3) {
                                        result += '<p>状　　态：' + url[2] + '</p>';
                                        if (itemIndex==1)
                                            result += '<a href="' + url[0] + '?examId=' + data.data[i].examId + '" class="btn btn_yanse">' + url[1] + '</a>';
                                    }
                                    else
                                    {
                                        result += '<p id="resout_' + data.data[i].id + '">状　　态：' + data.data[i].statusName + '</p>';

                                        result += '<p id="resout_' + data.data[i].id + '">分　　数：' + data.data[i].score + ' 分</p>';
                                        result += '<p id="resout_' + data.data[i].id + '">参考时间：' + data.data[i].startTime + '-' + data.data[i].endTime + '</p>';
                                    }
                                    result += '</div>';
                                }
                                if ((i + 1) >= data.data.length) {
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
    loadSetting(init);
});

 