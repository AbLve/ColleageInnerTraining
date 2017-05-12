var setting = {
    view: {
        dblClickExpand: false
    },
    data: {
        simpleData: {
            enable: true,
            idKey: "id",
            pIdKey: "parentId",
            rootPId: 0
        }
    },
    callback: {
        beforeClick: beforeClick,
        onClick: onClick
    }
};
var zNodes = [];
function beforeClick(treeId, treeNode) {
    return true;
}
function onClick(e, treeId, treeNode) {   
    var zTree = $.fn.zTree.getZTreeObj("treeList"),
    nodes = zTree.getSelectedNodes(),
    v = "";
    knowledgeId = "";
    nodes.sort(function compare(a, b) { return a.id - b.id; });
    for (var i = 0, l = nodes.length; i < l; i++) {
        v += nodes[i].name + ",";
        knowledgeId += nodes[i].id + ",";
    }
    if (v.length > 0) v = v.substring(0, v.length - 1);
    var knowledgeNameObj = $("#knowledgeName");
    var knowledgeIdObj = $("#knowledgeId");
    knowledgeNameObj.attr("value", v);
    knowledgeIdObj.attr("value", knowledgeId);
}

function showMenu() {
    var cityObj = $("#knowledgeName");
    var cityOffset = $("#knowledgeName").offset();
    $("#menuContent").css({ left: cityOffset.left + "px", top: cityOffset.top + cityObj.outerHeight() + "px" }).slideDown("fast");
    $("body").bind("mousedown", onBodyDown);
}

