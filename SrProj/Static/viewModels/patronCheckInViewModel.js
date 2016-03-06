
function PatronCheckInViewModel() {
  this.controller = 'Patron';

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
          errors.push('Please specify Ethnicity');
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
  this.serviceSelection = ko.observable();

  this.foundPatrons = ko.observableArray([]);

  this.autoComplete = function() {
    var action = 'FindPatron';
    app.post(this.controller, action, ko.toJSON(this))
    .success(function(data) {
      this.foundPatrons(data || []);
    }.bind(this));
  }.bind(this);

  this.firstName.subscribe(this.autoComplete);
  this.middleName.subscribe(this.autoComplete);
  this.lastName.subscribe(this.autoComplete);
  this.dateOfBirth.subscribe(this.autoComplete);

  this.showCheckInModal = function () {
    var errors = this.validate();
    if(!errors.length)
      $('.ui.modal').modal('show');
    else
        alert(errors.join('\n'));
  }.bind(this);

  this.clear = function() {
    //Clear out bindings
  }.bind(this);

  this.checkIn = function() {
    var action = 'CheckIn';
    if(app.services().length === 1)
      this.serviceSelection(app.services()[0]);
    else {
      this.serviceSelection($('.ui.dropdown.selection').dropdown('get text')[0]);
    }

    app.post(this.controller, action, ko.toJSON(this))
    .success(function(data, textStatus, request) {
      alert('Patron Checked In!');
    }.bind(this))
    .error(function(data) {
      if(data.responseJSON){
        if(Array.isArray(data.responseJSON) && data.responseJSON.length > 1) {
          //Aggregate errors

          return;
        }
        else if(Array.isArray(data.responseJSON) && data.responseJSON.length == 1) {
          data.responseJSON = data.responseJSON[0];
        }

        //Handle single error.
        alert('Invalid username or password.');
      }
    }.bind(this));
  }.bind(this);
}
