function CreateVolunteerViewModel() {
  this.firstname = ko.observable();
  this.lastname = ko.observable();
  this.email = ko.observable();
  this.username = ko.observable();
  this.password = ko.observable();
  this.confirmPassword = ko.observable();
  this.roles = ko.observableArray([]);
  this.services = ko.observableArray([]);

  this.controller = 'Volunteer';

  this.clearCredentials = () => {
    this.firstname('');
    this.lastname('');
    this.email('');
    this.username('');
    this.password('');
    this.confirmPassword('');
    this.roles([]);
    this.services([]);
  };

  this.validate = () => {
    var errors = [];
    if(!this.firstname()) {
      errors.push('\nYou need a First Name!');
    }

    if(!this.lastname()) {
      errors.push('\nYou need a Last Name!');
    }

    if(!this.username()) {
      errors.push('\nYou need a Username!');
    }

    if(!this.password()) {
      errors.push('\nYou need a Password!');
    }

    if(this.password() != this.confirmPassword()) {
      errors.push('\nYour passwords don\'t match!');
    }

    if(!this.roles().length) {
      errors.push('\nPlease provide at least one role!\n');
    }

    if(!this.services().length) {
      errors.push('\nPlease provide at least one service!\n');
    }

    return errors;
  };

  this.createVolunteer = function() {
    this.roles([]);
    this.services([]);

    $('#newVolunteerRoles div.ui.fluid.search.dropdown a.ui.label.transition.visible')
    .each((index, element) => {
      this.roles.push(parseInt($(element).data('value')));
      }
    );
    $('#newVolunteerServices div.ui.fluid.search.dropdown a.ui.label.transition.visible')
    .each((index, element) => {
      this.services.push(parseInt($(element).data('value')));
      }
    );

    var validationErrors = this.validate();
    if(validationErrors.length) {
      return alert(validationErrors);
    }

    var action = 'CreateVolunteer';
    app.post(this.controller, action, ko.toJSON(this))
    .success(function(data, textStatus, request) {
      alert('User Created!');
    }.bind(this))
    .error(function(data) {
      if(app.authToken())
        alert('Invalid username or password.');
    }.bind(this));
  }.bind(this);
}