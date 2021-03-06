
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
  this.hasReport = ko.observable(false);

  //Patron Reporting
  this.id = ko.observable();
  this.firstName = ko.observable();
  this.middleName = ko.observable();
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
  this.firstDateFieldLabel = ko.computed(function() {
    let tp = this.timePeriod();
    if(tp == 1)
      return 'What Day?';
    if(tp == 2)
      return 'Start Date';
  }, this);
  this.secondDateFieldLabel = ko.computed(function() {
    let tp = this.timePeriod();
    if(tp == 2)
      return 'End Date';
  }, this);

  //Patron auto-complete
  this.foundPatrons = ko.observableArray([]);
  this.search = ko.observable(true);
  this.patronSearchData = ko.computed(function() {
    return {
      firstName: this.firstName(),
      middleName: this.middleName(),
      lastName: this.lastName(),
      dateOfBirth: this.dateOfBirth()
    };
  }, this).extend({ rateLimit: { timeout: 500, method: "notifyWhenChangesStop" } });
  this.autoComplete = () => {
    this.hasReport(false);
    if(!this.firstName() && !this.middleName() && !this.lastName() && !this.dateOfBirth())
      return;

    let controller = 'Patron';
    let action = 'FindPatron';
    if(!this.search()) return;

    app.post(controller, action, ko.toJSON(this.patronSearchData))
    .success((patrons) => {
      patrons = patrons.map((patron) => {
        return {
          id: patron.id,
          dateOfBirth: moment(patron.dateOfBirth).format('MM/DD/YYYY'),
          firstName: patron.firstName,
          middleName: patron.middleName,
          lastName: patron.lastName
        };
      });
      this.foundPatrons(patrons || []);
    });
  };
  this.patronSearchData.subscribe(this.autoComplete);
  this.patronSearchData.subscribe(() => this.search(true));
  this.fillPatron = (patron) => {
    this.id(patron.id);
    this.firstName(patron.firstName);
    this.middleName(patron.middleName);
    this.lastName(patron.lastName);
    this.dateOfBirth(patron.dateOfBirth);
    setTimeout(() => {
      this.search(false);
    }, 600);
  };

  this.generateReport = () => {
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
    .each((index, element) => {
      services.push(parseInt($(element).data('value')));
    });
    //The binding for this doesn't work properly. I need to fix it.
    this.serviceTypeSelections = app.services().filter((service) => {
      return services.includes(service.id);
    });

    this.timePeriod($('.ui.timePeriod.dropdown.selection div.item.active.selected').text());

    app.post(this.controller, action, ko.toJSON(this))
      .success((data) => {
        jsonExport(data, (err, csv) => {
          if(err)
            return alert('There was an issue generating the report, please try again or contact your system administrator.');
          if(csv) {
            csv = encodeURIComponent(csv);
            $('#downloadReport').attr('href', `data:text/csv;charset=utf-8,${csv}`);
            this.hasReport(true);
          }
          else
            alert('No data found!');
        });
      })
      .error((data) => {
        if(app.authToken())
          alert('There was an issue generating the report, please try again or contact your system administrator.');
      });
  };

  this.clear = function() {
    this.firstName('');
    this.middleName('');
    this.lastName('');
    this.dateOfBirth('');
    this.startDate('');
    this.endDate('');
    //this.serviceTypeSelections([]);
    this.hasReport(false);

    //timePeriod
    //reportingType
  }.bind(this);
}

function flatten(obj) {
  if(typeof obj === 'object') {

  }
  else if (true) { }
}
