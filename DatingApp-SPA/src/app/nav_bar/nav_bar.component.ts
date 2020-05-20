import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  // tslint:disable-next-line: component-selector
  selector: 'app-nav_bar',
  templateUrl: './nav_bar.component.html',
  styleUrls: ['./nav_bar.component.css']
})
// tslint:disable-next-line: class-name
export class Nav_barComponent implements OnInit {

  title = 'Dating App';
  modelInputLogin: userInputModel = new userInputModel() ;
 
  constructor(private authService: AuthService) {
    this.modelInputLogin.userName = '';
    this.modelInputLogin.password = '';

   }

  ngOnInit() {
  }

  login(){
    this.authService.login(this.modelInputLogin).subscribe(next => {
      this.loggedIn();

    }, error => {
      console.log('Error de inicio de sesion' + error.message);
    } );
  }

  loggedIn()
  {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout()
  {
    localStorage.removeItem('token');
  }

}
// tslint:disable-next-line: class-name
export class userInputModel {
   userName: string;
   password: string;
}