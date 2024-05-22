import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Author } from '../Models/author';
import { Observable } from 'rxjs/internal/Observable';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  //https://localhost:7117/api/Author
  private get apiUrl(): string {
    return environment.Apiurl + "Author";
  }
  constructor(private http: HttpClient) { }

  getAll(): Observable<Author[]> {
    return this.http.get<Author[]>(this.apiUrl);
}

  getById(id: number): Observable<Author> {
  return this.http.get<Author>(`${this.apiUrl}/${id}`);
}

create(Author: Author): Observable<Author> {
  return this.http.post<Author>(`${this.apiUrl}`, Author);
}

  update(id:number,Author:Author):Observable<Author>{
  return this.http.put<Author>(this.apiUrl+'/'+ id, Author);
}

  delete(id:number):Observable<Author>{
  return this.http.delete<Author>(this.apiUrl+'/'+id);
}
}
