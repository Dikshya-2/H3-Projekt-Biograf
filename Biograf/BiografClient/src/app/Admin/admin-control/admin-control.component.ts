import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-control',
  templateUrl: './admin-control.component.html',
  styleUrls: ['./admin-control.component.css']
})
export class AdminControlComponent {
  constructor(private router: Router) { }

  ngOnInit(): void {

  }

  public route(selector: number): void {
    switch (selector) {
      case 0:
        this.router.navigate([ 'admin/actor' ]);
        return;
      case 1:
        this.router.navigate([ 'admin/author' ]);
        return;
      case 2:
        this.router.navigate([ 'admin/category' ]);
        return;
      case 3:
        this.router.navigate([ 'admin/movie' ]);
        return;
      case 4:
        this.router.navigate([ 'admin/language' ]);
        return;
        case 5:
        this.router.navigate([ 'admin/user' ]);
        return;
    }
  }

}
