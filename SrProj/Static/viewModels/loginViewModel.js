function LoginViewModel() {
  this.username = ko.observable();
  this.password = ko.observable();

  this.login = function() {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        contentType: "application/json",
        url: app.API + 'Login',
        data: ko.toJSON(this),
        success: function(data, textStatus, request) {
          var authToken = request.getResponseHeader('authToken');

          if(!authToken)
            return this.message().createMessage('Error', messageList.error.authError, true);

          this.message().createMessage('Success', messageList.success.login, false);
          Cookies.set(this.authTokenPrefix + this.clientName, authToken, Cookies.defaults);

          this.forwardUserIntoApp();
        }.bind(this),

        error: function(data) {
          var errorID = (data.responseJson || JSON.parse(data.responseText))[0].id;

          if(errorID == authenticationResult.InvalidUserNameOrPassword) {
            return this.message().createMessage('Error', messageList.error.invalidUserPass, true);
          }

          if(errorID == authenticationResult.PasswordIsExpired ||
             errorID == authenticationResult.PasswordChangeRequired)
          {
            window.location = '#passwordChange';
            this.currentPassword(this.loginPassword());
            return this.message().createMessage('Error', messageList.error.needNewPassword, true);
          }

          if(errorID == authenticationResult.AccountInactive  ||
             errorID == authenticationResult.AccountLockedOut ||
             errorID == authenticationResult.LoginDisallowed)
          {
            this.message().createMessage('Error', messageList.error.notAllowedToLogin, true);
          }

          if(errorID == authenticationResult.SSORequired) {
            this.message().createMessage('Error', messageList.error.ssoRequired, true); 
          }
        }.bind(this),

        complete: function() {
          this.actionEnd();
        }.bind(this)
      });
  }.bind(this);
}