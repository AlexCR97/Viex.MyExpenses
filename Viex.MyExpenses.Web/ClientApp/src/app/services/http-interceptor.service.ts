import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpResponse, HttpStatusCode } from '../models/HttpErrorResponse';

@Injectable({
  providedIn: 'root'
})
export class HttpInterceptorService implements HttpInterceptor {

  constructor() { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError(error => this.handleErrorResponse(request, next, error)),
    );
  }

  handleErrorResponse(request: HttpRequest<any>, next: HttpHandler, errorResponse: any) {
    const response = HttpResponse.fromHttpResponse(errorResponse)
    console.error('Response error:', response)
    console.error('Status:', response.statusCodeDescription)

    if (response.isBadRequest) {
      // TODO Handle error
    }
    else if (response.isUnauthorized) {
      // TODO Handle error
    }
    else if (response.isForbidden) {
      // TODO Handle error
    }
    else if (response.isNotFound) {
      // TODO Handle error
    }
    else if (response.isInternalServerError) {
      // TODO Handle error
    }
    else if (response.isServiceUnavailable) {
      // TODO Handle error
    }

    return throwError(errorResponse);
  }

}
