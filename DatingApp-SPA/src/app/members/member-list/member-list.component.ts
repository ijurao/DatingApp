import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../_services/users.service';
import { User } from '../../_models/user';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { IPagination, PaginatedResult } from 'src/app/_models/IPagination';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

   users: User[];
   pagination : IPagination;
   genderList = [{value: 'male', display: 'Males'},{value: 'female', display: 'Famales'}];
   orderList = [{value: 'lastAcces', display: 'Last Access'},{value: 'country', display: 'Country'}];

   userParams : any = {};
  constructor(private usersService: UsersService, private alertify: AlertifyService, private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    //this.loadUsers();
    this.route.data.subscribe(resul => {
      this.users = resul['users'].result;
      this.pagination = resul['users'].pagination;
     
    });
    this.userParams.gender = (this.authService.getCurrentUserGender() === 'male') ? 'female' : 'male';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
    this.userParams.orderBy = "country";
  }

  resetFilter(){
    this.userParams.gender = (this.authService.getCurrentUserGender() === 'male') ? 'female' : 'male';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
    this.userParams.orderBy = "country";

    this.loadUsers();
  }
  pageChanged(event:any):void{
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }
  applyFilters(){
    this.loadUsers();
  }

  loadUsers(){
    console.log(this.userParams);
    this.usersService.getUsers(this.pagination.currentPage,this.pagination.count,this.userParams).subscribe((res: PaginatedResult<User[]>) =>{
      this.users = res.result;
      this.pagination = res.pagination;
    },error => {
      this.alertify.error(error);
    });

  }
}
