import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from 'src/app/Models/category';
import { Movie } from 'src/app/Models/movie';
import { GenericService } from 'src/app/Services/generic.service';

@Component({
  selector: 'app-categorydetail',
  templateUrl: './categorydetail.component.html',
  styleUrls: ['./categorydetail.component.css']
})
export class CategorydetailComponent {

  category:Category={id:0,name:'',  movie: []};
  movies:Movie[]=[];
  errorMessage: string = ''; // Add a variable to store error messages
  categoryId: number = 0;

  constructor(private categoryService: GenericService<Category>, private router: ActivatedRoute) { }

  ngOnInit(): void {
    this.router.params.subscribe(params => {
      this.categoryId = params['id'];
      this.categoryService.getById('Category', this.categoryId).subscribe(
        (category: Category) => {
          console.log('Category:', category);
          this.category = category; // Assign fetched category data to the component property

          // Handle category data
        },
        (error) => {
          console.error('Error fetching category:', error);
          // Handle error
        }
      );
    });
    this.GetMovieByCategoryId(this.categoryId);
  }
GetMovieByCategoryId(id: number):void{
  // this.categoryService.getById('Movie/categoryId', id).subscribe(

  this.categoryService.getById('Movie/categoryId', id).subscribe(
    (data: any) => {
      console.log('Category:', data);
      this.movies = data; // Assign fetched category data to the component property
        // Handle category data
      },
      (error) => {
        console.error('Error fetching category:', error);
        // Handle error
      }
    );
  };
}
  

