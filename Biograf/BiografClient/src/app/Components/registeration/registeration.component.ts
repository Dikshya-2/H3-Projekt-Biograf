import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/user';
import { GenericService } from 'src/app/Services/generic.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-registeration',
  templateUrl: './registeration.component.html',
  styleUrls: ['./registeration.component.css']
})
export class RegisterationComponent {
  user: User= new User();
  constructor(private userService: UserService) { }

  onSubmit(): void {
    this.userService.register(this.user).subscribe(
      (response) => {
        // Registration successful
      },
      (error) => {
        // Handle registration error
      }
    );
  }
}
