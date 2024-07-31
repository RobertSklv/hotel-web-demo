
window.createTableComponent = function (tableId) {
    var table = {
        tableId: tableId,

        initialize: function () {
            var self = this;

            $('.btn-remove-filter', '#' + tableId).on('click', function () {
                self.removeFilter(this);
            });

            $('.grid-filters-form', '#' + tableId).on('submit', function () {
                self.applyFilters(this);
            })

            $('.filter-operator', '#' + tableId).on('change', function () {
                self.onOperatorChange(this);
            });
        },

        removeFilter: function (filter) {
            var propertyName = $(filter).data('remove-filter');

            if (propertyName == '__all') {
                $('.grid-filters-form').find(`[data-filter] .filter-value`).val('');
            } else {
                $('.grid-filters-form').find(`[data-filter="${propertyName}"] .filter-value`).val('');
            }

            $('#applyFiltersButton').click();
        },

        applyFilters: function (filtersForm) {
            var self = this;
            var filters = $(filtersForm).find('.filter');

            $.each(filters, function (i, e) {
                var $e = $(e);
                var operatorEl = $e.find('[data-name*=".Operator"]');
                var valueEl = $e.find('[data-name*=".Value"]');
                var secondaryValueEl = $e.find('[data-name*=".SecondaryValue"]');

                if (self.getFieldValue(valueEl)) {
                    operatorEl.attr('name', operatorEl.data('name'));
                    valueEl.attr('name', valueEl.data('name'));
                    secondaryValueEl.attr('name', secondaryValueEl.data('name'));
                }
            });
        },

        onOperatorChange: function (operator) {
            let propName = $(operator).data('filter-operator-for');
            let secValue = $(`[data-filter-secondary-value-for="${propName}"]`);

            if ($(operator).val() == 'btw') {
                secValue.show();
                secValue.prop('disabled', false);
            } else {
                secValue.hide();
                secValue.prop('disabled', true);
            }
        },

        getFieldValue: function (field) {
            let val = field.val();

            if (field.is('select')) {

                if (val == 0) {
                    return null;
                }
            }

            return val;
        }
    };

    table.initialize();

    return table;
};