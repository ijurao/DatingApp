import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { IMessage } from '../_models/IMessage';
import {  PaginatedResult } from '../_models/IPagination';
import { map } from 'rxjs/operators';





@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }


getUser(id: number): Observable<User>{
  return this.http.get<User>(this.baseUrl + 'users/' + id);

}
getUsers(page?, itemsPerPage?, userParams?, likesParam?): Observable<PaginatedResult<User[]>>{

  const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();
  let params  = new HttpParams();
  if (page !==  null && itemsPerPage !== null){
    params = params.append('pageNumber', page);
    params = params.append('pageSize', itemsPerPage);
  }
  if (userParams != null){
    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);
  }

  if (likesParam === 'Likees'){

    params = params.append('likees', 'true');
  }
  if (likesParam === 'Likers')
  {
    params = params.append('likers', 'true');

  }
  return this.http.get<User[]>(this.baseUrl + 'users', {observe: 'response', params})
  .pipe(
    map(response => {
      paginatedResult.result = response.body;
      if (response.headers.get('pagination') !== null){
        paginatedResult.pagination = JSON.parse(response.headers.get('pagination'));
      }
      return paginatedResult;
    })
  );

}

updateUser(id: number, user: User) {

   return this.http.put(this.baseUrl + 'users/' + id, user );

}

setMainPhoto(idUser: number, idPhoto: number) {

  return this.http.post(this.baseUrl + 'photos/' + idUser + '/' + idPhoto  + '/setMain', {});

}

deletePhoto(idUser: number, idPhoto: number) {

  return this.http.delete(this.baseUrl + 'photos/' + idUser + '/' + idPhoto  , {});

}

like(liker: number, likee: number){
  return this.http.post(this.baseUrl + 'users/' +  liker + '/like/' + likee , {});
}

getMessages(userId: number, pageNumber?, itemsPerPage?, messageContainer?: string){

  const paginatedResult: PaginatedResult<IMessage[]> = new PaginatedResult<IMessage[]>();
  let params  = new HttpParams();
  params = params.append('MessageContainer', messageContainer);

  if (pageNumber !==  null && itemsPerPage !== null){
    params = params.append('pageNumber', pageNumber);
    params = params.append('pageSize', itemsPerPage);
  }

  return this.http.get<IMessage[]>(this.baseUrl + 'users/' + userId + '/message', {observe: 'response', params})
  .pipe(
    map(response => {
      paginatedResult.result = response.body;
      if (response.headers.get('pagination') !== null){
        paginatedResult.pagination = JSON.parse(response.headers.get('pagination'));
      }
      return paginatedResult;
    })
  );

}

geMessaggesThread(idUser, recipienId){

  return this.http.get<IMessage[]>(this.baseUrl + 'users/' + idUser + "/message/thread/" + recipienId);

}

sendMessage(idUser: number, recipientId: number, message: IMessage){

  return this.http.post(this.baseUrl + 'users/'+  idUser + "/message",message);

}

deleteMessage(messagenId : number, userId: number){

  return this.http.post(this.baseUrl + 'users/' + userId + "/message/" + messagenId,{});


}


markAsRead(messagenId : number, userId: number){

  return this.http.post(this.baseUrl + 'users/' + userId + "/message/" + messagenId  + "/read",{}).subscribe();


}

}
