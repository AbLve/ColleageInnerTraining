$(document).ready(function () {
    var requestParams = {
        id: paperId,
        paperName: null,
        description: null,
        pattern: null,
        status: 0,
        checkMessage: null

    };
    //载入试卷信息
    function init() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/paper/get',
            dataType: 'JSON',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: requestParams.id
            },
            success: function (data) {
                if (data.status != 1) {
                    alert("必须是待审核状态的记录才能审核！");
                    location.href = "index";
                }
                setPaper(data);
            }
        });
    }

    function setPaper(data) {
        requestParams.paperName = data.name;
        requestParams.description = data.description;
        requestParams.pattern = data.pattern;
        requestParams.status = data.status;
        $("#paperName").val(data.name);
        $("#paperDescription").val(data.description);
        $("#paperPattern").val(data.pattern);
        $("#checkMessage").val(data.checkMessage);

        if (data.status == 0)
            $("#formSubmit").css("display", "block");
        else
            $("#formSubmit").css("display", "none");

    }
    function getPaper() {
        requestParams.paperName = $("#paperName").val();
        requestParams.description = $("#paperDescription").val();
        requestParams.pattern = $("#paperPattern").val();
        requestParams.checkMessage = $("#checkMessage").val();
    }
    //保存
    $("#formApproe").click(function () {
        if (requestParams.status != 1) {
            alert("必须是待审核状态的记录才能进行审核！");
            return false;
        }
        else
            requestParams.status = 2;
        save();
    });

    //退回
    $("#formBack").click(function () {        
        if (requestParams.status != 1) {
            alert("必须是待审核状态的记录才能进行审核！");
            return false;
        }
        else
            requestParams.status = 3;
        save();
    });

    function save() {
        getPaper();
        $.ajax({
            type: 'POST',
            url: commSetting.apiUrl + '/api/exam/paper/updateStatus',
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify({
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "id": requestParams.id,
                status: requestParams.status,
                checkMessage: requestParams.checkMessage,
                "updater": commSetting.userId,
                "updaterName": commSetting.userName
            }),
            success: function (result) {
                alert('保存成功！');
                window.location.href = "index";
                //init();
            }
        });

    }
    //重置
    $("#formReset").click(function () { init(); });
    //载入配置并回调init方法
    loadSetting(init);
});