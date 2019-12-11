export const CommonConstants = {
  returnUrlSnapshot: 'returnUrl'
};

export const ValidatorLengths = {
  passwordMin: 8,
  phoneNumberMax: 14
};

export const CommonRegEx = {
  url: /^((https?|ftp|smtp):\/\/)?(www.)?[a-z0-9]+\.[a-z]+(\/[a-zA-Z0-9#]+\/?)*/,
  phoneNumber: /^(\+47)-? *(\d{3})?-? *\d{2}-? *-?\d{3}/,
  passwordAlphaNumber: /((?=.*[0-9])(?=.*[A-Za-z]).{8,})/,
  onlyLetters: /^[a-zA-Z ]+$/,
  notNumber: /[^0-9]/,
  currencyReg: /\w K/
};
