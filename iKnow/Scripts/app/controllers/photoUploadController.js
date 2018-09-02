﻿var PhotoUploadController = (function (questionService, photoUploadService) {
    var targetSelector;
    var canvas, ctx, image;

    var toggleModalEditProfilePhoto = function (event) {
        var userId = targetSelector.attr("data-user-id");
        $(".modal-container").removeClass("new-question-form-loaded");
        var eventState = {};

        var redrawImage = function (offsetX, offsetY) {
            var imageSize = 160;
            var startX = (canvas.width - imageSize) / 2;
            var startY = (canvas.height - imageSize) / 2;
            var endX = imageSize + startX;
            var endY = imageSize + startY;
            if (!eventState.currentX) eventState.currentX = startX;
            if (!eventState.currentY) eventState.currentY = startY;
            if (!eventState.currentWidth) eventState.currentWidth = imageSize;
            if (!eventState.currentHeight) eventState.currentHeight = imageSize;
            var scale = $('.photo-resizer input').val();

            ctx.clearRect(0, 0, canvas.width, canvas.height);
            ctx.fillStyle = "#f6f6f6";
            ctx.fillRect(0, 0, canvas.width, canvas.height);

            var newWidth = image.width * scale;
            var newHeight = image.height * scale;
            var newX = eventState.currentX - (newWidth - eventState.currentWidth) / 2 + (offsetX ? offsetX : 0);
            if (newX > startX) newX = startX;
            if (newX < startX - (newWidth - imageSize)) newX = startX - (newWidth - imageSize);
            var newY = eventState.currentY - (newHeight - eventState.currentHeight) / 2 + (offsetY ? offsetY : 0);
            if (newY > startY) newY = startX;
            if (newY < startY - (newHeight - imageSize)) newY = startY - (newHeight - imageSize);

            ctx.drawImage(image, newX, newY, newWidth, newHeight);
            eventState.currentX = newX;
            eventState.currentY = newY;
            eventState.currentWidth = newWidth;
            eventState.currentHeight = newHeight;

            ctx.globalAlpha = 0.5;
            ctx.fillRect(startX, 0, imageSize, startY);
            ctx.fillRect(0, 0, startX, canvas.height);
            ctx.fillRect(startX, endY, imageSize, canvas.height - endY);
            ctx.fillRect(endX, 0, canvas.width - endX, canvas.height);
            ctx.globalAlpha = 1.0;
        }

        var moving = function (e) {
            var mouse = {}, touches;
            e.preventDefault();
            e.stopPropagation();

            touches = e.originalEvent.touches;

            mouse.x = (e.clientX || e.pageX || touches[0].clientX) + $(window).scrollLeft();
            mouse.y = (e.clientY || e.pageY || touches[0].clientY) + $(window).scrollTop();
            redrawImage(mouse.x - eventState.originalX, mouse.y - eventState.originalY);
            eventState.originalX = mouse.x;
            eventState.originalY = mouse.y;
        };

        var startMoving = function (e) {
            e.preventDefault();
            e.stopPropagation();
            eventState.originalX = (e.clientX || e.pageX || touches[0].clientX) + $(window).scrollLeft();
            eventState.originalY = (e.clientY || e.pageY || touches[0].clientY) + $(window).scrollTop();
            $(document).on('mousemove touchmove', moving);
            $(document).on('mouseup touchend', endMoving);
        };

        var endMoving = function (e) {
            e.preventDefault();
            $(document).off('mouseup touchend', endMoving);
            $(document).off('mousemove touchmove', moving);
        };

        var success = function (html) {
            if (event.currentTarget.files && event.currentTarget.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#photo-canvas').off('mousedown touchstart', startMoving);
                    canvas = $('#photo-canvas')[0];
                    ctx = canvas.getContext("2d");

                    image = new Image();
                    image.src = e.currentTarget.result;
                    image.onload = function () { redrawImage() };

                    var range = $('.photo-resizer input');
                    range.on('input change', function () {
                        redrawImage();
                    });
                    range.val(1);

                    $('#photo-canvas').on('mousedown touchstart', startMoving);
                };
                reader.readAsDataURL(event.currentTarget.files[0]);
                ModalController.toggleModalCommonCallback(html);
            }
        }

        photoUploadService.getForm(success, userId);
    }

    var init = function (upload, target) {
        targetSelector = $(target);
        $(upload).on("change", toggleModalEditProfilePhoto);
    };

    return {
        init: init
    }
})(QuestionService, PhotoUploadService);

