$(function () {
    var departmentption = $('#txtDepartment').html();
    $("#txtType").change(function () {
        var tyid = $("#txtType").val();
        if (tyid == 0) {
            $("#txtTypeValue option").remove();
            $("#txtTypeValue").append(departmentption);
        }
        if (tyid == 1) {
            $("#txtTypeValue option").remove();
            $("#txtTypeValue").append(jobOption);
        }
        if (tyid == 2) {
            $("#txtTypeValue option").remove();
            $("#txtTypeValue").append(classOption);
        }
        if (tyid == 3) {
            $("#txtTypeValue option").remove();
        }
    });


});

function set() {
    var typeId = $("#txtType").val();
    var typeValueId = $("#txtTypeValue").val();
    if (typeId == null || typeId == undefined || typeValueId == null || typeValueId == undefined) {
        $.modalMsg('请选择类型', 'warning');
        return false;
    }
    $.getJSON("/Admin/CBCT/SetBoundCourseByType",
     { courseId: CourseID, typeId: typeId, typeCaseId: typeValueId },
     function (result) {
         if (result.status == "0" && result.msg == "") {
             $.modalMsg('设置成功', 'success');
             window.location.href = "/Admin/CBCT/Index?id=" + CourseID;
         }
         else if (result.status == "0" && result.msg != "") {
             $.modalMsg('已有设置的用户为：' + result.msg, 'error');
         }
         else if (result.status == "-1") {
             $.modalMsg(result.msg, 'error');
         }
         else if (result.status == "-2") {
             $.modalMsg(result.msg, 'error');
         }
         else {
             $.modalMsg('设置失败', 'warning');
         }
     });
}

//单个取消
function remove(id, BusinessId) {
    $.getJSON("/Admin/CBCT/CancellBount",
        { courseId: CourseID, typeId: id, businId: BusinessId },
        function (result) {
            if (result.status == "0" && (result.msg == "")) {
                $.modalMsg('取消绑定成功', 'success');
                window.location.href = "/Admin/CBCT/Index?id=" + CourseID;
            }
            else if (result.status == "0" && result.msg != "") {
                $.modalMsg('该用户没有绑定：' + result.msg, 'error');
            } else {
                $.modalMsg('设置失败', 'warning');
            }
        });
}
$("#txtDepartment").bindSelect({
    url: "GetTreeDepartSelectJson"
});