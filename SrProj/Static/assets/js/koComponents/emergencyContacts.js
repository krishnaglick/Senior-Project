ko.components.register(`emergencyContacts`, {
  viewModel: function(params) {
    this.data = params.data;
  },
  template:`
    <button class="ui positive labeled icon button" style="margin-bottom: 10px;" data-bind="click: $root.addEmergencyContact">
      <i class="plus icon"></i>
      Add An Emergency Contact
    </button>
    <!-- ko foreach: data -->
    <div class="fields">
      <div class="field">
        <input type="text" placeholder="First Name" data-bind="textInput: firstName" />
      </div>
      <div class="field">
        <input type="text" placeholder="Last Name" data-bind="textInput: lastName" />
      </div>
      <div class="field">
        <input type="text" placeholder="XXX-XXX-XXXX" data-bind="textInput: phoneNumber" class="phoneField" />
      </div>
      <div class="field">
        <button class="circular red ui icon button" data-bind="click: $root.removeEmergencyContact">
          <i class="close icon"></i>
        </button>
      </div>
    </div>
    <!-- /ko -->`
});