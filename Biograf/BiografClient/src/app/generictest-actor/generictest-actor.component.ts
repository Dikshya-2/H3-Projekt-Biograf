import { Component } from '@angular/core';
import { GenericService } from '../Services/generic.service';
import { Actor } from '../Models/actor';

@Component({
  selector: 'app-generictest-actor',
  templateUrl: './generictest-actor.component.html',
  styleUrls: ['./generictest-actor.component.css']
})
export class GenerictestActorComponent {

  ngOnInit():void{
    this.service.getAll("endpoint").subscribe(obj=>{
      console.log(obj);
    })
  }
  constructor (private service: GenericService<Actor>){}

}
