import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from 'src/app/_models/user';
import { Photo } from 'src/app/_models/Photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { UsersService } from 'src/app/_services/users.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-member-upload-photo',
  templateUrl: './member-upload-photo.component.html',
  styleUrls: ['./member-upload-photo.component.css']
})
export class MemberUploadPhotoComponent implements OnInit {

  @Input() photos: Photo[];
  @Output() getMemberPhotoChange = new EventEmitter<string>();
  uploader: FileUploader;
  hasBaseDropZoneOver:boolean;
  response:string;
  baseUrl = environment.apiUrl;
  currentPhoto : Photo;
  constructor(private authService: AuthService, private userServices : UsersService, private alertify : AlertifyService) { 

  }

  ngOnInit() {
    this.initializeUploader();
  }

   fileOverBase(e:any):void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader()
  {
    this.uploader = new FileUploader({
      url: this.baseUrl    + 'photos/' + this.authService.getCurrentUserId(),
      authToken : "Bearer " + localStorage.getItem("token"),
      isHTML5 : true,
      allowedFileType: ['image'],
      removeAfterUpload : true,
      autoUpload : false,
      maxFileSize : ( 10 * 1024 * 1024),



    });

    //this.uploader.onAfterAddingAll = (file) => { console.log("ddd"); file.withCredentials = false; };
    this.uploader.onBeforeUploadItem = (item) => {
      item.withCredentials = false;

    this.uploader.onSuccessItem = (item,response,status,header) => {
      if(response){
        
        const res : Photo = JSON.parse(response);
        const photo : Photo = {
          id : res.id,
          description : res.description,
          isMain : res.isMain,
          dateAdded : res.dateAdded,
          url : res.url
          
        };
        this.photos.push(photo);

      }
    }
    }

    
  }
  setPhotoAsMain(photo : Photo){
    const userId = this.authService.getCurrentUserId();
    const photoId = photo.id;
    this.userServices.setMainPhoto(userId,photoId).subscribe(() => {this.alertify.succes("Photo seted as Main successfulls!");
    this.currentPhoto = this.photos.filter(p => p.isMain === true) [0];
  this.currentPhoto.isMain = false;
  photo.isMain = true;
  this.getMemberPhotoChange.emit(photo.url);
},
    error => this.alertify.error("Error!"));
  }

  deletePhoto(photo: Photo){

    const userId = this.authService.getCurrentUserId();
    this.userServices.deletePhoto(userId,photo.id).subscribe(() =>{ this.alertify.succes("photo deleted!");
    this.photos.splice(this.photos.findIndex(p => p.id === photo.id),1)}
    ,error => this.alertify.error(error));

  }
  
}
