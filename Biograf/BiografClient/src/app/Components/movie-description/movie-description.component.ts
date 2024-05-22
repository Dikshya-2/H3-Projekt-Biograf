import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from 'src/app/Models/category';
import { Movie } from 'src/app/Models/movie';
import { Photo } from 'src/app/Models/photo';
import { GenericService } from 'src/app/Services/generic.service';

@Component({
  selector: 'app-movie-description',
  templateUrl: './movie-description.component.html',
  styleUrls: ['./movie-description.component.css']
})
export class MovieDescriptionComponent {

  photos:Photo[]=[];
  movieId: number = 0;
  categories: Category = { id: 0, name: '' }; // Assuming Category is an interface or type
  movie: Movie = new Movie();
 constructor(private service:GenericService<Movie>, private router: ActivatedRoute,private service1: GenericService<Photo>){}

//  this.categoryService.getById('Category', categoryId).subscribe(
  // ngOnInit(): void {
  //   this.router.params.subscribe(params => {
  //     const categoryId = params['id'];
  //     this.categoryService.getById('Category', categoryId).subscribe(
  //       (category: Category) => {
  //         console.log('Category:', category);
  //         // Handle category data
  //       },


 ngOnInit():void{
  this.router.paramMap.subscribe(params=>{
    this.movieId = Number(params.get('id'));

    // this.service.getById('Movie', movieId).subscribe(movie => {
      //   this.movie = movie;
      //   console.log(this.movie)
    });
    this.filtermovie(this.movieId);


  }
  filtermovie(id:number):void{
    console.log(this.movieId);
    this.service.getById('Movie', id).subscribe(
    (data) => {
      this.movie = data
      console.log('Movie:', this.movie);
      // Handle category data
    },
  );

 }

}
