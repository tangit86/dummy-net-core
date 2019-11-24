import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { AuthGuard } from "./auth/auth.guard";
import { InterceptorService } from "./auth/interceptor.service";
import { CreateComponent } from "./pages/create/create.component";
import { EditComponent } from "./pages/edit/edit.component";
import { HomeComponent } from "./pages/home/home.component";
import { ProfilesComponent } from "./pages/profiles/profiles.component";
import { SearchComponent } from "./pages/search/search.component";

const routes: Routes = [
  {
    path: "create",
    component: CreateComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Admin", "Standard"] }
  },
  {
    path: "edit/:id",
    component: EditComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Admin", "Standard"] }
  },
  {
    path: "search",
    component: SearchComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Admin"] }
  },
  {
    path: "profiles",
    component: ProfilesComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Admin", "Standard"] }
  },
  {
    path: "",
    component: HomeComponent,
    pathMatch: "full"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorService,
      multi: true
    }
  ]
})
export class AppRoutingModule {}
