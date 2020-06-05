import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UsersService } from 'src/app/_services/users.service';
import { AuthService } from 'src/app/_services/auth.service';


@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
 
  user: User;

  @ViewChild('editForm') editForm : NgForm;
  @HostListener('window:beforeunload',['$event'])
  unloadNotification($event:any)
  {
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }
  constructor( private route: ActivatedRoute, private alertrify: AlertifyService, private userService: UsersService,
    private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
       this.user = data['user'];
    });
  }

  updateProfile()
  {
    this.alertrify.confirm('Are sure to update your changes', () => {this.saveChanges(this.user)} );

  }

  saveChanges(user: User){
    const id = this.authService.getCurrentUserId();
    this.userService.updateUser(id, user).subscribe(ok =>{
      this.editForm.reset(this.user);
      this.alertrify.succes("profile Updated!");
    },error => {
      this.alertrify.error(error);
    });
  } 

  setMainPhoto(photoUtrl: string)
  {
     this.user.mainPhotoUrl = photoUtrl;
  }



}
