$(document).ready(function () {
    $("#imghead").attr('src', PUrl);


    $("#uploadify_Head").uploadify({
        'buttonText': "上传头像",
        'swf': '/images/uploadfy/uploadify.swf',
        'uploader': abp.appPath + 'Admin/TeacherUser/UploadfyPic',
        'fileObjName': 'importFile',
        'fileTypeDesc': 'excel Files',
        'fileTypeExts': '*.gif; *.jpg; *.png',
        'formData': {
            'appkey': '', 'sign': '', 'sTimestamp': '', 'creater': '999', 'createrName': 'hello', 'departmentId': '999'
        },
        'onUploadSuccess': function (file, data, response) {
            var result = JSON.parse(data);
            if (result.IsSuccess == "0") {
                PUrl = result.filePath;
                $("#imghead").attr('src', result.filePath);
                $.modalMsg("上传成功");
            }
            else {
                $.modalMsg('上传失败！');
            }
        },
        'onUploadError': function (file, errorCode, errorMsg, errorString) {
            console.log('errorCode: ' + errorCode + ", errorMsg: " + errorMsg + ", errorString: " + errorString);
        }
    });

    $("#subTeacher").click(function () {
        $("#PortraitUrl").val(PUrl);

        $("#fmTeacher").submit();
    })
});


