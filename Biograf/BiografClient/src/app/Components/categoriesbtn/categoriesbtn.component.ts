import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from 'src/app/Models/category';
import { GenericService } from 'src/app/Services/generic.service';

@Component({
  selector: 'app-categoriesbtn',
  templateUrl: './categoriesbtn.component.html',
  styleUrls: ['./categoriesbtn.component.css']
})
export class CategoriesbtnComponent {
  categories:Category[]=[];
  constructor(private categoryService: GenericService<Category>, private router: ActivatedRoute, private route: Router) { }

  ngOnInit():void{
    this.categoryService.getAll("category").subscribe(obj=>{
      this.categories = obj;
      console.log(obj);
    });
  }

  btnClick(categoryId: number):void{
    this.route.navigate(['/categorydetail', categoryId]);


  }
}
