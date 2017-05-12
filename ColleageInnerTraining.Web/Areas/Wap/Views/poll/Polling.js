$(function () {
    // 开始问卷请求参数


    //初始化问卷页面，调用开始问卷接口
    function initExaming() {
        var params = {
            appkey: commSetting.appkey,
            sTimestamp: commSetting.sTimestamp,
            sign: commSetting.sign,
            pollId: pollId,//问卷id
            userId: commSetting.userId,//当前用户id
            departmentId: commSetting.departmentId,//当前用户部门id
            username: commSetting.userName
        };
        var json = JSON.stringify(params);
        $.ajax({
            url: commSetting.apiUrl + '/api/exam/polling/start',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: json,
            success: function (data) {
                console.log(data);

                // 通过返回数据，初始化头信息
                initHeader(data);
                // 通过返回数据，初始化试题信息
                initQuestions(data);
            },
            error: function (data) {
                alert(data.responseJSON.errmsg);
                window.history.back();
            }

        });
    }
    //初始化问卷时间
    function initHeader(paperView) {
        // 问卷时间
        var pollName = '问卷名称：';
        pollName = paperView.pollRecord.pollName;
        var recordId = paperView.pollRecord.id;

        $('#pollName').html(pollName);
        $('#recordId').val(recordId);

    }
    //初始化试题
    function initQuestions(pollView) {
        //
        var gettpl = document.getElementById('questions').innerHTML;
        laytpl(gettpl).render(pollView, function (html) {
            document.getElementById('questionPoll').innerHTML = html;
        });
    }
   
    //显示题目
    function showQuestion(index) {
        $('#curIndex').val(index);
        var qNum = $('#questionNumber').val();
        var total = (parseInt(index) + 1) + "/" + qNum;
    }

    // 作答
    function updateRecordItem(recordItemId, answer) {
        var recordId = $('#recordId').val();
        var params = {
            appkey: commSetting.appkey,//
            sTimestamp: commSetting.sTimestamp,
            sign: commSetting.sign,
            recordId: recordId,//问卷记录id
            recordItemId: recordItemId,//问卷记录项id
            answer: answer,// 回答
            updater: commSetting.userId,//当前用户id
            updaterName: commSetting.userName//当前用户姓名
        };
        //转为json字符串
        var userAnswer = JSON.stringify(params);
        //更新后台
        $.ajax({
            url: commSetting.apiUrl + '/api/exam/polling/answer',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: userAnswer,
            success: function (data) {
                console.log('updateRecordItem: ' + data);
            }
        });
    }

    // 初始化问卷页面
    loadSetting(initExaming);
    //单选题和判断题回答
    $(document).on('click', 'input[data-type="SINGLE"],input[data-type="JUDGE"]', function () {
        var recordItemId = $(this).attr('name');
        var answer = $(this).attr('data-ident');
        console.log(recordItemId + "," + answer);
        updateRecordItem(recordItemId, answer);
    });
    //多选题和不定项选择题
    $(document).on('click', 'input[data-type="MULTI"],input[data-type="INDEF"]', function () {
        var $input = $(this);
        var recordItemId = $input.attr('name');
        var answer;
        $input.parent().parent().find('input:checked').each(function (i) {
            if (i == 0) {
                answer = $(this).attr('data-ident');
            } else {
                answer += '|' + $(this).attr('data-ident');
            }
        });
        console.log(recordItemId + "," + answer);
        updateRecordItem(recordItemId, answer);
    });
    //填空题
    $(document).on('blur', 'input[data-type="BLANK"]', function () {
        var $input = $(this);
        var recordItemId = $input.attr('name');
        var answer;
        $input.parent().find('input[data-type="BLANK"]').each(function () {
            if (answer) {
                answer += '|' + $(this).val();
            } else {
                answer = $(this).val();
            }
        });
        console.log(recordItemId + "," + answer);
        updateRecordItem(recordItemId, answer);
    });
    //问答题
    $(document).on('blur', 'textarea[data-type="ESSAY"]', function () {
        var $input = $(this);
        var recordItemId = $input.attr('name');
        var answer = $(this).val();
        console.log(recordItemId + "," + answer);
        updateRecordItem(recordItemId, answer);
    });

    $('#submit').click(function () {
        submit();
    });

    //交卷
    function submit() {
        var recordId = $('#recordId').val();
        var params = {
            appkey: commSetting.appkey,//
            sTimestamp: commSetting.sTimestamp,
            sign: commSetting.sign,
            recordId: recordId,//问卷记录id
            userId: commSetting.userId,//当前用户id
            username: commSetting.userName//当前用户姓名
        };
        //转为json字符串
        var record = JSON.stringify(params);
        //更新后台
        $.ajax({
            url: commSetting.apiUrl + '/api/exam/polling/submit',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: record,
            success: function (data) {
                console.log(data);
                alert("问卷提交成功，问卷完毕");
                location.href = "index";

            }
        });
    }
});
