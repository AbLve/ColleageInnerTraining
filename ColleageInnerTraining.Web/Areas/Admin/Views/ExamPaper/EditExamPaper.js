$(document).ready(function () { 
    var requestParams = {
        id:paperId,
        paperName: null,
        description: null,
        pattern: null,
        status:0
    };    
    //载入试卷信息
    function init(){
        $.ajax({
            type: 'GET',
            url:commSetting.apiUrl+ '/api/exam/paper/get',
            dataType: 'JSON',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: requestParams.id
            },
            success: function (data) {
                console.log(data);
                setPaper(data);
            }
        });
    }
    //为控件赋值
    function setPaper(data) {
        requestParams.paperName = data.name;
        requestParams.description = data.description;
        requestParams.pattern = data.pattern;
        requestParams.status = data.status;
        $("#paperName").val(data.name);
        $("#paperDescription").val(data.description);
        $("#paperPattern").val(data.pattern);
        if (data.status == 0 || data.status == 3)
            $("#formSubmit").css("display", "block");
        else
            $("#formSubmit").css("display", "none");        
        if (data.status == 1 || data.status == 2) {
            $("#formSave").attr("disabled", "true");
            $("#formReset").attr("disabled", "true");
            $("#paperName").attr("readonly", "true");
            $("#paperDescription").attr("readonly", "true");
            $("#paperPattern").attr("readonly", "true");
        }

    }
    //取回控件上的数据
    function getPaper() {
        requestParams.paperName = $("#paperName").val();
        requestParams.description = $("#paperDescription").val();
        requestParams.pattern = $("#paperPattern").val();
    }

    //保存
    $("#formSave").click(function () {
        if ($("#paperName").val() != "") {
            if ($("#paperName").val().length > 255) {
                alert("试卷名称太长不能超过255！");
                return false;
            }
        }
        else {
            alert("试卷名称不能为空！");
            return false;
        }
        if ($("#paperDescription").val() != "") {
            if ($("#paperDescription").val().length > 1024) {
                alert("试卷名称太长不能超过1024！");
                return false;
            }
        }
        getPaper();
        $.ajax({
            type: 'POST',
            url: commSetting.apiUrl + '/api/exam/paper/update',
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify({
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "id": requestParams.id,
                name: requestParams.paperName,
                description: requestParams.description,
                pattern: requestParams.pattern,
                status: requestParams.status,
                "updater": commSetting.userId,
                "updaterName": commSetting.userName
            }),
            success: function (result) {
                alert('保存成功！');
                window.location.href = "index";
                //init();
            }
        });


    });
    //提交
    $("#formSubmit").click(function () {
        if ($("#paperName").val() != "") {
            if ($("#paperName").val().length > 255) {
                alert("试卷名称太长不能超过255！");
                return false;
            }
        }
        else {
            alert("试卷名称不能为空！");
            return false;
        }
        if ($("#paperDescription").val() != "") {
            if ($("#paperDescription").val().length > 1024) {
                alert("试卷名称太长不能超过1024！");
                return false;
            }
        }
        if (requestParams.status != 0&&requestParams.status != 3) {
            alert("必须是草稿或未通过状态的才可以提交审核！");
            return false;
        }
        else
            requestParams.status = 1;

        getPaper();
        $.ajax({
            type: 'POST',
            url: commSetting.apiUrl + '/api/exam/paper/update',
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify({
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "id": requestParams.id,
                name: requestParams.paperName,
                description: requestParams.description,
                pattern: requestParams.pattern,
                status: requestParams.status,
                "updater": 999,
                "updaterName": "hello"
            }),
            success: function (result) {
                alert('提交审核成功！');
                window.location.href = "index";
            }
        });
    });
    //重置
    $("#formReset").click(function () { init(); });
    //载入配置并回调init方法
    loadSetting(init);
});