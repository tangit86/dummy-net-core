import { Component, OnInit } from "@angular/core";
import { Profile } from "../../model/profile";
import { Location } from "../../model/location";

@Component({
  selector: "app-create",
  templateUrl: "./create.component.html",
  styleUrls: ["./create.component.css"]
})
export class CreateComponent implements OnInit {
  profile = new Profile(0, "", [], new Location("", 0.0, 0.0));
  constructor() {}

  ngOnInit() {}
}
