var ModalController = (function () {
    var toggleModalClose = function () {
        if (event.target !== event.currentTarget) {
            // click on the Modal form
            return;
        }

        var $modalContainer = $(".modal-container");

        if ($modalContainer.hasClass("open")) {
            $modalContainer.removeClass("open");
            $(document.body).removeClass("modal-open");
            return;
        }
    };

    var open = function () {
        $(".modal-container").addClass("open");
        $(document.body).addClass("modal-open");
    }

    var toggleModalCommonCallback = function (html) {
        if (html) {
            var $modalContainer = $(".modal-container");
            $modalContainer.html(html);
            open();
            $(".topic-select").chosen({ width: "100%", max_selected_options: 5 });
        }
    }

    var textareaAutoGrow = function() {
        var scrollHeight = this.scrollHeight;
        $(this).css('height', scrollHeight + 'px');
    };

    var init = function () {
        $(".modal-container").on("click", toggleModalClose);
        $(".modal-container").on("click", ".js-button-delete", appController.deleteEntity);
        $(".modal-container").on("input", "textarea", textareaAutoGrow);
        $(".modal-container").on("click", ".error li", WarningErrorController.hideError);
    };

    return {
        init: init,
        toggleModalCommonCallback: toggleModalCommonCallback,
        open: open
    }
})();

