
function ManageVolunteersViewModel() {
  this.pageNumber = ko.pureComputed({
    read: function() {
      return this.val;
    },
    write: function(value) {
      if(value > 1 && value < Math.ceil(this.context.volunteers().length / 10))
        this.val = value;
    },
    owner: { val: 1, context: this }
  });
  this.volunteers = ko.observableArray([]);
  this.paginatedVolunteers = ko.computed(function() {
    var lowerBound = (this.pageNumber() - 1) * 10;
    var upperBound = lowerBound + 10; //Slice grabs the index from lowerBound to upperBound - 1.
    return this.volunteers().slice(lowerBound, upperBound);
  }, this);
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

    this.targetVolunteer().roles = [];
    this.targetVolunteer().services = [];

    $('#volunteerRoles div.ui.fluid.search.dropdown a.ui.label.transition.visible')
    .each((index, element) => {
      this.targetVolunteer().roles.push(parseInt($(element).data('value')));
      }
    );
    $('#volunteerServices div.ui.fluid.search.dropdown a.ui.label.transition.visible')
    .each((index, element) => {
      this.targetVolunteer().services.push(parseInt($(element).data('value')));
      }
    );

    app.post(this.controller, action, ko.toJSON(this.targetVolunteer))
    .success(function() {
      alert('Volunteer successfully modified!');
    }.bind(this))
    .error(function() {
      if(app.authToken())
        alert('There was an issue modifying the volunteer!');
    }.bind(this));
  }.bind(this);

  //Duplicate code is the best!
  this.parseVolunteerRoles = function(roles) {
    return roles.map(function(role) {
      return {
        id: role.ID || role.id,
        name: role.RoleName || role.roleName || role.name
      };
    }.bind(this));
  }.bind(this);
  //Duplicate code is the best!
  this.parseVolunteerServices = (services) => {
    return services.map((service) => {
      return {
        id: service.ID || service.id,
        name: service.ServiceName || service.serviceName || service.name
      };
    });
  };

  this.editVolunteer = (data, event) => {
    data.fullName = data.firstName + ' ' + data.lastName;
    this.targetVolunteer(data);
    $('#editVolunteer').modal('show');
    $('.ui.dropdown').dropdown();
  };

  this.changePassword = () => {
    let action = 'ChangePassword';
    if(this.targetVolunteer().password() !== this.targetVolunteer().confirmPassword()) {
      alert('Passwords need to match!');
      return false;
    }

    app.post(this.controller, action, ko.toJSON(this.targetVolunteer))
    .success(() => {
      alert('Password Changed Successfully');
    })
    .error(() => {
      if(app.authToken())
        alert('Unable to change password, plese try again later!');
    });
  };

  this.editPassword = (data, event) => {
    data.fullName = data.firstName + ' ' + data.lastName;
    data.password = ko.observable();
    data.confirmPassword = ko.observable();
    this.targetVolunteer(data);
    $('#changePassword').modal('show');
  };
}
