import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Movie } from 'src/app/Models/movie';
import { Photo } from 'src/app/Models/photo';
import { GenericService } from 'src/app/Services/generic.service';

@Component({
  selector: 'app-randomovie',
  templateUrl: './randomovie.component.html',
  styleUrls: ['./randomovie.component.css']
})
export class RandomovieComponent {
  photos:Photo[]=[];
  movies:Movie[]=[];
   numbers:number[]=[];
  filteredMovies:Movie[]=[];
constructor (private service: GenericService<Photo> ,private serviceMovie: GenericService<Movie>,private router: Router){}

ngOnInit(): void {
    //path
    this.service.getAll('Photo').subscribe((photos: Photo[]) => {
     this.photos = photos;

this.getMovie([1, 2, 3, 4, 5, 6, 7,]); // Pass an array of movie IDs to filter
});
this.serviceMovie.getAll('Movie').subscribe((movies: Movie[]) => {
 this.movies = this.movies;
});
}

getMovie(id:number[]):void{
  let size: number;
  this.filteredMovies = [];
  // this.productService.getById(id)
  this.serviceMovie.getAll("movie")
    .subscribe(
      (movies: Movie[]) => {
        size = movies.length;
         this.getbyMovie(size);
         console.log(this.numbers);
        movies.map(
          movie => {

            for (let i = 0; i < 8; i++) {
               if (movie.id == this.numbers[i]) {
                this.filteredMovies.push(movie);
              console.log("movie added");
              }

            }
          }
        )
      }

    );

}
getbyMovie(size: number): void {

  for(let i=0;i<1;i++){
   this.numbers[i] = Math.floor(Math.random() * size + 1)
  }
}
}

