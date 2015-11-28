function ManageVolunteersViewModel() {
  this.volunteers = ko.observableArray([]);

  this.controller = 'Volunteer';

  this.loadVolunteers = function() {
    var action = 'GetVolunteers';
    
    app.get(this.controller, action)
    .success(function(data) {
      this.volunteers(data.volunteers);
    }.bind(this));
  }.bind(this);
}