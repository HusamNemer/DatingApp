import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
decoding: any;
helper = new JwtHelperService();
baseUrl = 'http://localhost:5000/api/Auth/';

constructor(private http: HttpClient) { }
login(model: any) {
return this.http.post( this.baseUrl + 'login', model).pipe(
  map(( response: any) => {
const user = response;
if (user) {
localStorage.setItem('token', user.token);
this.decoding = this.helper.decodeToken(user.token);
console.log(this.decoding );
}

})

);

}

register(model: any) {
 return this.http.post(this.baseUrl + 'register', model);
}
Loggedin() {

 const token =  localStorage.getItem('token');

 return !!this.helper.isTokenExpired(token);

}

}
