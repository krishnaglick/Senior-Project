
function ReportingViewModel() {
  this.controller = 'Reporting';

  this.validate = function() {
    var errors = [];
    if(this.reportingType === '1') {
      //user validation
    }
    else if(this.reportingType === '2') {
      //service validation
    }

    return errors;
  }.bind(this);

  this.reportingType = ko.observable();

  this.serviceTypeSelections = ko.observableArray([]);
  this.serviceTypes = app.services;

  //Visibility
  this.patronSectionVisible = ko.computed(function() {
    return this.reportingType() === '1';
  }, this);
  this.serviceSectionVisible = ko.computed(function() {
    return this.reportingType() === '2';
  }, this);

  //Patron Reporting
  this.firstName = ko.observable();
  this.lastName =  ko.observable();
  this.dateOfBirth =  ko.observable();

  //Service Reporting
  this.andSearch = ko.observable(false);
  this.zipCode = ko.observable();
  this.timePeriod = ko.observable();
  this.startDate = ko.observable();
  this.endDate = ko.observable();
  this.canSelectTimePeriod = ko.computed(function() {
    return ['1', '2'].indexOf(this.timePeriod()) > -1;
  }, this);
  this.timePeriodSelectIsDateRange = ko.computed(function() {
    return this.timePeriod() === '2';
  }, this);

  this.getReport = function() {
    var validationErrors = this.validate();
    if(validationErrors.length)
      return alert(validationErrors);

    var action = '';
    if(this.reportingType() === '1')
      action = 'GetPatronData';
    else if(this.reportingType() === '2')
      action = 'GetServiceData';
    else
      return alert('Please select a reporting type before trying to get a report!');

    var services = [];
    $('div.ui.fluid.search.dropdown a.ui.label.transition.visible')
    .each(function(index, element) {
      services.push(parseInt($(element).data('value')));
      }.bind(this)
    );
    //The binding for this doesn't work properly. I need to fix it.
    this.serviceTypeSelections = app.services().filter(function(service) {
      return services.includes(service.id);
    });
    //TODO: Remap this to use this.timePeriod, when it works.
    this.timePeriod($('.ui.timePeriod.dropdown.selection div.item.active.selected').text());

    app.post(this.controller, action, ko.toJSON(this))
      .success(function(data, textStatus, request) {
        //Export data to CSV.
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
          alert('Phhbt.');
        }
        //debugger;
      }.bind(this));
  }.bind(this);

  this.clear = function() {
    this.firstName('');
    this.lastName('');
    this.dateOfBirth('');
    this.startDate('');
    this.endDate('');
    this.serviceTypeSelections([]);

    //timePeriod
    //reportingType
  }.bind(this);
}
