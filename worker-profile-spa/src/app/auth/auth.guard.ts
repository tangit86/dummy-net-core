import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  CanActivate,
  Router
} from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "./auth.service";
import { tap } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean | UrlTree> | boolean {
    return this.auth.isAuthenticated$.pipe(
      tap(loggedIn => {
        if (!loggedIn) {
          this.auth.login(state.url);
        } else {
          var requiredRoles = next.data.roles;
          this.auth.userAllowed(requiredRoles).then(isAllowed => {
            if (!isAllowed) {
              console.log("user not allowed to view the page");
              this.router.navigate([""]);
            }
          });
          return false;
        }
      })
    );
  }
}
