
function PatronCheckInViewModel() {
  this.controller = 'Patron';

  this.neccessaryPaperwork = ko.observable(false);
    this.neccessaryPaperwork.default = false;
  this.search = ko.observable(true);
    this.search.default = true;
  this.serviceSelection = ko.observable();
  this.servicesUsed = ko.observableArray();
  this.servicesUsed.default = [];

  this.paperworkPreviouslyValidated = function() {
    function getDays(millisec) {
      var days = (millisec / (1000 * 60 * 60 * 24)).toFixed(1);
      return days;
    }
    if(this.neccessaryPaperwork()) return;
    if(!this.servicesUsed().length || !this.serviceSelection()) return;
    this.servicesUsed().forEach(function(service) {
      var serviceTypeID = service.serviceType.id;
      var medicalID = 4;
      var dentalID = 5;
      if(~~this.serviceSelection() === serviceTypeID) {
        var serviceUsedDate = new Date(service.createDate);
        var now = Date.now();
        if(serviceTypeID === medicalID || serviceTypeID === dentalID) {
          //If user has visited this year
          if(now.getFullYear() === serviceUsedDate.getFullYear())
            this.neccessaryPaperwork(true);
        }
        else {
          var milisecDiff = serviceUsedDate - now;
          //Has it been a year
          if(getDays(milisecDiff) < 365) {
            this.neccessaryPaperwork(true);
          }
        }
      }
    }.bind(this));
  }.bind(this);

  this.serviceSelection.subscribe(this.paperworkPreviouslyValidated);

  //Patron Properties
  this.firstName = ko.observable('');
    this.firstName.default = '';
  this.middleName = ko.observable('');
    this.middleName.default = '';
  this.lastName = ko.observable('');
    this.lastName.default = '';

  this.fullName = ko.computed(function() {
    return this.firstName() + ' ' + (this.middleName() ? this.middleName() + ' ' : '') + this.lastName();
  }, this);

  this.dateOfBirth = ko.observable('');
    this.dateOfBirth.default = '';
  this.householdOccupants = ko.observable(1);
    this.householdOccupants.default = 1;
  this.veteran = ko.observable(false);
    this.veteran.default = false;

  this.banned = ko.observable(false);
    this.banned.default = false;

  this.maritalStatusID = ko.observable();
    this.maritalStatusID.default = null;
  this.genderID = ko.observable();
    this.genderID.default = null;
  this.ethnicityID = ko.observable();
    this.ethnicityID.default = null;
  this.residenceStatusID = ko.observable();
    this.residenceStatusID.default = null;

  this.addresses = ko.observableArray([ new Address() ]);
    this.addresses.default = [ new Address() ];
  this.addAddress = function() {
    this.addresses.push(new Address());
    $('.zipField').mask('00000-0000');
  }.bind(this);
  this.removeAddress = function(address) {
    //Confirm Alert
    this.addresses.remove(address);
  }.bind(this);

  this.phoneNumbers = ko.observableArray([ new PhoneNumber() ]);
    this.phoneNumbers.default = [ new PhoneNumber() ];
  this.addPhoneNumber = function() {
    this.phoneNumbers.push(new PhoneNumber());
    $('.phoneField').mask('(000) 000-0000');
  }.bind(this);
  this.removePhoneNumber = function(phoneNumber) {
    //Confirm Alert
    this.phoneNumbers.remove(phoneNumber);
  }.bind(this);

  this.emergencyContacts = ko.observableArray([ new EmergencyContact() ]);
    this.emergencyContacts.default = [ new EmergencyContact() ];
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
    if(!this.firstName() && !this.middleName() && !this.lastName() && !this.dateOfBirth())
      return;
    var action = 'FindPatron';
    if(!this.search()) return;
    app.post(this.controller, action, ko.toJSON(this.patronSearchData))
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
          if(key === 'dateOfBirth') patron[key] = moment(patron[key]).format('MM/DD/YYYY');
          this[myKey || key](patron[key].id || patron[key]);
        }
      }
      catch(x) {
        console.log('Issue with key ', key);
      }
    }
    setTimeout(function() {
      // :/
      this.search(false);
      this.paperworkPreviouslyValidated();
    }.bind(this), 600);
  }.bind(this);

  this.patronSearchData = ko.computed(function() {
    return {
      firstName: this.firstName(),
      middleName: this.middleName(),
      lastName: this.lastName(),
      dateOfBirth: this.dateOfBirth()
    };
  }, this).extend({ rateLimit: { timeout: 500, method: "notifyWhenChangesStop" } });
  this.patronSearchData.subscribe(this.autoComplete);
  this.patronSearchData.subscribe(function showSearch() {
    this.search(true);
  }.bind(this));

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
    for(var key in this) {
      try {
        if(this[key].default !== undefined) {
          if(ko.isWriteableObservable(this[key])) {
            this[key](this[key].default);
          }
        }
      }
      catch(x) {
        console.log('Issue with key ', key);
      }
    }
  }.bind(this);

  this.checkIn = function() {
    if(this.banned()) {
      var allowBanned = confirm('This Patron is banned. Are you sure you want to check them in?');
      if(!allowBanned) return $('.ui.modal').modal('hide');
    }
    var action = 'CheckIn';
    if(app.services().length === 1)
      this.serviceSelection(app.services()[0]);

    app.post(this.controller, action, ko.toJSON(this))
    .success(function(data, textStatus, request) {
      alert('Patron Checked In Successfully!');
    }.bind(this))
    .error(function() {
      alert('There was a problem checking the patron in. Please try again later!');
    }.bind(this));
  }.bind(this);
}

PatronCheckInViewModel.prototype.validate = function() {
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
  if (!this.residenceStatusID()) {
      errors.push('Please specify Residence Status');
  }
  if (!this.addresses().length) {
      errors.push('Please include at least one Address!');
  }
  if(~~this.householdOccupants() < 1) {
    errors.push('Please include at least one household occupant!');
  }
  if(this.phoneNumbers().length < 1) {
    errors.push('Please provide at least one phone number!');
  }
  this.addresses().forEach(function(address) {
    address.validate(errors);
  });
  this.emergencyContacts().forEach(function(emergencyContact) {
    emergencyContact.validate(errors);
  });

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