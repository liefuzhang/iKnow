var photoUploadController = (function () {
    var targetSelector;

    var readURL = function () {
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
        $(upload).on("change", readURL);
    };
    
    return {
        init: init
    }
})();

