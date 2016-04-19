
function PatronCheckInViewModel() {
  this.controller = 'Patron';

  this.neccessaryPaperwork = ko.observable(false);
    this.neccessaryPaperwork.default = false;
  this.search = ko.observable(true);
    this.search.default = true;
  this.serviceSelection = ko.observable();
  this.servicesUsed = ko.observableArray([]);
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
        let now = Date.now();
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
  this.id = ko.observable(-1);
    this.id.default = -1;
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
  this.minorHouseholdOccupants = ko.observable(0);
    this.minorHouseholdOccupants.default = 0;
  this.veteran = ko.observable(false);
    this.veteran.default = false;

  this.banned = ko.observable(false);
    this.banned.default = false;
  this.disabledPatron = ko.observable(false);
    this.disabledPatron.default = false;

  this.maritalStatusID = ko.observable();
    this.maritalStatusID.default = '';
  this.genderID = ko.observable();
    this.genderID.default = '';
  this.ethnicityID = ko.observable();
    this.ethnicityID.default = '';
  this.residenceStatusID = ko.observable();
    this.residenceStatusID.default = '';

  this.addresses = ko.observableArray([ new Address() ]);
    this.addresses.default = [ new Address() ];
  this.addAddress = function() {
    this.addresses.push(new Address());
    setTimeout(() => $('.zipField').mask('00000-0000'), 50);
  }.bind(this);
  this.removeAddress = function(address) {
    //Confirm Alert
    this.addresses.remove(address);
  }.bind(this);

  this.phoneNumbers = ko.observableArray([ new PhoneNumber() ]);
    this.phoneNumbers.default = [ new PhoneNumber() ];
  this.addPhoneNumber = function() {
    this.phoneNumbers.push('');
    setTimeout(() => $('.phoneField').mask('000-000-0000'), 50);
  }.bind(this);
  this.removePhoneNumber = function(phoneNumber) {
    //Confirm Alert
    this.phoneNumbers.remove(phoneNumber);
  }.bind(this);

  this.emergencyContacts = ko.observableArray([ new EmergencyContact() ]);
    this.emergencyContacts.default = [ new EmergencyContact() ];
  this.addEmergencyContact = function() {
    this.emergencyContacts.push(new EmergencyContact());
    setTimeout(() => $('.phoneField').mask('000-000-0000'), 50);
  }.bind(this);
  this.removeEmergencyContact = function(emergencyContact) {
    //Confirm Alert
    this.emergencyContacts.remove(emergencyContact);
  }.bind(this);
  //End Patron Properties

  //Patron Searching Functionality
  this.foundPatrons = ko.observableArray([]);
    this.foundPatrons.default = [];

  this.autoComplete = function() {
    if(!this.firstName() && !this.middleName() && !this.lastName() && !this.dateOfBirth())
      return;
    var action = 'FindPatron';
    if(!this.search()) return;
    app.post(this.controller, action, ko.toJSON(this.patronSearchData))
    .success(function(data) {
      data = data.map((d) => {
        if(d.dateOfBirth)
          d.dateOfBirth = moment(d.dateOfBirth).format('MM/DD/YYYY');
        return d;
      });
      this.foundPatrons(data || []);
    }.bind(this));
  }.bind(this);

  this.fillPatron = (patron) => {
    for(var key in patron) {
      try {
        var myKey;
        if(!this[key] && this[key + 'ID']) myKey = key + 'ID';

        if(ko.isWriteableObservable(this[myKey || key])) {
          if(key === 'dateOfBirth') patron[key] = moment(patron[key]).format('MM/DD/YYYY');
          if(key === 'phoneNumbers') patron[key] = patron[key].map((pn) => new PhoneNumber(pn));
          if(key === 'addresses') patron[key] = patron[key].map((adr) => new Address(adr));
          if(key === 'emergencyContacts') patron[key] = patron[key].map((ec) => new EmergencyContact(ec));
          this[myKey || key](patron[key].id || patron[key]);
        }
      }
      catch(x) {
        console.log('Issue with key ', key, '\n', x);
      }
    }
    setTimeout(() => {
      // :/
      this.search(false);
      this.paperworkPreviouslyValidated();
    }, 600);
  };

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

  this.showCheckInModal = () => {
    if(app.services().length === 1)
      this.serviceSelection(app.services()[0].id);
    this.servicesUsed = ko.observableArray(this.servicesUsed.default);

    var errors = this.validate();
    if(!errors.length)
      $('.ui.modal.patronCheckIn').modal('show');
    else
      alert(errors.join('\n'));
  };

  this.clear = (hideConfirm) => {
    if(hideConfirm || confirm("Are you sure you want to clear?")) {
      for(var key in this) {
        try {
          if(this[key].default !== undefined) {
            this[key](this[key].default);
          }
        }
        catch(x) {
          console.log('Issue with key ', key);
        }
      }
      $('.error').each((i, element) => $(element).removeClass('error'));
    }
  };

  this.checkIn = function() {
    if(this.banned()) {
      var allowBanned = confirm('This Patron is banned. Are you sure you want to check them in?');
      if(!allowBanned) return $('.ui.modal').modal('hide');
    }
    var action = 'CheckIn';

    app.post(this.controller, action, ko.toJSON(this))
    .success(function(data, textStatus, request) {
      alert('Patron Checked In Successfully!');
      this.clear(true);
    }.bind(this))
    .error(function() {
      if(app.authToken())
        alert('There was a problem checking the patron in. Please try again later!');
    }.bind(this));
  }.bind(this);
}

