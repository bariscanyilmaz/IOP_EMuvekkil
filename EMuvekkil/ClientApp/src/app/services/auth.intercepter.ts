import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs";

export class AuthInterceptor implements HttpInterceptor
{
    intercept(req: HttpRequest<any>, next:HttpHandler): Observable<HttpEvent<any>> {
        const token=localStorage.getItem('token');
        const newReq = req.clone({
            headers: req.headers.set(
                'Authorization', 'Bearer ' + token
            )
        });

        return next.handle(newReq);

    }
    
}