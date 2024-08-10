$(function () {
    var table = $('#table-Room');
    var refreshBtn = $('#refresh-btn');
    var reservedRooms = $('#reserved-rooms');
    var reserveButtons = table.find('[data-row-action=Reserve]');

    reserveButtons.each(function (i, e) {
        var $e = $(e);
        var itemId = $e.data('item-id');

        $e.on('click', function (ev) {
            ev.preventDefault();

            let idInput = $(`<input type="hidden" name="RoomsToReserve" value="${itemId}" />`);
            reservedRooms.append(idInput);

            refreshBtn.trigger('click');
        });
    });
});