(function () {
    layui.use(['table', 'laydate'], function () {
        var table = layui.table,
            layer = layui.layer
            ,laydate = layui.laydate
        
        
        InitTable();
        function InitTable() {
            var searchEntity = {};
            searchEntity.BegDate = $("#BegDate").val();
            searchEntity.EndDate = $("#EndDate").val();
            table.render({
                elem: '#tb_Bills'
                , height: 'full-200'
                , cellMinWidth: 80
                , url: '/Bill/GetBillsList' //数据接口
                , where: {
                    searchInput:JSON.stringify(searchEntity)
                }
                , page: true //开启分页
                , cols: [[ //表头
                    //{ field: 'id', title: '收支项目ID', sort: true, fixed: 'left' }
                    { field: 'billType', title: '收入/支出', align: 'center' }
                    , { field: 'accountDate', title: '收支日期', sort: true, align: 'center' }
                    , { field: 'accountMoney', title: '收支金额', sort: true, align: 'center' }
                    , { field: 'itemType', title: '收支项目', sort: true, align: 'center' }
                    , { field: 'accountDate', title: '收支日期', sort: true, align: 'center' }
                    //, { field: 'createDisPlayName', title: '用户', sort: true, align: 'center' }
                    , { title: '操作', align: 'center', toolbar: '#table-OperationTemplate' }
                ]]
                , done: function (res, curr, count) {
                    $("[data-field = 'billType']").children().each(function () {
                        if ($(this).text() == '0') {
                            $(this).text("收入");
                        } else if ($(this).text() == '1') {
                            $(this).text("支出");
                        }
                    });
                }
            });

        }

        $("#btnSearch").on('click', function () {
            InitTable();
        });

        laydate.render({
            elem: '#BegDate',
            trigger: 'click'//呼出事件改成click
        });

        laydate.render({
            elem: '#EndDate',
            trigger: 'click'//呼出事件改成click
        });

        //监听工具条
        table.on('tool(lay_tb_Items)', function (obj) {
            var data = obj.data;
            if (obj.event === 'bondTemplateEdit') { //编辑
                layer.open({
                    type: 2,
                    title: "修改账簿",
                    area: ['950px', '450px'], //宽高
                    content: '/Bill/Create?Id=' + data.id
                });
            } else if (obj.event === 'bondTemplateDlete') { //编辑
                layer.confirm('是否删除？', {
                    btn: ['是', '否'] //按钮
                }, function () {
                    $.ajax({
                        type: "post",
                        dataType: "json",
                        async: true,
                        url: '/Bill/DeleteBill',
                        data: { 'Id': data.id },
                        success: function (data) {
                            var result = data;
                            if (result.ifSuccess == true) {
                                layer.msg('删除成功', {
                                    offset: '15px'
                                    , icon: 1
                                    , time: 1000
                                }, function () {
                                    window.location.href = '/Bill/Index';
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
                title: '新增收支记录',
                area: ['950px', '450px'],
                offset: '10px',
                maxmin: true,
                content: '/Bill/Create'
            });
            layer.full(createLayer);
        });
    });
})(jQuery)