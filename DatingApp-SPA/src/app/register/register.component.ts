import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig} from 'ngx-bootstrap/datepicker';
import { User } from '../_models/user';
import { Router } from '@angular/router';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter()
  registerForm : FormGroup;
  bsConfig : Partial<BsDatepickerConfig>;
  user : User;

  constructor(private authService: AuthService, private alertify: AlertifyService,
              private formBuilder: FormBuilder, private router: Router) {
   

   }

  ngOnInit() {
    this.createRegisterForm();
    this.bsConfig = {
      containerClass : 'theme-red'
    }

  }

  createRegisterForm(){
    this.registerForm = new FormGroup({

     gender: new FormControl('male',Validators.required),

      knownAs: new FormControl('',Validators.required),
      dateOfBirth: new FormControl(null,Validators.required),
      city: new FormControl('',Validators.required),
      country: new FormControl('',Validators.required),
      userName: new FormControl('',Validators.required),
      password : new FormControl('', [Validators.required,Validators.minLength(4) ]),
      passwordRepeat : new FormControl('',[Validators.required,Validators.minLength(4) ])
    },this.passwordMatchValidator);

  }

  passwordMatchValidator(form: FormGroup){

    return form.get("password").value === form.get("passwordRepeat").value ? null : {mismatch: true};
  }

  register()  {
  
     if(this.registerForm.valid){
       this.user = Object.assign({},this.registerForm.value);
       console.log(this.user);
       this.authService.register(this.user).subscribe(next => {this.alertify.succes("user registered")},
       error => {this.alertify.error(error); },
       () => {this.authService.login(this.user).subscribe(() => {
         this.router.navigate(['/members']);
       });
         
       });
       

     }

    console.log(this.registerForm.value);
  }
  cancel(){
    this.cancelRegister.emit(false); // emite el evento para q lo escuche el componente padre
  
  }

}


