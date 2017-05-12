var ue = UE.getEditor('editor');
ue.addListener("ready", function () {
    ue.setContent($.htmldecode($('#Content').val()));
});


function getContent() {
    var html = $.htmlencode(ue.getContent());
    $('#Content').val(html);
}

$("#uploadbtn").uploadify({
    buttonText: "上传图片",
    swf: '/images/uploadfy/uploadify.swf',
    fileSizeLimit: "2mb",
    uploader: 'UploadFile',
    fileObjName: 'knowledgeImg',
    fileTypeDesc: 'excel Files',
    fileTypeExts: '*.gif; *.jpg; *.png',
    formData: { appkey: '', sign: '', sTimestamp: '', create: '999', createrName: 'hello', departmentId: '999' },
    onUploadSuccess: function (file, data, response) {
        var result = JSON.parse(data);
        if (result.type === 'success') {
            $('#ImageUrl').val(result.data);
            $('#previewimg').attr('src', result.data);
            $.modalMsg(result.msg, result.type);
        }
    },
    onUploadError: function (file, errorCode, errorMsg, errorString) {
        console.log('errorCode: ' + errorCode + ", errorMsg: " + errorMsg + ", errorString: " + errorString);
    }
});

$("#TypeId").bindSelect({
    url: "GetCTreeSelectJson"
});