import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/Models/user';
import { GenericService } from 'src/app/Services/generic.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent {
  users:User[]=[]
  Form: FormGroup = this.resetForm();
  showCreateForm: boolean = false;

  constructor (private service: GenericService<User>){}

  ngOnInit():void{
    this.service.getAll("user").subscribe(obj=>{
      this.users = obj;
      console.log(obj);
    });

  }

  resetForm(): FormGroup{
    return new FormGroup ({
      fullName: new FormControl(null,Validators.required),
      address: new FormControl(null,Validators.required),
      email: new FormControl(null,Validators.required),
      phoneNumber: new FormControl(null,Validators.required),

    });
  }
  saveForm(): void {

    if (this.Form.valid && this.Form.value.id) {
      // debugger;
        this.update();
      } else {
        // debugger;
        this.create();
    }
  }

  create():void{
    console.log(this.Form.value);
  //It defines a constant reference to a value
    const body = this.Form.value; // using the form value directly
    this.service.create("User",body).subscribe((a)=>{this.users.push(a);
      this.Form.reset();// Reset the form after creation
    },
    (error)=>{
    console.error('Error creating category:', error);
    console.log(error.message)
  });
  }
  update():void{
    // id:number,endpoint:string
    const id = this.Form.value.id;
    const body = this.Form.value;
    this.service.update( "user", body,id).subscribe({
      next: () => {
        // Update the category in the list
        const index = this.users.findIndex(c => c.id === id);
        if (index !== -5) {
          this.users[index].fullName = body;
        }
        this.resetForm();
      },
      error: (err)=> {
        console.warn(Object.values(err.error.errors).join(', '));
      },
      complete:()=> {
        this.service.getAll("category").subscribe(x=> this.users =x);
        this.cancel();
      }
    });
  }

  delete(entityToDelete: number): void {
    this.service.delete("User", entityToDelete).subscribe({
      next: () => {
        this.users = this.users.filter(c => c.id !== entityToDelete);
      },
      error: (error) => {
        console.error('Error deleting language:', error);
        console.log(error.message);
      }
    });
  }
  edit(user: User): void {
    this.Form.patchValue({
      id: user.id,
      name: user.fullName,
      address: user.address,
      email: user.email,
      phone: user.phone,
      role: user.role

    });
  }
  toggleCreateForm(): void {
    this.showCreateForm = !this.showCreateForm;
  }
  cancel():void{
    this.Form= this.resetForm();
  }

}
