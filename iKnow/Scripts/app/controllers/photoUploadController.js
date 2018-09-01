var PhotoUploadController = (function (questionService, photoUploadService) {
    var targetSelector;

    var toggleModalEditProfilePhoto = function () {
        var userId = targetSelector.attr("data-user-id");
        $(".modal-container").removeClass("new-question-form-loaded");

        photoUploadService.getForm(ModalController.toggleModalCommonCallback, userId);
    }

    var readUrl = function () {
        if (event.currentTarget.files && event.currentTarget.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                targetSelector.attr("src", e.currentTarget.result);
            };
            reader.readAsDataURL(event.currentTarget.files[0]);
        }
    }

    var init = function (upload, target) {
        targetSelector = $(target);
        $(upload).on("change", toggleModalEditProfilePhoto);
    };
    
    return {
        init: init
    }
})(QuestionService, PhotoUploadService);

