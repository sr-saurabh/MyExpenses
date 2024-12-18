export class Register{
  email:string;
  password: string;
  firstName: string;
  LastName: string;
  contactNumber: string;

  constructor(email: string, password: string, firstName: string, lastName: string, contactNumber: string) {
    this.email = email;
    this.password = password;
    this.firstName = firstName;
    this.LastName = lastName;
    this.contactNumber = contactNumber;
  }
}
