import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TimeagoModule } from 'ngx-timeago';

import { NgModule } from '@angular/core';
import {HttpClientModule}   from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ValuesComponent } from './values/values.component';
import { Nav_barComponent } from './nav_bar/nav_bar.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/http.error.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {TabsModule, TabsetComponent} from 'ngx-bootstrap/tabs';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListComponent } from './list/list.component';
import { MessagesComponent } from './messages/messages.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { UsersService } from './_services/users.service';
import { MemberCardComponent } from './members/member-list/member-card/member-card.component';
import { JwtModule } from '@auth0/angular-jwt';
import { MemberDetailsComponent } from './members/member-list/member-details/member-details.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { GaleeryComponent } from './galeery/galeery.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { MemberEditlResolver } from './_resolvers/member-edit.resolver';
import { MemberEditComponent } from './members/member-list/member-edit/member-edit.component';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { MemberUploadPhotoComponent } from './members/member-list/member-upload-photo/member-upload-photo.component';
import { FileUploadModule } from 'ng2-file-upload';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ListResolver } from './_resolvers/lists.resolver';
import { MessagesReolver } from './_resolvers/messages.resolver';
import { MembersThreadMessagesComponent } from './members/member-list/members-threadMessages/members-threadMessages.component';


export function TokenGetter() {
   return localStorage.getItem("token");
 }


   
@NgModule({
   declarations: [
      AppComponent,
      ValuesComponent,
      Nav_barComponent,
      HomeComponent,
      RegisterComponent,
      MemberListComponent,
      ListComponent,
      MessagesComponent,
      MemberCardComponent,
      MemberDetailsComponent,
      GaleeryComponent,
      MemberEditComponent,
      MemberUploadPhotoComponent,
      MembersThreadMessagesComponent
      
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      NgxGalleryModule,
      TimeagoModule.forRoot(),
      PaginationModule.forRoot(),
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      MDBBootstrapModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
         config: {
           tokenGetter: TokenGetter,
           whitelistedDomains: ["localhost:44397"],
           blacklistedRoutes: ["localhost:44397/api/auth"],
         },
       }),
       TabsModule.forRoot(),
       FileUploadModule,
       ReactiveFormsModule
   ],
   providers: [
      AuthService,
      PreventUnsavedChanges,
      UsersService,
      ErrorInterceptorProvider,
      MemberDetailResolver,
      MemberListResolver,
      MemberEditlResolver,
      ListResolver,
      MessagesReolver
      ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
