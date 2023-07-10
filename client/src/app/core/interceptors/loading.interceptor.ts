import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, delay, finalize } from 'rxjs';
import { BusyService } from '../services/busy.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // Get rid of the full-page loading while checking e-mail exist async validation is working at register page.
    if (!request.url.includes('emailExists')) {
      this.busyService.busy();
    }
    return next.handle(request).pipe(
      delay(1000),
      finalize(() => this.busyService.idle())
    )
  }
}
