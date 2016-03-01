
function PatronCheckInViewModel() {
  this.controller = 'Patron';

  this.validate = function() {
    var errors = [];
    //Validation here.
    if(errors.length)
      return errors;
  }.bind(this);

  //Patron Check In
  this.serviceStatus = ko.observable();
  this.firstName = ko.observable();
  this.middleName = ko.observable();
  this.lastName = ko.observable();
  this.dateOfBirth = ko.observable();
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

  this.foundPatrons = ko.observableArray([]);

  (function testData() {
    var testPatron = {
      firstName: 'Tom',
      lastName: 'Hardy',
      dateOfBirth: '12/12/2012',
      address: '12345 SW 1st St, Apt 783' //This will be aggregate data maybe I don't know
    };
    for(var i = 0; i < 5; i++)
      this.foundPatrons.push(testPatron);
  }).call(this);
}
