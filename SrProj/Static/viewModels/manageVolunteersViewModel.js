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

  this.modifyVolunteer = function() {
    var action = 'ModifyVolunteer';

    this.targetVolunteer().roles = $('div.ui.fluid.search.dropdown')
    .dropdown('get value')
    .map(function(data) {
      return parseInt(data);
      return {
        id: parseInt(data)
      };
    });

    /*this.targetVolunteer().roles = this.availableRoles().map(function(role) {
      return chosenRoleIDs.indexOf(role.id) > -1 ? this.roleToRoleModel(role) : null;
    }.bind(this));*/

    console.log(this.targetVolunteer().roles);

    app.post(this.controller, action, ko.toJSON(this.targetVolunteer))
    .success(function() {
      alert('success!');
    }.bind(this))
    .error(function() {
      alert('error');
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

  this.roleToRoleModel = function(role) {
    return {
      ID: role.id,
      RoleName: role.name,
      RoleDescription: role.description
    };
  };

  this.editVolunteer = function(data, event) {
    this.targetVolunteer(data);
    $('#editVolunteer').modal('show');
    $('.ui.dropdown').dropdown();
  }.bind(this);
}