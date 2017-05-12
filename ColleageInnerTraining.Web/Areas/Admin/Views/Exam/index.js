

var setting = {
    view: {
        addHoverDom: addHoverDom,
        removeHoverDom: removeHoverDom,
        selectedMulti: false
    },
    edit: {
        enable: true,
        editNameSelectAll: true,
        showRemoveBtn: showRemoveBtn,
        showRenameBtn: showRenameBtn
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
        beforeDrag: beforeDrag,
        beforeEditName: beforeEditName,
        beforeRemove: beforeRemove,
        beforeRename: beforeRename,
        onRemove: onRemove,
        onRename: onRename
    }
};

var zNodes = [
];
var log, className = "dark";
function beforeDrag(treeId, treeNodes) {
    return false;
}
function beforeEditName(treeId, treeNode) {
    className = (className === "dark" ? "" : "dark");
    showLog("[ " + getTime() + " beforeEditName ]&nbsp;&nbsp;&nbsp;&nbsp; " + treeNode.name);
    var zTree = $.fn.zTree.getZTreeObj("knowledgeTree");
    zTree.selectNode(treeNode);
    setTimeout(function () {
        if (confirm("进入节点 -- " + treeNode.name + " 的编辑状态吗？")) {
            setTimeout(function () {
                zTree.editName(treeNode);

            }, 0);
        }
    }, 0);
    return false;
}
function beforeRemove(treeId, treeNode) {
    className = (className === "dark" ? "" : "dark");
    showLog("[ " + getTime() + " beforeRemove ]&nbsp;&nbsp;&nbsp;&nbsp; " + treeNode.name);
    var zTree = $.fn.zTree.getZTreeObj("knowledgeTree");
    zTree.selectNode(treeNode);

    if (confirm("确认删除 节点 -- " + treeNode.name + " 吗？")) {

        $.ajax({
            type: 'POST',
            contentType: 'application/json',
            url: commSetting.apiUrl + 'api/exam/knowledge/remove',
            dataType: 'JSON',
            data: JSON.stringify({
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                id: treeNode.id,
                updater: commSetting.userId,
                updaterName: commSetting.userName
    }),
            success: function (result) {
                init();
                $.fn.zTree.init($("#knowledgeTree"), setting, zNodes);
            }
        });
    }
}
function onRemove(e, treeId, treeNode) {
    showLog("[ " + getTime() + " onRemove ]&nbsp;&nbsp;&nbsp;&nbsp; " + treeNode.name);
}
function beforeRename(treeId, treeNode, newName, isCancel) {
    className = (className === "dark" ? "" : "dark");
    showLog((isCancel ? "<span style='color:red'>" : "") + "[ " + getTime() + " beforeRename ]&nbsp;&nbsp;&nbsp;&nbsp; " + treeNode.name + (isCancel ? "</span>" : ""));
    if (newName.length == 0) {
        setTimeout(function () {
            var zTree = $.fn.zTree.getZTreeObj("knowledgeTree");
            zTree.cancelEditName();
            alert("节点名称不能为空.");
        }, 0);
        return false;
    }
    return true;
}
function onRename(e, treeId, treeNode, isCancel) {
    showLog((isCancel ? "<span style='color:red'>" : "") + "[ " + getTime() + " onRename ]&nbsp;&nbsp;&nbsp;&nbsp; " + treeNode.name + (isCancel ? "</span>" : ""));
}
function showRemoveBtn(treeId, treeNode) {
    return true;
}
function showRenameBtn(treeId, treeNode) {
    return true;
}
function showLog(str) {
    if (!log) log = $("#log");
    log.append("<li class='" + className + "'>" + str + "</li>");
    if (log.children("li").length > 8) {
        log.get(0).removeChild(log.children("li")[0]);
    }
}
function getTime() {
    var now = new Date(),
    h = now.getHours(),
    m = now.getMinutes(),
    s = now.getSeconds(),
    ms = now.getMilliseconds();
    return (h + ":" + m + ":" + s + " " + ms);
}

var newCount = 1;
function addHoverDom(treeId, treeNode) {
    var sObj = $("#" + treeNode.tId + "_span");
    if (treeNode.editNameFlag || $("#addBtn_" + treeNode.tId).length > 0) return;
    var addStr = "<span class='button add' id='addBtn_" + treeNode.tId
        + "' title='add node' onfocus='this.blur();'></span>";
    sObj.after(addStr);
    var btn = $("#addBtn_" + treeNode.tId);
    if (btn) btn.bind("click", function () {
        var zTree = $.fn.zTree.getZTreeObj("knowledgeTree");
        zTree.addNodes(treeNode, { id: (100 + newCount), parentId: treeNode.id, name: "new node" + (newCount++), isNew: true });
        return false;
    });
};
function removeHoverDom(treeId, treeNode) {
    $("#addBtn_" + treeNode.tId).unbind().remove();
};
function selectAll() {
    var zTree = $.fn.zTree.getZTreeObj("knowledgeTree");
    zTree.setting.edit.editNameSelectAll = $("#selectAll").attr("checked");
}
function add(e) {
    var zTree = $.fn.zTree.getZTreeObj("knowledgeTree"),
    isParent = e.data.isParent,
    nodes = zTree.getSelectedNodes(),
    treeNode = nodes[0];
    treeNode = zTree.addNodes(null, { id: (100 + newCount), parentId: 0, isParent: isParent, name: "new node" + (newCount++), isNew: true });
    console.log(zNodes);
    if (treeNode) {
        zTree.editName(treeNode[0]);
    } else {
        alert("叶子节点被锁定，无法增加子节点");
    }
};
$(document).ready(function () {
    $("#selectAll").bind("click", selectAll);
    $("#addParent").bind("click", { isParent: true }, add);

    $('#saveknowledgeTree').click(function () {
        var treeObj = $.fn.zTree.getZTreeObj("knowledgeTree");
        var data = treeObj.transformToArray(treeObj.getNodes());
        console.log(JSON.stringify(data));
        $.ajax({
            type: 'POST',
            contentType: 'application/json',
            url: commSetting.apiUrl + '/api/exam/knowledge/saveOrUpdate',
            dataType: 'JSON',
            data: JSON.stringify({
                sTimestamp: commSetting.sTimestamp,
                appkey: commSetting.appkey,
                sign: commSetting.sign,
                updater: commSetting.userId,
                updaterName: commSetting.userName,
                userId: commSetting.userId,
                userName: commSetting.userName,
                departmentId: commSetting.departmentId,
                data: data
            }),
            success: function (result) {
                alert('保存成功！');
                init();
                $.fn.zTree.init($("#knowledgeTree"), setting, zNodes);
            }
        });
    });
});

loadSetting(init);
function init() {
    console.log(commSetting.apiUrl);
    $.ajax({
        type: 'GET',
        url: commSetting.apiUrl + "/api/exam/knowledge/list",
        dataType: 'JSONP',
        jsonp: 'jsonp',
        jsonpCallback: 'callback',
        data: {
            "pageSize": 10000,
            sTimestamp: commSetting.sTimestamp,
            appkey: commSetting.appkey,
            sign: commSetting.sign
        },
        success: function (result) {
            zNodes = result.data;
            console.log(zNodes);
            $.fn.zTree.init($("#knowledgeTree"), setting, zNodes);
        }
    });
}