import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, delay, finalize, identity } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoadingEffectService } from '../loadingEffects/loading-effect.service';

@Injectable()
export class LoadingEffectInterceptor implements HttpInterceptor {

  constructor(private loadingEffectService: LoadingEffectService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.loadingEffectService.busy();

    return next.handle(request).pipe(
      (environment.production ? identity : delay(100)),
      finalize(() => {
        this.loadingEffectService.idle();
      })
    )
  }
}
