$(document).ready(function () {
    $('.btnStatus').off('click').on('click', function (e) {
        e.preventDefault();
        var btn = $(this);
        $.get('/Contact/ChangeStatus', {id : $(btn).data('id')}, function (status) {
            if (status == "True")
                $(btn).text('Đã đọc').removeClass('text-danger').addClass('text-success');
            else
                $(btn).text('Chưa đọc').removeClass('text-success').addClass('text-danger');
        });
    });
})


