$(document).ready(function () { 
   

    var requestParams = {
        id:paperId,
        paperName: null,
        description: null,
        pattern: null,
        status: 0,
        checkMessage:null
    };
     
    function dataLoad(result) {

        if (result.status == 0||result.status == 3)
            $('#uploadQuestion').css("display", "block");
        else
            $('#uploadQuestion').css("display", "none");

        if (result.status == 1) {
            $('#formApproe').css("display", "block");
            $('#formBack').css("display", "block");
            $('#checkMessage').css("display", "block");
            $('#checkMessageLable').css("display", "block");
        }
        else {
            $('#formApproe').css("display", "none");
            $('#formBack').css("display", "none");
            $('#checkMessage').css("display", "none");
            $('#checkMessageLable').css("display", "none");
        }
        $('#paperName').html(result.name);

        requestParams.status = result.status;
    }

    //下载模板
    $("#templeDown").click(function () {
        var url = abp.appPath + "Areas/Admin/Views/ExamPaper/FileTemple/importPaperQuestion.xls";
        window.open(url, "_blank");

    });
    function init(){
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
            success: function (result) {
                console.log(result.paperQuestions);
                var gettpl = document.getElementById('questions').innerHTML;
                laytpl(gettpl).render(result, function (html) { 
                    $('#Render_List').html("");
                    if (result.paperQuestions != null) {
                        $('#Render_List').html(html);
                    }
                    
                    dataLoad(result);

                    $('#imageFile').uploadify({
                        'buttonImage': '/images/browse-btn.png',
                        'swf': '/images/uploadfy/uploadify.swf',
                        'uploader': commSetting.apiUrl + '/api/exam/paper/importPaperQuestion',
                        'fileObjName': 'importFile',
                        'fileTypeDesc': 'excel Files',
                        'fileTypeExts': '*.xls; *.xlsx',
                        'formData': {
                            paperId: paperId,
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



    //保存
    $("#formApproe").click(function () {
        if (requestParams.status != 1) {
            alert("必须是待审核状态的记录才能审核！");
            return false;
        }
        else
            requestParams.status = 2;
        save();
    });

    //退回
    $("#formBack").click(function () {

        if (requestParams.status != 1) {
            alert("必须是待审核状态的记录才能审核！");
            return false;
        }
        else
            requestParams.status = 3;
        save();
    });

    function save() {
        requestParams.checkMessage = $("#checkMessage").val();
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
                alert('审核完成！');
                window.location.href = "index";
                //init();
            }
        });

    }

});