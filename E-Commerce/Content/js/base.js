﻿var common = {
    init: function () {
        common.registerEvent();
    },

    registerEvent: function () {
        $("#txtKeyword").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/SanPham/ListName",
                    dataType: "jsonp",
                    data: {
                        value: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            }, ,
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            }
        })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append("<div>" + item.label + "<br>" + item.desc + "</div>")
                    .appendTo(ul);
            };
    }
}

common.init();