import { Component, Input, OnInit } from "@angular/core";

import { Location } from "../../model/location";
import { Profile } from "../../model/profile";
import { ApiService } from "../../services/api.service";

@Component({
  selector: "app-save-form",
  templateUrl: "./save-form.component.html",
  styleUrls: ["./save-form.component.css"]
})
export class SaveFormComponent implements OnInit {
  @Input() initProf: Profile;

  curProf: Profile = new Profile(-1, "", [], new Location("", 0.0, 0.0));

  allowNext: boolean = false;

  error: string = "";
  constructor(private api: ApiService) {}

  ngOnInit() {
    if (this.initProf) {
      this.curProf.id = this.initProf.id;
      this.curProf.name = this.initProf.name;
      this.curProf.skills = this.initProf.skills;
      this.curProf.location.address = this.initProf.location.address;
      this.curProf.location.latitude = this.initProf.location.latitude;
      this.curProf.location.longitude = this.initProf.location.longitude;
    }
  }

  nameChanged(name) {
    this.curProf.name = name;
  }

  locationChanged(location) {
    console.log("location changed", location);
    this.curProf.location.address = location.address;
    this.curProf.location.latitude = location.latitude;
    this.curProf.location.longitude = location.longitude;
  }

  skillsChanged(skills) {
    console.log("skills change", skills);
    this.curProf.skills = [];
    skills.forEach(element => {
      if (isNaN(element.id)) {
        element.id = 0;
      }
      this.curProf.skills.push(element);
    });
  }

  save() {
    if (this.curProf.id > 0) {
      this.update();
    } else {
      this.create();
    }
  }

  create() {
    console.log("to create....");
    this.api.createProfile$(this.curProf).subscribe(
      res => {
        this.error = "";
        this.curProf.id = res.id;
        this.allowNext = true;
      },
      err => {
        if (err.error) {
          this.error = err.error.Description;
        } else {
          this.error = "Something weird happened...";
        }
      }
    );
  }

  update() {
    console.log("to update...");
    this.api.updateProfile$(this.curProf.id, this.curProf).subscribe(
      res => {
        this.error = "";
        this.allowNext = true;
      },
      err => {
        if (err.error) {
          this.error = err.error.Description;
        } else {
          this.error = "Something weird happened...";
        }
      }
    );
  }
}
