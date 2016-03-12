
function PatronCheckInViewModel() {
  this.controller = 'Patron';

  this.validate = function () {
      var errors = [];
      if (!this.FirstName()) {
          errors.push('Please enter a First Name');
      }
      if (!this.LastName()) {
          errors.push('Please enter a Last Name');
      }
      if (!this.DateOfBirth()) {
          errors.push('Please enter Date of Birth');
      }
      if (!this.Gender()) {
          errors.push('Please specify Gender');
      }
      if (!this.Ethnicity()) {
          errors.push('Please specify Ethnicity');
      }
      if (!this.MaritalStatus()) {
          errors.push('Please specify Marital Status');
      }
      if (!this.StreetAddress()) {
          errors.push('Please input Street Address');
      }
      if (!this.PostalCode()) {
          errors.push('Please enter Postal Code');
      }
      if (!this.HouseholdOccupants()) {
          errors.push('Please enter Household Occupants');
      }

      return errors;
  }.bind(this);

  //Patron Check In
  this.ServiceStatus = ko.observable();
  this.FirstName = ko.observable();
  this.MiddleName = ko.observable();
  this.LastName = ko.observable();
  this.DateOfBirth = ko.observable();
  this.Gender = ko.observable();
  this.Ethnicity = ko.observable();
  this.streetAddress = ko.observable();
  this.ApartmentNumber = ko.observable();
  this.PostalCode = ko.observable();
  this.HouseholdOccupants = ko.observable();
  this.VeteranStatus = ko.observable(false);
  this.MaritalStatus = ko.observable();
  this.ServiceEligibility = ko.observable();
  this.NeccessaryPaperwork = ko.observable(false);
  this.ServiceSelection = ko.observable();

  this.FoundPatrons = ko.observableArray([]);

  this.AutoComplete = function() {
    var action = 'FindPatron';
    app.post(this.controller, action, ko.toJSON(this))
    .success(function(data) {
      this.FoundPatrons(data || []);
    }.bind(this));
  }.bind(this);

  this.FirstName.subscribe(this.autoComplete);
  this.MiddleName.subscribe(this.autoComplete);
  this.LastName.subscribe(this.autoComplete);
  this.DateOfBirth.subscribe(this.autoComplete);

  this.ShowCheckInModal = function () {
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

