import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { userInputModel } from '../nav_bar/nav_bar.component';
import {map} from 'rxjs/operators';
import { UserRegisterModel } from '../register/register.component';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
   url = 'https://localhost:44397/Auth/';
   jwthelper = new JwtHelperService();
   decodedToken : any;
constructor(private http: HttpClient) { }

 login(model: userInputModel)
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
 register(modelInputRegister: UserRegisterModel){
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

  if (this.decodedToken !== null)
  {
    return this.decodedToken.unique_name;
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
