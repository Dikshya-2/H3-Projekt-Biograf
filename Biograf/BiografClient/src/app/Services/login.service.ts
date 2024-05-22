import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';
import { User } from '../Models/user';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { map } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private currentUserSubject: BehaviorSubject<User| null>;
  currentUser: Observable<User | null>;
  constructor(private http: HttpClient) {


  this.currentUserSubject = new BehaviorSubject<User | null>(
    JSON.parse(sessionStorage.getItem('currentUser') as string)
  );
   this.currentUser = this.currentUserSubject.asObservable();
}
public get currentUserValue(): User | null {
  return this.currentUserSubject.value;
}

login(email: string, password: string) {
   let authenticateUrl = `${environment.Apiurl}UserControllerNew/Login`;
   return this.http.post<any>(authenticateUrl, { "email": email, "password": password })

    .pipe(map((user: User) => {
      // store user details and jwt token in local storage to keep user logged in between page refreshes
      sessionStorage.setItem('currentUser', JSON.stringify(user));
      this.currentUserSubject.next(user);
      return user;
    }));
  }

  logout() {
    // remove user from local storage to log user out
   sessionStorage.removeItem('currentUser');
    // reset CurrentUserSubject, by fetching the value in sessionStorage, which is null at this point
    // this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(sessionStorage.getItem('currentUser') as string));
    // reset CurrentUser to the resat UserSubject, as an obserable
    // this.currentUser = this.currentUserSubject.asObservable();
    this.currentUserSubject.next(null);

  }
}
