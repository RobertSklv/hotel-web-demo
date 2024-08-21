
window.createTableComponent = function (tableId) {
    var table = {
        tableId: tableId,
        selectAllCheck: null,
        selectChecks: [],
        massActionForm: null,
        massActionFormInputsContainer: null,
        massActionButtons: [],
        searchBarWrapper: null,
        searchBarWrapperInput: null,
        searchBarWrapperButton: null,
        filtersWrapper: null,

        initialize: function () {
            var self = this;

            var idSelector = '#' + tableId;

            $('.btn-remove-filter', idSelector).on('click', function () {
                self.removeFilter(this);
            });

            this.filtersWrapper = $('.filters-wrapper', idSelector);

            $('#applyFiltersButton').parents('form').on('submit', function () {
                self.applyFilters(this);
            });

            this.filtersWrapper.find('.filter-operator').on('change', function () {
                self.onOperatorChange(this);
            });

            this.filtersWrapper.find('select[name="PageSize"]', idSelector).on('change', function () {
                if (!self.searchBarWrapperInput.hasClass('active-search')) {
                    self.searchBarWrapperInput.removeAttr('name');
                }
                $('.filters-form').trigger('submit');
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

            this.searchBarWrapper = $('.search-bar-wrapper', idSelector);
            this.searchBarWrapperInput = $('.search-bar-wrapper input[type=search]', idSelector);
            this.searchBarWrapperButton = $('.search-bar-wrapper button[type=submit]', idSelector);

            this.searchBarWrapperInput.on('input', function () {
                self.toggleSearchBarButtonState(this);
            });
        },

        removeFilter: function (filter) {
            var propertyName = $(filter).data('remove-filter');

            if (propertyName == '__all') {
                this.filtersWrapper.find(`[data-filter] .filter-value`).val('');
            } else {
                this.filtersWrapper.find(`[data-filter="${propertyName}"] .filter-value`).val('');
            }

            $('#applyFiltersButton').click();
        },

        applyFilters: function (filtersWrapper) {
            var self = this;
            var filters = $(filtersWrapper).find('.filter');

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
        },

        toggleSearchBarButtonState: function (searchInput) {
            this.searchBarWrapperButton.prop('disabled', !$(searchInput).val());
        },
    };

    table.initialize();

    return table;
};