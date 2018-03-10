$(document).ready(function () {
    $(".topic-form-container .js-button-delete").on("click", deleteEntity);
    $(".modal-container").on("click", ".js-button-delete", deleteEntity);
    $(".add-answer-panel").on("click", ".js-button-delete", deleteEntity);


    $(".add-question-button").on("click", function (e) { toggleModal(e, "addQuestion") });
    $(".modal-container").on("click", function (e) { toggleModal(e, "close") });
    $(".question-header-panel .js-edit-question").on("click", function (e) { toggleModal(e, "editQuestion") });
    $(".question-header-panel .js-edit-topic").on("click", function (e) { toggleModal(e, "editTopic") });
    $(".register-container a").on("click", function (e) { toggleModal(e, "register") });

    $(".modal-container").on("input", "textarea", textareaAutoGrow);
    $(".user-profile-inner").on("click", showUserProfileDropDown);
    $(".js-profile-photo-upload").on("change", function (e) { readURL(e, ".edit-profile-photo img") });
    $(".js-topic-photo-upload").on("change", function (e) { readURL(e, ".topic-form-container .img-container img") });
    $(".mask-content").on("click", function () { $(".js-profile-photo-upload").click(); });
    $(".search").on("keyup", search);
    $(".search-container .btn").on("click", search);
    $(".error").on("click", "li", hideError);
    $(".modal-container").on("click", ".error li", hideError);
    $(".warning").on("click", "div", hideError);
    $(document).on("click", pageClickHandler);
    $(window).scroll(backToTopDetect);
    $(".back-top").on("click", goToTop);

    if (iknow.pageError) {
        showWarning(iknow.pageError, isSevere = true);
    }

    if (iknow.statusMessage) {
        showWarning(iknow.statusMessage);
    }
});

function deleteEntity(e) {
    var button = $(this);
    var message = button.attr("data-delete-message");
    return confirm(message);
}

function toggleModal(e, action) {
    var $modalContainer = $(".modal-container");
    var $body = $(document.body);
    var $target = $(e.currentTarget);
    var commonCallback = function (html) {
        if (html) {
            $modalContainer.html(html);
            $modalContainer.addClass("open");
            $body.addClass("modal-open");
            $(".topic-select").chosen({ width: "100%", max_selected_options: 5 });
        }
    };

    switch (action) {
        case "addQuestion":
            if (iknow.isUserAuthorized !== true) {
                showWarning("Please log in before you add question");
                return;
            }
            if ($modalContainer.hasClass("new-question-form-loaded")) {
                // already loaded
                $modalContainer.addClass("open");
                $body.addClass("modal-open");
                return;
            }

            $.ajax({
                url: "/question/getform",
                dataType: "html",
                success: function (html) {
                    commonCallback(html);
                    $modalContainer.addClass("new-question-form-loaded");
                }
            });
            break;
        case "editQuestion":
            var questionId = $target.attr("data-question-id");
            $modalContainer.removeClass("new-question-form-loaded");

            $.ajax({
                url: "/question/getform/" + questionId,
                dataType: "html",
                success: function (html) {
                    commonCallback(html);
                    var $textArea = $(".add-question-description textarea");
                    var scrollHeight = $(".add-question-description textarea").get(0).scrollHeight;
                    $textArea.css('height', scrollHeight + 'px');
                }
            });
            break;
        case "editTopic":
            questionId = $target.attr("data-question-id");
            $modalContainer.removeClass("new-question-form-loaded");

            $.ajax({
                url: "/question/gettopic/" + questionId,
                dataType: "html",
                success: commonCallback
            });
            break;
        case "register":
            if ($modalContainer.hasClass("register-form-loaded")) {
                // already loaded
                $modalContainer.addClass("open");
                $body.addClass("modal-open");
                return;
            }

            var queryString = $target.attr("data-return-url") ? "?returnUrl=" + $target.attr("data-return-url") : "";
            $.ajax({
                url: "/account/register" + queryString,
                dataType: "html",
                success: function (html) {
                    commonCallback(html);
                    $modalContainer.addClass("register-form-loaded");
                }
            });
            break;
        case "close":
        default:
            if (e.target !== e.currentTarget) {
                // click on the Modal form
                return;
            }

            if ($modalContainer.hasClass("open")) {
                $modalContainer.removeClass("open");
                $body.removeClass("modal-open");
                return;
            }
    }
}

function preventOuterScrolling(e) {
    if (e.currentTarget.scrollHeight === e.currentTarget.offsetHeight) {
        return;
    }

    var delta = 0;
    if (e.originalEvent.wheelDelta) { // will work in most cases
        delta = e.originalEvent.wheelDelta;
    } else if (e.originalEvent.detail) { // fallback for Firefox
        delta = -e.originalEvent.detail;
    }
    var scrollTop = $(e.currentTarget).scrollTop();
    if (delta < 0 && scrollTop > e.currentTarget.scrollHeight - e.currentTarget.offsetHeight - 5
        || delta > 0 && scrollTop < 5) {
        e.preventDefault();
    }
}

function textareaAutoGrow() {
    var scrollHeight = this.scrollHeight;
    $(this).css('height', scrollHeight + 'px');
}

function showUserProfileDropDown() {
    event.stopPropagation();
    $(".user-profile-dropdown").show();
}

function pageClickHandler() {
    $(".user-profile-dropdown").hide();
    closeSearchResult(event);
}

function readURL(event, target) {
    if (event.currentTarget.files && event.currentTarget.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(target).attr("src", e.currentTarget.result);
        };
        reader.readAsDataURL(event.currentTarget.files[0]);
    }
}

function search(e) {
    var input = $(".search").val();
    if (input === "" ||
        ($(e.currentTarget).hasClass("search") && e.keyCode === 27)) {
        $(".search-result-container").html("");
        return;
    }

    if ($(e.currentTarget).hasClass("search") && e.keyCode === 13 ||
        $(e.currentTarget).hasClass("btn")) {
        $.ajax({
            url: "/search/getresult?input=" + input,
            dataType: "html",
            success: function (html) {
                if (html) {
                    $(".search-result-container").html(html);
                }
            }
        });
    }
}

function closeSearchResult(e) {
    if ($(e.target).parents(".search-container").length > 0) {
        return;
    }

    $(".search-result-container").html("");
}

function hideError() {
    $(this).hide();
}

function showWarning(message, isSevere) {
    $warning = $(".warning");
    cleanUpErrorAndWarning();
    $warning.html("");
    $("<div>").html(message).appendTo($warning);
    if (isSevere) {
        $warning.addClass("severe-warning");
    }
    setTimeout(function () {
        $warning.addClass("warning-display");
    }, 50);
}

function cleanUpErrorAndWarning() {
    $(".warning").removeClass("warning-display severe-warning");
    $(".error").removeClass("validation-summary-errors");
}

function backToTopDetect() {
    if ($(this).scrollTop() > 500) {
        $(".back-top-container").fadeIn();
    } else {
        $(".back-top-container").fadeOut();
    }
}

function goToTop() {
    $("html, body")
        .animate({
            scrollTop: 0
        }, 300);
}