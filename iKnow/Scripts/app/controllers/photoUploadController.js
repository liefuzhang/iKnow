var PhotoUploadController = (function () {
    var targetSelector;

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
        $(upload).on("change", readUrl);
    };
    
    return {
        init: init
    }
})();

