$(document).ready(function () {
    var requestParams = {
        id: 0,
        paperName: null,
        description: null,
        pattern: null,
        departmentId:1,
        status: 0
    };

    
    function getPaper() {
        requestParams.paperName = $("#paperName").val();
        requestParams.description = $("#paperDescription").val();
        requestParams.pattern = $("#paperPattern").val();

    }

    //提交保存
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
            url: commSetting.apiUrl + '/api/exam/paper/add',
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
                departmentId: commSetting.departmentId,
                "creater": commSetting.userId,
                "createrName": commSetting.userName
            }),
            success: function (result) {
                alert('保存成功！');
                window.location.href = "index";
            }
        });


    });
    //初始化
    function init() {
        $("#paperName").val();
        $("#paperDescription").val();
        $("#paperPattern").val();
    }
    //载入配置
    loadSetting(init);
    //重置
    $("#formReset").click(function () { init(); });
});