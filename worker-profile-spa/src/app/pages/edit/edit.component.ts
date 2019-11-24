import { Component, OnInit, Input } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { Profile } from "src/app/model/profile";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: "app-edit",
  templateUrl: "./edit.component.html",
  styleUrls: ["./edit.component.css"]
})
export class EditComponent implements OnInit {
  profileLoaded: boolean = false;
  profile: Profile;

  constructor(
    private api: ApiService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    var profileId = this.route.snapshot.params.id;
    if (profileId) {
      this.api.getProfile$(profileId).subscribe(
        res => {
          this.profile = res;
          this.profileLoaded = true;
        },
        err => {
          if (err.status == 403) {
            this.router.navigate(["profiles"]);
          }
        }
      );
    }
  }
}
