$(document).ready(function () {
    var requestParams = {
        id: pollId,
        pollName: null,
        description: null,
    };

    //下载模板
    $("#templeDown").click(function () {
        var url = abp.appPath + "Areas/Admin/Views/Poll/FileTemple/importPollQuestion.xls";
        window.open(url, "_blank");

    });

    function dataLoad(result) {         
        $('#pollName').html(result.name);
    }
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
            success: function (result) {
                console.log(result);
                var gettpl = document.getElementById('questions').innerHTML;
                laytpl(gettpl).render(result, function (html) {
                    $('#Render_List').html("");
                    if (result.pollQuestionList != null) {
                        $('#Render_List').html(html);
                    }
                    //问卷信息
                    dataLoad(result);
                    $('#imageFile').uploadify({
                        'buttonImage': '/images/browse-btn.png',
                        'swf': '/images/uploadfy/uploadify.swf',
                        'uploader': commSetting.apiUrl + '/api/exam/pollquestion/import',
                        'fileObjName': 'importFile',
                        'fileTypeDesc': 'excel Files',
                        'fileTypeExts': '*.xls; *.xlsx',
                        'formData': {
                            pollId: pollId,
                            'appkey': commSetting.appkey.toString(),
                            'sign': commSetting.sign.toString(),
                            'sTimestamp': commSetting.sTimestamp.toString(),
                            'creater': commSetting.userId.toString(),
                            'createrName': commSetting.userName,
                            'departmentId': commSetting.departmentId.toString()
                        },
                        'onUploadSuccess': function (file, data, response) {
                            var result = JSON.parse(data);
                            if (result.errcode == "0") {
                                alert("上传成功");
                                init();
                            }
                            else {
                                alert('上传失败！');
                            }
                            console.log(result.errcode);
                        },
                        'onUploadError': function (file, errorCode, errorMsg, errorString) {
                            console.log('errorCode: ' + errorCode + ", errorMsg: " + errorMsg + ", errorString: " + errorString);
                        }
                    });

                });
            }
        });
    }


    loadSetting(init);


});