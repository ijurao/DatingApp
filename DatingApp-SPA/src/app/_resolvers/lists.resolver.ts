import {Injectable} from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/user';
import { UsersService } from '../_services/users.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
@Injectable()
export class ListResolver implements Resolve<User[]>{
  pageNumber = 1;
  pageSize = 5;
  likeParams = 'Likers';
  constructor(private userService: UsersService,private router: Router, private alerttify:AlertifyService){}
    resolve(route: ActivatedRouteSnapshot): Observable<User[]>
    {
      return this.userService.getUsers(this.pageNumber,this.pageSize,null,this.likeParams).pipe(
          catchError(error =>
        {
            this.alerttify.error(error);
            this.router.navigate(['/home']);
             return of(null);
        }));
    }
    }

