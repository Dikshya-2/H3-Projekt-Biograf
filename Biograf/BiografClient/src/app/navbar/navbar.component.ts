import { Component } from '@angular/core';
import { Movie } from '../Models/movie';
import { GenericService } from '../Services/generic.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SearchService } from '../Services/search.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  movies:Movie[]=[];
  searchQuery: string = '';

  //  constructor(private service: GenericService<Movie>, private router:ActivatedRoute,private route:Router) { }
  constructor(private service: SearchService, private router:ActivatedRoute,private route:Router) {
    this.service.search.subscribe((term)=>{
      this.searchQuery= term;
    });
   }

  // ngOnInit(): void {
  //   // Subscribe to changes in the route parameters
  //   this.router.params.subscribe(params => {
  //     const searchQuery = params['search_query']; // Get the search query parameter from the route
  //     if (searchQuery) {
  //       // If there's a search query, perform the search
  //       this.search(searchQuery);
  //     } else {
  //       // If there's no search query, reset the movies array
  //       this.movies = [];
  //     }
  //   });
  // }

  search(event:any){
    this.searchQuery=(event.target as HTMLInputElement).value;
    console.log(this.searchQuery);
    this.service.search.next(this.searchQuery);
  }
  // search(query: string): void {
  //   this.service.search(query).subscribe(
  //     (data: Movie[]) => {
  //       this.movies = data; // Assign the search results to the movies array
  //     },
  //     (error) => {
  //       console.error('Error performing search:', error);
  //     }
  //   );
  // }

  // Method to navigate to the search page with the given query
  // navigateToSearch(): void {
  //   if (this.searchQuery) {
  //     this.route.navigate(['/search', { query: this.searchQuery }]);
  //   } else {
  //     // Handle case where search query is empty
  //   }
  // }
}



