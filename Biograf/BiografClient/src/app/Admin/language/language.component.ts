import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Language } from 'src/app/Models/language';
import { Movie } from 'src/app/Models/movie';
import { GenericService } from 'src/app/Services/generic.service';

@Component({
  selector: 'app-language',
  templateUrl: './language.component.html',
  styleUrls: ['./language.component.css']
})
export class LanguageComponent {
  languages: Language[]=[]
  language?: Language
  Form: FormGroup = this.resetForm();
  showCreateForm: boolean = false;

  constructor (private service: GenericService<Language>){}

  resetForm(): FormGroup{
    return new FormGroup ({
       id: new FormControl(''),
      name: new FormControl(null,Validators.required),
      movieId: new FormControl(null,Validators.required),

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
  ngOnInit():void{
      this.service.getAll("language").subscribe(obj=>{
        this.languages = obj;
        console.log(obj);
      });
}

create() {
  console.log(this.Form.value);
  const body = this.Form.value;
  this.service.create("Language",body).subscribe((a)=>{this.languages.push(a);
    this.Form.reset();
  },
  (error)=>{
  console.error('Error creating language:', error);
  console.log(error.message)
  });
}
update():void{
  // id:number,endpoint:string
  const id = this.Form.value.id;
  const body = this.Form.value;
  this.service.update( "Language",body,id, ).subscribe({
    error: (err)=> {
      console.warn(Object.values(err.error.errors).join(', '));
    },
    complete:()=> {
      this.service.getAll("Language").subscribe(x=> this.languages =x);
      this.cancel();
    }
  });
}
edit(language: Language): void {
  this.Form.patchValue({
    id: language.id,
    name: language.name,
    movieId: language.movieId
  });
}
delete(entityToDelete: number): void {
  this.service.delete("Language", entityToDelete).subscribe({
    next: () => {
      this.languages = this.languages.filter(c => c.id !== entityToDelete);
    },
    error: (error) => {
      console.error('Error deleting language:', error);
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
