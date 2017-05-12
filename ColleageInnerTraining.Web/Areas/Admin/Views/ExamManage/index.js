$(document).ready(function () {
    //分页
    var pageSize = 10;
    var pageNumber = 1;
    var totalPage = 0;
    //参数对像
    var requestParams = {
        skipCount: 0,
        pageNumber: pageNumber,
        maxResultCount: pageSize,
        sorting: null,
        name: null,
        content: null,
        enabled: null,
        status: null,
        knowledgeIds: null,
        departmentId: null,
        ids: null
    };

    //取得被选择的对像
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
    //传参对像取值
    function setRequestParams() {
        requestParams.pageNumber = 1;
        requestParams.name = null; 
        requestParams.enabled = null;
        requestParams.status = null; 
        requestParams.departmentId = null;

        if ($("#name").val() != "")
            requestParams.name = $("#name").val(); 
        if ($("#enabled").val() != "")
            requestParams.enabled = $("#enabled").val();
        if ($("#status").val() != "")
            requestParams.status = $("#status").val();        
        if ($("#departmentId").val() != "") {
            requestParams.departmentId = $("#departmentId").val();
        }
    }
    //查询试题
    $("#btnSearch").click(function () {

        setRequestParams();
        init();
    })

    //载入试题
    function init() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/exam/list',
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
                "status": requestParams.status,
                "departmentId": requestParams.departmentId
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

                        $(".deleteExam").click(function () {
                            if (!window.confirm("您确认要删除考试吗？"))
                                return;
                            var examId = $(this).attr("title");
                            $.ajax({
                                type: 'POST',
                                url: commSetting.apiUrl + '/api/exam/exam/remove',
                                sTimestamp: commSetting.sTimestamp,
                                appkey: commSetting.appkey,
                                sign: commSetting.sign,
                                dataType: 'JSON',
                                contentType: 'application/json',
                                data: JSON.stringify({
                                    sTimestamp: commSetting.sTimestamp,
                                    appkey: commSetting.appkey,
                                    sign: commSetting.sign,
                                    "id": examId,
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
    //加载配置
    loadSetting(init);

   

});

