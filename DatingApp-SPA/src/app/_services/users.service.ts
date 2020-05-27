import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';





@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }


getUser(id: number): Observable<User>{
  return this.http.get<User>(this.baseUrl + 'users/' + id);

}
getUsers(): Observable<User[]>{

  return this.http.get<User[]>(this.baseUrl + 'users');

}
}
