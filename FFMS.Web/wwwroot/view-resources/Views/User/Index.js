(function () {
    layui.use('table', function () {
        var table = layui.table;
        var layer = layui.layer;
        //第一个实例
        table.render({
            elem: '#tb_User'
            , height: 'full-200'
            ,cellMinWidth: 80
            , url: '/User/GetUserList' //数据接口
            , page: true //开启分页
            , cols: [[ //表头
                { field: 'id', title: '用户ID', sort: true, fixed: 'left' }
                , { field: 'userName', title: '用户名', align: 'center'}
                , { field: 'disPlayName', title: '姓名', sort: true, align: 'center' }
                , { field: 'sex', title: '性别', width: 85, templet: '#switchSexTpl', unresize: true }
                , { field: 'isActive', title: '是否启用',  templet: '#switchIsActiveTpl', unresize: true }
                , { title: '操作', align: 'center', toolbar: '#table-OperationTemplate'}
            ]]
        });

        //监听工具条
        table.on('tool(lay_tb_User)', function (obj) {
            var data = obj.data;
            if (obj.event === 'bondTemplateEdit') { //编辑
                layer.open({
                    type: 2,
                    title: "修改人员信息",
                    area: ['950px', '450px'], //宽高
                    content: '/User/Create?Id=' + data.id
                });
            }
            return false;
        });


        $('.layui-btn-normal[name=btnCreate]').click(function (e) {
            e.preventDefault();
            var createLayer = layer.open({
                type: 2,
                title: '新增用户',
                area: ['950px', '450px'],
                offset: '10px',
                maxmin: true,
                content: '/User/Create'
            });
            layer.full(createLayer);
        });
    });
})(jQuery)