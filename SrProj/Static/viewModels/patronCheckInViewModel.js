function CheckInViewModel() {
    this.controller = 'patronCheckIn';
    
    this.validate = function() {
        var errors= [];
        if (this. patronCheckIn === '1'){
            //Patron Check In Validation
        }
        return errors;
    }.bind(this);

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
}.bind(this);