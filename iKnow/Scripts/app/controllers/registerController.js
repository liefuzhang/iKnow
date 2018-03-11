var RegisterController = (function (registerService) {
    var toggleModalRegister = function () {
        if ($(".modal-container").hasClass("register-form-loaded")) {
            ModalController.open();
            return;
        }

        var queryString = $(event.currentTarget).attr("data-return-url") ? "?returnUrl=" + $(event.currentTarget).attr("data-return-url") : "";
        registerService.getRegisterForm(
            function (html) {
                ModalController.toggleModalCommonCallback(html);
                $(".modal-container").addClass("register-form-loaded");
            },
            queryString);
    };

    var init = function () {
        $(".register-container a").on("click", toggleModalRegister);
    };

    return {
        init: init
    }
})(RegisterService);