function markFieldAsErrored(bindingName, mark, index) {
  let element;
  if(index)
    element = $($(`.field.${bindingName}`)[index]);
  else
    element = $(`.field.${bindingName}`);

  if(mark) {
    element.addClass('error');
  }
  else {
    element.removeClass('error');
  }
}

PatronCheckInViewModel.prototype.validate = function() {
  var errors = [];
  if (!this.firstName()) {
    errors.push('Please enter a First Name');
    markFieldAsErrored('firstName', true);
  }
  else {
    markFieldAsErrored('firstName', false);
  }
  if (!this.lastName()) {
      errors.push('Please enter a Last Name');
      markFieldAsErrored('lastName', true);
  }
  else {
    markFieldAsErrored('lastName', false);
  }
  if (!this.dateOfBirth()) {
      errors.push('Please enter Date of Birth');
      markFieldAsErrored('dateOfBirth', true);
  }
  else {
    markFieldAsErrored('dateOfBirth', false);
  }
  if (!this.genderID()) {
      errors.push('Please specify Gender');
      markFieldAsErrored('genderID', true);
  }
  else {
    markFieldAsErrored('genderID', false);
  }
  if (!this.ethnicityID()) {
      errors.push('Please specify Ethnicity');
      markFieldAsErrored('ethnicityID', true);
  }
  else {
    markFieldAsErrored('ethnicityID', false);
  }
  if (!this.maritalStatusID()) {
      errors.push('Please specify Marital Status');
      markFieldAsErrored('maritalStatusID', true);
  }
  else {
    markFieldAsErrored('maritalStatusID', false);
  }
  if (!this.residenceStatusID()) {
      errors.push('Please specify Residence Status');
      markFieldAsErrored('residenceStatusID', true);
  }
  else {
    markFieldAsErrored('residenceStatusID', false);
  }
  if(!this.serviceSelection()) {
    errors.push('Please choose a service selection!');
    markFieldAsErrored('serviceSelection', true);
  }
  else {
    markFieldAsErrored('serviceSelection', false);
  }
  if(!this.neccessaryPaperwork()) {
    errors.push('Please ensure the Patron has provided the neccessary paperwork!');
    markFieldAsErrored('neccessaryPaperwork', true);
  }
  else {
    markFieldAsErrored('neccessaryPaperwork', false);
  }
  if (!this.addresses().length) {
      errors.push('Please include at least one Address!');
  }
  if(~~this.householdOccupants() < 1) {
    errors.push('Please include at least one household occupant!');
    markFieldAsErrored('householdOccupants', true);
  }
  else {
    markFieldAsErrored('householdOccupants', false);
  }
  if(~~this.minorHouseholdOccupants() > ~~this.householdOccupants() - 1) {
    errors.push(`At least one member of the household must be over 18!`);
    markFieldAsErrored('minorHouseholdOccupants', true);
    markFieldAsErrored('householdOccupants', true);
  }
  else {
    markFieldAsErrored('minorHouseholdOccupants', false);
    markFieldAsErrored('householdOccupants', false);
  }
  for(let i = 0; i < this.phoneNumbers().length; i++) {
    this.phoneNumbers()[i].validate(errors, i);
  }
  for(let i = 0; i < this.addresses().length; i++) {
    this.addresses()[i].validate(errors, i);
  }
  for(let i = 0; i < this.emergencyContacts().length; i++) {
    this.emergencyContacts()[i].validate(errors, i);
  }

  return errors;
};

