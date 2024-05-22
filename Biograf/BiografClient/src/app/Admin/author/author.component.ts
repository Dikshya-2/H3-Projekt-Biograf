import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Author } from 'src/app/Models/author';
import { AuthorService } from 'src/app/Services/author.service';
import { GenericService } from 'src/app/Services/generic.service';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.css']
})
export class AuthorComponent {
authors: Author[]=[]
Form: FormGroup = this.resetForm();
// Form: FormGroup = new FormGroup({
//   id: new FormControl(''),
//   name: new FormControl(''),
//   age: new FormControl('')
//});
showCreateForm: boolean = false;

// instance of FormGroup to handle the category form
//newAuthor: Author={id:0, name: '', age: 0};
constructor(private service: GenericService<Author>){}

ngOnInit(): void{
  this.service.getAll("author").subscribe(a=>{this.authors=a;});
  console.log(this.authors);
}
resetForm(): FormGroup{
  return new FormGroup ({
    id: new FormControl(null),
    name: new FormControl(null,Validators.required),
    age: new FormControl(null,Validators.required)
  });
}

submitForm(): void {
   if (this.Form.valid) {
    if (this.Form.value.id) {
      this.update();
    } else {
      this.create();
    }
  }
}
// resetAuthor():Author{
//   return{id:0, name:'', age:0};
// }
create():void{
  console.log(this.Form.value);
  const body = this.Form.value;
  //subscribe method, which is used to listen for responses from the create method.
  this.service.create("Author",body).subscribe((a)=>{this.authors.push(a);
    this.Form.reset();
  },
  (error)=>{
  console.error('');
});
}

update():void{
  const id = this.Form.value.id;
  const body = this.Form.value;
  this.service.update("Author",body,id, ).subscribe({
    error: (err)=> {
      console.warn(Object.values(err.error.errors).join(', '));
    },
    complete:()=> {
      this.service.getAll("Author").subscribe(x=> this.authors =x);
      this.cancel();
    }
  });
}

delete(entityToDelete: number): void {
  this.service.delete("Author", entityToDelete).subscribe({
    next: () => {
      this.authors = this.authors.filter(c => c.id !== entityToDelete);
    },
    error: (error) => {
      console.error('Error deleting author:', error);
      console.log(error.message);
    }
  });
}
edit(author:Author):void{
  this.Form.patchValue({
    id:author.id,
    name: author.name,
    age: author.age
  });
}
cancel():void{
  this.Form= this.resetForm();
}

toggleCreateForm(): void {
  this.showCreateForm = !this.showCreateForm;
}
}

