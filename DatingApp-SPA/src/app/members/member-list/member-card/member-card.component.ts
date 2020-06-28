import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
import { UsersService } from 'src/app/_services/users.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

  @Input() user: User;

  constructor(private authService: AuthService, private userService: UsersService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  like(user: User){
    const liker = this.authService.getCurrentUserId();
    const likee = user.id;
    this.userService.like(liker, likee).subscribe(data => {

      this.alertify.succes("all god");
    },error => {
      this.alertify.error(error);
  });

}}
