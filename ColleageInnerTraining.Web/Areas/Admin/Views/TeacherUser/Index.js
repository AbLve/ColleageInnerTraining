//审核通过，审核不通过
function CheckCourse(id, status, cStatus) {
    if (cStatus == "2" || cStatus == "3") {
        $.modalMsg('该内训师已经审核过了', 'error');
        return false;
    }
    $.modalConfirm('您确定要审核吗？',
        function (a) {
            if (a) {
                $.get("/Admin/TeacherUser/Check",
                    { id: id, status: status },
                    function (result) {
                        if (result == "0") {
                            location.href = "/Admin/TeacherUser/Index"
                        } else {
                            $.modalMsg('审核失败', 'error');
                        }
                    })
            }
        });
}