import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { PaginatedResult, IPagination } from '../_models/IPagination';
import { AuthService } from '../_services/auth.service';
import { UsersService } from '../_services/users.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  users : User[];
  pagination: IPagination;
  likesParam : string;

  constructor(private authService: AuthService, private userService: UsersService, private route: ActivatedRoute, 
    private alertify: AlertifyService) { }

  ngOnInit() {

    this.route.data.subscribe(resul => {
      console.log(resul['users'].pagination)
      this.users = resul['users'].result;
      this.pagination = resul['users'].pagination;
    });
    this.likesParam = "Likers";
  }

  loadUsers(){
    console.log(this.likesParam)
    this.userService.getUsers(this.pagination.currentPage,this.pagination.count,null,this.likesParam)
    .subscribe((res: PaginatedResult<User[]>) =>{
      this.users = res.result;
      this.pagination = res.pagination;
    },error => {
      this.alertify.error(error);
    });

  }

  pageChanged(event:any):void{
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }


}
