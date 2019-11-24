import { Component, OnInit } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { Profile } from "../../model/profile";
import { AuthService } from "../../auth/auth.service";

@Component({
  selector: "app-profiles",
  templateUrl: "./profiles.component.html",
  styleUrls: ["./profiles.component.css"]
})
export class ProfilesComponent implements OnInit {
  profiles: any = [];

  collectionSize = 0;
  page = 1;
  pageSize = 5;
  objectKeys = Object.keys;

  uid = "";

  constructor(private api: ApiService, private auth: AuthService) {
    this.uid = this.auth.getUser().uid;
    this.loadProfiles(this.page, this.pageSize);
  }

  loadProfiles(page, pageSize) {
    return this.api
      .getUserProfiles$(this.uid, this.page, this.pageSize)
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
    this.loadProfiles(page, pageSize);
  }
  ngOnInit() {}
}
