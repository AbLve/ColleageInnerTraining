$(document).ready(function () {
    $.datetimepicker.setLocale('ch');
    var requestParams = {
        id: 0,
        name: null,
        startTime: null,
        endTime: null,
        status: null,
        timeUnlimited: 0,
        duration: 0,
        durationUnlimited: 1,
        number: 0,
        numberUnlimited: 1,
        score: 0,
        bookClosed: 0,
        qunordered: 0,
        ounordered: 0,
        opened: 1,
        ipaddrs: "" 
    };

    //保存修改
    $("#formSave").click(function () {
        if (!getExam())
            return;
        $.ajax({
            type: 'Post',
            url: commSetting.apiUrl + '/api/exam/exam/add',
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify({
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: requestParams.id,
                name: requestParams.name,
                startTime: requestParams.startTime,
                endTime: requestParams.endTime,
                status: requestParams.status,
                timeUnlimited: requestParams.timeUnlimited,
                duration: requestParams.duration,
                durationUnlimited: requestParams.durationUnlimited,
                number: requestParams.number,
                numberUnlimited: requestParams.numberUnlimited,
                score: requestParams.score,
                bookClosed: requestParams.bookClosed,
                qunordered: requestParams.qunordered,
                ounordered: requestParams.ounordered,
                opened: requestParams.opened,
                ipaddrs: requestParams.ipaddrs,
                departmentId: commSetting.departmentId,
                "creater": commSetting.userId,
                "createrName": commSetting.userName
            }),
            success: function (data) {
                console.log(data);
                
                alert("保存成功！");
                window.location.href = "Index";
            }, error: function (mes) {
                alert("Server端报错，保存失败！");
            }
        });


    });



    //将数据传到对像中，并对数据进行校验
    function getExam() {    
        if ($("#examName").val() != "") {
            if ($("#examName").val().length > 255) {
                alert("考试名称太长不能超过255！");
                return false;
            }
        }
        else {
            alert("考试名称不能为空！");
            return false;
        }

        if (!$("#timeUnlimited").is(':checked')) {
            if ($("#startTime").val() == "" || $("#endTime").val() == "") {
                alert("限时考试时间必须设置开始时间和结束时间");
                $("#startTime").focus();
                return false;
            }
        }
        if ($("#score").val() == "") {
            alert("通过分数不可以为空！");
            $("#score").focus();
            return false;
        }
        requestParams.name = $("#examName").val()
        requestParams.startTime = $("#startTime").val();
        requestParams.endTime = $("#endTime").val();
        requestParams.status = $("#status").val();

        requestParams.timeUnlimited = $("#timeUnlimited").is(':checked');
        requestParams.duration = $("#duration").val();
        requestParams.durationUnlimited = $("#durationUnlimited").is(':checked');

        requestParams.number = $("#number").val();
        requestParams.numberUnlimited = $("#numberUnlimited").is(':checked');
        requestParams.score = $("#score").val();

        requestParams.bookClosed = $("#bookClosed").is(':checked');
        requestParams.qunordered = $("#qunordered").is(':checked');
        requestParams.ounordered = $("#ounordered").is(':checked');
        requestParams.opened = $("#opened").is(':checked');
        requestParams.ipaddrs = $("#ipaddrs").val();

        return true;


    }
    //时间是否不限控件事件绑定
    $("#timeUnlimited").click(function () {
        if ($("#timeUnlimited").is(':checked')) {
            $("#startTime").val("");
            $("#endTime").val("");
            $("#startTime").attr("disabled", true);
            $("#endTime").attr("disabled", true);
        }
        else {
            $("#startTime").attr("disabled", false);
            $("#endTime").attr("disabled", false);
        }
    });


    //时长是否不限控件事件绑定
    $("#durationUnlimited").click(function () {
        if ($("#durationUnlimited").is(':checked')) {
            $("#duration").val("");
            $("#duration").attr("disabled", true);
        }
        else {
            $("#duration").attr("disabled", false);
        }
    });
    //可考次数是否不限控件事件绑定
    $("#numberUnlimited").click(function () {
        if ($("#numberUnlimited").is(':checked')) {
            $("#number").val("");
            $("#number").attr("disabled", true);
        }
        else {
            $("#number").attr("disabled", false);
        }
    });

    $("#score").keyup(function (e) {
        var c = $(this);
        if (/[^\d]/.test(c.val())) {//替换非数字字符  
            var temp_amount = c.val().replace(/[^\d]/g, '');
            $(this).val(temp_amount);
        }
    });

    //初始化
    function init() {
        $('#startTime').datetimepicker({
            value: new Date(), step: 5, format: 'Y-m-d H:i',
            formatDate: 'Y-m-d H:i'
        });

        $('#endTime').datetimepicker({
            value: new Date(), step: 5, format: 'Y-m-d H:i',
            formatDate: 'Y-m-d H:i'
        });
    }
    //载入配置
    loadSetting(init);

});