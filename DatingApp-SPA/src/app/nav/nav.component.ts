import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService, private alerti: AlertifyService, private router: Router) {}

  ngOnInit() {
  }
  login() {
this.authService.login(this.model).subscribe(
  next => {this.alerti.success('logged successfully'); } ,
 error => {this.alerti.error('falied'); });

  }


LoggedIn() {
 // const token = localStorage.getItem('token');
  return !this.authService.Loggedin();
  }
  Logout() {
    localStorage.removeItem('token');
    this.alerti.message('logged out');
    this.router.navigate(['/home']);
  }

}
