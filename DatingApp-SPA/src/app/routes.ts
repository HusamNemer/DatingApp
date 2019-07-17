import { Route, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './Lists/Lists.component';
import { MembersComponent } from './members/members.component';
import { GuardGuard } from './_guards/guard.guard';

export const appRoutes: Routes = [
{ path: '' , component: HomeComponent },
 {
path: '',
runGuardsAndResolvers: 'always',
canActivate: [GuardGuard],
children: [
{ path: 'list' , component: ListsComponent , canActivate: [GuardGuard] },
{ path: 'members', component: MembersComponent },
{ path: 'messages' , component: MembersComponent}
]
 },
{ path: '**' , redirectTo: '' , pathMatch: 'full' },

];
