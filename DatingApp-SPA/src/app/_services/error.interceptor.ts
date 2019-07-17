import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { error } from '@angular/compiler/src/util';
import { AlertifyService } from './alertify.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

   constructor(private alertyi: AlertifyService) {}
    intercept(req: HttpRequest<any> , next: HttpHandler ): Observable<HttpEvent<any>> {

    return next.handle(req).pipe(
     catchError(Error => {
                 if (Error instanceof HttpErrorResponse) {
                 const applicationError = Error.headers.get('Application-Error');
                 if (applicationError) {
                 console.log(applicationError);
                 return throwError(applicationError);
                 }
                 const serverError = Error.error;
                 let modalStateErrors = '';
                 if (serverError && typeof serverError === 'object') {
                    for (const key in serverError) {
                       if (serverError[key]) {
                        modalStateErrors += serverError[key] + ' \n';
                       }

                    }
                 }
                
                 return throwError(modalStateErrors || serverError || 'Server error' );
          }

         })


    );
    }
}
export const ErrorInterceptorProvider = {
provide: HTTP_INTERCEPTORS,
useClass: ErrorInterceptor,
multi: true

};
