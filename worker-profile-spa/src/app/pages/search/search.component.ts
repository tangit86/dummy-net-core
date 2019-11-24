import { Component, OnInit } from "@angular/core";
import { ApiService } from "src/app/services/api.service";
import { Skill } from "../../model/skill";

@Component({
  selector: "app-search",
  templateUrl: "./search.component.html",
  styleUrls: ["./search.component.css"]
})
export class SearchComponent implements OnInit {
  profiles: any = [];

  collectionSize = 0;
  page = 1;
  pageSize = 5;
  objectKeys = Object.keys;

  skillsQuery = [];
  radiusQuery = { lat: 0.0, lng: 0.0, radius: 0 };

  constructor(private api: ApiService) {}

  radiusChanged(e) {
    console.log(e);
    this.radiusQuery.radius = e.value;
  }

  locationChanged(e) {
    console.log(e, "location");
    this.radiusQuery.lat = e.latitude;
    this.radiusQuery.lng = e.longitude;
  }

  skillsChanged(updSkills) {
    console.log("skills changed", updSkills);
    this.skillsQuery.length = 0;
    updSkills.forEach(element => {
      this.skillsQuery.push(element.id);
    });

    console.log(this.skillsQuery);
  }

  searchProfiles(pg, pgSize) {
    if (!this.radiusQuery.radius) {
      this.radiusQuery.radius = 0;
    }
    console.log("current state:", this.radiusQuery, this.skillsQuery);
    return this.api
      .searchProfiles$(
        this.skillsQuery,
        this.radiusQuery,
        this.page,
        this.pageSize
      )
      .subscribe(data => {
        this.profiles = [];
        for (const p in data.payload) {
          var profs = data.payload[p];
          this.profiles.push(profs);
          this.page = data.page;
          this.pageSize = data.pageSize;
          this.collectionSize = data.totalElements;
        }
        console.log(data, this.profiles);
      });
  }

  changePage(page, pageSize) {
    this.searchProfiles(page, pageSize);
  }
  ngOnInit() {
    this.searchProfiles(this.page, this.pageSize);
  }
}
