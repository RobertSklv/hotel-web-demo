$(function () {
    const roomsToReserveName = "RoomsToReserve";

    var table = $('#table-Room');
    var refreshBtn = $('#refresh-btn');
    var reservedRooms = $('#reserved-rooms');
    var reserveButtons = table.find('[data-row-action=Reserve]');
    var removeReservedRoomButton = $('.btn-remove-reserved-room');
    var customGrandTotalCheck = $('#HasCustomGrandTotal');
    var customGrandTotalInput = $('#Totals_CustomGrandTotal');

    reserveButtons.each(function (i, e) {
        var $e = $(e);
        var itemId = $e.data('item-id');

        $e.on('click', function (ev) {
            ev.preventDefault();

            let idInput = $(`<input type="hidden" name="${roomsToReserveName}" value="${itemId}" />`);
            reservedRooms.append(idInput);

            refreshBtn.trigger('click');
        });
    });

    removeReservedRoomButton.on('click', function () {
        let id = $(this).data('remove-reserved-room');
        let input = $(`input[name=${roomsToReserveName}][value=${id}]`);
        input.remove();
        refreshBtn.trigger('click');
    });

    function updateCustomGrandTotalFieldState(check) {
        customGrandTotalInput.prop('disabled', !$(check).is(':checked'));
    }

    customGrandTotalCheck.on('change', function () {
        updateCustomGrandTotalFieldState($(this));
    });
    updateCustomGrandTotalFieldState(customGrandTotalCheck);
});