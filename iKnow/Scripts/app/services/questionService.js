var QuestionService = (function() {
    var getForm = function (success) {
        $.ajax({
            url: "/question/getform",
            dataType: "html",
            success: success,
            fail: function() {
                console.error("Something went wrong with QuestionService getForm.");
            }
        });
    };

    return {
        getForm: getForm
    }
})();