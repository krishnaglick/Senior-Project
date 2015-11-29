ko.components.register('multipleSearchSelection', {
  viewModel: function(params) {
    this.data = params.data;
    this.defaultValue = params.defaultValue;
    this.activeSelection = params.activeSelection;
  },
  template:
    '<div class="ui fluid search dropdown selection multiple">' +
      '<select multiple="" data-bind="foreach: data">' +
        '<option data-bind="value: $data, text: $data"></option>' +
      '</select>' +
      '<i class="dropdown icon"></i>' +
      '<!-- ko foreach: activeSelection -->' +
        '<a class="ui label transition visible" data-bind="attr: { \'data-value\': $data }" style="display: inline-block !important;">' +
          '<span data-bind="text: $data"></span>' +
          '<i class="delete icon"></i>' +
        '</a>' +
      '<!-- /ko -->' +
      '<input class="search" autocomplete="off" tabindex="0" style="width: 2.1428em;" />' +
      '<div class="default text" data-bind="text: defaultValue"></div>' +
      '<div class="menu transition hidden" tabindex="1" data-bind="foreach: data">' +
        '<div class="item" data-bind="attr: { \'data-value\': $data }, text: $data, css: { \'active filtered\': $parent.activeSelection.indexOf($data) > -1 } "></div>' +
      '</div>' +
    '</div>',
    synchronous: true
});