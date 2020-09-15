(function () {

    layui.use(['table', 'laydate', 'form'], function () {
        var table = layui.table
            , laydate = layui.laydate
            , form = layui.form;
        
        form.render();
        InitTable();
        function InitTable() {
            var searchEntity = {};
            searchEntity.BegDate = $("#BegDate").val();
            searchEntity.EndDate = $("#EndDate").val();
            searchEntity.UserID = $("#UserIDSel").val();
            table.render({
                elem: '#tb_Details'
                , height: 'full-200'
                , cellMinWidth: 80
                , url: '/Report/GetAllUserBillsList' //数据接口
                , where: {
                    searchInput: JSON.stringify(searchEntity)
                }
                , page: true //开启分页
                , cols: [[ //表头
                    { field: 'billType', title: '收入/支出', align: 'center' }
                    , { field: 'itemType', title: '收支项目', sort: true, align: 'center' }
                    , { field: 'accountDate', title: '收支日期', sort: true, align: 'center' }
                    , { field: 'createDisPlayName', title: '家庭成员', sort: true, align: 'center' }
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

        form.on('select(UserID)', function (data) {
            $("#UserIDSel").val(data.value);
        }); 
    });
})(jQuery)