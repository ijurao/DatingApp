import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/user';
import { UsersService } from '../_services/users.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
@Injectable()
export class MemberEditlResolver implements Resolve<User>{
  constructor(private userService: UsersService,private router: Router, private alerttify:AlertifyService, private authSetrvice: AuthService){}
    resolve(route: ActivatedRouteSnapshot): Observable<User>
    {
      const userId = this.authSetrvice.getCurrentUserId();
      return this.userService.getUser(userId).pipe(
          catchError(error =>
        {
            this.alerttify.error(error);
            this.router.navigate(['/members']);
             return of(null);
        }));
    }
    }

