import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter()
  modelInputRegister: UserRegisterModel = new UserRegisterModel() ;

  constructor(private authService: AuthService) {
    this.modelInputRegister.userName = '';
    this.modelInputRegister.password = '';
    this.modelInputRegister.passwordRepeat = '';

   }

  ngOnInit() {
  }

  register()  {
    this.authService.register(this.modelInputRegister).subscribe(reslut => {
    console.log('All good');
    }, error => console.log(error.message));
    console.log(this.modelInputRegister);
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
