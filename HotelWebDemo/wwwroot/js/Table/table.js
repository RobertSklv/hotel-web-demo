
window.createTableComponent = function (tableId) {
    var table = {
        tableId: tableId,
        selectAllCheck: null,
        selectChecks: [],
        massActionForm: null,
        massActionFormInputsContainer: null,
        massActionButtons: [],

        initialize: function () {
            var self = this;

            var idSelector = '#' + tableId;

            $('.btn-remove-filter', idSelector).on('click', function () {
                self.removeFilter(this);
            });

            $('.grid-filters-form', idSelector).on('submit', function () {
                self.applyFilters(this);
            })

            $('.filter-operator', idSelector).on('change', function () {
                self.onOperatorChange(this);
            });

            var massActionFormIdSelector = '#massActionForm-' + tableId;

            this.selectAllCheck = $('.mass-action-check-all', idSelector);
            this.selectChecks = $('.mass-action-check', idSelector);
            this.massActionForm = $(massActionFormIdSelector);
            this.massActionFormInputsContainer = $('.inputs', massActionFormIdSelector);
            this.massActionButtons = $('.mass-action-button', massActionFormIdSelector);

            this.selectAllCheck.on('change', function () {
                self.selectAllRows(this);
            });
            this.selectChecks.on('change', function () {
                self.selectRow(this);
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
        },

        selectAllRows: function (checkbox) {
            checkbox = $(checkbox);

            let isChecked = checkbox.is(':checked');
            let condSelector = isChecked
                ? `:not(:checked)`
                : `:checked`;

            this.selectChecks.filter(condSelector).trigger('click');
        },

        selectRow: function (checkbox) {
            checkbox = $(checkbox);

            if (checkbox.is(':checked')) {
                let dummyInput = this.createDummyInput(checkbox);
                this.massActionFormInputsContainer.append(dummyInput);

                if (this.selectChecks.filter(':not(:checked)').length == 0) {
                    this.selectAllCheck.prop('checked', true);
                }

            } else {
                let itemId = checkbox.data('select-item');
                this.massActionFormInputsContainer.find(`input[data-select-item="${itemId}"]`).remove();

                if (this.selectAllCheck.is(':checked')) {
                    this.selectAllCheck.prop('checked', false);
                }
            }

            this.massActionButtons.prop('disabled', this.selectChecks.filter(':checked').length == 0);
        },

        createDummyInput: function (original) {
            let value = original.val();
            let itemId = original.data('select-item');

            return $(`<input type="hidden" name="selectedItemIds[]" value="${value}" data-select-item="${itemId}" />`);
        }
    };

    table.initialize();

    return table;
};