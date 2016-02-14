function CreateVolunteerViewModel() {
  this.firstname = ko.observable();
  this.lastname = ko.observable();
  this.email = ko.observable();
  this.username = ko.observable();
  this.password = ko.observable();
  this.confirmPassword = ko.observable();

  this.controller = 'Volunteer';

  this.clearCredentials = function() {
    this.firstname('');
    this.lastname('');
    this.email('');
    this.username('');
    this.password('');
    this.confirmPassword('');
  }.bind(this);

  this.validate = function() {
    var errors = [];
        if(!this.firstname()) {
      errors.push('You need a firstname!');
    }

    if(!this.lastname()) {
      errors.push('You need a lastname!');
    }

    if(!this.email()) {
      errors.push('You need an email!');
    }

    if(!this.username()) {
      errors.push('You need a username!');
    }

    if(this.password() != this.confirmPassword()) {
      errors.push('Your passwords don\'t match!');
    }

    return errors;
  }.bind(this);

  this.createVolunteer = function() {
    var validationErrors = this.validate();
    if(validationErrors.length) {
      alert(validationErrors);
      return;
    }

    var action = 'CreateVolunteer';
    app.post(this.controller, action, ko.toJSON(this))
      .success(function(data, textStatus, request) {
        alert('User Created!');
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
        //debugger;
      }.bind(this));
  }.bind(this);
}