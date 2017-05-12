$(document).ready(function () {

    $.datetimepicker.setLocale('ch');
    var requestParams = {
    id: examId,
    name: null,
    startTime: null,
    endTime: null,
    status:null,
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
    papers: [],
    ipaddrs:""
};

    //为控件赋值
    function setExam(data) {
        requestParams.name = data.name;
        requestParams.startTime = data.startTime;
        requestParams.endTime = data.endTime;
        requestParams.status = data.status;
        requestParams.timeUnlimited = data.timeUnlimited;
        requestParams.duration = data.duration;
        requestParams.durationUnlimited = data.durationUnlimited;
        requestParams.number = data.number;
        requestParams.numberUnlimited = data.numberUnlimited;
        requestParams.score = data.score;
        requestParams.bookClosed = data.bookClosed;
        requestParams.qunordered = data.qunordered;
        requestParams.ounordered = data.ounordered;
        requestParams.opened = data.opened;
        requestParams.papers = data.papers;
        requestParams.ipaddrs = data.ipaddrs;
        //为控件赋值
        $("#examName").val(data.name);
       
        if (data.timeUnlimited) {
            $("#timeUnlimited").prop("checked", true);
            $("#startTime").attr("disabled",true);
            $("#endTime").attr("disabled", true);
        }
        else {
            $("#timeUnlimited").prop("checked", false);
            $("#startTime").val(data.startTime);
            $("#endTime").val(data.endTime);
            
        }
        
        if (data.durationUnlimited){
            $("#durationUnlimited").prop("checked", true);
            $("#duration").attr("disabled", true);
            $("#duration").val("");
        }
        else{
            $("#durationUnlimited").prop("checked", false);
            $("#duration").val(data.duration);
        }

        
        if (data.numberUnlimited){
            $("#numberUnlimited").prop("checked", true);
            $("#number").attr("disabled", true);
            $("#number").val("");
        }
        else{
            $("#numberUnlimited").prop("checked", false);
            $("#number").val(data.number);
        }
        $("#score").val(data.score);
        if (data.bookClosed)
            $("#bookClosed").prop("checked", true);
        else
            $("#bookClosed").prop("checked", false);
        if (data.qunordered)
            $("#qunordered").prop("checked", true);
        else
            $("#qunordered").prop("checked", false);
        if (data.ounordered)
            $("#ounordered").prop("checked", true);
        else
            $("#ounordered").prop("checked", false);
        if (data.opened)
            $("#opened").prop("checked", true);
        else
            $("#opened").prop("checked", false);
        $("#ipaddrs").val(data.ipaddrs);

        $('#startTime').datetimepicker({
            value: data.startTime, step: 5, format: 'Y-m-d H:i',
            formatDate: 'Y-m-d H:i'
        });

        $('#endTime').datetimepicker({
            value: data.endTime, step: 5, format: 'Y-m-d H:i',
            formatDate: 'Y-m-d H:i'
        });
         
        $("#score").keyup(function (e) {
            var c=$(this);  
            if(/[^\d]/.test(c.val())){//替换非数字字符  
                var temp_amount=c.val().replace(/[^\d]/g,'');  
                $(this).val(temp_amount);  
            }  
        });

        if (data.status == "0") {
            $("#formSubmit").css("display", "block");
        }
        else {
            $("#formSubmit").css("display", "none");
        }

        
    }



    //载入考试信息
    function init() {

        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/exam/get',
            dataType: 'JSON',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: requestParams.id
            },
            success: function (data) {
                console.log(data);
                setExam(data);
            }
        });
    }
    //载入参数及数据
    loadSetting(init);

     //保存修改
    $("#formSave").click(function () {
        save();
    });

    //提交修改
    $("#formSubmit").click(function () {
        requestParams.status = 1;
        save();
    });

    function save() {


        if (!getExam())
            return;
        $.ajax({
            type: 'Post',
            url: commSetting.apiUrl + '/api/exam/exam/update',
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
                papers: requestParams.papers,
                "updater": commSetting.userId,
                "updaterName": commSetting.userName
            }),
            success: function (data) {
                console.log(data);
                alert("保存成功！");
                window.location.href="index"
                init()
            }, error: function (mes) {
                alert("Server端报错，保存失败！");
            }
        });

    }



    //将数据传到对像中，并对数据进行校验
    function getExam() {
        if ($("#examName").val() == "") {
            alert("考试名称不可以为空！");
            $("#examName").focus();
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
    

});