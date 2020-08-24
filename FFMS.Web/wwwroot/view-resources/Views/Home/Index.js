(function () {
    $('#changePasswordClick').click(function (e) {
        e.preventDefault();
        layer.open({
            type: 2,
            title: '修改密码',
            area: ['500px', '300px'],
            offset: '10px',
            maxmin: true,
            content: '/User/OpenChangeUserPasswordView'
        });
    });
})(jQuery)