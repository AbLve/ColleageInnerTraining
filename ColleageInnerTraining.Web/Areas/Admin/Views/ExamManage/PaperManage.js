$(document).ready(function () {
    //分页
    var pageSize = 10000;
    var pageNumber = 1;
    var totalPage = 0;
    //参数对像
    var requestParams = {
        id:examId,
        skipCount: 0,
        pageNumber: pageNumber,
        maxResultCount: pageSize,
        sorting: null,
        name: null,
        content: null,
        enabled: null,
        status: null,
        knowledgeIds: null,
        departmentId: null,
        ids: null
    };

    

    function paperLoad() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/paper/list',
            dataType: 'JSONP',
            jsonp: 'jsonp',
            jsonpCallback: 'callback',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "pageSize": requestParams.maxResultCount,
                "currentPage": requestParams.pageNumber,
                "name": requestParams.name,
                "enabled": requestParams.enabled,
                "status": 2,
                "departmentId": requestParams.departmentId,
                "pattern": requestParams.pattern
            },
            success: function (result) {
                var gettpl = document.getElementById('papers').innerHTML;
                laytpl(gettpl).render(result, function (html) {
                    console.log(result.data);
                    $('#paperId').html("");
                    if (result.data != null) {
                        $('#paperId').html(html);
                    }
                });
            }
        });

    }

    //载入试题信息
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
                var gettpl = document.getElementById('questions').innerHTML;
                laytpl(gettpl).render(data, function (html) { 
                    console.log(data);
                $('#Render_List').html("");
                if (data.papers != null) {
                    $('#Render_List').html(html);
                }
                $('#examName').html(data.name);
                paperLoad();

                $(".deleteExamPaper").click(function () {
                    if (!window.confirm("您确认要删除试卷吗？"))
                        return;
                    var paperId = $(this).attr("title");
                    $.ajax({
                        type: 'POST',
                        url: commSetting.apiUrl + '/api/exam/exam/deleteExamPaper',
                        sTimestamp: commSetting.sTimestamp,
                        appkey: commSetting.appkey,
                        sign: commSetting.sign,
                        dataType: 'JSON',
                        contentType: 'application/json',
                        data: JSON.stringify({
                            sTimestamp: commSetting.sTimestamp,
                            appkey: commSetting.appkey,
                            sign: commSetting.sign,
                            "examId": requestParams.id,
                            "paperId": paperId,
                            "updater": commSetting.userId,
                            "updaterName": commSetting.userName
                        }),
                        success: function (result) {
                            alert('删除成功！');
                            init();
                        }
                    });
                });


                });
            }
        });
    }

    //载入参数及数据
    loadSetting(init);

    //添加试卷
    $("#formSave").click(function () {
        var paperId = $('#paperId').val();
        if (paperId == ""){
            alert("考卷不能为空！");
            return;
        }
        $.ajax({
            type: 'POST',
            url: commSetting.apiUrl + '/api/exam/exam/saveExamPaper',
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify({
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "examId": requestParams.id,
                "paperId":paperId,
                name: requestParams.paperName,
                description: requestParams.description,
                pattern: requestParams.pattern,
                status: requestParams.status,
                departmentId: commSetting.departmentId,
                "creater": commSetting.userId,
                "createrName": commSetting.userName
            }),
            success: function (result) {
                alert('保存成功！');
                init();
            },
            error: function (result) {
                alert(result);
                init();
            }
        });

    });




});

