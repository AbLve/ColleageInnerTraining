loadSetting(GetExamOrPoll);

$('body').click(function () {
    $('#errorlal').hide();
});
var url = "/api/exam/exam/list";
function typeChange(e) {
    var tid = $(e).val();
    if (tid === '2') {//考试类型调接口
        //加载配置
        url = '/api/exam/exam/list';
        loadSetting(GetExamOrPoll);

    } else if (tid === '3') {//问卷调接口
        url = '/api/exam/poll/list';
        loadSetting(GetExamOrPoll);
    }
    //else {
    //    $.getJSON('GetProList?tId=' + tid, function (result) {
    //        $('#prosel').empty();
    //        var list = new Array();
    //        $.each(result, function (i, data) {
    //            var item = $("<option></option>");
    //            console.log(data);
    //            list.push(item.html(data.CourseName).val(data.Id));
    //        })
    //        $('#prosel').append(list);
    //    });
    //}
}

function GetExamOrPoll() {
    var requestParams = {
        skipCount: 0,
        pageNumber: 1,
        maxResultCount: 1000,
        sorting: null,
        name: null,
        content: null,
        enabled: null,
        status: null,
        knowledgeIds: null,
        departmentId: null,
        ids: null
    };
    $.ajax({
        type: 'GET',
        url: commSetting.apiUrl + url,
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
            $("#prosel").empty();
            var optionhtml = "";
            if (result.data != null) {
                $.each(result.data, function () {
                    var _this = this;
                    optionhtml += "<option value='" + _this.id + "'>" + _this.name + "</option>";
                });
                $("#prosel").append(optionhtml);
            }
        }
    });
}
//提交表单
function sf() {

    var type = $('#TrainingType').val();
    var name = $('#prosel option:selected').text();
    $('#Name').val(name);

    var bizId = $('#ClassId').val();
    var pollorExamId = $('#prosel').val();
    var data = new Object();

    switch (type) {
        case '2'://提交的考试
            url = "/api/exam/exam/saveExamRequire";
            data = {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                examId: pollorExamId,
                type: 2,
                bizId: bizId,
                bizName: name
            }
            break;
        case '3'://问卷
            url = "/api/exam/poll/savePollRequire";
            data = {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                pollId: pollorExamId,
                type: 2,
                bizId: bizId,
                bizName: name
            };
            break;
        default:
            return false;
    }

    $.ajax({
        type: 'POST',
        url: commSetting.apiUrl + url,
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (result) {
            console.log(result);
            AddClassProject();
        }
    })
}

function AddClassProject() {
    $('#TypeId').val($('#prosel').val());

    if (!$('#StartTime').val()) {
        $('#StartTime').focus();
        return false;
    }

    if (!$('#EndTime').val()) {
        $('#EndTime').focus();
        return false;
    }
    var options = {
        url: 'ClassesProjectSave',
        success: function (result) {
            $.modalMsg(result.msg, result.type);
            if (result.data) {
                location.href = result.data;
            }
        },
        dataType: 'json',
        clearForm: false,
        resetForm: false,
        type: 'post',
        timeout: 3000,
        error: function (result) {
            console.log(result);
            $.modalMsg(result.msg, result.type);
        }
    };
    $('#form').ajaxSubmit(options);
}

