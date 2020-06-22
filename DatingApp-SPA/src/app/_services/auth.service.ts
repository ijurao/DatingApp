import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { userInputModel } from '../nav_bar/nav_bar.component';
import {map} from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import {environment} from '../../environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
   url = environment.apiUrl +  'Auth/';
   jwthelper = new JwtHelperService();
   decodedToken : any;
constructor(private http: HttpClient) { }

 login(model: any)
 {
     const uri = 'login';
     return this.http.post(this.url + uri, model).pipe(
      map((response: any) => {
        const token = response;
        if (token){
          localStorage.setItem('token', token);
          this.decodedToken = this.jwthelper.decodeToken(token);
          
        }

      })
     );

 }
 register(modelInputRegister: User){
  const uri = 'register';
  return this.http.post(this.url + uri , modelInputRegister);
 }
 loggedIn()
 {
   const token = localStorage.getItem('token');
   if (token === null){
     return false;
   }
   return !this.jwthelper.isTokenExpired(token);
 }

getCurrentUserName(){

  if (this.decodedToken)
  { 
    return this.decodedToken.unique_name;
  }
  return '';
}



getCurrentPhotoUrlUser(){

  if (this.decodedToken)
  {
    return this.decodedToken.UrlMainPhoto;
  }
  return '';
}

getCurrentUserId(){

  if (this.decodedToken)
  {
    return this.decodedToken.nameid;
  }
  return '';
}

getCurrentUserGender(){

  if (this.decodedToken)
  {
    return this.decodedToken.Gender;
  }
  return '';

}

setDecodedToken(token)
{
  this.decodedToken = token;
}

logout()
{
  localStorage.removeItem('token');
}


}
