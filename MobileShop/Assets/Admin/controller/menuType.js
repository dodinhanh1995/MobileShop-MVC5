$(document).ready(function () {
    // Editing MenuType management
    $('.editingMenuType').click(function () {
        var btn = $(this);
        var id = btn.data('id');
        var name = btn.parent().prev().children();
        if (id != '') {
            $.post('/MenuType/Edit/' + id, { name: name.val() }, function (result) {
                var message;
                switch (result.Status) {
                    case 1: message = 'Cập nhật thành công.';
                        $(name).val(result.Name);
                        break;
                    case 2: message = 'Tên loại menu đã tồn tại';
                        break;
                    default: message = 'Có lỗi xảy ra khi cập nhật! Vui lòng thử lại.';
                        break;
                }
                alert(message);
            });
        }
    });

    $('#btnCreateNewMenuType').click(function () {
        var html = '<tr>';
        html += '<td>';
        html += '<button type="button" class="btn btn-outline btn-warning" id="btnCancel"><i class="fa fa-times"></i></button>';
        html += '</td>';
        html += '<td><input type="text" class="form-control" /></td>';
        html += '<td>';
        html += '<button id="btnCreate" type="button" class="btn btn-primary"><i class="fa fa-plus"></i> Thêm</button>';
        html += '</td>';
        html += '</tr>';
        $('.table tbody').prepend(html);
        $(this).attr('disabled', true);
    });

    $('.table tbody').on('click', '#btnCreate', function () {
        var name = $(this).parent().prev().children().val();
        if (name != '') {
            $.post('/MenuType/Create', { name: name }, function (result) {
                var message;
                switch (result.Status) {
                    case 1: message = 'Cập nhật thành công.';
                        location.reload();
                        break;
                    case 2: message = 'Tên loại menu đã tồn tại';
                        break;
                    default: message = 'Có lỗi xảy ra khi cập nhật! Vui lòng thử lại.';
                        break;
                }
                alert(message);
            });
        }
        else
            alert('Vui lòng nhập tên loại menu!');
    });

    $('tbody').on('click', '#btnCancel', function () {
        $(this).parents('tr').remove();
        $('#btnCreateNewMenuType').removeAttr('disabled')
    });
    //===========================================
});