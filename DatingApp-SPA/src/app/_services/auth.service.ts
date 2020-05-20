import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { userInputModel } from '../nav_bar/nav_bar.component';
import {map} from 'rxjs/operators';
import { UserRegisterModel } from '../register/register.component';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
   url = 'https://localhost:44397/Auth';
   
constructor(private http: HttpClient) { }

 login(model: userInputModel)
 {
     const uri = 'login';
     return this.http.post(this.url +uri, model).pipe(

      map((response: any) => {
        
        const token = response;
        if (token){
          localStorage.setItem('token', token);
        }

      })
     );

 }
 register(modelInputRegister: UserRegisterModel){
  const uri = 'register';
  return this.http.post(this.url + uri , modelInputRegister);
 }

}
