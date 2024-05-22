import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Category, CategoryModel } from 'src/app/Models/category';
import { Movie, MovieCreateModel } from 'src/app/Models/movie';
import { GenericService } from 'src/app/Services/generic.service';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent {
  movies: Movie[]=[]
  categories: Category[]=[]
  categoriesList: CategoryModel[] = [];
  movieCreate?: MovieCreateModel;
  Form: FormGroup = this.resetForm();
  showCreateForm: boolean = false;
  constructor(private service: GenericService<Movie>, private serviceCategory: GenericService<Category>){}

ngOnInit(): void{
  this.service.getAll("movie").subscribe(a=>{this.movies=a;
    console.log(this.movies);
  });
  // console.log(this.movies);
  this.serviceCategory.getAll("category").subscribe(c=>{this.categories=c;
    console.log(this.categories);
  });
}

resetForm(): FormGroup{
  return new FormGroup ({
    title: new FormControl(null,Validators.required),
    description: new FormControl(null,Validators.required),
    duration:new FormControl(null,Validators.required),
    releaseDate: new FormControl(new Date().toISOString().split('T')[0], Validators.required), // Set default date
    categories: new FormControl([], Validators.required),
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
   this.categoriesList.push(this.Form.value.categories);
   this.Form.patchValue({
    categories: this.categoriesList
   });
   this.movieCreate = this.Form.value; // using the form value directly
   console.log(this.movieCreate);
  // this.service.create("movie",this.movieCreate!).subscribe((a)=>{this.movies.push(a);
  this.service.create("movie", this.movieCreate!).subscribe(
    (a) => {
      // Success handling
      console.log('Movie created:', a);
      this.movies.push(a);

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
  this.categoriesList.push(this.Form.value.categories);
  this.Form.patchValue({
   categories: this.categoriesList
  });
  this.movieCreate = this.Form.value;
  this.service.update( "Movie",this.movieCreate!,id, ).subscribe({
    error: (err)=> {
      console.warn(Object.values(err.error.errors).join(', '));
    },
    complete:()=> {
      this.service.getAll("Movie").subscribe(x=> this.movies =x);
      this.cancel();
    }
  });
}
edit(movie: Movie): void {
  if (movie.categories && movie.categories.length > 0) {
    this.Form.patchValue({
      title: movie.title,
      description: movie.description,
      duration: movie.duration,
      releasedate: movie.releasedDate,
      categories: movie.categories.map(c => c.id) // Assuming categories is an array of ids
    });
    this.showCreateForm = true;
  }
}

delete(entityToDelete: number): void {
  if(confirm('Are you sure you want to delete?'))
    {
     this.service.delete("Movie",entityToDelete).subscribe((a)=>{
       this.movies= this.movies.filter((c)=>c.id !==entityToDelete);
     });
   }
}
cancel():void{
  this.Form= this.resetForm();
  this.showCreateForm = false;
}
toggleCreateForm(): void {
  this.showCreateForm = !this.showCreateForm;
}
}
