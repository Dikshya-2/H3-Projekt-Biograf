import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment.development';
import { User } from '../Models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private get apiUrl(): string {
    return environment.Apiurl + "User";
}
constructor(private http: HttpClient) { }

register(user:User):Observable<any>{

  return this.http.post<any>(`${this.apiUrl}/registration`, user);

}

login(credentials:{email:string,password:string,role:string}):Observable<any>{
  return this.http.post(`${this.apiUrl}/login`,credentials);
}
}

