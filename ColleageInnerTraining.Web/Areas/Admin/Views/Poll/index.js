$(document).ready(function () {
    var pageSize = 10;
    var pageNumber = 1;
    var totalPage = 0;

    var requestParams = {
        skipCount: 0,
        pageNumber: pageNumber,
        maxResultCount: pageSize,
        sorting: null,
        timestamp: null,
        appkey: null,
        sign: null,
        apiUrl: null,
        content: null,
        enabled: null,
        knowledgeIds: null,
        departmentId: null,
        ids: null
    };



    //选中的行
    function getSelectedIds() {
        var checkObj = $("#questionTable :checkbox");
        var ids = "";
        for (var i = 0; i < checkObj.length; i++) {
            if (checkObj[i].id == "selected" && checkObj[i].checked)
                ids += checkObj[i].value + ",";
        }
        if (ids != "")
            ids = ids.substring(0, ids.length - 1);
        requestParams.ids = ids;
    }


    //设置查询参数
    function setRequestParams() {
        requestParams.pageNumber = 1;
        requestParams.name = null;
        requestParams.enabled = null;
        requestParams.pattern = null;
        requestParams.departmentId = null;

        if ($("#name").val() != "")
            requestParams.name = $("#name").val();
        if ($("#enabled").val() != "")
            requestParams.enabled = $("#enabled").val();
        if ($("#pattern").val() != "") {
            requestParams.pattern = $("#pattern").val();
        }
        if ($("#departmentId").val() != "") {
            requestParams.departmentId = $("#departmentId").val();
        }
    }
    //查询
    $("#btnSearch").click(function () {
        setRequestParams();
        init();
    })
    //页面载入方法
    function init() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/poll/list',
            dataType: 'JSONP',
            jsonp: 'jsonp',
            jsonpCallback: 'callback',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "pageSize": requestParams.maxResultCount,
                "currentPage": requestParams.pageNumber,
                "name": requestParams.name,
                "enabled": requestParams.enabled,
                "departmentId": requestParams.departmentId,
                "pattern": requestParams.pattern
            },
            success: function (result) {
                var gettpl = document.getElementById('questions').innerHTML;
                laytpl(gettpl).render(result, function (html) {
                    totalPage = result.totalPage;
                    $('#Render_List').html("");
                    if (result.data != null) {
                        $('#Render_List').html(html);
                        pager(result.currentPage, result.totalPage);
                        $("#PageNext").bind("click", function () {
                            pageNumber++;
                            if (pageNumber > totalPage) { pageNumber = totalPage }
                            requestParams.skipCount = (pageNumber - 1) * pageSize;
                            requestParams.pageNumber = pageNumber;
                            requestParams.maxResultCount = pageSize;
                            init();
                        });

                        $("#PagePrev").bind("click", function () {
                            pageNumber--;
                            if (pageNumber <= 0) { pageNumber = 1 }
                            requestParams.skipCount = (pageNumber - 1) * pageSize;
                            requestParams.pageNumber = pageNumber;
                            requestParams.maxResultCount = pageSize;
                            init();
                        });

                        $(".goPage").bind("click", function () {
                            pageNumber = this.innerHTML;
                            if (pageNumber < 0) { pageNumber = 1 }
                            if (pageNumber > totalPage) { pageNumber = totalPage }
                            requestParams.skipCount = (pageNumber - 1) * pageSize;
                            requestParams.pageNumber = pageNumber;
                            requestParams.maxResultCount = pageSize;
                            init();
                        });
                        $(".selected").bind("click", function () {
                            getSelectedIds();
                        });
                        $(".deletePoll").click(function () {
                            if (!window.confirm("您确认要删除试卷吗？"))
                                return;
                            var pollId = $(this).attr("title");
                            $.ajax({
                                type: 'POST',
                                url: commSetting.apiUrl + '/api/exam/poll/remove',
                                sTimestamp: commSetting.sTimestamp,
                                appkey: commSetting.appkey,
                                sign: commSetting.sign,
                                dataType: 'JSON',
                                contentType: 'application/json',
                                data: JSON.stringify({
                                    sTimestamp: commSetting.sTimestamp,
                                    appkey: commSetting.appkey,
                                    sign: commSetting.sign,
                                    "id": pollId,
                                    "updater": commSetting.userId,
                                    "updaterName": commSetting.userName
                                }),
                                success: function (result) {
                                    alert('删除成功！');
                                    init();
                                }
                            });
                        });
                    }
                });
            }
        });
    }

    //分页
    function pager(currentPage, totalPage) {
        strpage = "";
        var startPage = 1;
        var endPage = currentPage + 3;
        if (endPage > totalPage)
            endPage = totalPage;
        startPage = currentPage - 3;
        if (startPage <= 0)
            startPage = 1;
        for (var i = startPage; i <= endPage; i++) {
            strpage += "<a><span  class='goPage ";
            if (i == currentPage)
                strpage += "active";
            strpage += "'>" + i + "</span></a>";
        }
        strpage = "<p><a class='prev' id='PagePrev'><span>上一页</span></a>" + strpage + "<a class='prev' id='PageNext'><span>下一页</span></a></p>";

        $('#pager').html(strpage);
    }


    loadSetting(init);

    //全选取消全选
    $("#checkAll").click(function () {
        if (this.checked) {
            $("#questionTable :checkbox").prop("checked", true);
            getSelectedIds()
        } else {
            $("#questionTable :checkbox").prop("checked", false);
            requestParams.ids = null;
        }
    });

});



