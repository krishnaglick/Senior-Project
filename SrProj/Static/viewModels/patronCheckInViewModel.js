
function PatronCheckInViewModel() {
  this.controller = 'Patron';

  this.neccessaryPaperwork = ko.observable(false);
  this.serviceSelection = ko.observable();

  //Patron Properties
  this.firstName = ko.observable('').extend({ rateLimit: { timeout: 500, method: "notifyWhenChangesStop" } });
  this.middleName = ko.observable('').extend({ rateLimit: { timeout: 500, method: "notifyWhenChangesStop" } });
  this.lastName = ko.observable('').extend({ rateLimit: { timeout: 500, method: "notifyWhenChangesStop" } });
  this.fullName = ko.computed(function() {
    return this.firstName() + ' ' + (this.middleName() ? this.middleName() + ' ' : '') + this.lastName();
  }, this);

  this.dateOfBirth = ko.observable('').extend({ rateLimit: { timeout: 500, method: "notifyWhenChangesStop" } });
  this.householdOccupants = ko.observable(1);
  this.veteran = ko.observable(false);

  this.banned = ko.observable(false);

  this.maritalStatusID = ko.observable();
  this.genderID = ko.observable();
  this.ethnicityID = ko.observable();
  this.residenceStatusID = ko.observable();

  this.addresses = ko.observableArray([ new Address() ]);
  this.addAddress = function() {
    this.addresses.push(new Address());
  }.bind(this);
  this.removeAddress = function(address) {
    //Confirm Alert
    this.addresses.remove(address);
  }.bind(this);

  this.phoneNumbers = ko.observableArray([ new PhoneNumber() ]);
  this.addPhoneNumber = function() {
    this.phoneNumbers.push(new PhoneNumber());
  }.bind(this);
  this.removePhoneNumber = function(phoneNumber) {
    //Confirm Alert
    this.phoneNumbers.remove(phoneNumber);
  }.bind(this);

  this.emergencyContacts = ko.observableArray([ new EmergencyContact() ]);
  this.addEmergencyContact = function() {
    this.emergencyContacts.push(new EmergencyContact());
  }.bind(this);
  this.removeEmergencyContact = function(emergencyContact) {
    //Confirm Alert
    this.emergencyContacts.remove(emergencyContact);
  }.bind(this);
  //End Patron Properties

  //Patron Searching Functionality
  this.foundPatrons = ko.observableArray([]);

  this.autoComplete = function() {
    var action = 'FindPatron';
    app.post(this.controller, action, ko.toJSON(this))
    .success(function(data) {
      this.foundPatrons(data || []);
    }.bind(this));
  }.bind(this);

  this.fillPatron = function(patron) {
    for(var key in patron) {
      try {
        var myKey;
        if(!this[key] && this[key + 'ID']) myKey = key + 'ID';

        if(ko.isWriteableObservable(this[myKey || key])) {
          console.log(key, ': ', patron[key]);
          this[myKey || key](patron[key].id || patron[key]);
        }
      }
      catch(x) {
        console.log('Issue with key ', key);
      }
    }
  }.bind(this);

  this.firstName.subscribe(this.autoComplete);
  this.middleName.subscribe(this.autoComplete);
  this.lastName.subscribe(this.autoComplete);
  this.dateOfBirth.subscribe(this.autoComplete);

  this.parseAddress = function(address) {
    if(address && address[0] && address[0].streetAddress)
      return address[0].streetAddress;
    return '';
  }.bind(this);
  //End Patron Searching Functionality

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

PatronCheckInViewModel.prototype.validate = function() {
  //TODO: Go over this.
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
  if (!this.genderID()) {
      errors.push('Please specify Gender');
  }
  if (!this.ethnicityID()) {
      errors.push('Please specify Ethnicity');
  }
  if (!this.maritalStatusID()) {
      errors.push('Please specify Marital Status');
  }
  if (!this.addresses().length) {
      errors.push('Please include at least one Address!');
  }

  this.addresses().forEach(function(address) {
    address.validate(errors);
  });

  if (!this.householdOccupants()) {
      errors.push('Please enter Household Occupants');
  }
  return errors;
};

function Address() {
  this.streetAddress = ko.observable('');
  this.city = ko.observable('');
  this.state = ko.observable('');
  this.zip = ko.observable('');

  this.validate = function(errors) {
    if(!this.streetAddress()) {
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

function EmergencyContact() {
  this.firstName = ko.observable('');
  this.lastName = ko.observable('');
  this.fullName = ko.computed(function() {
    return this.firstName() + ' ' + this.lastName();
  }.bind(this));

  this.phoneNumber = ko.observable('');

  this.validate = function(errors) {
    if(!this.firstName())
      errors.push('Emergency Contacts need a First Name!');
    if(!this.lastName())
      errors.push('Emergency Contacts need a Last Name!');
    if(!this.phoneNumber())
      errors.push('Emergency Contacts need a Phone Number!');

    return errors;
  }.bind(this);
}

function PhoneNumber() {
  this.phoneNumber = ko.observable('');
}