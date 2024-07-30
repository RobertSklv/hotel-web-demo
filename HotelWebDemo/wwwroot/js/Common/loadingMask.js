$(function () {
    var loadingMask = $('#loading-mask');

    window.showLoadingMask = function () {
        loadingMask.removeClass('d-none');
        loadingMask.addClass('d-flex');
    }

    window.hideLoadingMask = function () {
        loadingMask.removeClass('d-flex');
        loadingMask.addClass('d-none');
    }
});