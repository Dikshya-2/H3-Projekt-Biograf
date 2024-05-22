import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Category } from 'src/app/Models/category';
import { Movie } from 'src/app/Models/movie';
import { GenericService } from 'src/app/Services/generic.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent {
  categories: Category[]=[]
  // movies: Movie[]=[];
  catrgoriessend?:Category;
  Form: FormGroup = this.resetForm();
  showCreateForm: boolean = false;

  // Form: FormGroup = new FormGroup({
  //   // id : new FormControl(''), // this can be a mistake,
  //   name: new FormControl(''),

  // });
  constructor (private service: GenericService<Category>){}

  ngOnInit():void{
    this.service.getAll("category").subscribe(obj=>{
      this.categories = obj;
      console.log(obj);
    });
}
resetForm(): FormGroup{
  return new FormGroup ({
    // id: new FormControl(''),
    name: new FormControl('',Validators.required),
  });
}
saveForm(): void {
  if (this.Form.valid) {
    if (this.Form.value.id) {
      this.update();
    } else {
      this.create();
    }
  }
}

create():void{

  // console.log(this.Form.value);
//It defines a constant reference to a value
  this.catrgoriessend = this.Form.value; // using the form value directly
 console.log(this.catrgoriessend);
  this.service.create("category",this.catrgoriessend!).subscribe((a)=>{this.categories.push(a);
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
  this.service.update( "category", body,id).subscribe({
    next: () => {
      // Update the category in the list
      const index = this.categories.findIndex(c => c.id === id);
      if (index !== -5) {
        this.categories[index].name = body;
      }
      this.resetForm();
    },
    error: (err)=> {
      console.warn(Object.values(err.error.errors).join(', '));
    },
    complete:()=> {
      this.service.getAll("category").subscribe(x=> this.categories =x);
      this.cancel();
    }
  });
}

edit(category: Category): void {
  this.Form.patchValue({
    id: category.id,
    name: category.name
  });
}
delete(entityToDelete: number):void{
    this.service.delete("Category", entityToDelete).subscribe({
      next: () => {
        this.categories = this.categories.filter(c => c.id !== entityToDelete);
      },
      error: (error) => {
        console.error('Error deleting category:', error);
        console.log(error.message);
      }
    });
  }
  cancel():void{
    this.Form= this.resetForm();
  }
  toggleCreateForm(): void {
    this.showCreateForm = !this.showCreateForm;
  }

}

