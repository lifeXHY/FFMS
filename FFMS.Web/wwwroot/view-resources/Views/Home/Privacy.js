(function () {
    var chart = echarts.init(document.getElementById('MyExpenseInfo'));
    var columLabel = []; //图例
    var columName = [];//X轴刻度名称
    var arrData = [];
    chart.showLoading();
    $.ajax({
        type: "post",
        dataType: "json",
        async: true,
        contentType: 'application/json; charset=utf-8',
        url: "/Home/GetEchartsData",
        success: function (data) {
            var arr = data;
            chart.hideLoading();
            arr.forEach((item, index, array) => {
                columLabel.push(item.ItemType);
                arrData.push({ "name": item.ItemType, "value": item.AccountMoney });
            });
            console.log(arrData)
            chart.setOption(optionchart(), true);
        },
        error: function () {
            return;
        }
    });
    //指定图表配置项和数据
    var optionchart = function () {
        
        return {
            title: {
                text: '当月支出',
                x: 'center' //标题居中
            },
            tooltip: {
                // trigger: 'item' //悬浮显示对比
            },
            legend: {
                orient: 'vertical', //类型垂直,默认水平
                left: 'left', //类型区分在左 默认居中
                data: columLabel
            },
            series: [{
                type: 'pie', //饼状
                radius: '60%', //圆的大小
                center: ['50%', '50%'], //居中
                data: arrData
            }]
        }
    }
    
    
})(jQuery)