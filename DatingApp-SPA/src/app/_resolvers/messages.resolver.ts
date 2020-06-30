import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/user';
import { UsersService } from '../_services/users.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IMessage } from '../_models/IMessage';
import { AuthService } from '../_services/auth.service';
@Injectable()
export class MessagesReolver implements Resolve<IMessage[]>{
  pageNumber = 1;
  pageSize = 5;
  messageContainer = 'UnRead';
  constructor(private authServices: AuthService, private userService: UsersService, 
    private router: Router, private alerttify: AlertifyService){}
    resolve(route: ActivatedRouteSnapshot): Observable<IMessage[]>
    {
      const userId = this.authServices.getCurrentUserId();
      return this.userService.getMessages(userId, this.pageNumber, this.pageSize, this.messageContainer).pipe(
          catchError(error =>
        {
          console.log(error);
          this.alerttify.error(error);
          this.router.navigate(['/home']);
          return of(null);
        }));
    }
    }

