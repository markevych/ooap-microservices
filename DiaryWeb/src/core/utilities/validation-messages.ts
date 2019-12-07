export const ValidationMessages = {
  UpdateProfile: {
      userName: {
          required: 'User name is required.'
      },
      userMail: {
          required: 'Email is required.'
      }
  },
  ResetPassword: {
      oldPassword: {
          required: 'Please set up your old password.'
      },
      newPassword: {
          required: 'Please set up new password.'
      },
      repeatPassword: {
          required: 'Please repeat new password.',
          mustMatch: 'Passwords do not match'
      }
  }
};
