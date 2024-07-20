$(function () {
    $('.active-filter').find('button[data-bs-dismiss="toast"]').click(function () {
        var propertyName = $(this).data('remove-filter');
        $('.grid-filters-form').find(`[data-filter="${propertyName}"] .filter-value`).val('');

        $('#applyFiltersButton').click();
    });

    $('.grid-filters-form').submit(function () {
        var filters = $(this).find('.filter');

        $.each(filters, function (i, e) {
            var $e = $(e);
            var operatorEl = $e.find('[data-name*=".Operator"]');
            var valueEl = $e.find('[data-name*=".Value"]');
            var secondaryValueEl = $e.find('[data-name*=".SecondaryValue"]');

            if (getFieldValue(valueEl)) {
                operatorEl.attr('name', operatorEl.data('name'));
                valueEl.attr('name', valueEl.data('name'));
                secondaryValueEl.attr('name', secondaryValueEl.data('name'));
            }
        });
    })

    $('.filter-operator').on('change', function () {
        let propName = $(this).data('filter-operator-for');
        let secValue = $(`[data-filter-secondary-value-for="${propName}"]`);

        if ($(this).val() == 'btw') {
            secValue.show();
            secValue.prop('disabled', false);
        } else {
            secValue.hide();
            secValue.prop('disabled', true);
        }
    });

    function getFieldValue(field) {
        let val = field.val();

        if (field.is('select')) {

            if (val == 0) {
                return null;
            }
        }

        return val;
    }
})