import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../_services/users.service';
import { User } from '../../_models/user';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

   users: User[];
  constructor(private usersService: UsersService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    //this.loadUsers();
    this.route.data.subscribe(resul => {
      this.users = resul['users'];
      console.log(this.users);
    });
  }

 /* loadUsers()
  {
    this.usersService.getUsers().subscribe( 
      (users: User[]) => {
          
            this.users = users;
      }, error => console.log(error)
  );

}*/
}
