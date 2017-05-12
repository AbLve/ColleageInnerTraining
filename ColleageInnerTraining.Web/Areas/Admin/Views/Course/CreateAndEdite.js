//courseType(课程类型)
//courseDetailed(开课信息)
//teachingPPT(授课PPT)
//courseIntroduce(课程介绍)
//complete(完成)

var ktype;


$(document).ready(function () {
    var ue = UE.getEditor('editor', {
        autoHeightEnabled: false
    });
    ue.addListener("ready", function () {
        // editor准备好之后才可以使用
        var resultHtml = htmlDecode(contentText);
        ue.setContent(resultHtml);
    });

    readShow();
    $("#uploadify_Head").uploadify({
        'buttonText': "上传图片",
        'swf': '/images/uploadfy/uploadify.swf',
        'uploader': abp.appPath + 'Admin/Course/UploadfyPic',
        'fileObjName': 'importFile',
        'fileTypeDesc': 'excel Files',
        'fileTypeExts': '*.gif; *.jpg; *.png',
        'formData': {
            'appkey': '', 'sign': '', 'sTimestamp': '', 'creater': '999', 'createrName': 'hello', 'departmentId': '999'
        },
        'onUploadSuccess': function (file, data, response) {
            var result = JSON.parse(data);
            if (result.IsSuccess == "0") {
                iurl = result.filePath;
                $("#imghead").attr('src', result.filePath);
                $.modalMsg("上传成功");
            }
            else {
                $.modalMsg('上传失败！');
            }
        },
        'onUploadError': function (file, errorCode, errorMsg, errorString) {
            console.log('errorCode: ' + errorCode + ", errorMsg: " + errorMsg + ", errorString: " + errorString);
        }
    });

    //开课信息(提交信息)
    $("#subcourse").click(function () {
        $("#Type").val(ktype);
        $("#TeacherId").val($("#InternalT").val());
        $("#TeacherName").val($("#InternalT").find("option:selected").text());
        $("#ImageUrl").val(iurl);
        var html = htmlEncode(ue.getContent());
        $("#Content").val(html);
        $("#ExaminationId").val($("#selectexamin").val());
        $("#PollId").val($("#selectPoll").val());
        $("#courseType").css('display', 'none');
        $("#courseDetailed").css('display', 'none');
        $("#teachingPPT").css('display', 'none');
        $("#courseIntroduce").css('display', 'none');
        $("#complete").css('display', 'block');
        $("#fmCourse").submit();
    })

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
                    $("#selectexamin").val(examinId);
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
                    $("#selectPoll").val(pollId);
                }
            }
        });
    }

    loadSetting(init);

    CheckType(type); //绑定类型   

    $("#imghead").attr('src', iurl);
    $("#InternalT").val(teacharId);
});

//初始显示页面，课程类型
function readShow() {
    $("#courseType").css('display', 'block');
    $("#courseDetailed").css('display', 'none');
    $("#teachingPPT").css('display', 'none');
    $("#courseIntroduce").css('display', 'none');
    $("#complete").css('display', 'none');
}


//课程类型(下一步)
function courseTypeNext() {
    var type = $(".knowledge-tab a");
    $(type).each(function () {
        var _this = $(this);
        if (_this.attr("class") == "cur") {
            if (_this.html() == "系列课") {
                ktype = 1;
            }
            if (_this.html() == "直播") {
                ktype = 2;
            }
            if (_this.html() == "点播") {
                ktype = 3;
            }
            if (_this.html() == "线下") {
                ktype = 4;
                $("#courseType").css('display', 'none');
                $("#courseDetailed").css('display', 'block');
                $("#teachingPPT").css('display', 'none');
                $("#courseIntroduce").css('display', 'none');
                $("#complete").css('display', 'none');
            } else {//系列课，直播，点播，新增信息
                $.modalMsg("功能未开放");
                return false;
            }
        }
    });
}

//开课信息(上一步)
function courseDetailedStep() {
    readShow();
}

//开课信息(下一步)
function courseDetailedNext() {
    var isNext = true;
    var courseName = $("#CourseName").val();
    if (courseName == "" || courseName == undefined) {
        $("#CourseName").addClass("cbor");
        isNext = false;
    }
    var type = $("#CategoryType").val();
    if (type == "" || type == undefined) {
        $("#CategoryType").addClass("cbor");
        isNext = false;
    }
    var int = $("#InternalT").val();
    if (int == "" || int == undefined) {
        $("#InternalT").addClass("cbor");
        isNext = false;
    }
    if (isNext) {
        $("#courseType").css('display', 'none');
        $("#courseDetailed").css('display', 'none');
        $("#teachingPPT").css('display', 'none');
        $("#courseIntroduce").css('display', 'block');
        $("#complete").css('display', 'none');
    }
}

//开课信息(上一步)
function courseIntroduceStep() {
    var type = $(".knowledge-tab a");
    $(type).each(function () {
        var _this = $(this);
        if (_this.attr("class") == "cur") {
            if (_this.html() == "线下") {
                $("#courseType").css('display', 'none');
                $("#courseDetailed").css('display', 'block');
                $("#teachingPPT").css('display', 'none');
                $("#courseIntroduce").css('display', 'none');
                $("#complete").css('display', 'none');
            } else {
                $("#courseType").css('display', 'none');
                $("#courseDetailed").css('display', 'none');
                $("#teachingPPT").css('display', 'block');
                $("#courseIntroduce").css('display', 'none');
                $("#complete").css('display', 'none');
            }
        }
    });
}
//转义HTML加密
function htmlEncode(value) {
    return $('<div/>').text(value).html();
}
//转义HTML解密
function htmlDecode(value) {
    return $('<div/>').html(value).text();
}

function CheckType(type) {
    var typeA = $(".knowledge-tab a");
    $(typeA).each(function () {
        var _this = $(this);
        _this.removeClass("cur");
        if (type == 1 && _this.html() == "系列课") {
            _this.addClass("cur");
        }
        if (type == 2 && _this.html() == "直播") {
            _this.addClass("cur");
        }
        if (type == 3 && _this.html() == "点播") {
            _this.addClass("cur");
        }
        if (type == 4 && _this.html() == "线下") {
            _this.addClass("cur");
        }
    })
}

$("#CategoryType").bindSelect({
    url: "GetCateTreeSelectJson"
});