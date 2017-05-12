
(function () {
    

    $(function () {
        var _menuService = abp.services.app.menu;
        var _$menusTable = $('#MenusTable');
        
        var _permissions = {
            create: abp.auth.hasPermission("Pages.Menu.CreateMenu"),
            edit: abp.auth.hasPermission("Pages.Menu.EditMenu"),
            'delete': abp.auth.hasPermission("Pages.Menu.DeleteMenu")

        };

        _$menusTable.jtable({

            title: app.localize('Menu'),
            paging: true,
            sorting: true,
            //  multiSorting: true,
            actions: {
                listAction: {
                    method: _menuService.getPagedMenusAsync
                }
            },

            fields: {

                actions: {
                    title: app.localize('Actions'),
                    width: '10%',
                    sorting: false,
                    display: function (data) {
                        var $span = $('<span></span>');
                        //编辑
                        if (_permissions.edit) {
                            $('<button class="btn btn-default btn-xs" title="' + app.localize('Edit') + '"><i class="fa fa-edit"></i></button>')
                                .appendTo($span)
                                .click(function () {
                                    location.href = abp.appPath + 'Mpa/MenuManage/CreateOrEditMenuModal?id=' + data.record.id;
                                });
                        }
                        //删除
                        if (_permissions.delete) {
                            $('<button class="btn btn-default btn-xs" title="' + app.localize('Delete') + '"><i class="fa fa-trash-o"></i></button>')
                                .appendTo($span)
                                .click(function () {
                                    deleteMenu(data.record);
                                });
                        }
                        //添加
                        if (_permissions.create) {
                            $("<button class='btn btn-default  btn-xs'  title='" + app.localize("CreateMenu") + "' ><i class='fa fa-plus'></i></button>")
                                .appendTo($span)
                                .click(function () {
                                    location.href = abp.appPath + 'Mpa/MenuManage/CreateOrEditMenuModal'

                                });
                        }

                        return $span;
                    }
                },
                //此处开始循环数据



                id: {
                    key: true,
                    list: false
                },


                enabled: {
                    title: app.localize('Enabled'),
                    width: '10%',
                    display: function (data) {

                        if (data.record.enabled) {
                            return "<span class=\"label label-success\"> 是</span>";
                        }

                        return "<span class=\"label label-danger\"> 否</span>";

                    }
                },




                menuName: {
                    title: app.localize('MenuName'),
                    width: '10%'
                },


                url: {
                    title: app.localize('Url'),
                    width: '10%'
                },


                parentId: {
                    title: app.localize('ParentId'),
                    width: '10%'
                },


                permissionCode: {
                    title: app.localize('PermissionCode'),
                    width: '10%'
                },


                sort: {
                    title: app.localize('Sort'),
                    width: '10%'
                },


                menuType: {
                    title: app.localize('MenuType'),
                    width: '10%'
                },


                path: {
                    title: app.localize('Path'),
                    width: '10%'
                },

            }

        });




        //  打开添加窗口MPA
        $('#CreateNewMenuButton').click(function () {
            //        console.log(abp.appPath);的默认值为"/";
            location.href = abp.appPath + 'Admin/MenuManage/CreateOrEditMenuModal';
        });





        //刷新表格信息
        $("#ButtonReload").click(function () {
            getMenus();
        });




        //模糊查询功能
        function getMenus(reload) {
            if (reload) {
                _$menusTable.jtable('reload');
            } else {
                _$menusTable.jtable('load', {
                    filtertext: $('#MenusTableFilter').val()
                });
            }
        }
        //
        //删除当前menu实体
        function deleteMenu(menu) {
            abp.message.confirm(
                app.localize('MenuDeleteWarningMessage', menu.enabled),
                    function (isConfirmed) {
                        if (isConfirmed) {
                            _menuService.deleteMenuAsync({
                                id: menu.id
                            }).done(function () {
                                getMenus(true);
                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                            });
                        }
                    }
                );
        }



        //导出为excel文档
        $('#ExportMenusToExcelButton').click(function () {
            _menuService
                .getMenusToExcel({})
                    .done(function (result) {
                        app.downloadTempFile(result);
                    });
        });
        //搜索
        $('#GetMenusButton').click(function (e) {
            e.preventDefault();
            getMenus();
        });

        //制作Menu事件,用于请求变化后，刷新表格信息
        abp.event.on('app.createOrEditMenuModalSaved', function () {
            getMenus(true);
        });

        getMenus();


    });
})();
