import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { UsersService } from 'src/app/_services/users.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import {NgxGalleryOptions} from '@kolkov/ngx-gallery';
import {NgxGalleryImage} from '@kolkov/ngx-gallery';
import {NgxGalleryAnimation} from '@kolkov/ngx-gallery';
import { Photo } from 'src/app/_models/Photo';
import { TabsModule } from 'ngx-bootstrap/tabs/tabs.module';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css'],


})
export class MemberDetailsComponent implements OnInit {

  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
   user: User;
  constructor(private userService: UsersService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(resul => {
      const userParam = 'user';
      this.user = resul[userParam];
    });

   
}
}
