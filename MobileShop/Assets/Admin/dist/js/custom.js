$(document).ready(function () {
    $('#chkDeleteAll').click(function () {
        if ($(this).is(":checked"))
            $('.chkFieldId').prop('checked', true);
        else
            $('.chkFieldId').prop('checked', false);
    });

    $('#alertBox').removeClass('hide').delay(3000).fadeOut(1000);
});
