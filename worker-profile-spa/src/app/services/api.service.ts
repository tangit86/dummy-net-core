import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import config from "../../../spa_config.json";
import { Paginated } from "../model/paginated.js";
import { Profile } from "../model/profile.js";
import { Skill } from "../model/skill";
import spa_config from "../../../spa_config.json";

@Injectable({
  providedIn: "root"
})
export class ApiService {
  constructor(private http: HttpClient) {}

  getProfile$(profileId): Observable<any> {
    return this.http.get<Profile>(config.apiBaseUrl + "/profiles/" + profileId);
  }

  getUserProfiles$(uid, page, pageSize): Observable<any> {
    return this.http.get<Paginated<Profile>>(
      config.apiBaseUrl + "/users/" + uid + "/profiles",
      { params: { page: page, pageSize: pageSize } }
    );
  }

  searchProfiles$(skillIds, radius, page, pageSize): Observable<any> {
    var myParams = { page: page, pageSize: pageSize };

    myParams["skills"] = skillIds;
    myParams["lat"] = radius.lat;
    myParams["lng"] = radius.lng;
    myParams["radius"] = radius.radius;
    return this.http.get<Paginated<Profile>>(
      config.apiBaseUrl + "/profiles/search",
      { params: myParams }
    );
  }

  getSkills$(term): Observable<any> {
    return this.http.get<Skill>(config.apiBaseUrl + "/skills", {
      params: { q: term }
    });
  }

  getLocations$(term): Observable<any> {
    return this.http.get<Location>(config.apiBaseUrl + "/locations", {
      params: { q: term }
    });
  }

  createProfile$(profile: Profile): Observable<any> {
    return this.http.post(config.apiBaseUrl + "/profiles", profile);
  }

  updateProfile$(productId, profile: Profile): Observable<any> {
    return this.http.put(config.apiBaseUrl + "/profiles/" + productId, profile);
  }
}
