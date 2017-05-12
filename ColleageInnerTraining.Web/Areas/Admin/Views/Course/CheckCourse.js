$(':submit').click(function () {
    var txtcname = $('#courseName').val() == "" ? "" : $('#courseName').val();
    var txttname = $('#txtTeacherName').val() == "" ? "" : $('#txtTeacherName').val();
    var txttype = $('#txtType').val();
    var txtcate = $('#txtCate').val();
    var strurl = $.StringFormat("GetCheckCourseDataList?txtcname={0}&txttname={1}&txttype={2}&txtcate={3}", txtcname, txttname, txttype, txtcate);
    $.loading(true, "���ڼ�������....");
    window.setTimeout(function () {
        $('div#dataListDiv').load(strurl, function () {
            $.loading(false);
        });
    }, 500);
});

//���ͨ������˲�ͨ��
function CheckCourse(id, status, cStatus) {
    if (cStatus == "2" || cStatus == "3") {
        $.modalMsg('�ÿγ��Ѿ���˹���', 'error');
        return false;
    }
    $.modalConfirm('��ȷ��Ҫ�����',
        function (a) {
            if (a) {
                $.get("/Admin/Course/Check",
                    { id: id, status: status },
                    function (result) {
                        if (result == "0") {
                            location.href = "/Admin/Course/CheckCourse"
                        } else {
                            $.modalMsg('���ʧ��', 'error');
                        }
                    })
            }


        });
}