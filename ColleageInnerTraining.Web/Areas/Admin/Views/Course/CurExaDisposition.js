$(function () {
    //分页
    var pageSize = MaxPageSize;
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

    //考试的所有信息
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
                $("#selectexamin").empty();
                var optionhtml = "";
                if (result.data != null) {
                    optionhtml += "<option value='0'>请选择" + "</option>";
                    $.each(result.data, function () {
                        var _this = this;
                        optionhtml += "<option value='" + _this.id + "'>" + _this.name + "</option>";
                    });
                    $("#selectexamin").append(optionhtml);
                    $("#selectexamin").val(ExamId);
                }
                initPoll();
            }
        });

    }

    //问卷的所有信息
    function initPoll() {
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
                "status": requestParams.status,
                "departmentId": requestParams.departmentId
            },
            success: function (result) {
                $("#selectPoll").empty();
                var optionhtml = "";
                if (result.data != null) {
                    optionhtml += "<option value='0'>请选择" + "</option>";
                    $.each(result.data, function () {
                        var _this = this;
                        optionhtml += "<option value='" + _this.id + "'>" + _this.name + "</option>";
                    });
                    $("#selectPoll").append(optionhtml);
                    $("#selectPoll").val(PollId);
                }
            }
        });
    }

    loadSetting(init);

    //提交
    $("#subcourse").click(function () {
        var isSub = true;
        var courseId = $("#Id").val();
        if (courseId == null || courseId == "" || courseId == undefined) {
            isSub = false;
        }
        var ExamId = $("#selectexamin").val();
        if (ExamId == null || ExamId == "" || ExamId == undefined) {
            $("#selectexamin").addClass("cbor");
            isNext = false;
        }
        var PollId = $("#selectPoll").val();
        if (PollId == null || PollId == "" || PollId == undefined) {
            $("#selectPoll").addClass("cbor");
            isNext = false;
        }

        if (isSub) {
            $.get("/Admin/Course/UpdateCurExa",
                   { courseId: courseId, ExaId: ExamId, PollId: PollId },
                   function (result) {
                       if (result == "0") {
                           location.href = "/Admin/Course/Index"
                       } else {
                           $.modalMsg('修改失败', 'error');
                       }
                   })
        }
    });

});