$(document).ready(function () {
    $.datetimepicker.setLocale('ch');
    var requestParams = {
        id: 0,
        name: null,
        description: null
    };

    //保存修改
    $("#formSave").click(function () {
        if (!getPoll())
            return;
        $.ajax({
            type: 'Post',
            url: commSetting.apiUrl + '/api/exam/poll/add',
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify({
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: requestParams.id,
                name: requestParams.name,
                description: requestParams.description,
                departmentId: commSetting.departmentId,
                "creater": commSetting.userId,
                "createrName": commSetting.userName
            }),
            success: function (data) {
                console.log(data);
                alert("保存成功！");
                window.location.href = "index";

            }, error: function (mes) {
                alert("Server端报错，保存失败！");
            }
        });


    });



    //将数据传到对像中，并对数据进行校验
    function getPoll() {
        if ($("#pollName").val() == "") {
            alert("问卷名称不可以为空！");
            $("#pollName").focus();
            return false;
        } else {
            if ($("#pollName").val().length > 255) {
                alert("问卷名称太长！");
                $("#pollName").focus();
            }
        }
        if ($("#pollDescription").val() != "") {
            if ($("#pollDescription").val().length > 1024) {
                alert("试卷名称太长不能超过1024！");
                return false;
            }
        }
        requestParams.name = $("#pollName").val()
        requestParams.description = $("#pollDescription").val()
        return true;


    }

    function init() { }
    //载入配置
    loadSetting(init);


});