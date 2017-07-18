$(document).ready(function () {
    $('#chkDeleteAll').click(function () {
        if ($(this).is(":checked"))
            $('.chkFieldId').prop('checked', true);
        else
            $('.chkFieldId').prop('checked', false);
    });

    $('#alertBox').removeClass('hide').delay(3000).fadeOut(1000);

    $('#browse').click(function () {
        var finder = new CKFinder();
        finder.selectActionFunction = function (fileUrl) {
            fileUrl = fileUrl.substring(fileUrl.lastIndexOf('/') + 1);
            $('#Image').val(fileUrl);
        };
        finder.popup();
    });

    CKEDITOR.replace('Description');
});

function selectImage(action, id) {
    var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl) {
        $.ajax({
            url: action,
            method: "GET",
            data: { image: fileUrl.substring(fileUrl.lastIndexOf('/') + 1) }
        }).done(function (path) {
            if (path != '')
                alert('Lỗi cập nhật hình ảnh' + path);
            else
                $('#' + id).attr('src', fileUrl);
        });
    };
    finder.popup();
}



