function ManageVolunteersViewModel() {
  this.volunteers = ko.observableArray([]);
  this.targetVolunteer = ko.observable();

  this.availableRoles = ko.observableArray([]);

  this.controller = 'Volunteer';

  this.loadVolunteers = function() {
    var action = 'GetVolunteers';
    
    app.get(this.controller, action)
    .success(function(data) {
      this.volunteers(data.volunteers);
    }.bind(this));
  }.bind(this);

  this.loadRoles = function() {
    var action = 'GetRoles';
    var controller = 'Role';
    
    app.get(controller, action)
    .success(function(data) {
      this.availableRoles(data.roles);
    }.bind(this));
  }.bind(this);

  this.editVolunteer = function(data, event) {
    this.targetVolunteer(data);
    //TODO: Fix the modal opening twice :/
    $('#editVolunteer').modal('show');
    $('.ui.dropdown').dropdown();
  }.bind(this);
}