function Address(address) {
  address = address || {}; //WTB default params :(
  this.streetAddress = ko.observable(address.streetAddress || '');
  this.city = ko.observable(address.city || '');
  this.state = ko.observable(address.state || '');
  this.zip = ko.observable(address.zip || '');

  this.validate = (errors, index) => {
    if(!this.streetAddress()) {
      errors.push('Please include a street address!');
      markFieldAsErrored('streetAddress', true, index);
    }
    else {
      markFieldAsErrored('streetAddress', false, index);
    }
    if(!this.city()) {
      errors.push('Please include a city!');
      markFieldAsErrored('city', true, index);
    }
    else {
      markFieldAsErrored('city', false, index);
    }
    if(!this.state()) {
      errors.push('Please include a state!');
      markFieldAsErrored('state', true, index);
    }
    else {
      markFieldAsErrored('state', false, index);
    }
    if(!this.zip()) {
      errors.push('Please include a zip code!');
      markFieldAsErrored('zip', true, index);
    }
    else {
      markFieldAsErrored('zip', false, index);
    }
    return errors;
  };
}

function EmergencyContact(emergencyContact, index) {
  emergencyContact = emergencyContact || {};
  this.firstName = ko.observable(emergencyContact.firstName || '');
  this.lastName = ko.observable(emergencyContact.lastName || '');
  this.fullName = ko.computed(function() {
    return this.firstName() + ' ' + this.lastName();
  }.bind(this));

  this.phoneNumber = ko.observable(emergencyContact.phoneNumber || '');

  this.validate = (errors) => {
    //Remove return to make emergency contacts required.
    return errors;
    if(!this.firstName()) {
      errors.push('Emergency Contacts need a First Name!');
      markFieldAsErrored('emergencyContactFirstName', true, index);
    }
    else {
      markFieldAsErrored('emergencyContactFirstName', false, index);
    }
    if(!this.lastName()) {
      errors.push('Emergency Contacts need a Last Name!');
      markFieldAsErrored('emergencyContactLastName', true, index);
    }
    else {
      markFieldAsErrored('emergencyContactLastName', false, index);
    }
    if(!this.phoneNumber()) {
      errors.push('Emergency Contacts need a Phone Number!');
      markFieldAsErrored('emergencyContactPhoneNumber', true, index);
    }
    else {
      markFieldAsErrored('emergencyContactPhoneNumber', false, index);
    }

    return errors;
  };
}

function PhoneNumber(phoneNumber, index) {
  phoneNumber = phoneNumber || {};
  this.phoneNumber = ko.observable(phoneNumber.phoneNumber || '');

  this.validate = (errors) => {
    if(!this.phoneNumber()) {
      errors.push('A phone number can not be blank!');
      markFieldAsErrored('phoneNumber', true, index);
    }
    else {
      markFieldAsErrored('phoneNumber', false, index);
    }

    return errors;
  };
}