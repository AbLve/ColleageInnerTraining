$(function () {
   
    var InCollection = false;
    //加入收藏
    $('.addCollection').click(function () {
        if (!InCollection) {
            $.ajax({
                type: 'GET',
                url: abp.appPath + 'Wap/Collection/AddCollection',
                data: { bizId: courseId, name: courseName, type: type },
                success: function (result) {
                    alert('加入收藏成功！');
                    window.location.reload();
                }
            });
        }
        else {
            $.ajax({
                type: 'GET',
                url: abp.appPath + 'Wap/Collection/DelCollection',
                data: { bizId: courseId, name: courseName, type: type },
                success: function (result) {
                    alert('取消收藏成功！');
                    window.location.reload();
                    
                }
            });
        }
    }); 

    //报名
    $('.singleUpCourse').click(function () {
        if (!IsSingleUp){
             $.ajax({
                type: 'GET',
                url: abp.appPath + 'Wap/Course/SingleUpCourse',
                data: { courseId: courseId },
                success: function (result) {
                    window.location.reload();
                    alert('报名成功！');
                },
                error: function (result) {
                    alert('报名失败，请刷新重试！');
                }
             });
        }
        else
            alert('您已经报过名了，不可以重复报名！');
    });
    

    function LoadCollection()
    {
        $.ajax({
            type: 'GET',
            url: abp.appPath + 'Wap/Collection/Get',
            data: { bizId: courseId, type: type },
            success: function (result) {
                console.log(result);
                if (result!=null) {
                    InCollection = true;
                    $('.collectionTxt').html("取消收藏");
                   
                }
                else{
                    InCollection = false;
                    $('.collectionTxt').html("收藏");
                }
            }
        });
    }
    function init()
    {
        if (IsSingleUp)
            $('.singleUpCourse').html("已报名");
        if (IsComplete)
            $('.exam_btn').css("display","none");

        $.ajax({
            type: 'GET',
            url: abp.appPath + 'Wap/ReadTimes/AddReadTime',
            data: { bizId: courseId, name: courseName, type: type },
            success: function (result) {
                LoadCollection();
                if (examId != null && examId != 0) {
                    $("#examArea").css("display", "block");
                    examLoad();
                }
                if (pollId != null&&pollId != 0) {
                    $("#pollArea").css("display", "block");
                    pollLoad();
                }
                


            }
        });
    }


    //载入考试信息
    function examLoad() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/exam/get',
            dataType: 'JSON',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: examId
            },
            success: function (data) {
                console.log(data);
                setExam(data);
            }
        });
    }

    function setExam(data) {
        $("#examName").html(data.name);
        if (data.timeUnlimited)
            $("#examDateTime").html("不限时间");
            else
            $("#examDateTime").html(data.startTime + " 至 " + data.endTime);

        if (data.durationUnlimited)
            $("#examDateTime").html("不限时长");
        else
            $("#examTime").html(data.duration);


    }


    //载入考试信息
    function pollLoad() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/poll/get',
            dataType: 'JSON',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: pollId
            },
            success: function (data) {
                console.log(data);
                setPoll(data);
            }
        });
    }

    function setPoll(data) {
        $("#pollName").html(data.name);        

    }



    loadSetting(init);

})