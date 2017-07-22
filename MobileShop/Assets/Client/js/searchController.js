var search = {
    init: function () {
        search.registerEvent();
    },
    registerEvent: function () {
        var projects = function (request, response) {
            $.ajax({
                url: "/Home/ListName",
                method: "GET",
                dataType: "json",
                data: {
                    term: request.term
                },
                success: function (result) {
                    response(result.data);
                }
            });
        };

        $("#txtKeyword").autocomplete({
            minLength: 3,
            source: projects,
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
              .append("<div>" + item.label + "</div>")
              .appendTo(ul);
        };
    }
}
search.init();