
$(document).ready(function () {
    var pageSize = 10;
    var pageNumber = 1;
    var totalPage = 0;

    var requestParams = {
        skipCount: 0,
        pageNumber: pageNumber,
        maxResultCount: pageSize,
        pollId,pollId,
        sorting: null,
        timestamp: null,
        appkey: null,
        sign: null,
        apiUrl: null,
        content: null,
        enabled: null,
        status: null,
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
        requestParams.status = null;
        requestParams.pattern = null;
        requestParams.departmentId = null;

        if ($("#name").val() != "")
            requestParams.name = $("#name").val();
        if ($("#enabled").val() != "")
            requestParams.enabled = $("#enabled").val();
        if ($("#status").val() != "")
            requestParams.status = $("#status").val();
        if ($("#pattern").val() != "") {
            requestParams.pattern = $("#pattern").val();
        }
        if ($("#departmentId").val() != "") {
            requestParams.departmentId = $("#departmentId").val();
        }
    }

    //页面载入方法
    function requireLoad() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/poll/listPollRequireByPollId',
            dataType: 'JSONP',
            jsonp: 'jsonp',
            jsonpCallback: 'callback',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "pollId": requestParams.pollId
            },
            success: function (result) {
                var data = { data: result };
                var gettpl = document.getElementById('questions').innerHTML;
                laytpl(gettpl).render(data, function (html) {
                    console.log(data);
                    $('#Render_List').html("");
                    if (data!= null) {
                        $('#Render_List').html(html);

                        $(".deletePoll").click(function () {
                            if (!window.confirm("您确认要删除试卷吗？"))
                                return;
                            var values = $(this).attr("title");
                            var val = values.split(',');
                            var bizId = val[0];
                            var type = val[1];
                            $.ajax({
                                type: 'POST',
                                url: commSetting.apiUrl + '/api/exam/poll/deletePollRequire',
                                sTimestamp: commSetting.sTimestamp,
                                appkey: commSetting.appkey,
                                sign: commSetting.sign,
                                dataType: 'JSON',
                                contentType: 'application/json',
                                data: JSON.stringify({
                                    sTimestamp: commSetting.sTimestamp,
                                    appkey: commSetting.appkey,
                                    sign: commSetting.sign,
                                    "pollId": requestParams.pollId,
                                    "bizId": bizId,
                                    "type": type
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



    //载入配置信息
    function init() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/poll/get',
            dataType: 'JSON',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: requestParams.pollId
            },
            success: function (data) {
                $('#pollName').html(data.name);
                requireLoad();
            }
        });
    }


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
            url: commSetting.apiUrl + '/api/exam/poll/savePollRequire',
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify({
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "pollId": requestParams.pollId,
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
    loadSetting(init);
     
});



