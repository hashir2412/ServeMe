import { Inject, Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AppInterceptorInterceptor implements HttpInterceptor {
  private url: string;

  constructor(@Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl;
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const contentHeaders = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    const secureReq = request.clone({
      url: this.url + request.url,
      headers: contentHeaders
    });
    console.log(secureReq.url);
    // send the cloned, "secure" request to the next handler.
    return next.handle(secureReq);
  }
}
