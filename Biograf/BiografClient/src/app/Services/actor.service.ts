import { Injectable } from '@angular/core';
import { Actor } from '../Models/actor';
import { Observable } from 'rxjs/internal/Observable';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ActorService {
  //apiUrl="https://localhost:7117/api/Actor"
  //apiUrl : string = "https://localhost:7117/api/";
  private get apiUrl(): string {
    return environment.Apiurl + "Actor";
  }
  constructor(private http: HttpClient) { }

  getAll(): Observable<Actor[]> {
    return this.http.get<Actor[]>(this.apiUrl);
}

  getById(id: number): Observable<Actor> {
  return this.http.get<Actor>(`${this.apiUrl}/${id}`);
}

create(Actor: Actor): Observable<Actor> {
  return this.http.post<Actor>(`${this.apiUrl}`, Actor);
}

update(id:number,Actor:Actor):Observable<Actor>{
  return this.http.put<Actor>(this.apiUrl+'/'+ id,Actor);
}
// https://localhost:7117/api/Actor?id=5
delete(id:number):Observable<Actor>{
  return this.http.delete<Actor>(`https://localhost:7117/api/Actor?id=5`);
}
// delete(id:number):Observable<Actor>{
//   return this.http.delete<Actor>(this.apiUrl+'/'+id);

//   }
}