function hideMenu() {
    $("#menuContent").fadeOut("fast");
    $("body").unbind("mousedown", onBodyDown);
}
function onBodyDown(event) {
    if (!(event.target.id == "menuBtn" || event.target.id == "menuContent" || $(event.target).parents("#menuContent").length > 0)) {
        hideMenu();
    }
}



 
$(document).ready(function () {
    //分页
    var pageSize = 10;
    var pageNumber = 1;
    var totalPage = 0;
    //参数对像
    var requestParams = {
        skipCount: 0,
        pageNumber: pageNumber,
        maxResultCount: pageSize,
        sorting: null,
        type:null,
        content:null,
        enabled:null,
        level: null,
        knowledgeIds: null,
        departmentId: null,
        ids:null
    };



    //取得被选择的对像
    function getSelectedIds() {
        var checkObj = $("#questionTable :checkbox");
        var ids = "";
        for (var i = 0; i < checkObj.length; i++) {
            if (checkObj[i].id == "selected"&&checkObj[i].checked)
                ids += checkObj[i].value + ",";
        }
        if (ids != "")
            ids = ids.substring(0, ids.length - 1);
        requestParams.ids = ids;
    }
    //传参对像取值
    function setRequestParams()
    {
        requestParams.pageNumber = 1;
        requestParams.type = null;
        requestParams.content = null;
        requestParams.enabled = null;
        requestParams.level = null;
        requestParams.knowledgeIds = null;
        requestParams.departmentId = null;

        if ($("#type").val() != "")
            requestParams.type = $("#type").val();
        if ($("#content").val() != "")
            requestParams.content = $("#content").val();
        if ($("#enabled").val() != "")
            requestParams.enabled = $("#enabled").val();
        if ($("#level").val() != "")
            requestParams.level = $("#level").val();
        if ($("#knowledgeId").val() != "") {
            var knowledgeIds = $("#knowledgeId").val();
            knowledgeIds = knowledgeIds.substring(0, knowledgeIds.length - 1)
            requestParams.knowledgeIds = knowledgeIds;
        }
        if ($("#departmentId").val() != "") {
            requestParams.departmentId = $("#departmentId").val();
        }
    }
    //查询试题
    $("#btnSearch").click(function () {
       
        setRequestParams();
        init();
    })

    //导出试题
    $("#exportExam").click(function () {
        setRequestParams();
        init();
        var url = commSetting.apiUrl + "/api/exam/question/export?sTimestamp=" + commSetting.sTimestamp
        + "&appkey=" + commSetting.appkey + "&sign=" + commSetting.sign;
        if (requestParams.type != null)
            url += "&type=" + requestParams.type
        if (requestParams.content != null)
            url += "&content=" + requestParams.content
        if (requestParams.enabled != null)
            url += "&enabled=" + requestParams.enabled
        if (requestParams.level != null)
            url += "&level=" + requestParams.level
        if (requestParams.knowledgeIds != null)
            url += "&knowledgeIds=" + requestParams.knowledgeIds
        if (requestParams.departmentId != null)
            url += "&departmentId=" + requestParams.departmentId
        window.open(url);
    });
    //下载模板
    $("#templeDown").click(function () {
        var url = abp.appPath + "Areas/Admin/Views/ExamList/FileTemple/importQuestion.xls";
        window.open(url,"_blank");
    
    });
    //载入试题
    function init() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl + '/api/exam/question/list',
            dataType: 'JSONP',
            jsonp: 'jsonp',
            jsonpCallback: 'callback',
            data: {
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                "pageSize": requestParams.maxResultCount,
                "currentPage": requestParams.pageNumber,
                "type": requestParams.type,
                "content": requestParams.content,
                "enabled": requestParams.enabled,
                "level": requestParams.level,
                "knowledgeIds": requestParams.knowledgeIds,
                "departmentId": requestParams.departmentId
            },
            success: function (result) {
                var gettpl = document.getElementById('questions').innerHTML;
                laytpl(gettpl).render(result, function (html) {
                    totalPage = result.totalPage;
                    $('#Render_List').html("");
                    if (result.data != null){
                        $('#Render_List').html(html);
                        pager(result.currentPage, result.totalPage);
                        $("#PageNext").bind("click", function () {
                            pageNumber++;
                            if (pageNumber > totalPage) { pageNumber = totalPage }
                            requestParams.skipCount = (pageNumber - 1) * pageSize;
                            requestParams.pageNumber = pageNumber;
                            requestParams.maxResultCount = pageSize;
                            init();
                        });

                        $("#PagePrev").bind("click", function () {
                            pageNumber--;
                            if (pageNumber <= 0) { pageNumber = 1 }
                            requestParams.skipCount = (pageNumber - 1) * pageSize;
                            requestParams.pageNumber = pageNumber;
                            requestParams.maxResultCount = pageSize;
                            init();
                        });

                        $(".goPage").bind("click", function () {
                            pageNumber = this.innerHTML;
                            if (pageNumber < 0) { pageNumber = 1 }
                            if (pageNumber > totalPage) { pageNumber = totalPage }
                            requestParams.skipCount = (pageNumber - 1) * pageSize;
                            requestParams.pageNumber = pageNumber;
                            requestParams.maxResultCount = pageSize;
                            init();
                        });
                        $(".selected").bind("click", function () {
                            getSelectedIds();
                        });
                    }
                });
            }
        });
    }

    //删除选择的试题
    $("#deleteExam").click(function () {
        if (!window.confirm("您确认要删除选择的试题吗？"))
            return;
        $.ajax({
            type: 'POST',
            url: commSetting.apiUrl + '/api/exam/question/batchRemove',
            sTimestamp: commSetting.sTimestamp,
            appkey: commSetting.appkey,
            sign: commSetting.sign,
            dataType: 'JSON',
            contentType:'application/json',
            data: JSON.stringify({
                    sTimestamp: commSetting.sTimestamp,
                    appkey:commSetting.appkey,
                    sign: commSetting.sign,
                    "questionIds": requestParams.ids,
                    "updater": commSetting.userId,
                    "updaterName": commSetting.userName
            }),
            success: function (result) {
                alert('删除成功！');
                init();
            }
        });
    });


    //分页
    function pager(currentPage, totalPage) {
        strpage = "";
        var startPage = 1;
        var endPage = currentPage + 3;
        if(endPage>totalPage)
            endPage = totalPage;
        startPage = currentPage - 3;
        if (startPage <= 0)
            startPage = 1;
        for (var i = startPage; i <= endPage; i++) {
            strpage += "<a><span  class='goPage ";
            if (i == currentPage)
                strpage += "active";
            strpage +="'>" + i + "</span></a>";
        }
        strpage = "<p><a class='prev' id='PagePrev'><span>上一页</span></a>" + strpage + "<a class='prev' id='PageNext'><span>下一页</span></a></p>";       

        $('#pager').html(strpage);
    }



    //加载部门Tree
    function treeLoad() {
        $.ajax({
            type: 'GET',
            url: commSetting.apiUrl +'/api/exam/knowledge/list',            
            dataType: 'JSONP',
            jsonp: 'jsonp',
            jsonpCallback: 'callback',
            data: {
                "pageSize": 10000,
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign},
                success: function (result) {
                zNodes = result.data;
                console.log(zNodes);
                $.fn.zTree.init($("#treeList"), setting, zNodes);
                //初始化上传控件
                $('#imageFile').uploadify({
                    'buttonImage': '/images/browse-btn.png',
                    'swf': '/images/uploadfy/uploadify.swf',
                    'uploader': commSetting.apiUrl + '/api/exam/question/import',
                    'fileObjName': 'importFile',
                    'fileTypeDesc': 'excel Files',
                    'fileTypeExts': '*.xls; *.xlsx',
                    'formData': {
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
                init();
            }
        });
    }

   


    //全选取消全选
    $("#checkAll").click(function () {
        if (this.checked) {
            $("#questionTable :checkbox").prop("checked", true);
            getSelectedIds()
        } else {
            $("#questionTable :checkbox").prop("checked", false);
            requestParams.ids = null;
        }
    });
    //加载配置
    loadSetting(treeLoad);

});



