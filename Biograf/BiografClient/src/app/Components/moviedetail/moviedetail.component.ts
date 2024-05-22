import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Movie } from 'src/app/Models/movie';
import { Photo } from 'src/app/Models/photo';
import { GenericService } from 'src/app/Services/generic.service';
import { SearchService } from 'src/app/Services/search.service';
import { RandomovieComponent } from '../randomovie/randomovie.component';

@Component({
  selector: 'app-moviedetail',
  templateUrl: './moviedetail.component.html',
  styleUrls: ['./moviedetail.component.css']
})
export class MoviedetailComponent {
  movies:Movie[]=[];
  filteredMovies:Movie[]=[];
  photos:Photo[]=[];
  public   searchQuery: string = '';
  qnty: number= 1;

  constructor(private service:GenericService<Movie>, private router:ActivatedRoute,private route:Router, private searchService: SearchService) { }

  ngOnInit(): void {
     this.fetchMovies();
     this.searchService.search.subscribe((searchQuery)=>{
      this.searchQuery= searchQuery;
      this.searchMovie();
     });

  }
  searchMovie(){
     if(this.searchQuery==null || this.searchQuery =='' &&(onkeyup)){
      //if (!this.searchQuery || this.searchQuery.trim() === '') {

      alert("search field is empty")
      this.fetchMovies();
    }
    else if(this.searchQuery.length >=0)
      {
        this.service.getAll('Movie').subscribe((movies: Movie[]) => {
          this.movies= movies.filter((movie)=>{
            const searchQuery = this.searchQuery.toLowerCase();
            return(
              movie.title.toLowerCase().includes(searchQuery)||
              movie.description.toLowerCase().includes(searchQuery)
            );
          });
           if(this.movies.length===0){
            alert("Not found");
          }
      });
  }
}
  fetchMovies(){
    console.log("List of movies fetching");
    this.service.getAll('movie').subscribe((movies: Movie[]) =>{
      this.movies= movies;
    })

  }
}


