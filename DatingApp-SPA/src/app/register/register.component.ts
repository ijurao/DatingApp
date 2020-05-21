import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter()
  modelInputRegister: UserRegisterModel = new UserRegisterModel() ;

  constructor(private authService: AuthService, private alertify: AlertifyService) {
    this.modelInputRegister.userName = '';
    this.modelInputRegister.password = '';
    this.modelInputRegister.passwordRepeat = '';

   }

  ngOnInit() {
  }

  register()  {
    this.authService.register(this.modelInputRegister).subscribe(reslut => {
    this.alertify.succes('User registered!');
    }, error => this.alertify.error(error.message));
  }
  cancel(){
    this.cancelRegister.emit(false); // emite el evento para q lo escuche el componente padre
    this.modelInputRegister.userName = '';
    this.modelInputRegister.password = '';
    this.modelInputRegister.passwordRepeat = '';
  }

}

export class UserRegisterModel
{
   userName: string;
   password: string;
  passwordRepeat: string;
}
