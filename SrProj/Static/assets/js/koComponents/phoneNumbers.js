ko.components.register(`phoneNumbers`, {
  viewModel: function(params) {
    this.data = params.data;
  },
  template:
  `<button class="ui positive labeled icon button" style="margin-bottom: 10px;" data-bind="click: $root.addPhoneNumber">
      <i class="plus icon"></i>
      Add A Phone Number
    </button>
    <!-- ko foreach: data -->
    <div class="two fields phoneNumber">
      <div class="field">
        <div class="ui input">
          <input type="text" class="phoneField" placeholder="XXX-XXX-XXXX" data-bind="textInput: $data" />
        </div>
      </div>
      <div class="field">
        <button class="circular red ui icon button" data-bind="click: $root.removePhoneNumber">
          <i class="close icon"></i>
        </button>
      </div>
    </div>
    <!-- /ko -->`,
  synchronous: true
});