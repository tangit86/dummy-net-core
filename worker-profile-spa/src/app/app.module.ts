import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSliderModule } from "@angular/material/slider";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { AutocompleteLibModule } from "angular-ng-autocomplete";
import json from "highlight.js/lib/languages/json";
import { TagInputModule } from "ngx-chips";
import { HighlightModule } from "ngx-highlightjs";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { HomeContentComponent } from "./components/home-content/home-content.component";
import { LoadingComponent } from "./components/loading/loading.component";
import { LocationsAutocompleteComponent } from "./components/locations-autocomplete/locations-autocomplete.component";
import { NavBarComponent } from "./components/nav-bar/nav-bar.component";
import { SaveFormComponent } from "./components/save-form/save-form.component";
import { TagsAutocompleteComponent } from "./components/tags-autocomplete/tags-autocomplete.component";
import { CreateComponent } from "./pages/create/create.component";
import { EditComponent } from "./pages/edit/edit.component";
import { HomeComponent } from "./pages/home/home.component";
import { ProfilesComponent } from "./pages/profiles/profiles.component";
import { SearchComponent } from "./pages/search/search.component";
import { ApiService } from "./services/api.service";

export function hljsLanguages() {
  return [{ name: "json", func: json }];
}

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    HomeContentComponent,
    LoadingComponent,
    SearchComponent,
    ProfilesComponent,
    EditComponent,
    CreateComponent,
    TagsAutocompleteComponent,
    LocationsAutocompleteComponent,
    SaveFormComponent
  ],
  imports: [
    MatAutocompleteModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSliderModule,
    FormsModule,
    TagInputModule,
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    AutocompleteLibModule,

    NgbModule,
    HighlightModule.forRoot({
      languages: hljsLanguages
    }),
    FontAwesomeModule
  ],
  providers: [{ provide: "apiService", useClass: ApiService }],
  bootstrap: [AppComponent]
})
export class AppModule {}
