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

updateUser(id: number, user: User) {

   return this.http.put(this.baseUrl + 'users/' + id, user );

}

setMainPhoto(idUser: number, idPhoto: number) {

  return this.http.post(this.baseUrl + 'photos/' + idUser + "/" + idPhoto  + "/setMain",{});

}

deletePhoto(idUser: number, idPhoto: number) {

  return this.http.delete(this.baseUrl + 'photos/' + idUser + "/" + idPhoto  ,{});

}
}
