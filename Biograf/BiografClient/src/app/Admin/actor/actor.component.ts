import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Actor } from 'src/app/Models/actor';
import { ActorService } from 'src/app/Services/actor.service';
import { GenericService } from 'src/app/Services/generic.service';

@Component({
  selector: 'app-actor',
  templateUrl: './actor.component.html',
  styleUrls: ['./actor.component.css']
})
export class ActorComponent {
  actors: Actor[]=[]
  //newActor: Actor = { name: '', age: 0 }; // Declare newActor property
  Form: FormGroup = this.resetForm(); // instance of FormGroup to handle the category form
  showCreateForm: boolean = false;


  // constructor(private actorService: ActorService){}
  constructor(private actorService: GenericService<Actor>){}
  ngOnInit() : void {

    this.actorService.getAll("Actor").subscribe(a=>{this.actors=a;});
    console.log(this.actors);
  }
  resetForm(): FormGroup{
    return new FormGroup ({
      id: new FormControl(null),
      name: new FormControl(null,Validators.required),
      age: new FormControl(null,Validators.required),

    });
  }
  submitForm(): void {
     if (this.Form.valid) {
      console.log("ID THING " + this.Form.value.id);

      if (this.Form.value.id) {
        this.update();
      } else {
        this.create();
      }
    }
  }
  create() {
    console.log(this.Form.value);
    const body = this.Form.value;
    this.actorService.create("Actor",body).subscribe((a) => {
      this.actors.push(a);
      this.Form.reset();
    });
  }
  update():void{
    // id:number,endpoint:string
    const id = this.Form.value.id;
    const body = this.Form.value;
    this.actorService.update( "Actor",body,id, ).subscribe({
      error: (err)=> {
        console.warn(Object.values(err.error.errors).join(', '));
      },
      complete:()=> {
        this.actorService.getAll("Actor").subscribe(x=> this.actors =x);
        this.cancel();
      }
    });
  }
  edit(actor: Actor): void {
    this.Form.patchValue({
      id: actor.id,
      name: actor.name,
      age: actor.age
    });
  }
       // delete method
  delete(entityToDelete: number): void {
     if(confirm('Are you sure you want to delete?'))
       {
        this.actorService.delete("actor",entityToDelete).subscribe((a)=>{
          this.actors= this.actors.filter((c)=>c.id !==entityToDelete);
        });
      }
  }
  cancel():void{
    this.Form= this.resetForm();
  }
  toggleCreateForm(): void {
    this.showCreateForm = !this.showCreateForm;
  }
}
