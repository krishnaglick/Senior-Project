function ManageVolunteersViewModel() {
  this.volunteers = ko.observableArray([]);
  this.targetVolunteer = ko.observable();

  this.availableRoles = ko.observableArray([]);

  this.controller = 'Volunteer';

  this.loadVolunteers = function() {
    var action = 'GetVolunteers';
    
    app.get(this.controller, action)
    .success(function(data) {
      this.volunteers(
        data.volunteers.map(function(volunteer) {
          volunteer.roles = this.parseVolunteerRoles(volunteer.roles);
          return volunteer;
        }.bind(this))
      );
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

  this.parseVolunteerRoles = function(roles) {
    return roles.map(function(role) {
      return {
        id: role.ID || role.id,
        name: role.RoleName || role.roleName || role.name
      };
    });
  };

  this.editVolunteer = function(data, event) {
    this.targetVolunteer(data);
    $('#editVolunteer').modal('show');
    $('.ui.dropdown').dropdown();
  }.bind(this);
}