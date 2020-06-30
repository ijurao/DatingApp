import { Component, OnInit } from '@angular/core';
import { IMessage } from '../_models/IMessage';
import { PaginatedResult, IPagination } from '../_models/IPagination';
import { AuthService } from '../_services/auth.service';
import { UsersService } from '../_services/users.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: IMessage[];
  pagination: IPagination;
  messageContainer = 'UnRead';

  constructor(private authService: AuthService, private usersService: UsersService, private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(resul => {
      this.messages = resul['messages'].result;
      this.pagination = resul['messages'].pagination;
    });

    this.loadMessages();
  }

  loadMessages(){

    const userId = this.authService.getCurrentUserId();
    this.usersService.getMessages(userId,this.pagination.currentPage,this.pagination.count,this.messageContainer)
    .subscribe((res: PaginatedResult<IMessage[]>) =>{
      this.messages = res.result;
      console.log(this.messages[0])
      this.pagination = res.pagination;
    },error => {
      this.alertify.error(error);

  });


}



pageChanged(event:any):void{
  this.pagination.currentPage = event.page;
  this.loadMessages();
}

deleteMessage(id: number)
{
  alert(id);

     this.alertify.confirm('Are you sure', () => {
       this.usersService.deleteMessage(id, this.authService.getCurrentUserId()).subscribe( () => {
         this.messages.splice(this.messages.findIndex(m => m.id === id), 1);
         this.alertify.succes("messages deleted");
       }, error => {this.alertify.error(error);
       } );
});
}

}
