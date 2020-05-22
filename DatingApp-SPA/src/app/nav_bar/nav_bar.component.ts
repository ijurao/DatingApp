import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

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
  currentUser: string;
  constructor(private authService: AuthService, private alertify: AlertifyService, private routerService: Router) {
    this.modelInputLogin.userName = '';
    this.modelInputLogin.password = '';

   }

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUserName();

  }

  login(){
    this.authService.login(this.modelInputLogin).subscribe(next => {
      this.alertify.succes('Welcome!');
      this.currentUser = this.authService.getCurrentUserName();

    }, error => {
      this.alertify.error(error);
    } ,() =>{
      this.routerService.navigate(['/members']);
    });
  }
  
  loggedIn()
  {
    return this.authService.loggedIn();
  }

  logout()
  {
    this.authService.logout();
    this.routerService.navigate(['/home']);
  }

}
// tslint:disable-next-line: class-name
export class userInputModel {
   userName: string;
   password: string;
}