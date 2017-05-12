$(function () {
    // 开始考试请求参数
    

    //初始化考试页面，调用开始考试接口
    function initExaming() {
        var params = {
            appkey:commSetting.appkey,
            sTimestamp: commSetting.sTimestamp,
            sign: commSetting.sign,
            examId: examId,//考试id
            userId:commSetting.userId,//当前用户id
            departmentId: commSetting.departmentId,//当前用户部门id
            userName: commSetting.userName,//当前用户姓名
            courseId: courseId//如果是课程配置的考试，则填上课程id
        };
        var json = JSON.stringify(params);
        $.ajax({
            url: commSetting.apiUrl + '/api/exam/examing/start',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: json,
            success: function (data) {
                // 通过返回数据，初始化头信息
                initHeader(data);
                // 通过返回数据，初始化试题信息
                initQuestions(data);
                // 通过返回数据，尾部信息
                initFooter(data);
            },
            error: function (data) {
                alert(data.responseJSON.errmsg);
                location.href = "index";
            }

        });
    }
    //初始化考试时间
    function initHeader(paperView) {
        // 考试时间
        var duration = '考试时间：';
        if (paperView.exam.durationUnlimited) {
            duration += '不限';
        } else {
            duration += paperView.exam.duration + '分钟';
        }
        $('#duration').html(duration);

    }
    //初始化试题
    function initQuestions(paperView) {
        //
        var gettpl = document.getElementById('questions').innerHTML;
        laytpl(gettpl).render(paperView, function (html) {
            document.getElementById('questionWrapper').innerHTML = html;
        });
    }
    //初始化尾部
    function initFooter(paperView) {
        //
        var gettpl = document.getElementById('footer').innerHTML;
        laytpl(gettpl).render(paperView, function (html) {
            document.getElementById('examFooter').innerHTML = html;
        });
    }
    //显示题目
    function showQuestion(index) {
        $('#curIndex').val(index);
        var qNum = $('#questionNumber').val();
        var total = (parseInt(index) + 1) + "/" + qNum;
        $('#totalNumber').html(total);
        $('.question_section').addClass('hide');
        $('.question_section').eq(index).removeClass('hide');
    }
    //上一题
    $('#prev').click(function () {
        var curIndex = $('#curIndex').val();
        if (curIndex == 0) {
            return;
        }
        curIndex--;
        showQuestion(curIndex);
    });
    //下一题
    $('#next').click(function () {
        var curIndex = $('#curIndex').val();
        var qNum = $('#questionNumber').val();        
        if (curIndex < (qNum - 1)) {
            curIndex++
            showQuestion(curIndex);
        }
        else {
            alert("没有下一题了！如果已完成考试请点交卷！")
        }
    });
    //底部题目导航
    $(document).on('click', '.footerNav', function () {
        var index = $(this).attr('data-index');
        showQuestion(index);
    });
    // 作答
    function updateRecordItem(recordItemId, answer) {
        var recordId = $('#recordId').val();
        var params = {
            appkey: commSetting.appkey,//
            sTimestamp:commSetting.sTimestamp,
            sign: commSetting.sign,
            recordId: recordId,//考试记录id
            recordItemId: recordItemId,//考试记录项id
            answer: answer,// 回答
            updater: commSetting.userId,//当前用户id
            updaterName: commSetting.userName//当前用户姓名
        };
        //转为json字符串
        var userAnswer = JSON.stringify(params);
        //更新后台
        $.ajax({
            url: commSetting.apiUrl + '/api/exam/examing/answer',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: userAnswer,
            success: function (data) {
                console.log('updateRecordItem: ' + data);
            }
        });
    }

    // 初始化考试页面
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
            sTimestamp:commSetting.sTimestamp,
            sign: commSetting.sign,
            recordId: recordId,//考试记录id
            updater: commSetting.userId,//当前用户id
            updaterName: commSetting.userName//当前用户姓名
        };
        //转为json字符串
        var record = JSON.stringify(params);
        //更新后台
        $.ajax({
            url: commSetting.apiUrl + '/api/exam/examing/submit',
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: record,
            success: function (data) {
                console.log(data);
                alert("交卷成功，考试完毕，您此次考试的得分为：" + data.score);
                location.href = "index";
                
            }
        });
    }
});
