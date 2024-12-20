export class Register {
  email: string;
  password: string;
  confirmPassword: string;
  // firstName: string;
  // LastName: string;
  // contactNumber: string;

  constructor(
    email: string,
    password: string,
    firstName: string,
    lastName: string,
    contactNumber: string,
    confirmPassword: string
  ) {
    this.email = email;
    this.password = password;
    this.confirmPassword = confirmPassword;
    // this.firstName = firstName;
    // this.LastName = lastName;
    // this.contactNumber = contactNumber;
  }
}
