

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
    var zTree = $.fn.zTree.getZTreeObj("departTree");
    zTree.selectNode(treeNode);
    setTimeout(function () {
        setTimeout(function () {
            zTree.editName(treeNode);

        }, 0);
    }, 0);
    return false;
}
function beforeRemove(treeId, treeNode) {
    className = (className === "dark" ? "" : "dark");
    showLog("[ " + getTime() + " beforeRemove ]&nbsp;&nbsp;&nbsp;&nbsp; " + treeNode.name);
    var zTree = $.fn.zTree.getZTreeObj("departTree");
    zTree.selectNode(treeNode);
    if (confirm("确认删除 节点 -- " + treeNode.name + " 吗？")) {
        if (treeNode.isNew === true) {
            $("a#" + treeId).remove();
            return;
        }
        $.get('Delete?id=' + treeNode.id, function (result) {
            if (result.code == 200) {
                init();
            }
            else {
                $.modalMsg('result.msg', 'error');
            }
        }, 'json')
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
            var zTree = $.fn.zTree.getZTreeObj("departTree");
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
var newCount = getLastId();
function addHoverDom(treeId, treeNode) {

    var sObj = $("#" + treeNode.tId + "_span");
    if (treeNode.editNameFlag || $("#addBtn_" + treeNode.tId).length > 0) return;
    var addStr = "<span class='button add' id='addBtn_" + treeNode.tId
        + "' title='add node' onfocus='this.blur();'></span>";
    sObj.after(addStr);

    var btn = $("#addBtn_" + treeNode.tId);
    if (btn) btn.bind("click", function () {
        var zTree = $.fn.zTree.getZTreeObj("departTree");
        zTree.addNodes(treeNode, { id: newCount, parentId: treeNode.id, name: "new node" + (newCount++), isNew: true });
        return false;
    });
};
function removeHoverDom(treeId, treeNode) {
    $("#addBtn_" + treeNode.tId).unbind().remove();
};
function selectAll() {
    var zTree = $.fn.zTree.getZTreeObj("departTree");
    zTree.setting.edit.editNameSelectAll = $("#selectAll").attr("checked");
}
function add(e) {
    var zTree = $.fn.zTree.getZTreeObj("departTree"),
    isParent = e.data.isParent,
    nodes = zTree.getSelectedNodes(),
    treeNode = nodes[0];
    treeNode = zTree.addNodes(null, { id: newCount, parentId: 0, isParent: isParent, name: "new node" + (newCount++), isNew: true });
    console.log(zNodes);
    if (treeNode) {
        zTree.editName(treeNode[0]);
        $("#addParent").hide();
    } else {
        alert("叶子节点被锁定，无法增加子节点");
    }
};
$(document).ready(function () {


    init();
    $("#selectAll").bind("click", selectAll);
    $("#addParent").bind("click", { isParent: true }, add);

    $('#savedepartTree').click(function () {
        var treeObj = $.fn.zTree.getZTreeObj("departTree");
        var listdata = treeObj.transformToArray(treeObj.getNodes());
        console.log(listdata);

        var namelist = new Array;
        $.each(listdata, function (i, data) {
            namelist.push(data.name);
        })
        var nary = namelist.sort();
        for (var i = 0; i < namelist.length; i++) {
            if (nary[i] == nary[i + 1]) {
                $.modalMsg("重复名称：" + nary[i], "warning");
                return;
            }
        }
        $.ajax({
            type: 'POST',
            url: 'SaveOrUpdate',
            data: { strlist: JSON.stringify(listdata) },
            success: function (result) {
                init();
                $.fn.zTree.init($("#departTree"), setting, zNodes);
            }
        });
    });
});


function init() {
    $.get('GetdepartList', null, function (result) {
        zNodes = result.data;
        if (zNodes.length > 0) {
            $("#addParent").hide();
        }
        else {
            $("#addParent").show();
        }
        console.log(zNodes);
        //if (zNodes.length > 0) {
        //    newCount = zNodes[zNodes.length - 1].id + 1;
        //}
        $.fn.zTree.init($("#departTree"), setting, zNodes);
    }, 'json')
}

function getLastId() {
    var id = 0;
    $.ajax({
        type: "get",
        url: "getLastId",
        async: false,
        success: function (data) {
           id= parseInt(data) + 1;
        }
    });
    return id;
}