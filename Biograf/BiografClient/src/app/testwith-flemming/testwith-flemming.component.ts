import { Component } from '@angular/core';

@Component({
  selector: 'app-testwith-flemming',
  templateUrl: './testwith-flemming.component.html',
  styleUrls: ['./testwith-flemming.component.css']
})
export class TestwithFlemmingComponent {
  title ='BiografProjectUI';
  //string s = "BiografProjectUI";
  name: string ="Dikshya";
  age: number=33;
  sand: boolean =true;
  list :string[]=["one", "Two", "Three"]
  person: string[]=["FirstName", "LastName", "Email","Address"]

  ngOnTnit():void{
    console.log("test");
    console.log(this.list);
    console.log(this.person);
    // invoke a function
    this.create(); // This is a pointer to the object.
    this.Edit();
  }
  //Function
  create(): void{
    console.log("hello world!");
    console.log(this.person);
  }

  Edit():void{
    console.log("You can edit now");
  }
  // abc(name:string): string{
  //   let obj=name.split('').sort().join('');
  //   let obj1=obj.replace('kanban','banKan')
  //   return obj1;
  //   console.log(this.abc);

  //   let adr:any='Falkevej 45';
  //   let obj3=adr.substring(0,3);
  //   console.log(obj3)
  // }
}
