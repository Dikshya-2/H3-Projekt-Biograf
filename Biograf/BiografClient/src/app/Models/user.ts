export class User{
  id:number=0;
  fullName: string='';
  email:string='';
  address:string='';
  phone:number=0;
  role: string='';
  password: string = '';
  token?: string;

}

export class Role {
  User = 'Guest';
  Admin = 'Admin';
}



