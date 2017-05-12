$(document).ready(function () {
    //分页
    var pageSize = 10000;
    var pageNumber = 1;
    var totalPage = 0;
    //参数对像
    var requestParams = {
        id: examId,
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
    function requireLoad() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/exam/listExamRequireByExamId',
            dataType: 'JSONP',
            jsonp: 'jsonp',
            jsonpCallback: 'callback',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "departmentId": requestParams.departmentId,
                "examId": requestParams.id
            },
            success: function (data) {
                if (data != null) {
                    var result = { data: data }
                    var gettpl = document.getElementById('questions').innerHTML;
                    laytpl(gettpl).render(result, function (html) {
                        console.log(result);
                        $('#Render_List').html("");
                        if (result.data != null) {
                            $('#Render_List').html(html);



                            $(".deleteExamPaper").click(function () {
                                if (!window.confirm("您确认要删除必考关系吗？"))
                                    return;
                                var values = $(this).attr("title");
                                var arryId = values.split(",");
                                var bizId = arryId[0];
                                var type = arryId[1];

                                $.ajax({
                                    type: 'POST',
                                    url: commSetting.apiUrl + '/api/exam/exam/deleteExamRequire',
                                    sTimestamp: commSetting.sTimestamp,
                                    appkey: commSetting.appkey,
                                    sign: commSetting.sign,
                                    dataType: 'JSON',
                                    contentType: 'application/json',
                                    data: JSON.stringify({
                                        sTimestamp: commSetting.sTimestamp,
                                        appkey: commSetting.appkey,
                                        sign: commSetting.sign,
                                        "examId": requestParams.id,
                                        "bizId": bizId,
                                        "type": type,
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
            }
        });

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

    //载入参数及数据
    loadSetting(init);


    $("#typeId").change(function () {
        switch ($(this).val()) {
            case "0":
                $("#departmentId").css("display", "block");
                $("#postId").css("display", "none");
                $("#classesId").css("display", "none");
                break;
            case "1":
                $("#departmentId").css("display", "none");
                $("#postId").css("display", "block");
                $("#classesId").css("display", "none");
                break;
            case "2":
                $("#departmentId").css("display", "none");
                $("#postId").css("display", "none");
                $("#classesId").css("display", "block");
                break;
        }
    });
    //添加试卷
    $("#formSave").click(function () {
        var bizId = "";
        var bizName = "";
        var values = "";
        var typeId = $("#typeId").val();
        switch (typeId) {
            case "0":
                values = $("#departmentId").val();
                if (values == null || values == "") {
                    alert("部门不可以为空！");
                    return;
                }
                break;
            case "1":
                values = $("#postId").val();
                if (values == null || values == "") {
                    alert("岗位不可以为空！");
                    return;
                }
                break;
            case "2":
                values = $("#classesId").val();
                if (values == null || values == "") {
                    alert("班级不可以为空！");
                    return;
                }
                break;
        }

        var arryId = values.split(",");
        bizId = arryId[0];
        bizName = arryId[1];

        if (bizId == "") {
            alert("必考对像不能为空！");
            return;
        }

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
                "type": typeId,
                "bizId": bizId,
                "bizName": bizName,
                "creater": commSetting.userId,
                "createrName": commSetting.userName
            }),
            success: function (result) {
                alert('保存成功！');
                init();
            },
            error: function (result) {
                alert(result);
                init();
            }
        });

    });

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



});

