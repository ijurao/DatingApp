import { Component, OnInit, Input } from '@angular/core';
import { IMessage } from 'src/app/_models/IMessage';
import { UsersService } from 'src/app/_services/users.service';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-members-threadMessages',
  templateUrl: './members-threadMessages.component.html',
  styleUrls: ['./members-threadMessages.component.css']
})
export class MembersThreadMessagesComponent implements OnInit {

   @Input() recipientId: number;
   messages : IMessage[];
   newMessage : any = {};
  constructor(private userService:  UsersService, private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadMThread();
  }

  loadMThread(){
    const senderId = this.authService.getCurrentUserId();
    this.userService.geMessaggesThread(senderId,this.recipientId).subscribe(data =>{
         this.messages = data;}
    , error => this.alertify.error(error));
  }

  sendMessage(){
    this.newMessage.recipientId = this.recipientId;
    const userId = this.authService.getCurrentUserId();
    this.userService.sendMessage(userId,this.recipientId,this.newMessage).
    subscribe( (res: IMessage) => {this.alertify.succes('Message Snet');
    this.newMessage.content = '';
    console.log(res);
    this.messages.unshift(res);}
    ,error => this.alertify.error(error));
  }

  
}
