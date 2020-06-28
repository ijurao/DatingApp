import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MessagesComponent } from './messages/messages.component';
import { ListComponent } from './list/list.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailsComponent } from './members/member-list/member-details/member-details.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './members/member-list/member-edit/member-edit.component';
import { MemberEditlResolver } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { ListResolver } from './_resolvers/lists.resolver';

export const  appRoutes: Routes = [
  {path: '', component: HomeComponent},
  {
      path: '',
      runGuardsAndResolvers: 'always',
      canActivate: [AuthGuard],
      children: [
        {path: 'messages' , component: MessagesComponent},
        {path: 'lists', component: ListComponent, resolve : {users: ListResolver}},
        {path: 'members' , component : MemberListComponent , resolve: {users: MemberListResolver}},
        {path: 'member/edit', component : MemberEditComponent, resolve: {user: MemberEditlResolver},
          canDeactivate: [PreventUnsavedChanges]},
        {path: 'members/:id' , component : MemberDetailsComponent, resolve: {user: MemberDetailResolver}},
 
      ]

  },
  {path: '**'  , redirectTo: 'home', pathMatch: 'full' },

 

];

