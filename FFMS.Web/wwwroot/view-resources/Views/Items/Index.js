(function () {
    layui.use('table', function () {
        var table = layui.table;
        var layer = layui.layer;
        //第一个实例
        table.render({
            elem: '#tb_Items'
            , height: 'full-200'
            , cellMinWidth: 80
            , url: '/Items/GetItemsList' //数据接口
            , page: true //开启分页
            , cols: [[ //表头
                { field: 'id', title: '收支项目ID', sort: true, fixed: 'left' }
                , { field: 'itemType', title: '收支项目', align: 'center' }
                , { field: 'memo', title: '备注', sort: true, align: 'center' }
                , { title: '操作', align: 'center', toolbar: '#table-OperationTemplate' }
            ]]
        });

        //监听工具条
        table.on('tool(lay_tb_Items)', function (obj) {
            var data = obj.data;
            if (obj.event === 'bondTemplateEdit') { //编辑
                layer.open({
                    type: 2,
                    title: "修改收支项目信息",
                    area: ['950px', '450px'], //宽高
                    content: '/Items/Create?Id=' + data.id
                });
            } else if (obj.event === 'bondTemplateDlete') { //编辑
                layer.confirm('是否删除？', {
                    btn: ['是', '否'] //按钮
                }, function () {
                        $.ajax({
                            type: "post",
                            dataType: "json",
                            async: true,
                            url: '/Items/DeleteItem',
                            data: { 'Id':data.id},
                            success: function (data) {
                                var result = data;
                                if (result.ifSuccess == true) {
                                    layer.msg('删除成功', {
                                        offset: '15px'
                                        , icon: 1
                                        , time: 1000
                                    }, function () {
                                        //var index = parent.layer.getFrameIndex(window.name); //获取窗口索引
                                        //parent.layer.close(index);
                                        window.location.href = '/Items/Index';
                                    });
                                } else {
                                    layer.alert(result.message);
                                }
                            },
                            err: function (data) {
                                console.log(data);
                                layer.alert("发生了些异常！");
                            }
                        });
                     }
                 )
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
                content: '/Items/Create'
            });
            layer.full(createLayer);
        });
    });
})(jQuery)