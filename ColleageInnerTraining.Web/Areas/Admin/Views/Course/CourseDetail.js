$(function () {
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
                id: ExamId
            },
            success: function (data) {
                if (data != null) {
                    $("#ExamName").html(data.name);
                }
                initPoll()
            }
        });
    }

    //载入试卷信息
    function initPoll() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/poll/get',
            dataType: 'JSON',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: PollId
            },
            success: function (data) {
                if (data != null) {
                    $("#PollName").html(data.name);
                }
            }
        });
    }
    //载入参数及数据
    loadSetting(init);
    $("#imghead").attr('src', imgUrl);
    $("#ContentText").html(htmlDecode(Content));
})
function htmlDecode(value) {
    return $('<div/>').html(value).text();
}