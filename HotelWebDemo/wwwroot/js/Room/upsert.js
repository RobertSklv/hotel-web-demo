$(function () {
    var hotelSelectInput = $('#HotelId');
    var categorySelectInput = $('#CategoryId');
    var categoryField = $('.field-category');

    function updateCategories(hotelId, categories) {
        if (!categories || categories.length == 0) {
            categoryField.hide();

            return;
        }

        categoryField.show();

        categorySelectInput.children('option').each(function (index, el) {
            $el = $(el);

            let id = $el.attr('id');

            if (inCategories(hotelId, categories) || id == 0) {
                $el.show();
            } else {
                $el.hide();
            }
        });
    }

    function inCategories(hotelId, categories) {
        for (let category of categories) {
            if (category.hotelId == hotelId) {
                return true;
            }
        }

        return false;
    }

    hotelSelectInput.on('change', function () {
        var selectedHotelId = hotelSelectInput.val();

        if (!selectedHotelId || selectedHotelId == 0) {
            categoryField.hide();

            return;
        }

        $.ajax({
            url: `/Admin/RoomCategory/GetHotelRoomCategories?hotelId=${selectedHotelId}`,
            method: 'GET',
            success: function (response) {
                updateCategories(selectedHotelId, response)
            },
            error: function (response) {
                console.error(response);
            }
        });
    });
});