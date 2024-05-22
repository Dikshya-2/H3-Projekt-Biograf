import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Movie } from 'src/app/Models/movie';
import { Photo } from 'src/app/Models/photo';
import { GenericService } from 'src/app/Services/generic.service';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent {
  photos: Photo[] = [];
  movies: Movie[] = [];

  constructor(private photoService: GenericService<Photo>, private movieService: GenericService<Movie>, private router: Router) { }

  ngOnInit(): void {
    this.photoService.getAll('Photo').subscribe((photos: Photo[]) => {
      this.photos = photos;
    });

    this.movieService.getAll('Movie').subscribe((movies: Movie[]) => {
      this.movies = movies;
    });
  }

  navigateToMovieDetails(movieId: number): void {
    this.router.navigate(['/movie', movieId]);
  }
}

