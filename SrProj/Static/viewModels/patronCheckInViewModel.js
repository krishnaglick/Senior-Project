
function PatronCheckInViewModel() {
  this.controller = 'Patron';

  this.NeccessaryPaperwork = ko.observable(false);
  this.ServiceSelection = ko.observable();

  //Patron Properties
  this.FirstName = ko.observable('');
  this.MiddleName = ko.observable('');
  this.LastName = ko.observable('');
  this.FullName = ko.computed(function() {
    return this.FirstName() + ' ' + (this.MiddleName() ? this.mMddleName() + ' ' : '') + this.LastName();
  }, this);

  this.DateOfBirth = ko.observable('');
  this.HouseholdOccupants = ko.observable(0);
  this.Veteran = ko.observable(false);

  this.Banned = ko.observable(false);

  this.maritalstatus = ko.observable();
  this.gender = ko.observable();
  this.ethnicity = ko.observable();
  this.residencestatus = ko.observable();

  this.addresses = ko.observableArray([ new address() ]);
  this.addAddress = function() {
    this.addresses.push(new address());
  }.bind(this);
  this.removeaddress = function(address) {
    //Confirm Alert
    this.addresses.remove(address);
  }.bind(this);

  this.phonenumbers = ko.observableArray([ new phonenumber() ]);
  this.addphonenumber = function() {
    this.phonenumbers.push(new phonenumber());
  }.bind(this);
  this.removephonenumber = function(phonenumber) {
    //Confirm Alert
    this.phonenumbers.remove(phonenumber);
  }.bind(this);

  this.emergencycontacts = ko.observableArray([ new emergencycontact() ]);
  this.addemergencycontact = function() {
    this.emergencycontacts.push(new emergencycontact());
  }.bind(this);
  this.removeemergencycontact = function(emergencycontact) {
    //Confirm Alert
    this.emergencycontacts.remove(emergencycontact);
  }.bind(this);
  //End Patron Properties

  //Patron Searching Functionality
  this.foundpatrons = ko.observableArray([]);

  this.autoComplete = function() {
    var action = 'FindPatron';
    app.post(this.controller, action, ko.toJSON(this))
    .success(function(data) {
      this.foundpatrons(data || []);
    }.bind(this));
  }.bind(this);

  this.firstname.subscribe(this.autocomplete);
  this.middlename.subscribe(this.autocomplete);
  this.lastname.subscribe(this.autocomplete);
  this.dateOfbirth.subscribe(this.autocomplete);

  this.parseaddress = function(address) {
    if(address && address[0] && address[0].streetaddress)
      return address[0].streetaddress;
    return '';
  }.bind(this);
  //End Patron Searching Functionality

  this.showcheckinmodal = function () {
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

PatronCheckInViewModel.prototype.validate = function() {
  //TODO: Go over this.
  var errors = [];
  if (!this.firstname()) {
    errors.push('Please enter a First Name');
  }
  if (!this.lastname()) {
      errors.push('Please enter a Last Name');
  }
  if (!this.dateOfbirth()) {
      errors.push('Please enter Date of Birth');
  }
  if (!this.gender()) {
      errors.push('Please specify Gender');
  }
  if (!this.ethnicity()) {
      errors.push('Please specify Ethnicity');
  }
  if (!this.maritalstatus()) {
      errors.push('Please specify Marital Status');
  }
  if (!this.addresses().length) {
      errors.push('Please include at least one Address!');
  }

  this.addresses().forEach(function(address) {
    address.validate(errors);
  });

  if (!this.householdoccupants()) {
      errors.push('Please enter Household Occupants');
  }
  return errors;
};

function Address() {
  this.streetaddress = ko.observable('');
  this.city = ko.observable('');
  this.state = ko.observable('');
  this.zip = ko.observable('');

  this.validate = function(errors) {
    if(!this.streetaddress()) {
      errors.push('Please include a street address!');
    }
    if(!this.city()) {
      errors.push('Please include a city!');
    }
    if(!this.state()) {
      errors.push('Please include a state!');
    }
    if(!this.zip()) {
      errors.push('Please include a zip code!');
    }
    return errors;
  }.bind(this);
}

function emergencycontact() {
  this.firstname = ko.observable('');
  this.lastname = ko.observable('');
  this.fullname = ko.computed(function() {
    return this.firstname() + ' ' + this.lastname();
  }.bind(this));

  this.phonenumber = ko.observable('');

  this.validate = function(errors) {
    if(!this.firstname())
      errors.push('Emergency Contacts need a First Name!');
    if(!this.lastname())
      errors.push('Emergency Contacts need a Last Name!');
    if(!this.phonenumber())
      errors.push('Emergency Contacts need a Phone Number!');

    return errors;
  }.bind(this);
}

function phonenumber() {
  this.phonenumber = ko.observable('');
}