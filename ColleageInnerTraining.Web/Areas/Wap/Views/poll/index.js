var statuses = new Array(1, 2, 3);

$(function () {
    var itemIndex = 0;
    var tab1LoadEnd = false;
    var tab2LoadEnd = false;

    var requestParams = {
        skipCount: 0,
        pageNumber: 1,
        maxResultCount: MaxPageSize,
        sorting: null,
        polled: null
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


    function init() {
        dropload = $('.content').dropload({
            scrollArea: window,
            loadDownFn: function (me) {
                switch (itemIndex) {
                    case 0:
                        requestParams.polled = 0;
                        pageStart = pageStart0;
                        pageEnd = pageEnd0;
                        counter = counter0;
                        break;
                    case 1:
                        requestParams.polled = 1;
                        pageStart = pageStart1;
                        pageEnd = pageEnd1;
                        counter = counter1;
                        break;
                }
                $.ajax({
                    type: 'GET',
                    url: commSetting.apiUrl + '/api/exam/userpoll/list',
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
                        "polled": requestParams.polled
                    },
                    success: function (data) {
                        var result = '';
                        counter++;
                        pageEnd = num * counter;
                        pageStart = pageEnd - num;
                        console.log(data);
                        var url = new Array("Poll/Polling", "参加问卷", "预考中");
                        if (pageStart <= data.data.length) {
                            switch (itemIndex) {
                                case 0:
                                    requestParams.type = 0;
                                    pageStart0 = pageStart;
                                    pageEnd0 = pageEnd;
                                    counter0 = counter;
                                    url[0] = "/Wap/Poll/Polling";
                                    url[1] = "填写问卷";
                                    url[2] = "未提交";
                                    break;
                                case 1:
                                    requestParams.type = 1;
                                    pageStart1 = pageStart;
                                    pageEnd1 = pageEnd;
                                    counter1 = counter;
                                    url[0] = "#";
                                    url[1] = "已提交";
                                    url[2] = "已提交";
                                    break;
                            }
                            for (var i = pageStart; i < pageEnd; i++) {
                                if (data.data.length != 0) {
                                    result += ' <div class="exam_section">'
                                                + '<p>问卷名称：' + data.data[i].pollInfo.name + '</p>'
                                                + '<p>问卷描述：' + data.data[i].pollInfo.description + '</p>';
                                    
                                    result += '<p id="resout_' + data.data[i].id + '">状　　态：' + url[2] + '</p>'
                                        + '<a href="' + url[0] + '?pollId=' + data.data[i].pollInfo.id + '" class="btn btn_yanse">' + url[1] + '</a>';

                                    
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

