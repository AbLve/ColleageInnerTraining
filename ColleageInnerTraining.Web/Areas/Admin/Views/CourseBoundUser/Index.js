$(':submit').click(function () {
    var userName = $('#txtUserName').val() == "" ? "" : $('#txtUserName').val();
    var demartment = $('#txtDepartment').val();
    var ibound = $('#IsBound').val() == "" ? "0" : $('#IsBound').val();
    var strurl = $.StringFormat("GetBoundUserDataList?userName={0}&demartment={1}&ibound={2}&courseID={3}"
        , userName, demartment, ibound, CourseID);
    $.loading(true, "正在加载数据....");
    window.setTimeout(function () {
        $('div#dataListDiv').load(strurl, function () {
            $.loading(false);
        });
    }, 500);
})

//绑定用户(多个)
function setmanyUser() {
    var checkData = $('[name=cbsingle]:checked');
    if (checkData.length < 1) {
        $.modalMsg('请选择一项', 'warning');
    }
    else {
        var userList = new Array();//设置人员
        //var learnuserIds = new Array();//必学人员
        checkData.each(function () {
            var _this = $(this);
            var parenttr = _this.parent().parent();
            var userId = parenttr.children('td').eq(1).html(); 
            //var objlearn = parenttr.children('td').eq(5).children();
            //if ($(objlearn).is(':checked')) {
            //    learnuserIds.push(userId);
            //}
            userList.push(userId);
        });

        var strListUserId = userList.join(',');
        //var strLearnListUserId = learnuserIds.join(',');
        $.getJSON("/Admin/CourseBoundUser/SetBoundCourseByUser",
            { courseId: CourseID, userIds: strListUserId },
            function (result) {
                if (result.status == "0" && result.msg == "") {
                    $.modalMsg('设置成功', 'success');
                    $(':submit').trigger('click');
                }
                else if (result.status == "0" && result.msg != "") {
                    $.modalMsg('已有设置的用户为：' + result.msg, 'error');
                }
                else if (result.status == "-1") {
                    $.modalMsg(result.msg, 'error');
                }
                else {
                    $.modalMsg('设置失败', 'warning');
                }
            });
    }
}

//单个绑定
function set(id) {
    var _this = this;
    $.getJSON("/Admin/CourseBoundUser/SetBoundCourseByUser",
 { courseId: CourseID, userIds: id },
 function (result) {
     if (result.status == "0" && result.msg == "") {
         $.modalMsg('设置成功', 'success');
         $(':submit').trigger('click');
     }
     else if (result.status == "0" && result.msg != "") {
         $.modalMsg('已有设置的用户为：' + result.msg, 'error');
     }
     else if (result.status == "-1") {
         $.modalMsg(result.msg, 'error');
     }
     else {
         $.modalMsg('设置失败', 'warning');
     }
 });
}

//单个取消
function remove(id) {
    $.getJSON("/Admin/CourseBoundUser/CancellBount",
        { courseId: CourseID, userIds: id },
        function (result) {
            if (result.status == "0" && (result.msg == "")) {
                $.modalMsg('取消绑定成功', 'success');
                $(':submit').trigger('click');
            }
            else if (result.status == "0" && result.msg != "") {
                $.modalMsg('该用户没有绑定：' + result.msg, 'error');
            } else {
                $.modalMsg('设置失败', 'warning');
            }
        });
}

$(function () {
    $("#IsBound").change(function () {
        $(':submit').trigger('click');
    });
})
$("#txtDepartment").bindSelect({
    url: "GetTreeDepartSelectJson"
});