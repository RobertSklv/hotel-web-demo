
window.newKnownCustomerComponent = function (fieldsWrapperId) {
    var component = {
        wrapperId: fieldsWrapperId,
        wrapperIdSelector: '#' + fieldsWrapperId,
        nationalIdField: null,
        passportIdField: null,
        restCustomerFields: null,
        mapToFields: null,
        knownCustomerAlert: null,
        linkCustomerLink: null,
        cancelAutofillAlert: null,
        cancelAutofillLink: null,
        fieldsMap: {
            Id: "Id",
            CitizenshipId: "CitizenshipId",
            FirstName: "FirstName",
            MiddleName: "MiddleName",
            LastName: "LastName",
            DateOfBirth: "DateOfBirth",
            StreetLine1: "Address.StreetLine1",
            StreetLine2: "Address.StreetLine2",
            StreetLine3: "Address.StreetLine3",
            CountryId: "Address.CountryId",
            City: "Address.City",
            PostalCode: "Address.PostalCode",
            Phone: "Address.Phone",
        },
        isAutofillActive: false,
        currentAutofill: null,

        init: function () {
            var self = this;
            this.nationalIdField = $('.national-id-field', this.wrapperIdSelector);
            this.passportIdField = $('.passport-id-field', this.wrapperIdSelector);
            this.restCustomerFields = $('.rest-customer-fields', this.wrapperIdSelector);
            this.knownCustomerAlert = $('.known-customer-alert', this.wrapperIdSelector);
            this.linkCustomerLink = $('.known-customer-alert .link-customer-link', this.wrapperIdSelector);
            this.cancelAutofillAlert = $('.cancel-autofill-alert', this.wrapperIdSelector);
            this.cancelAutofillLink = $('.cancel-autofill-alert .cancel-autofill-link', this.wrapperIdSelector);
            this.mapToFields = $('[data-map-to]', this.wrapperIdSelector);

            this.nationalIdField.on('input', function () {
                self._onNationalIdChanged($(this).val());
            });

            this.passportIdField.on('input', function () {
                self._onPassportIdChanged($(this).val());
            });

            this.linkCustomerLink.on('click', function (e) {
                e.preventDefault();
                self._autofill();
            });

            this.cancelAutofillLink.on('click', function (e) {
                e.preventDefault();
                self._cancelAutofill();
            });
        },

        _statusCheck: function () {
            if (this.isAutofillActive) {
                this.cancelAutofillAlert.show();
            } else {
                this.cancelAutofillAlert.hide();
            }

            if (!!this.currentAutofill && (this.currentAutofill.isNew || !this.isAutofillActive)) {
                this.knownCustomerAlert.find('.id-type').text(this.currentAutofill.idType);
                this.knownCustomerAlert.find('.id').text(this.currentAutofill.idValue);
                this.knownCustomerAlert.show();
                this.currentAutofill.isNew = false;
            } else {
                this.knownCustomerAlert.find('.id-type').text('');
                this.knownCustomerAlert.find('.id').text('');
                this.knownCustomerAlert.hide();
            }
        },

        _onNationalIdChanged: function (nationalId) {
            this._requestCustomerData(
                'GetByNationalId',
                'nationalId',
                nationalId,
                (customerData) => this._setCurrentAutofill(customerData, 'national ID', nationalId)
            );
        },

        _onPassportIdChanged: function (passportId) {
            this._requestCustomerData(
                'GetByPassportId',
                'passportId',
                passportId,
                (customerData) => this._setCurrentAutofill(customerData, 'passport ID', passportId)
            );
        },

        _requestCustomerData: function (actionName, parameterKey, parameterValue, onSuccess) {
            $.ajax({
                url: `/Admin/Customer/${actionName}?${parameterKey}=${parameterValue}`,
                method: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    onSuccess(response);
                },
                error: function (response) {
                    console.error('Error at requesting customer data', response);
                }
            });
        },

        _setCurrentAutofill: function (customerData, idType, idValue) {
            if (customerData) {
                this.currentAutofill = {
                    customerData,
                    idType,
                    idValue,
                    isNew: !!this.currentAutofill
                        ? this.currentAutofill.idValue !== idValue
                        : true
                };
            } else {
                this.currentAutofill = null;
            }

            this._statusCheck();
        },

        _autofill: function () {
            this.isAutofillActive = true;
            this._statusCheck();
            this._mapFieldsAndLock();
        },

        _cancelAutofill: function () {
            if (this.isAutofillActive) {
                this._resetAndUnlockFields();
            }

            this.isAutofillActive = false;
            this._statusCheck();
        },

        _mapFieldsAndLock: function () {
            this.mapToFields.each(function (i, e) {
                let mapToField = $(e).data('map-to');
                let mappedField = this.fieldsMap[mapToField];
                $(e).val(this.getAtPath(this.currentAutofill.customerData, mappedField));
                $(e).prop('disabled', !$(e).is('[type=hidden]'));
            }.bind(this));
        },

        _resetAndUnlockFields: function () {
            this.mapToFields.each(function (i, e) {
                $(e).val('');
                $(e).prop('disabled', false);
            }.bind(this));
        },



        //TODO: to be moved to a utility file
        getAtPath: function (object, path, throwsError = false) {
            if (typeof path !== 'string') {
                console.error('The path argument is expected to be a string, actual value: ' + path);
            }

            let names = path.split('.');
            let tempLevel = object;

            for (let i = 0; i < names.length; i++) {
                let n = names[i];

                if (typeof tempLevel[n] === 'undefined') {
                    if (throwsError) {
                        console.error(`No value found at the path: ${path}`);
                    }
                    return false;
                }

                tempLevel = tempLevel[n];
            }

            return tempLevel;
        },

        getOrCreateAtPath: function(object, path) {
            if (typeof path !== 'string') {
                console.error('The path argument is expected to be a string, actual value: ' + path);
            }

            let names = path.split('.');
            let tempLevel = object;

            for (let i = 0; i < names.length; i++) {
                let n = names[i];
                let isArray = false;

                if (i + 1 < names.length) {
                    //next name exists
                    let nextName = names[i + 1];
                    let firstChar = nextName.charAt(0);
                    var startsWithNumber = firstChar >= 0 && firstChar <= 9;
                    isArray = startsWithNumber;
                }

                if (typeof tempLevel[n] === 'undefined') {
                    tempLevel[n] = isArray ? [] : {};
                }

                tempLevel = tempLevel[n];
            }

            return tempLevel;
        },

        setAtPath: function (object, path, value) {
            if (typeof path !== 'string') {
                console.error('The path argument is expected to be a string, actual value: ' + path);
            }

            let names = path.split('/');
            let parentPath = names.slice(0, names.length - 1);
            let propName = names.slice(-1);
            let data = getOrCreateAtPath(object, parentPath.join('/'));
            data[propName] = value;
        }
    };

    component.init();
};