$(document).ready(function () {
    var requestParams = {
        id: pollId,
        pollName: null,
        description: null,
    };
    //载入问卷信息
    function init() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/poll/get',
            dataType: 'JSON',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: requestParams.id
            },
            success: function (data) {
                console.log(data);
                if (data != null) {
                    setPaper(data);
                }
                else {
                    alert("问卷不存在！");
                    location.href = "index";
                }
            }
        });
    }
    //为控件赋值
    function setPaper(data) {
        requestParams.pollName = data.name;
        requestParams.description = data.description;
        $("#pollName").val(data.name);
        $("#pollDescription").val(data.description);

    }
    //取回控件上的数据
    function getPaper() {
        requestParams.pollName = $("#pollName").val();
        requestParams.description = $("#pollDescription").val();
    }

    //保存
    $("#formSave").click(function () {
        if ($("#pollName").val() != "") {
            if ($("#pollName").val().length > 255) {
                alert("问卷名称太长不能超过255！");
                return false;
            }
        }
        else {
            alert("问卷名称不能为空！");
            return false;
        }
        if ($("#pollDescription").val() != "") {
            if ($("#pollDescription").val().length > 1024) {
                alert("问卷名称太长不能超过1024！");
                return false;
            }
        }
        getPaper();
        $.ajax({
            type: 'POST',
            url: commSetting.apiUrl + '/api/exam/poll/update',
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify({
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "id": requestParams.id,
                name: requestParams.pollName,
                description: requestParams.description,
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
    //重置
    $("#formReset").click(function () { init(); });
    //载入配置并回调init方法
    loadSetting(init);
});