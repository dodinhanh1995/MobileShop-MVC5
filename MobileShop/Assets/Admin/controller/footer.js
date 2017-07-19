$(document).ready(function () {
    CKEDITOR.replace('Content');

    $('#btnStatus').click(function () {
        var btn = $(this);
        $.get('/Footer/ChangeStatus', {}, function (status) {
            if (status == "True")
                $(btn).text('Hiển thị').removeClass('btn-danger').addClass('btn-success');
            else
                $(btn).text('Ẩn').removeClass('btn-success').addClass('btn-danger');
        });
    });
})


