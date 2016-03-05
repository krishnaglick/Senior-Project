function CheckInViewModel() {
    //Patron Check In
    this.serviceStatus = ko.observable();
    this.firstName = ko.observable();
    this.middleName = ko.observable();
    this.lastName = ko.observable();
    this.dateOfBirth = ko.obervable();
    this.gender = ko.observable();
    this.ethnicity = ko.observable();
    this.streetAdress = ko.observable();
    this.apartmentNumber = ko.observable();
    this.postalCode = ko.observable();
    this.householdOccupants = ko.observable();
    this.veteranStatus = ko.observable(false);
    this.maritalStatus = ko.observable();
    this.serviceEligibility = ko.observable();
    this.neccessaryPaperwork = ko.observable(false);

    this.controller = 'patronCheckIn';

    this.validate = function () {
        var errors = [];
        if (!this.firstName()) {
            errors.push('Please enter a First Name');
        }
        if (!this.lastName()) {
            errors.push('Please enter a Last Name');
        }
        if (!this.dateOfBirth()) {
            errors.push('Please enter Date of Birth');
        }
        if (!this.gender()) {
            errors.push('Please specify Gender');
        }
        if (!this.ethnicity()) {
            errors.push('Please specify ethnicity');
        }
        if (!this.maritalStatus()) {
            errors.push('Please specify Marital Status');
        }
        if (!this.streetAddress()) {
            errors.push('Please input Street Address');
        }
        if (!this.postalCode()) {
            errors.push('Please enter Postal Code');
        }
        if (!this.householdOccupants()) {
            errors.push('Please enter Household Occupants');
        }

        return errors;
    }.bind(this);

    this.checkInVolunteer = function () {
        var validationErrors = this.validate();
        if (validationErrors.length) {
            alert(validationErrors);
            return;
        }
    }.bind(this);

}