$(document).ready(function () {
    //分页
    var pageSize = 10;
    var pageNumber = 1;
    var totalPage = 0;
    //参数对像
    var requestParams = {
        filterText: null,
        id: examId,
        skipCount: 0,
        pageNumber: pageNumber,
        maxResultCount: pageSize,
        sorting: null,
        username: null,
        content: null,
        enabled: null,
        status: null,
        knowledgeIds: null,
        departmentId: null,
        selectedId: null
    };

    function requireLoad() {

        $.ajax({
            type: 'GET',
            url: '/Admin/AccountUser/GetPagedUserAccounts',
            data: {
                Username: requestParams.username,
                MaxResultCount: requestParams.maxResultCount,
                SkipCount: requestParams.skipCount
            },
            success: function (result) {
                if (result != null) {
                    console.log(result);
                    var gettpl = document.getElementById('questions').innerHTML;
                    laytpl(gettpl).render(result, function (html) {

                        var totalCount = result.TotalCount;
                        var r = totalCount % 10;
                        if (r > 0) {

                            totalPage = parseInt(totalCount / 10) + 1;
                        }
                        else
                            totalPage = parseInt(totalCount / 10)

                        $('#Render_List').html("");
                        if (result.Items != null) {
                            $('#Render_List').html(html);
                            $(".selected").bind("click", function () {
                                getSelectedIds();
                            });

                            pager(pageNumber, totalPage);
                            $("#PageNext").bind("click", function () {
                                pageNumber++;
                                if (pageNumber > totalPage) { pageNumber = totalPage }
                                requestParams.skipCount = (pageNumber - 1) * pageSize;
                                requestParams.pageNumber = pageNumber;
                                requestParams.maxResultCount = pageSize;
                                requireLoad();
                            });

                            $("#PagePrev").bind("click", function () {
                                pageNumber--;
                                if (pageNumber <= 0) { pageNumber = 1 }
                                requestParams.skipCount = (pageNumber - 1) * pageSize;
                                requestParams.pageNumber = pageNumber;
                                requestParams.maxResultCount = pageSize;
                                requireLoad();
                            });

                            $(".goPage").bind("click", function () {
                                pageNumber = this.innerHTML;
                                if (pageNumber < 0) { pageNumber = 1 }
                                if (pageNumber > totalPage) { pageNumber = totalPage }
                                requestParams.skipCount = (pageNumber - 1) * pageSize;
                                requestParams.pageNumber = pageNumber;
                                requestParams.maxResultCount = pageSize;
                                requireLoad();
                            });

                        }

                    });
                }
            }
        });
    }


    //选中的行
    function getSelectedIds() {
        var checkObj = $("#questionTable :radio");
        var ids = "";
        for (var i = 0; i < checkObj.length; i++) {
            if (checkObj[i].id == "selected" && checkObj[i].checked)
                requestParams.selectedId = checkObj[i].value;
        }

    }

    //载入配置信息
    function init() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/exam/get',
            dataType: 'JSON',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: requestParams.id
            },
            success: function (data) {
                $('#examName').html(data.name);
                requireLoad()

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



    $("#btnSearch").click(function () {
        requestParams.username = $("#UserName").val();
        requireLoad();
    });

    //载入参数及数据
    loadSetting(init);


    //添加试卷
    $("#formSave").click(function () {
        var bizId = "";
        var bizName = "";
        var values = "";

        if (requestParams.selectedId == "" || requestParams.selectedId==null) {
            alert("必考对像不能为空,请选择用户！");
            return;
        }
        var arryId = requestParams.selectedId.split(",");
        bizId = arryId[0];
        bizName = arryId[1];


        $.ajax({
            type: 'POST',
            url: commSetting.apiUrl + '/api/exam/exam/saveExamRequire',
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify({
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "examId": requestParams.id,
                "type": 3,
                "bizId": bizId,
                "bizName": bizName,
                "creater": commSetting.userId,
                "createrName": commSetting.userName
            }),
            success: function (result) {
                alert('用户添加成功！');
                init();
            },
            error: function (result) {
                alert("用户添加失败！");
                init();
            }
        });

    });





});

