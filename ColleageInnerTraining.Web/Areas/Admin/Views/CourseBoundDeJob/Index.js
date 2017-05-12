function set() {
    //部门
    var txtdepartmentId = $("#tetDepartment").val();
    var txtdepartmentName = $("#tetDepartment").find("option:selected").text();
    //岗位
    var txtJobpostId = $("#txtJobPost").val();
    var txtJobpostName = $("#txtJobPost").find("option:selected").text();
    $.getJSON("/Admin/CourseBoundDeJob/SetBoundCourseBydepatOrJob",
    {
        courseId: CourseID, departId: txtdepartmentId, departName: txtdepartmentName,
        jobId: txtJobpostId, jobName: txtJobpostName
    },
    function (result) {
        if (result.status == "0" && result.msg == "") {
            $.modalMsg('设置成功', 'success');
            window.location.href = "/Admin/CourseBoundDeJob/Index?id=" + CourseID;
        }
        else if (result.status == "0" && result.msg != "") {
            $.modalMsg('已有设置的用户为：' + result.msg, 'error');
        } else {
            $.modalMsg('设置失败', 'warning');
        }
    });
}

function remove(cId, dId, jId) {
    $.getJSON("/Admin/CourseBoundDeJob/CancellBount",
        {
            courseId: cId, departId: dId, jobId: jId
        },
        function (result) {
            if (result.status == "0" && (result.msg == "")) {
                $.modalMsg('取消绑定成功', 'success');
                window.location.href = "/Admin/CourseBoundDeJob/Index?id=" + cId;
            }
            else if (result.status == "0" && result.msg != "") {
                $.modalMsg('该用户没有绑定：' + result.msg, 'error');
            } else {
                $.modalMsg('设置失败', 'warning');
            }
        });
}