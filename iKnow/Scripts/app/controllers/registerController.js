var RegisterController = (function () {
    var toggleModalRegister = function () {
        var $modalContainer = $(".modal-container");

        if ($modalContainer.hasClass("register-form-loaded")) {
            // already loaded
            $modalContainer.addClass("open");
            $(document.body).addClass("modal-open");
            return;
        }

        var queryString = $(event.currentTarget).attr("data-return-url") ? "?returnUrl=" + $target.attr("data-return-url") : "";
        $.ajax({
            url: "/account/register" + queryString,
            dataType: "html",
            success: function (html) {
                ModalController.toggleModalCommonCallback(html);
                $modalContainer.addClass("register-form-loaded");
            }
        });
    };

    var init = function () {
        $(".register-container a").on("click", toggleModalRegister);
    };

    return {
        init: init
    }
})();

