import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment.development';

const httpOptions = {
  headers: new HttpHeaders({
    'content-type':'application/json'
  })
}
@Injectable({
  providedIn: 'root'
})
//

export class GenericService<Titems> {
  // url:string = environment.Apiurl +'Movie'
  url:string = environment.Apiurl;
  private baseurl='https://localhost:7117/api/Movie/'

  constructor(private http: HttpClient) {

  }
  getAll(endpoint:string): Observable<Titems[]> {
    return this.http.get<Titems[]>(this.url + endpoint);
  }
  delete(name: string, entityToDelete:number): Observable<Titems>{
    // debugger;
    return this.http.delete<Titems>(`${this.url}${name}`+"/"+entityToDelete, httpOptions);
  }

  getById(endpoint: string, id: number): Observable<Titems> {
    return this.http.get<Titems>(`${this.url}${endpoint}/${id}`);
  }
  create(endpoint:string, body:Titems): Observable<Titems> {
  return this.http.post<Titems>(`${this.url}${endpoint}`, body, httpOptions);
}

  update(endpoint:string, model:Titems,id:number):Observable<Titems>{
  return this.http.put<Titems>(`${this.url}${endpoint}/${id}`,model, httpOptions);
}
// search(endpoint: string, query: string): Observable<Titems[]> {
//   return this.http.get<Titems[]>(`${this.url}/${endpoint}/search?query=${query}`);
// }

search(query: string): Observable<Titems[]> {
  // Ensure base URL does not end with a trailing '/'
  const baseUrl = this.baseurl.endsWith('/') ? this.baseurl.slice(0, -1) : this.baseurl;

  // Construct the full URL without additional '/'
  const url = `${baseUrl}/search?query=${encodeURIComponent(query)}`;

  return this.http.get<Titems[]>(url);
}

}
